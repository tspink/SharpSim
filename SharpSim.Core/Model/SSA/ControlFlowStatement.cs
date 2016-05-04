//
// ControlFlowStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.SSA
{
    public abstract class ControlFlowStatement : SSAStatement
    {
        public abstract IEnumerable<SSABlock> TargetBlocks{ get; }

        public override SSAType Type
        {
            get {
                return PrimitiveType.Void;
            }
        }
    }
}

