//
// ISANode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class ISABlock : ASTNode
    {
        public ISABlock(ASTNode.ASTNodeLocation location) : base(location)
        {
            
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitISABlock(this);
        }
    }
}

