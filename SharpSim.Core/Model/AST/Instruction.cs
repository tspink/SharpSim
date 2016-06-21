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
	public class Instruction : ASTNode
	{
		private List<InstructionPart> parts = new List<InstructionPart>();

		public Instruction(ASTNode.ASTNodeLocation location, string name, string format)
			: base(location)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			if (string.IsNullOrEmpty(format))
				throw new ArgumentNullException(nameof(format));

			this.Name = name;
			this.FormatName = format;
		}

		public string Name{ get; private set; }

		public string FormatName{ get; private set; }

		public void AddPart(InstructionPart part)
		{
			parts.Add(part);
		}

		public IEnumerable<InstructionPart> Parts{ get { return this.parts.AsReadOnly(); } }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitInstruction(this);
		}
	}
}

