﻿//
// ArchFile.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
    public class ArchFile : ASTNode
    {
        private List<ISABlock> isaBlocks = new List<ISABlock>();
        private List<Behaviour> behaviours = new List<Behaviour>();
        private List<Helper> helpers = new List<Helper>();

        public ArchFile(ASTNode.ASTNodeLocation location, ArchIdentifier ident) : base(location)
        {
            this.Identifier = ident;
        }

        public ArchIdentifier Identifier{ get; private set; }

        public IEnumerable<ISABlock> ISABlocks{ get { return this.isaBlocks.AsReadOnly(); } }

        public void AddISABlock(ISABlock isa)
        {
            this.isaBlocks.Add(isa);
        }

        public IEnumerable<Behaviour> Behaviours{ get { return this.behaviours.AsReadOnly(); } }

        public void AddBehaviour(Behaviour behaviour)
        {
            this.behaviours.Add(behaviour);
        }

        public IEnumerable<Helper> Helpers{ get { return this.helpers.AsReadOnly(); } }

        public void AddHelper(Helper helper)
        {
            this.helpers.Add(helper);
        }

        public override void Accept(Visitor.IASTVisitor visitor)
        {
            visitor.VisitArchFile(this);
        }
    }
}