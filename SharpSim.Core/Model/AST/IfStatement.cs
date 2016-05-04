//
// IfStatement.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public class IfStatement : Statement
    {
        public IfStatement(ASTNode.ASTNodeLocation location, Expression cond, Statement ifTrue, Statement ifFalse) : base(location)
        {
            this.Condition = cond;
            this.IfTrue = ifTrue;
            this.IfFalse = ifFalse;
        }

        public Expression Condition{ get; private set; }

        public Statement IfTrue{ get; private set; }

        public Statement IfFalse{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitIfStatement(this);
        }
    }
}

