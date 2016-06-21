//
// MatchExpression.cs
//
// Copyright (C) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
	public abstract class MatchExpression : ASTNode
	{
		public MatchExpression(ASTNode.ASTNodeLocation location)
			: base(location)
		{
		}

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitMatchExpression(this);
		}
	}

	public class BinaryMatchExpression : MatchExpression
	{
		public enum BinaryMatchExpressionType
		{
			And,
			Or
		}

		public BinaryMatchExpression(ASTNode.ASTNodeLocation location, MatchExpression lhs, MatchExpression rhs, BinaryMatchExpressionType type)
			: base(location)
		{
			this.LHS = lhs;
			this.RHS = rhs;
			this.Type = type;
		}

		public MatchExpression LHS{ get; private set; }

		public MatchExpression RHS{ get; private set; }

		public BinaryMatchExpressionType Type{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitBinaryMatchExpression(this);
		}
	}

	public class ComparisonMatchExpression:MatchExpression
	{
		public enum ComparisonMatchExpressionType
		{
			Equal,
			NotEqual
		}

		public ComparisonMatchExpression(ASTNode.ASTNodeLocation location, string field, int value, ComparisonMatchExpressionType type)
			: base(location)
		{
			this.InstructionField = field;
			this.Value = value;
			this.Type = type;
		}

		public string InstructionField{ get; private set; }

		public int Value{ get; private set; }

		public ComparisonMatchExpressionType Type{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitComparisonMatchExpression(this);
		}
	}
}
