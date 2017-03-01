//
// Instruction.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model
{
	public class Instruction
	{
		public class DecodeMatch
		{
		}

		public Instruction(string name, InstructionFormat format, IEnumerable<InstructionBehaviourInstantiation> behaviours)
		{
			this.Name = name;
		}

		public string Name{ get; private set; }

		public void AddDecodeMatch(DecodeMatch match)
		{
		}
	}
}

