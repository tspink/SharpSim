//
// Extensions.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Parser
{
    using Diagnostics;
    using Model.AST;

    internal static class Extensions
    {
        public static ASTNode.ASTNodeLocation ToASTLocation(this Antlr4.Runtime.IToken token, string filename)
        {
            return new ASTNode.ASTNodeLocation
            {
                Filename = filename,
                Line = token.Line,
                Column = token.StartIndex
            };
        }

        public static DiagnosticLocation ToDiagnosticLocation(this Antlr4.Runtime.IToken token, string filename)
        {
            return new DiagnosticLocation
            {
                Filename = filename,
                Line = token.Line,
                Column = token.StartIndex
            };
        }
    }
}

