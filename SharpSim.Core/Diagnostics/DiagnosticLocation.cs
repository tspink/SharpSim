//
// DiagnosticLocation.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Diagnostics
{
    public class DiagnosticLocation
    {
        public string Filename{ get; set; }

        public int Line{ get; set; }

        public int Column{ get; set; }

        public static readonly DiagnosticLocation Empty = new DiagnosticLocation{ Filename = string.Empty, Line = 0, Column = 0 };
    }

    public static class DiagnosticLocationExtension
    {
        public static DiagnosticLocation ToDiagnosticLocation(this Model.AST.ASTNode.ASTNodeLocation location)
        {
            return new DiagnosticLocation
            {
                Filename = location.Filename,
                Line = location.Line,
                Column = location.Column
            };
        }
    }
}

