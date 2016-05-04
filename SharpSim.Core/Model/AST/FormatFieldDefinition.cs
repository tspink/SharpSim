//
// FormatFieldDefinition.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class FormatFieldDefinition : ASTNode
    {
        public FormatFieldDefinition(ASTNode.ASTNodeLocation location, string name, int width) : base(location)
        {
            this.Name = name;
            this.Width = width;
        }

        public string Name{ get; private set; }

        public int Width{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitFormatFieldDefinition(this);
        }
    }
}

