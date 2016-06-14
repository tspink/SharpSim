//
// VectorRegister.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class VectorRegisterBank : RegisterDefinition
    {
        public VectorRegisterBank(ASTNode.ASTNodeLocation location, string name, string type, int arity, int count, int width, int stride, int offset) : base(location, name, type, offset)
        {
            this.Arity = arity;
            this.Count = count;
            this.Width = width;
            this.Stride = stride;
        }

        public int Arity{ get; private set; }

        public int Count{ get; private set; }

        public int Width{ get; private set; }

        public int Stride{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitVectorRegisterBank(this);
        }
    }
}

