//
// StructAccessExpressionNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class StructAccess:Expression
    {
        public StructAccess(ASTNode.ASTNodeLocation location, Expression lhs, string member) : base(location)
        {
            this.LHS = lhs;
            this.Member = member;
        }

        public Expression LHS{ get; private set; }

        public string Member{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitStructAccess(this);
        }
    }
}

