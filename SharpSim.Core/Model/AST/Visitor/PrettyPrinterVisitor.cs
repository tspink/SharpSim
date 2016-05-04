//
// PrettyPrinterVisitor.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST.Visitor
{
    public class PrettyPrinterVisitor : ASTVisitor
    {
        private int indent;

        public override void VisitArchFile(ArchFile node)
        {
            Console.WriteLine("Arch File: {0}", node.Location.Filename);
            base.VisitArchFile(node);
        }

        public override void VisitArchIdentifier(ArchIdentifier node)
        {
            Console.WriteLine("Architecture: {0}", node.Identifier);
            base.VisitArchIdentifier(node);
        }

        public override void VisitBehaviour(Behaviour node)
        {
            Console.WriteLine("Behaviour: {0}", node.Name);

            indent = 0;
            base.VisitBehaviour(node);
        }

        public override void VisitHelper(Helper node)
        {
            Console.Write("Helper");

            if (node.IsNoInline)
                Console.Write(" noinline");

            Console.Write(": {0} {1}(", node.ReturnType, node.Name);

            bool first = true;
            foreach (var parameter in node.Parameters) {
                if (first)
                    first = false;
                else
                    Console.Write(", ");
                
                parameter.Accept(this);
            }

            Console.WriteLine(")");

            indent = 0;
            node.Body.Accept(this);
        }

        public override void VisitParameter(Parameter param)
        {
            Console.Write("{0}{1} {2}", param.Type, param.Reference ? "&" : string.Empty, param.Name);
        }

        public override void VisitVariableDeclaration(VariableDeclaration node)
        {
            Console.Write("{0} {1}", node.Type, node.Name);
            if (node.Assignment != null) {
                Console.Write(" = ");
                node.Assignment.Accept(this);
            }
        }

        public override void VisitAddOperator(AddOperator node)
        {
            node.LHS.Accept(this);
            switch (node.Type) {
            case AddOperatorType.Add:
                Console.Write(" + ");
                break;
            case AddOperatorType.Subtract:
                Console.Write(" - ");
                break;
            }
            node.RHS.Accept(this);
        }

        public override void VisitEqualityOperator(EqualityOperator equalityExpression)
        {
            equalityExpression.LHS.Accept(this);
            switch (equalityExpression.Type) {
            case EqualityOperatorType.Equal:
                Console.Write(" == ");
                break;
            case EqualityOperatorType.NotEqual:
                Console.Write(" != ");
                break;
            }
            equalityExpression.RHS.Accept(this);
        }

        public override void VisitReadRegisterBank(ReadRegisterBank node)
        {
            Console.Write("REGS[");
            node.Bank.Accept(this);
            Console.Write("][");
            node.Id.Accept(this);
            Console.Write("]");
        }

        public override void VisitReadRegister(ReadRegister readRegister)
        {
            Console.Write("REGS[");
            readRegister.Id.Accept(this);
            Console.Write("]");
        }

        public override void VisitFunctionBody(FunctionBody body)
        {
            Console.WriteLine("{");

            indent++;
            foreach (var stmt in body.Statements) {
                PrintIndent();

                stmt.Accept(this);
            
                Console.WriteLine();
            }
            indent--;

            PrintIndent();
            Console.WriteLine("}");
        }

        public override void VisitSymbolExpression(SymbolExpression node)
        {
            Console.Write("{0}", node.Symbol);
        }

        public override void VisitStructAccess(StructAccess structAccess)
        {
            structAccess.LHS.Accept(this);
            Console.Write(".");
            Console.Write("{0}", structAccess.Member);
        }

        public override void VisitWriteRegisterBank(WriteRegisterBank writeRegisterBank)
        {
            Console.Write("REGS[");
            writeRegisterBank.Bank.Accept(this);
            Console.Write("][");
            writeRegisterBank.Id.Accept(this);
            Console.Write("] = ");
            writeRegisterBank.Value.Accept(this);
        }

        public override void VisitReadPC(ReadPC readPC)
        {
            Console.Write("read_pc()");
        }

        public override void VisitFunctionCall(FunctionCall call)
        {
            bool first = true;

            Console.Write("{0}(", call.Name);
            foreach (var arg in call.Arguments) {
                if (first)
                    first = false;
                else
                    Console.Write(", ");
                arg.Accept(this);
            }
            Console.Write(")");
        }

        public override void VisitIntegerConstantExpression(IntegerConstantExpression constExpr)
        {
            Console.Write("{0}", constExpr.Value);
        }

        public override void VisitAssignmentExpression(AssignmentExpression asnExpression)
        {
            asnExpression.LHS.Accept(this);
            Console.Write(" = ");
            asnExpression.RHS.Accept(this);
        }

        public override void VisitIfStatement(IfStatement ifStatement)
        {
            Console.Write("if (");
            ifStatement.Condition.Accept(this);
            Console.Write(") ");
            ifStatement.IfTrue.Accept(this);

            if (ifStatement.IfFalse != null) {
                Console.Write(" else ");
                ifStatement.IfFalse.Accept(this);
            }
        }

        public override void VisitReturn(Return ret)
        {
            Console.Write("return ");
            ret.Value?.Accept(this);
        }

        public override void VisitBreak(Break brk)
        {
            Console.Write("break");
        }

        public override void VisitSwitch(SwitchStatement switchStatement)
        {
            Console.Write("switch (");
            switchStatement.Value.Accept(this);
            Console.Write(") {");

            foreach (var switchCase in switchStatement.Cases) {
                Console.Write("case ");
                switchCase.Item1.Accept(this);
                Console.Write(":");
                switchCase.Item2.Accept(this);
            }

            Console.WriteLine("}");
        }

        public override void VisitTernaryOperator(TernaryOperator ternary)
        {
            ternary.Condition.Accept(this);
            Console.Write(" ? ");
            ternary.TrueExpression.Accept(this);
            Console.Write(" : ");
            ternary.FalseExpression.Accept(this);
        }

        public override void VisitBitwiseOperator(BitwiseOperator bitop)
        {
            bitop.LHS.Accept(this);
            switch (bitop.Type) {
            case BitwiseOperatorType.And:
                Console.Write(" & ");
                break;
            case BitwiseOperatorType.Or:
                Console.Write(" | ");
                break;
            case BitwiseOperatorType.Xor:
                Console.Write(" ^ ");
                break;
            }
            bitop.RHS.Accept(this);
        }

        public override void VisitShiftOperator(ShiftOperator shop)
        {
            shop.LHS.Accept(this);
            switch (shop.Type) {
            case ShiftOperatorType.RotateLeft:
                Console.Write(" <<< ");
                break;
            case ShiftOperatorType.RotateRight:
                Console.Write(" >>> ");
                break;
            case ShiftOperatorType.ShiftLeft:
                Console.Write(" << ");
                break;
            case ShiftOperatorType.ShiftRight:
                Console.Write(" >> ");
                break;
            }
            shop.RHS.Accept(this);
        }

        public override void VisitCastOperator(CastOperator castop)
        {
            Console.Write("({0})", castop.Type);
            castop.Expression.Accept(this);
        }

        private void PrintIndent()
        {
            for (int i = 0; i < indent; i++) {
                Console.Write("  ");
            }
        }
    }
}

