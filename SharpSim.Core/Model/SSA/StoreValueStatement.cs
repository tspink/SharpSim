//
// StoreValueStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public class StoreValueStatement : SSAStatement
	{
		public StoreValueStatement(SSAOperand value, SymbolOperand symbol)
		{
			this.Value = value;
			this.Symbol = symbol;
		}

		public SymbolOperand Symbol { get; private set; }

		public SSAOperand Value{ get; private set; }

		public override Fixedness Fixed {
			get {
				if (this.Symbol.Fixed == Fixedness.Dynamic)
					return Fixedness.Dynamic;
				return this.Value.Fixed;
			}
		}

		public override SSAType Type {
			get {
				return SSAType.None;
			}
		}

		public override string ToString()
		{
			return string.Format("stv {0}, {1}", this.Value, this.Symbol);
		}
	}
}

