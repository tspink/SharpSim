//
// ModelAction.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model
{
    public abstract class ModelAction
    {
        public ModelAction(string name, SSA.SSAAction action)
        {
            this.Name = name;
            this.Action = action;
        }

        public string Name{ get; private set; }

        public SSA.SSAAction Action{ get; private set; }
    }
}

