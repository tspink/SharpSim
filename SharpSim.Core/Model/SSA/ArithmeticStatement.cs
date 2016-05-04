//
// ArithmeticStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class ArithmeticStatement : BinaryStatement
    {
        public enum ArithmeticOperation
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Modulo,
            ShiftLeft,
            LogicalShiftRight,
            ArithmeticShiftRight
        }

        public ArithmeticStatement(SSAOperand lhs, SSAOperand rhs, ArithmeticOperation type) : base(lhs, rhs)
        {
            this.Type = type;
        }

        public ArithmeticOperation Type{ get; private set; }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            switch (this.Type) {
            case ArithmeticOperation.Add:
                builder.Append("add");
                break;

            case ArithmeticOperation.ShiftLeft:
                builder.Append("shl");
                break;

            default:
                builder.Append("?");
                break;
            }

            builder.AppendFormat(" {0}, {1}", this.LHS, this.RHS);

            return builder.ToString();
        }
    }
}

