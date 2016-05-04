//
// BinaryOperatorNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public abstract class BinaryOperator : Expression
    {
        public BinaryOperator(ASTNode.ASTNodeLocation location, Expression lhs, Expression rhs) : base(location)
        {
            this.LHS = lhs;
            this.RHS = rhs;
        }

        public Expression LHS{ get; private set; }

        public Expression RHS{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitBinaryOperator(this);
        }
    }

    public abstract class TypedBinaryOperator<TType> : BinaryOperator
    {
        public TypedBinaryOperator(ASTNode.ASTNodeLocation location, TType type, Expression lhs, Expression rhs) : base(location, lhs, rhs)
        {
            this.Type = type;
        }

        public TType Type{ get; private set; }
    }

}

