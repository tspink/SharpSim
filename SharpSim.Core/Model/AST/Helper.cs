//
// Helper.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
    [Flags]
    public enum HelperAttributes
    {
        None = 0,
        NoInline = 1
    }

    public class Helper : ASTNode
    {
        private HelperAttributes attrs = HelperAttributes.None;
        private List<Parameter> parameters = new List<Parameter>();

        public Helper(ASTNode.ASTNodeLocation location, string returnType, string name, FunctionBody body) : base(location)
        {
            this.ReturnType = returnType;
            this.Name = name;
            this.Body = body;
        }

        public string ReturnType{ get; private set; }

        public string Name{ get; private set; }

        public FunctionBody Body{ get; private set; }

        public IEnumerable<Parameter> Parameters{ get { return this.parameters.AsReadOnly(); } }

        public void AddParameter(Parameter param)
        {
            this.parameters.Add(param);
        }

        public void SetAttributes(HelperAttributes attrs)
        {
            this.attrs |= attrs;
        }

        public void ClearAttributes(HelperAttributes attrs)
        {
            this.attrs &= ~attrs;
        }

        public bool IsAttributeSet(HelperAttributes attrs)
        {
            return (this.attrs & attrs) == attrs;
        }

        public bool IsNoInline
        {
            get{ return IsAttributeSet(HelperAttributes.NoInline); }
            set {
                if (value)
                    SetAttributes(HelperAttributes.NoInline);
                else
                    ClearAttributes(HelperAttributes.NoInline);
            }
        }

        public HelperAttributes Attributes{ get { return attrs; } }

        public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
        {
            visitor.VisitHelper(this);
        }
    }
}

