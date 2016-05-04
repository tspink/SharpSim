//
// ArchIdentifierNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class ArchIdentifier : ASTNode
    {
        public ArchIdentifier(ASTNode.ASTNodeLocation location, string ident) : base(location)
        {
            this.Identifier = ident;
        }

        public string Identifier{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitArchIdentifier(this);
        }
    }
}

