//
// IDiagnostics.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Diagnostics
{
    public interface IDiagnostics
    {
        void AddError(DiagnosticLocation loc, string message);

        void AddWarning(DiagnosticLocation loc, string message);

        void AddNotice(DiagnosticLocation loc, string message);

        void AddError(DiagnosticLocation loc, string format, params object[] args);

        void AddWarning(DiagnosticLocation loc, string format, params object[] args);

        void AddNotice(DiagnosticLocation loc, string format, params object[] args);

        bool HasErrors{ get; }
    }
}

