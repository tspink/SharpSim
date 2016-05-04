//
// SSAAction.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.SSA
{
    public class SSAAction
    {
        private List<SSABlock> blocks = new List<SSABlock>();
        private uint nextStatementIndex = 0;
        private uint nextBlockIndex = 0;

        public SSAAction(SSAContext owner, string name, SSAActionPrototype prototype)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (prototype == null)
                throw new ArgumentNullException("prototype");

            this.Owner = owner;
            this.Name = name;
            this.Prototype = prototype;
            this.EntryBlock = CreateBlock();
        }

        public SSAContext Owner{ get; private set; }

        public string Name{ get; private set; }

        public SSAActionPrototype Prototype{ get; private set; }

        public SSABlock CreateBlock()
        {
            var block = new SSABlock(this);
            block.Index = GetUniqueBlockIndex();

            this.blocks.Add(block);
            return block;
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
            builder.AppendLine(string.Format("SSA Action {0}:", this.Name));

            foreach (var block in this.blocks) {
                builder.AppendLine(block.ToString());
            }

            return builder.ToString();
        }
    }
}

