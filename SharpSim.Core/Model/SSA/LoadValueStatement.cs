//
// LoadValueStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class LoadValueStatement : SSAStatement
    {
        public LoadValueStatement(SymbolOperand symbol)
        {
            this.Symbol = symbol;
        }

        public SymbolOperand Symbol{ get; private set; }

        public override Fixedness Fixed
        {
            get {
                return Fixedness.Dynamic;
            }
        }

        public override SSAType Type
        {
            get {
                return this.Symbol.Value.Type;
            }
        }

        public override string ToString()
        {
            return string.Format("ldv {0}", this.Symbol);
        }
    }
}

