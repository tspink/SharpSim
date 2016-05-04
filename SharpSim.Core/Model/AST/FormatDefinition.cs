//
// FormatDefinition.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
    public class FormatDefinition : ASTNode
    {
        private List<FormatFieldDefinition> fieldDefinitions = new List<FormatFieldDefinition>();

        public FormatDefinition(ASTNode.ASTNodeLocation location, string name) : base(location)
        {
            this.Name = name;
        }

        public string Name{ get; private set; }

        public void AddFieldDefinition(FormatFieldDefinition fieldDef)
        {
            this.fieldDefinitions.Add(fieldDef);
        }

        public IEnumerable<FormatFieldDefinition> FieldDefinitions{ get { return this.fieldDefinitions.AsReadOnly(); } }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitFormatDefinition(this);
        }
    }
}

