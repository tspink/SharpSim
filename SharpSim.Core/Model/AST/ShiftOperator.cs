//
// ShiftOperator.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public enum ShiftOperatorType
    {
        ShiftLeft,
        RotateLeft,
        ShiftRight,
        RotateRight
    }

    public class ShiftOperator : TypedBinaryOperator<ShiftOperatorType>
    {
        public ShiftOperator(ASTNode.ASTNodeLocation location, ShiftOperatorType type, Expression lhs, Expression rhs)
            : base(location, type, lhs, rhs)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitShiftOperator(this);
        }
    }
}

