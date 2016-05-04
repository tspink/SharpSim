//
// AssignmentExpression.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class AssignmentExpression : Expression
    {
        public AssignmentExpression(ASTNode.ASTNodeLocation location, Expression lhs, Expression rhs) : base(location)
        {
            this.LHS = lhs;
            this.RHS = rhs;
        }

        public Expression LHS{ get; private set; }

        public Expression RHS{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitAssignmentExpression(this);
        }
    }

}

