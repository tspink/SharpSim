//
// IVisitable.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST.Visitor
{
    public interface IASTVisitable
    {
        void Accept(IASTVisitor visitor);
    }
}

