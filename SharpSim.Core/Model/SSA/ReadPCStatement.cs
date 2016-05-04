//
// ReadPCStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class ReadPCStatement : SSAStatement
    {
        public ReadPCStatement()
        {
        }

        public override Fixedness Fixed
        {
            get {
                return Fixedness.Dynamic;
            }
        }

        public override string ToString()
        {
            return "rpc";
        }
    }
}

