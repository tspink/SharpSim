//
// SSAStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public abstract class SSAStatement
    {
        public enum Fixedness
        {
            Unknown,
            AlwaysFixed,
            SometimesFixed,
            Dynamic
        }

        public uint Index{ get; internal set; }

        public SSABlock Owner{ get; internal set; }

        public abstract Fixedness Fixed{ get; }
    }
}

