//
// RegisterSlot.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class RegisterSlot : RegisterDefinition
    {
        public RegisterSlot(ASTNode.ASTNodeLocation location, string name, string tag, string type, int width, int offset) : base(location, name, type, offset)
        {
            this.Tag = tag;
            this.Width = width;
        }

        public string Tag{ get; private set; }

        public int Width{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitRegisterSlot(this);
        }
    }
}

