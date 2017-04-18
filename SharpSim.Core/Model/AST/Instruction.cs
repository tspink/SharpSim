//
// Instruction.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
	public class Instruction : InstructionBase
	{

		public Instruction (ASTNode.ASTNodeLocation location, string name, string format)
			: base (location)
		{
			if (string.IsNullOrEmpty (name))
				throw new ArgumentNullException (nameof (name));

			if (string.IsNullOrEmpty (format))
				throw new ArgumentNullException (nameof (format));

			this.Name = name;
			this.FormatName = format;
		}

		public string Name { get; private set; }

		public string FormatName { get; private set; }

		public override void Accept (SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitInstruction (this);
		}
	}
}

