//
// Diagnostics.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Diagnostics
{
    public abstract class Diagnostics : IDiagnostics
    {
        public void AddError(DiagnosticLocation loc, string format, params object[] args)
        {
            this.AddError(loc, string.Format(format, args));
        }

        public abstract void AddError(DiagnosticLocation loc, string message);

        public void AddWarning(DiagnosticLocation loc, string format, params object[] args)
        {
            this.AddWarning(loc, string.Format(format, args));
        }

        public abstract void AddWarning(DiagnosticLocation loc, string message);

        public void AddNotice(DiagnosticLocation loc, string format, params object[] args)
        {
            this.AddNotice(loc, string.Format(format, args));
        }

        public abstract void AddNotice(DiagnosticLocation loc, string message);

        public bool HasErrors{ get; protected set; }
    }
}

