//
// RegisterDefinition.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public abstract class RegisterDefinition : ASTNode
    {
        public RegisterDefinition(ASTNode.ASTNodeLocation location, string name, string type) : base(location)
        {
        }

        public string Name{ get; private set; }

        public string Type{ get; private set; }
    }
}

