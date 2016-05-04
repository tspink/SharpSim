//
// RegisterStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public abstract class RegisterAccessStatement : SSAStatement
    {
        public RegisterAccessStatement(SSAOperand fileOffset, SSAType registerType)
        {
            this.FileOffset = fileOffset;
            this.RegisterType = registerType;
        }

        public SSAOperand FileOffset{ get; private set; }

        public SSAType RegisterType{ get; private set; }

        public override Fixedness Fixed
        {
            get {
                return Fixedness.Dynamic;
            }
        }
    }
}

