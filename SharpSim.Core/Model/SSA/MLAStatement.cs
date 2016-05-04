//
// MLAStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class MLAStatement : SSAStatement
    {
        public MLAStatement(SSAOperand mlaBase, SSAOperand addend, SSAOperand shift)
        {
            this.Base = mlaBase;
            this.Addend = addend;
            this.Shift = shift;
        }

        public SSAOperand Base{ get; private set; }

        public SSAOperand Addend{ get; private set; }

        public SSAOperand Shift{ get; private set; }

        public override Fixedness Fixed
        {
            get {
                return this.Base.Fixed == Fixedness.AlwaysFixed &&
                this.Addend.Fixed == Fixedness.AlwaysFixed &&
                this.Shift.Fixed == Fixedness.AlwaysFixed ? Fixedness.AlwaysFixed : Fixedness.Dynamic;
            }
        }

        public override string ToString()
        {
            return string.Format("mla {0}, {1}, {2}", this.Base, this.Addend, this.Shift);
        }
    }
}
