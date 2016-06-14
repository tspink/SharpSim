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
        public RegisterDefinition(ASTNode.ASTNodeLocation location, string name, string type, int offset) : base(location)
        {
            this.Name = name;
            this.Type = type;
            this.Offset = offset;
        }

        public string Name{ get; private set; }

        public string Type{ get; private set; }

        public int Offset{ get; private set; }
    }
}

