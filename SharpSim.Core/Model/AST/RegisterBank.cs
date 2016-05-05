//
// RegisterBank.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class RegisterBank : RegisterDefinition
    {
        public RegisterBank(ASTNode.ASTNodeLocation location, string name, string type, int count, int width, int stride, int offset) : base(location, name, type)
        {
            this.Count = count;
            this.Width = width;
            this.Stride = stride;
            this.Offset = offset;
        }

        public int Count{ get; private set; }

        public int Width{ get; private set; }

        public int Stride{ get; private set; }

        public int Offset{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitRegisterBank(this);
        }
    }
}

