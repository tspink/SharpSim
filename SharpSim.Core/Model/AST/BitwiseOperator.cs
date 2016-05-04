//
// BitwiseOperator.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public enum BitwiseOperatorType
    {
        And,
        Or,
        Xor
    }

    public class BitwiseOperator : TypedBinaryOperator<BitwiseOperatorType>
    {
        public BitwiseOperator(ASTNode.ASTNodeLocation location, BitwiseOperatorType type, Expression lhs, Expression rhs)
            : base(location, type, lhs, rhs)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitBitwiseOperator(this);
        }
    }
}

