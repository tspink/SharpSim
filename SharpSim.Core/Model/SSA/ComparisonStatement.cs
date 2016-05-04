//
// ComparisonStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class ComparisonStatement : BinaryStatement
    {
        public enum ComparisonType
        {
            Equal,
            NotEqual,
            LessThan,
            LessThanOrEqual,
            GreaterThan,
            GreaterThanOrEqual
        }

        public ComparisonStatement(SSAOperand lhs, SSAOperand rhs, ComparisonType type) : base(lhs, rhs)
        {
            this.Type = type;
        }

        public ComparisonType Type{ get; private set; }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("cmp");

            switch (this.Type) {
            case ComparisonType.Equal:
                builder.Append("eq");
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
    