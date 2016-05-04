//
// SSAASTVisitor.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.SSA
{
    using AST;
    using AST.Visitor;

    public class SSAASTVisitor : ASTVisitor
    {
        private SSAAction action;
        private SSABlock currentBlock;
        private Stack<SSAScope> scope = new Stack<SSAScope>();
        private List<Tuple<ControlFlowStatement, int>> unresolvedControlFlow = new List<Tuple<ControlFlowStatement, int>>();
        private InstructionFormat format;

        public SSAASTVisitor(SSAAction action, InstructionFormat format)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            this.action = action;
            this.format = format;
        }

        public override void VisitBehaviour(Behaviour behaviour)
        {
            if (currentBlock != null)
                throw new InvalidOperationException();

            if (this.format == null)
                throw new InvalidOperationException();

            currentBlock = this.action.EntryBlock;

            var rootScope = new SSAScope();
            scope.Push(rootScope);

            foreach (var field in this.format.Fields) {
                rootScope.CreateSymbol("inst." + field.Name, PrimitiveType.UInt32);
            }

            base.VisitBehaviour(behaviour);

            if (scope.Pop() != rootScope)
                throw new Exception("Unbalanced scope");

            if (!(this.currentBlock.Last is ControlFlowStatement))
                CurrentBlock.AddStatement(new LeaveStatement());
        }

        public override void VisitHelper(Helper helper)
        {
            if (currentBlock != null)
                throw new InvalidOperationException();

            currentBlock = this.action.EntryBlock;

            var rootScope = new SSAScope();
            scope.Push(rootScope);

            foreach (var parameter in helper.Parameters) {
                rootScope.CreateSymbol(parameter.Name, SSAType.FromString(parameter.Type));
            }

            base.VisitHelper(helper);

            if (scope.Pop() != rootScope)
                throw new Exception("Unbalanced scope");

            if (!(this.currentBlock.Last is ControlFlowStatement))
                CurrentBlock.AddStatement(new LeaveStatement());
        }

        public override void VisitFunctionBody(FunctionBody body)
        {
            var newScope = new SSAScope(scope.Peek());
            scope.Push(newScope);
            base.VisitFunctionBody(body);
            if (scope.Pop() != newScope)
                throw new Exception("Unbalanced scope");
        }

        public override void VisitReadRegisterBank(ReadRegisterBank readRegisterBank)
        {
            if (!(readRegisterBank.Bank is SymbolExpression))
                throw new NotSupportedException();

            var offset = BankedRegisterOperand(readRegisterBank.Bank, readRegisterBank.Id);
            CurrentBlock.AddStatement(new LoadRegisterStatement(offset, PrimitiveType.UInt32));
        }

        public override void VisitReadRegister(ReadRegister readRegister)
        {
            var offset = RegisterOperand(readRegister.Id);
            CurrentBlock.AddStatement(new LoadRegisterStatement(offset, PrimitiveType.UInt32));
        }

        public override void VisitWriteRegisterBank(WriteRegisterBank writeRegisterBank)
        {
            if (!(writeRegisterBank.Bank is SymbolExpression))
                throw new NotSupportedException();

            var offset = BankedRegisterOperand(writeRegisterBank.Bank, writeRegisterBank.Id);
            var value = ExpressionToOperand(writeRegisterBank.Value);

            var ssa = new StoreRegisterStatement(value, offset, PrimitiveType.UInt32);
            currentBlock.AddStatement(ssa);
        }

        private SSAOperand BankedRegisterOperand(Expression bank, Expression id)
        {
            return new IntegerOperand(PrimitiveType.UInt32, 0);
        }

        private SSAOperand RegisterOperand(Expression id)
        {
            return new IntegerOperand(PrimitiveType.UInt32, 0);
        }

        public override void VisitFunctionCall(FunctionCall call)
        {
            var action = CurrentBlock.Owner.Owner.GetAction(call.Name);
            var ssa = new CallStatement(action.AsOperand());

            foreach (var arg in call.Arguments) {
                arg.Accept(this);
                ssa.AddArgument(currentBlock.Last.AsOperand());
            }

            currentBlock.AddStatement(ssa);
        }

        public override void VisitStructAccess(StructAccess structAccess)
        {
            var ssa = new LoadValueStatement(CurrentScope.ResolveSymbol("inst." + structAccess.Member).AsOperand());
            currentBlock.AddStatement(ssa);
        }

        public override void VisitAddOperator(AddOperator addExpression)
        {
            var lhs = ExpressionToOperand(addExpression.LHS);
            var rhs = ExpressionToOperand(addExpression.RHS);

            var ssa = new ArithmeticStatement(
                          lhs,
                          rhs,
                          ArithmeticStatement.ArithmeticOperation.Add);
            
            this.currentBlock.AddStatement(ssa);
        }

        public override void VisitSymbolExpression(SymbolExpression symbol)
        {
            this.currentBlock.AddStatement(new LoadValueStatement(CurrentScope.ResolveSymbol(symbol.Symbol).AsOperand()));
        }

        public override void VisitAssignmentExpression(AssignmentExpression asnExpression)
        {
            if (!(asnExpression.LHS is SymbolExpression))
                throw new NotSupportedException();

            var rhs = ExpressionToOperand(asnExpression.RHS);

            var ssa = new StoreValueStatement(rhs, CurrentScope.ResolveSymbol(((SymbolExpression)asnExpression.LHS).Symbol).AsOperand());
            this.currentBlock.AddStatement(ssa);
        }

        public override void VisitVariableDeclaration(VariableDeclaration varDecl)
        {
            var symbol = scope.Peek().CreateSymbol(varDecl.Name, SSAType.FromString(varDecl.Type));

            if (varDecl.Assignment != null) {
                var asn = ExpressionToOperand(varDecl.Assignment);
                var ssa = new StoreValueStatement(asn, symbol.AsOperand());
                this.currentBlock.AddStatement(ssa);
            }
        }

        public override void VisitIfStatement(IfStatement ifStatement)
        {
            var ssaCond = ExpressionToOperand(ifStatement.Condition);
            var branchBlock = this.currentBlock;

            var trueBlock = this.action.CreateBlock();
            this.currentBlock = trueBlock;

            ifStatement.IfTrue.Accept(this);

            var postBlock = this.action.CreateBlock();

            if (!(this.currentBlock.Last is ControlFlowStatement)) {
                this.currentBlock.AddStatement(new JumpStatement(postBlock.AsOperand()));
            }


            this.currentBlock = postBlock;

            var ssa = new BranchStatement(
                          BranchStatement.BranchPredicate.IfNonZero,
                          ssaCond,
                          trueBlock.AsOperand(),
                          postBlock.AsOperand());

            branchBlock.AddStatement(ssa);
        }

        public override void VisitEqualityOperator(EqualityOperator equalityExpression)
        {
            var lhs = ExpressionToOperand(equalityExpression.LHS);
            var rhs = ExpressionToOperand(equalityExpression.RHS);

            ComparisonStatement.ComparisonKind comparisonKind;
            switch (equalityExpression.Type) {
            case EqualityOperatorType.Equal:
                comparisonKind = ComparisonStatement.ComparisonKind.Equal;
                break;
            case EqualityOperatorType.NotEqual:
                comparisonKind = ComparisonStatement.ComparisonKind.NotEqual;
                break;
            default:
                throw new NotSupportedException();
            }

            var ssa = new ComparisonStatement(lhs, rhs, comparisonKind);

            this.currentBlock.AddStatement(ssa);
        }

        private TypedSSAOperand ExpressionToOperand(Expression expr)
        {
            if (expr is IntegerConstantExpression) {
                return ((IntegerConstantExpression)expr).Value.AsOperand();
            } else {
                expr.Accept(this);
            }

            return this.currentBlock.Last.AsOperand();
        }

        private SSABlock CurrentBlock{ get { return this.currentBlock; } }

        private SSAScope CurrentScope{ get { return this.scope.Peek(); } }
    }
}

