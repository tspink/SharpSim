//
// ASTNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    using Visitor;

    public abstract class ASTNode : IASTVisitable
    {
        public class ASTNodeLocation
        {
            public string Filename{ get; set; }

            public int Line{ get; set; }

            public int Column{ get; set; }
        }

        public ASTNode(ASTNodeLocation location)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            this.Location = location;
        }

        public ASTNodeLocation Location{ get; private set; }

        public abstract void Accept(IASTVisitor visitor);
    }
}
    