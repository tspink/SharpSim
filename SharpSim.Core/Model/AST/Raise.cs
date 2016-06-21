//
// Raise.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
	public class Raise : Statement
	{
		public Raise(ASTNode.ASTNodeLocation location, Expression value)
			: base(location)
		{
			this.Value = value;
		}

		public Expression Value{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitRaise(this);
		}
	}
}

