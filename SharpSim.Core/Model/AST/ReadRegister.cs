//
// ReadRegisterExpression.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class ReadRegister : Expression
    {
        public ReadRegister(ASTNode.ASTNodeLocation location, Expression id) : base(location)
        {
            this.Id = id;
        }

        public Expression Id{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitReadRegister(this);
        }
    }
}

