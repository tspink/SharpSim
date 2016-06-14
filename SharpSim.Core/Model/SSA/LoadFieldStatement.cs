//
// LoadFieldStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class LoadFieldStatement : SSAStatement
    {
        public LoadFieldStatement(SymbolOperand field)
        {
            this.Field = field;
        }

        public SymbolOperand Field{ get; private set; }

        public override Fixedness Fixed
        {
            get {
                return Fixedness.AlwaysFixed;
            }
        }

        public override string ToString()
        {
            return string.Format("ldf {0}", this.Field);
        }
    }
}
