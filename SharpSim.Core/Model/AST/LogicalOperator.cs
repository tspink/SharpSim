//
// LogicalOperatorExpression.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public enum LogicalOperatorType
    {
        And,
        Or
    }

    public class LogicalOperator : TypedBinaryOperator<LogicalOperatorType>
    {
        public LogicalOperator(ASTNode.ASTNodeLocation location, LogicalOperatorType type, Expression lhs, Expression rhs) : base(location, type, lhs, rhs)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitLogicalOperator(this);
        }
    }
}
