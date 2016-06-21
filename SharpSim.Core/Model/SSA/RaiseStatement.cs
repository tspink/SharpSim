//
// RaiseStatement.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public class RaiseStatement : ControlFlowStatement
	{
		public RaiseStatement(SymbolOperand exceptionType)
		{
			if (exceptionType == null)
				throw new ArgumentNullException(nameof(exceptionType));
			
			this.ExceptionType = exceptionType;
		}

		public SymbolOperand ExceptionType{ get; private set; }

		public override Fixedness Fixed {
			get {
				return Fixedness.AlwaysFixed;
			}
		}

		public override System.Collections.Generic.IEnumerable<SSABlock> TargetBlocks {
			get {
				return new SSABlock[0];
			}
		}

		public override string ToString()
		{
			return string.Format("raise {0}", this.ExceptionType);
		}
	}
}

