//
// UnaryOperator.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public abstract class UnaryOperator : Expression
    {
        public UnaryOperator(ASTNode.ASTNodeLocation location, Expression expr) : base(location)
        {
            this.Expression = expr;
        }

        public Expression Expression{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitUnaryOperator(this);
        }
    }
}

