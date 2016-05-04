//
// SwitchStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
    using Visitor;
    using CaseTuple = Tuple<BaseConstantExpression, Statement>;

    public class SwitchStatement : Statement
    {
        private List<CaseTuple> cases = new List<CaseTuple>();

        public SwitchStatement(ASTNode.ASTNodeLocation location, Expression value) : base(location)
        {
            this.Value = value;
        }

        public Expression Value{ get; private set; }

        public IEnumerable<CaseTuple> Cases{ get { return this.cases.AsReadOnly(); } }

        public void AddCase(BaseConstantExpression match, Statement stmt)
        {
            this.cases.Add(Tuple.Create(match, stmt));
        }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.VisitSwitch(this);
        }
    }
}

