//
// ReturnStatement.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public class ReturnStatement : ControlFlowStatement
	{
		public ReturnStatement (SSAOperand value)
		{
			this.Value = value;
		}

		public SSAOperand Value { get; private set; }

		public override System.Collections.Generic.IEnumerable<SSABlock> TargetBlocks {
			get {
				return new SSABlock [0];
			}
		}

		public override Fixedness Fixed {
			get {
				return Fixedness.AlwaysFixed;
			}
		}

		public override SSAStatement Clone ()
		{
			if (this.Value == null)
				return new ReturnStatement (null);
			else
				return new ReturnStatement (this.Value.Clone ());
		}

		public override string ToString ()
		{
			if (this.Value == null)
				return "ret";
			else
				return string.Format ("ret {0}", this.Value);
		}
	}
}

