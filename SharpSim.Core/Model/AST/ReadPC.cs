//
// ReadPC.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class ReadPC : Expression
    {
        public ReadPC(ASTNode.ASTNodeLocation location) : base(location)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitReadPC(this);
        }
    }
}

