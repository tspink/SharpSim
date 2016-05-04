//
// FinishStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class LeaveStatement : ControlFlowStatement
    {
        public override Fixedness Fixed
        {
            get {
                return Fixedness.AlwaysFixed;
            }
        }

        public override System.Collections.Generic.IEnumerable<SSABlock> TargetBlocks
        {
            get {
                return new SSABlock[0];
            }
        }

        public override string ToString()
        {
            return string.Format("leave");
        }
    }
}

