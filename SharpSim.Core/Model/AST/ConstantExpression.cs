//
// ConstantExpression.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
    public abstract class BaseConstantExpression : Expression
    {
        public BaseConstantExpression(ASTNode.ASTNodeLocation location) : base(location)
        {
        }
    }

    public abstract class ConstantExpression<T> : BaseConstantExpression
    {
        public ConstantExpression(ASTNode.ASTNodeLocation location, T val) : base(location)
        {
            this.Value = val;
        }

        public T Value{ get; private set; }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitConstantExpression<T>(this);
        }
    }

    public class IntegerConstantExpression:ConstantExpression<long>
    {
        public IntegerConstantExpression(ASTNode.ASTNodeLocation location, long val) : base(location, val)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitIntegerConstantExpression(this);
        }
    }

    public class FloatConstantExpression:ConstantExpression<double>
    {
        public FloatConstantExpression(ASTNode.ASTNodeLocation location, double val) : base(location, val)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitFloatConstantExpression(this);
        }
    }

    public class StringConstantExpression:ConstantExpression<string>
    {
        public StringConstantExpression(ASTNode.ASTNodeLocation location, string val) : base(location, val)
        {
        }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitStringConstantExpression(this);
        }
    }
}

