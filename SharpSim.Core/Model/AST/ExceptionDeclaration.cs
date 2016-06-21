//
// ExceptionDeclaration.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
	public class ExceptionDeclaration : ASTNode
	{
		public ExceptionDeclaration(ASTNode.ASTNodeLocation location, string type)
			: base(location)
		{
			this.ExceptionType = type;
		}

		public string ExceptionType{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitExceptionDeclaration(this);
		}
	}
}

