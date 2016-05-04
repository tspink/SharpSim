//
// CastOperator.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class CastOperator : UnaryOperator
    {
        public CastOperator(ASTNode.ASTNodeLocation location, string type, Expression expr) : base(location, expr)
        {
            this.Type = type;
        }

        public string Type{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitCastOperator(this);
        }
    }
}

