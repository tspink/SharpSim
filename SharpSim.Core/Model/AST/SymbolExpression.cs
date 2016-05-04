//
// SymbolExpressionNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class SymbolExpression : Expression
    {
        public SymbolExpression(ASTNode.ASTNodeLocation location, string symbol) : base(location)
        {
            this.Symbol = symbol;
        }

        public string Symbol{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitSymbolExpression(this);
        }
    }
}

