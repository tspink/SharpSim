//
// AddExpression.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public enum AddOperatorType
    {
        Add,
        Subtract
    }

    public class AddOperator : TypedBinaryOperator<AddOperatorType>
    {
        public AddOperator(ASTNode.ASTNodeLocation location, AddOperatorType type, Expression lhs, Expression rhs) : base(location, type, lhs, rhs)
        {
        }

        public override void Accept(Visitor.IASTVisitor visitor)
        {
            visitor.VisitAddOperator(this);
        }
    }
}

