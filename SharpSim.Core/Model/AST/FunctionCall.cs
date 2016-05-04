//
// FunctionCallNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
    public class FunctionCall : Expression
    {
        private List<Expression> parameters = new List<Expression>();

        public FunctionCall(ASTNode.ASTNodeLocation location, string name) : base(location)
        {
            this.Name = name;    
        }

        public string Name{ get; private set; }

        public void AddArgument(Expression expression)
        {
            this.parameters.Add(expression);
        }

        public IEnumerable<Expression> Arguments{ get { return this.parameters.AsReadOnly(); } }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitFunctionCall(this);
        }
    }
}

