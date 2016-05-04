//
// Behaviour.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model
{
    public class Behaviour
    {
        public Behaviour(string name, SSA.SSAContext ssa)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (ssa == null)
                throw new ArgumentNullException("ssa");

            this.Name = name;
            this.SSA = ssa;
        }

        public string Name{ get; private set; }

        public SSA.SSAContext SSA{ get; private set; }
    }
}

