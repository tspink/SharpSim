//
// ASTVisitor.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST.Visitor
{
    public abstract class ASTVisitor : IASTVisitor
    {
        public virtual void VisitAddOperator(AddOperator addExpression)
        {
            addExpression.LHS.Accept(this);
            addExpression.RHS.Accept(this);
        }

        public virtual void VisitArchFile(ArchFile file)
        {
            foreach (var isaBlock in file.ISABlocks)
                isaBlock.Accept(this);

            foreach (var behaviour in file.Behaviours)
                behaviour.Accept(this);

            foreach (var helper in file.Helpers)
                helper.Accept(this);
        }

        public virtual void VisitArchIdentifier(ArchIdentifier ident)
        {
        }

        public virtual void VisitAssignmentExpression(AssignmentExpression asnExpression)
        {
            asnExpression.LHS.Accept(this);
            asnExpression.RHS.Accept(this);
        }

        public virtual void VisitBehaviour(Behaviour behaviour)
        {
            behaviour.Body.Accept(this);
        }

        public virtual void VisitBinaryOperator(BinaryOperator binop)
        {
            binop.LHS.Accept(this);
            binop.RHS.Accept(this);
        }

        public virtual void VisitUnaryOperator(UnaryOperator unop)
        {
            unop.Expression.Accept(this);
        }

        public virtual void VisitConstantExpression<T>(ConstantExpression<T> constExpr)
        {
        }

        public virtual void VisitIntegerConstantExpression(IntegerConstantExpression constExpr)
        {
        }

        public virtual void VisitFloatConstantExpression(FloatConstantExpression constExpr)
        {
        }

        public virtual void VisitStringConstantExpression(StringConstantExpression constExpr)
        {
        }

        public virtual void VisitExpression(Expression expr)
        {
        }

        public virtual void VisitEqualityOperator(EqualityOperator equalityExpression)
        {
            equalityExpression.LHS.Accept(this);
            equalityExpression.RHS.Accept(this);
        }

        public virtual void VisitFunctionBody(FunctionBody body)
        {
            foreach (var stmt in body.Statements) {
                stmt.Accept(this);
            }
        }

        public virtual void VisitFunctionCall(FunctionCall call)
        {
            foreach (var arg in call.Arguments)
                arg.Accept(this);
        }

        public virtual void VisitHelper(Helper helper)
        {
            foreach (var parameter in helper.Parameters)
                parameter.Accept(this);
            
            helper.Body.Accept(this);
        }

        public virtual void VisitISABlock(ISABlock block)
        {
        }

        public virtual void VisitIfStatement(IfStatement ifStatement)
        {
            ifStatement.Condition.Accept(this);
            ifStatement.IfTrue?.Accept(this);
            ifStatement.IfFalse?.Accept(this);
        }

        public virtual void VisitParameter(Parameter param)
        {
        }

        public virtual void VisitReadRegister(ReadRegister readRegister)
        {
            readRegister.Id.Accept(this);
        }

        public virtual void VisitReadRegisterBank(ReadRegisterBank readRegisterBank)
        {
            readRegisterBank.Bank.Accept(this);
            readRegisterBank.Id.Accept(this);
        }

        public virtual void VisitReturn(Return ret)
        {
            ret.Value?.Accept(this);
        }

        public virtual void VisitBreak(Break brk)
        {
        }

        public virtual void VisitStructAccess(StructAccess structAccess)
        {
            structAccess.LHS.Accept(this);
        }

        public virtual void VisitSwitch(SwitchStatement switchStatement)
        {
            switchStatement.Value.Accept(this);
        }

        public virtual void VisitSymbolExpression(SymbolExpression symbol)
        {
        }

        public virtual void VisitVariableDeclaration(VariableDeclaration varDecl)
        {
            varDecl.Assignment?.Accept(this);
        }

        public virtual void VisitWriteRegister(WriteRegister writeRegister)
        {
            writeRegister.Id.Accept(this);
            writeRegister.Value.Accept(this);
        }

        public virtual void VisitWriteRegisterBank(WriteRegisterBank writeRegisterBank)
        {
            writeRegisterBank.Bank.Accept(this);
            writeRegisterBank.Id.Accept(this);
            writeRegisterBank.Value.Accept(this);
        }

        public virtual void VisitReadPC(ReadPC readPC)
        {
        }

        public virtual void VisitTernaryOperator(TernaryOperator ternary)
        {
            ternary.Condition.Accept(this);
            ternary.TrueExpression.Accept(this);
            ternary.FalseExpression.Accept(this);
        }

        public virtual void VisitShiftOperator(ShiftOperator shop)
        {
            shop.LHS.Accept(this);
            shop.RHS.Accept(this);
        }

        public virtual void VisitLogicalOperator(LogicalOperator logop)
        {
            logop.LHS.Accept(this);
            logop.RHS.Accept(this);
        }

        public virtual void VisitBitwiseOperator(BitwiseOperator bitop)
        {
            bitop.LHS.Accept(this);
            bitop.RHS.Accept(this);
        }

        public virtual void VisitCastOperator(CastOperator castop)
        {
            castop.Expression.Accept(this);
        }
    }
}
