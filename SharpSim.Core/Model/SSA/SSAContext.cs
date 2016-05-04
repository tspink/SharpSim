//
// SSAContext.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.SSA
{
    public class SSAContext
    {
        private List<SSABlock> blocks = new List<SSABlock>();
        private uint nextStatementIndex = 0;
        private uint nextBlockIndex = 0;

        public SSAContext()
        {
            this.EntryBlock = CreateBlock();
        }

        public SSABlock CreateBlock()
        {
            var block = new SSABlock();
            this.AddBlock(block);
            return block;
        }

        public void AddBlock(SSABlock block)
        {
            if (block.Owner != null)
                throw new Exception("Block is already owned by a context");
            
            block.Index = GetUniqueBlockIndex();
            block.Owner = this;

            this.blocks.Add(block);
        }

        public SSABlock EntryBlock{ get; private set; }

        internal uint GetUniqueStatementIndex()
        {
            uint ret = this.nextStatementIndex;
            this.nextStatementIndex++;
            return ret;
        }

        internal uint GetUniqueBlockIndex()
        {
            uint ret = this.nextBlockIndex;
            this.nextBlockIndex++;
            return ret;
        }

        public override string ToString()
        {
            var builder = new System.Text. StringBuilder();
            builder.AppendLine("SSA Context:");

            foreach (var block in this.blocks) {
                builder.AppendLine(block.ToString());
            }

            return builder.ToString();
        }
    }
}

