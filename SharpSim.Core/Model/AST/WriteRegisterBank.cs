//
// WriteRegisterBankStatement.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class WriteRegisterBank : Expression
    {
        public WriteRegisterBank(ASTNode.ASTNodeLocation location, Expression bank, Expression id, Expression value) : base(location)
        {
            this.Bank = bank;
            this.Id = id;
            this.Value = value;
        }

        public Expression Bank{ get; private set; }

        public Expression Id{ get; private set; }

        public Expression Value{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitWriteRegisterBank(this);
        }
    }
}
