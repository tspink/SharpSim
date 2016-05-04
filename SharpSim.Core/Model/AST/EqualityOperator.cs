//
// EqualityExpression.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public enum EqualityOperatorType
    {
        Equal,
        NotEqual
    }

    public class EqualityOperator : TypedBinaryOperator<EqualityOperatorType>
    {
        public EqualityOperator(ASTNode.ASTNodeLocation location, EqualityOperatorType type, Expression lhs, Expression rhs) : base(location, type, lhs, rhs)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitEqualityOperator(this);
        }

        public static EqualityOperatorType FromToken(string token)
        {
            switch (token) {
            case "==":
                return EqualityOperatorType.Equal;
            case "!=":
                return EqualityOperatorType.NotEqual;
            default:
                throw new Exception("Not a valid token");
            }
        }
    }
}

