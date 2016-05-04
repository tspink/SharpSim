//
// ISANode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
    public class ISABlock : ASTNode
    {
        private List<FormatDefinition> formatDefinitions = new List<FormatDefinition>();

        public ISABlock(ASTNode.ASTNodeLocation location, string name) : base(location)
        {
            this.Name = name;
        }

        public string Name{ get; private set; }

        public IEnumerable<FormatDefinition> FormatDefinitions{ get { return this.formatDefinitions.AsReadOnly(); } }

        public void AddFormatDefinition(FormatDefinition def)
        {
            if (def == null)
                throw new ArgumentNullException("def");
            this.formatDefinitions.Add(def);
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitISABlock(this);
        }
    }
}

