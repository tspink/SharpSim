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
        private SSAContext context;
        private SSABlock currentBlock;
        private Stack<SSAScope> scope = new Stack<SSAScope>();
        private List<Tuple<ControlFlowStatement, int>> unresolvedControlFlow = new List<Tuple<ControlFlowStatement, int>>();

        public SSAASTVisitor(SSAContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public override void VisitBehaviour(Behaviour behaviour)
        {
            if (currentBlock != null)
                throw new InvalidOperationException();

            currentBlock = this.context.EntryBlock;

            var rootScope = new SSAScope();
            scope.Push(rootScope);

            base.VisitBehaviour(behaviour);

            if (scope.Pop() != rootScope)
                throw new Exception("Unbalanced scope");

            if (!(this.currentBlock.Last is ControlFlowStatement))
                this.currentBlock.AddStatement(new LeaveStatement());
        }

        public override void VisitFunctionBody(FunctionBody body)
        {
            var newScope = new SSAScope();
            scope.Push(newScope);
            base.VisitFunctionBody(body);
            if (scope.Pop() != newScope)
                throw new Exception("Unbalanced scope");
        }

        public override void VisitReadRegisterBank(ReadRegisterBank readRegisterBank)
        {
            if (!(readRegisterBank.Bank is SymbolExpression))
                throw new NotSupportedException();

            var id = ExpressionToOperand(readRegisterBank.Id);

            var ssa = new LoadRegisterStatement(id);
            currentBlock.AddStatement(ssa);
        }

        public override void VisitWriteRegisterBank(WriteRegisterBank writeRegisterBank)
        {
            if (!(writeRegisterBank.Bank is SymbolExpression))
                throw new NotSupportedException();

            var id = ExpressionToOperand(writeRegisterBank.Id);
            var value = ExpressionToOperand(writeRegisterBank.Value);

            var ssa = new StoreRegisterStatement(value, id);
            currentBlock.AddStatement(ssa);
        }

        public override void VisitFunctionCall(FunctionCall call)
        {
            var ssa = new CallStatement(call.Name);

            foreach (var arg in call.Arguments) {
                arg.Accept(this);
                ssa.AddArgument(currentBlock.Last.AsOperand());
            }

            currentBlock.AddStatement(ssa);
        }

        public override void VisitStructAccess(StructAccess structAccess)
        {
            var ssa = new LoadFieldStatement(structAccess.Member.AsOperand());
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
            this.currentBlock.AddStatement(new LoadValueStatement(symbol.Symbol.AsOperand()));
        }

        public override void VisitAssignmentExpression(AssignmentExpression asnExpression)
        {
            if (!(asnExpression.LHS is SymbolExpression))
                throw new NotSupportedException();

            var rhs = ExpressionToOperand(asnExpression.RHS);

            var ssa = new StoreValueStatement(rhs, ((SymbolExpression)asnExpression.LHS).Symbol.AsOperand());
            this.currentBlock.AddStatement(ssa);
        }

        public override void VisitVariableDeclaration(VariableDeclaration varDecl)
        {
            if (varDecl.Assignment != null) {
                var asn = ExpressionToOperand(varDecl.Assignment);
                var ssa = new StoreValueStatement(asn, varDecl.Name.AsOperand());
                this.currentBlock.AddStatement(ssa);
            }
        }

        public override void VisitIfStatement(IfStatement ifStatement)
        {
            var ssaCond = ExpressionToOperand(ifStatement.Condition);
            var branchBlock = this.currentBlock;

            var trueBlock = this.context.CreateBlock();
            this.currentBlock = trueBlock;

            ifStatement.IfTrue.Accept(this);

            var postBlock = this.context.CreateBlock();

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

            ComparisonStatement.ComparisonType comparisonType;
            switch (equalityExpression.Type) {
            case EqualityOperatorType.Equal:
                comparisonType = ComparisonStatement.ComparisonType.Equal;
                break;
            case EqualityOperatorType.NotEqual:
                comparisonType = ComparisonStatement.ComparisonType.NotEqual;
                break;
            default:
                throw new NotSupportedException();
            }

            var ssa = new ComparisonStatement(lhs, rhs, comparisonType);

            this.currentBlock.AddStatement(ssa);
        }

        private SSAOperand ExpressionToOperand(Expression expr)
        {
            if (expr is IntegerConstantExpression) {
                return ((IntegerConstantExpression)expr).Value.AsOperand();
            } else {
                expr.Accept(this);
            }

            return this.currentBlock.Last.AsOperand();
        }
    }
}

