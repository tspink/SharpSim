//
// ReadRegisterBankExpressionNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class ReadRegisterBank : Expression
    {
        public ReadRegisterBank(ASTNode.ASTNodeLocation location, Expression bank, Expression id) : base(location)
        {
            this.Bank = bank;
            this.Id = id;
        }

        public Expression Bank{ get; private set; }

        public Expression Id{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitReadRegisterBank(this);
        }
    }
}
    