//
// ISANode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
	public class ISABlock : ASTNode
	{
		private List<FormatDefinition> formatDefinitions = new List<FormatDefinition> ();
		private List<Instruction> instructions = new List<Instruction> ();

		public ISABlock (ASTNode.ASTNodeLocation location, string name)
			: base (location)
		{
			this.Name = name;
		}

		public string Name { get; private set; }

		public IEnumerable<FormatDefinition> FormatDefinitions { get { return this.formatDefinitions.AsReadOnly (); } }

		public void AddFormatDefinition (FormatDefinition def)
		{
			if (def == null)
				throw new ArgumentNullException ("def");
			this.formatDefinitions.Add (def);
		}

		public IEnumerable<Instruction> Instructions { get { return this.instructions.AsReadOnly (); } }

		public DefaultInstruction DefaultInstruction { get; private set; }

		public void AddInstruction (Instruction instruction)
		{
			this.instructions.Add (instruction);
		}

		public void SetDefaultInstruction (DefaultInstruction defaultInstruction)
		{
			if (this.DefaultInstruction != null)
				throw new Exception ("Default instruction already registered");

			this.DefaultInstruction = defaultInstruction;
		}

		public override void Accept (SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitISABlock (this);
		}
	}
}

