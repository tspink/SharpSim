//
// InstructionBase.cs
//
// Copyright (c) 2017 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
	public abstract class InstructionBase : ASTNode
	{
		private List<InstructionPart> parts = new List<InstructionPart> ();

		public InstructionBase (ASTNodeLocation location) : base (location)
		{
		}

		public void AddPart (InstructionPart part)
		{
			parts.Add (part);
		}

		public IEnumerable<InstructionPart> Parts { get { return this.parts.AsReadOnly (); } }
	}
}
