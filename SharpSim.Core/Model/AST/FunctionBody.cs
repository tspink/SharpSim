//
// FunctionBodyNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
    public class FunctionBody : Statement
    {
        private List<Statement> statements = new List<Statement>();

        public FunctionBody(ASTNode.ASTNodeLocation location) : base(location)
        {
        }

        public void AddStatement(Statement statement)
        {
            this.statements.Add(statement);
        }

        public IEnumerable<Statement> Statements{ get { return this.statements.AsReadOnly(); } }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitFunctionBody(this);
        }
    }
}

