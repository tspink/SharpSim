//
// DefaultInstruction.cs
//
// Copyright (c) 2017 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
namespace SharpSim.Model.AST
{
	public class DefaultInstruction : InstructionBase
	{
		public DefaultInstruction (ASTNodeLocation location) : base (location)
		{
		}

		public override void Accept (Visitor.IASTVisitor visitor)
		{
			visitor.VisitDefaultInstruction (this);
		}
	}
}
