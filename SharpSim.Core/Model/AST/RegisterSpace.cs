//
// RegisterSpace.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model.AST
{
    public class RegisterSpace : ASTNode
    {
        private List<RegisterDefinition> registerDefinitions = new List<RegisterDefinition>();

        public RegisterSpace(ASTNode.ASTNodeLocation location) : base(location)
        {
        }

        public void AddRegisterDefinition(RegisterDefinition def)
        {
            this.registerDefinitions.Add(def);
        }

        public IEnumerable<RegisterDefinition> RegisterDefinitions{ get { return this.registerDefinitions.AsReadOnly(); } }

        public IEnumerable<RegisterBank> RegisterBanks{ get { return this.registerDefinitions.OfType<RegisterBank>(); } }

        public IEnumerable<RegisterSlot> RegisterSlots{ get { return this.registerDefinitions.OfType<RegisterSlot>(); } }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitRegisterSpace(this);
        }
    }
}

