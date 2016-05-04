//
// StatementNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public abstract class Statement : ASTNode
    {
        public Statement(ASTNode.ASTNodeLocation location) : base(location)
        {
        }
    }
}

