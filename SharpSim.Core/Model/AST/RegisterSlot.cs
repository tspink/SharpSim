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
        public RegisterSlot(ASTNode.ASTNodeLocation location, string name, string type, int width, int offset) : base(location, name, type)
        {
        }

        public int Width{ get; private set; }

        public int Offset{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitRegisterSlot(this);
        }
    }
}

