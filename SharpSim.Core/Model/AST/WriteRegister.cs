//
// WriteRegister.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class WriteRegister : Expression
    {
        public WriteRegister(ASTNode.ASTNodeLocation location, Expression id, Expression value) : base(location)
        {
            this.Id = id;
            this.Value = value;
        }

        public Expression Id{ get; private set; }

        public Expression Value{ get; private set; }
    }
}

