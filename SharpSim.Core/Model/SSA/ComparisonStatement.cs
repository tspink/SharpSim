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
		public enum ComparisonKind
		{
			Equal,
			NotEqual,
			LessThan,
			LessThanOrEqual,
			GreaterThan,
			GreaterThanOrEqual
		}

		public ComparisonStatement(TypedSSAOperand lhs, TypedSSAOperand rhs, ComparisonKind kind)
			: base(lhs, rhs)
		{
			this.Kind = kind;
		}

		public ComparisonKind Kind { get; private set; }

		public override SSAType Type {
			get {
				return PrimitiveType.Boolean;
			}
		}

		public override string ToString()
		{
			var builder = new System.Text.StringBuilder();
			builder.Append("cmp");

			switch (this.Kind) {
			case ComparisonKind.Equal:
				builder.Append("eq");
				break;
			case ComparisonKind.NotEqual:
				builder.Append("ne");
				break;
			case ComparisonKind.GreaterThan:
				builder.Append("gt");
				break;
			case ComparisonKind.GreaterThanOrEqual:
				builder.Append("ge");
				break;
			case ComparisonKind.LessThan:
				builder.Append("lt");
				break;
			case ComparisonKind.LessThanOrEqual:
				builder.Append("le");
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
    