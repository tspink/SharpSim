//
// LoadRegisterStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class LoadRegisterStatement : RegisterAccessStatement
    {
        public LoadRegisterStatement(SSAOperand fileOffset) : base(fileOffset)
        {
        }

        public override string ToString()
        {
            return string.Format("ldr {0}", this.FileOffset);
        }
    }
}

