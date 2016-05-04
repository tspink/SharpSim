//
// TernaryExpression.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    using Visitor;

    public class TernaryOperator : Expression
    {
        public TernaryOperator(ASTNode.ASTNodeLocation location, Expression cond, Expression trueExpr, Expression falseExpr) : base(location)
        {
            this.Condition = cond;
            this.TrueExpression = trueExpr;
            this.FalseExpression = falseExpr;
        }

        public Expression Condition{ get; private set; }

        public Expression TrueExpression { get; private set; }

        public Expression FalseExpression{ get; private set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.VisitTernaryOperator(this);
        }
    }
}

