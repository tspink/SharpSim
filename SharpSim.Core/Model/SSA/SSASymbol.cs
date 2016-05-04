//
// SSASymbol.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class SSASymbol
    {
        public SSASymbol(string name, SSAType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name{ get; private set; }

        public SSAType Type{ get; private set; }

        public override string ToString()
        {
            return string.Format("{0} ${1}", this.Type, this.Name);
        }
    }
}

