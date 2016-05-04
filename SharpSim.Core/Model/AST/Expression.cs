//
// ExpressionNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public abstract class Expression : Statement
    {
        public Expression(ASTNode.ASTNodeLocation location) : base(location)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitExpression(this);
        }
    }
}