//
// Diagnostics.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Diagnostics
{
    public class ConsoleDiagnostics : Diagnostics
    {
        public override void AddError(DiagnosticLocation loc, string message)
        {
            this.HasErrors = true;
            Console.WriteLine("Error: {0}:{1}: {2}", loc.Filename, loc.Line, message);
        }

        public override void AddWarning(DiagnosticLocation loc, string message)
        {
            Console.WriteLine("Warning: {0}:{1}: {2}", loc.Filename, loc.Line, message);
        }

        public override void AddNotice(DiagnosticLocation loc, string message)
        {
            Console.WriteLine("Note: {0}:{1}: {2}", loc.Filename, loc.Line, message);
        }
    }
}

