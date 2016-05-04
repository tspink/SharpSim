//
// BehaviourNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class Behaviour : ASTNode
    {
        public Behaviour(ASTNode.ASTNodeLocation location, string type, string name, FunctionBody body) : base(location)
        {
            this.Type = type;
            this.Name = name;
            this.Body = body;
        }

        public string Type{ get; private set; }

        public string Name{ get; private set; }

        public FunctionBody Body{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitBehaviour(this);
        }
    }
}

