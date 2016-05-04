//
// Parameter.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class Parameter : ASTNode
    {
        public Parameter(ASTNode.ASTNodeLocation location, string type, string name, bool reftype = false) : base(location)
        {
            this.Type = type;
            this.Name = name;
            this.Reference = reftype;
        }

        public string Type{ get; private set; }

        public string Name{ get; private set; }

        public bool Reference{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitParameter(this);
        }
    }
}

