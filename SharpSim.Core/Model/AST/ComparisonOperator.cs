//
// ComparisonOperator.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
	public enum ComparisonOperatorType
	{
		GreaterThan,
		GreaterThanOrEqual,
		LessThan,
		LessThanOrEqual
	}

	public class ComparisonOperator : TypedBinaryOperator<ComparisonOperatorType>
	{
		public ComparisonOperator(ASTNode.ASTNodeLocation loc, ComparisonOperatorType type, Expression lhs, Expression rhs)
			: base(loc, type, lhs, rhs)
		{
		}

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitComparisonOperator(this);
		}
	}
}

