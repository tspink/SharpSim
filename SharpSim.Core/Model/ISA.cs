//
// ISA.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model
{
	public class ISA
	{
		private Dictionary<string, InstructionFormat> formats = new Dictionary<string, InstructionFormat> ();
		private Dictionary<string, Instruction> instructions = new Dictionary<string, Instruction> ();

		public ISA (Architecture arch, string name)
		{
			if (arch == null)
				throw new ArgumentNullException (nameof (arch));

			if (string.IsNullOrEmpty (name))
				throw new ArgumentNullException ("name");

			this.Architecture = arch;
			this.Name = name;
		}

		public Architecture Architecture { get; private set; }

		public string Name { get; private set; }

		public InstructionFormat CreateInstructionFormat (string name)
		{
			var format = new InstructionFormat (this, name);
			formats.Add (name, format);
			return format;
		}

		public InstructionFormat GetInstructionFormat (string name)
		{
			InstructionFormat format;
			if (!this.formats.TryGetValue (name, out format))
				throw new Exception (string.Format ("Instruction format '{0}' does not exist.", name));

			return format;
		}

		public IEnumerable<InstructionFormat> InstructionFormats { get { return this.formats.Values; } }

		public Instruction CreateInstruction (string name, InstructionFormat format, IEnumerable<InstructionBehaviourInstantiation> behaviours)
		{
			var insn = new Instruction (name, format, behaviours);
			instructions.Add (name, insn);

			return insn;
		}

		public IEnumerable<Instruction> Instructions { get { return this.instructions.Values; } }
	}
}

