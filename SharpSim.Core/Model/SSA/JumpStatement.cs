//
// JumpStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public class JumpStatement : ControlFlowStatement
    {
        public JumpStatement(BlockOperand target)
        {
            this.Target = target;
        }

        public BlockOperand Target{ get; set; }

        public override Fixedness Fixed
        {
            get {
                return this.Target.Fixed;
            }
        }

        public override System.Collections.Generic.IEnumerable<SSABlock> TargetBlocks
        {
            get {
                return new SSABlock[]{ this.Target.Value };
            }
        }

        public override string ToString()
        {
            return string.Format("jmp {0}", this.Target);
        }
    }
}

