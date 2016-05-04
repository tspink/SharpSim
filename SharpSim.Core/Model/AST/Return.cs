//
// Return.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class Return : Statement
    {
        public Return(ASTNode.ASTNodeLocation location) : this(location, null)
        { 
        }

        public Return(ASTNode.ASTNodeLocation location, Expression value) : base(location)
        {
            this.Value = value;
        }

        public Expression Value{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitReturn(this);
        }
    }
}

