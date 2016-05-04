//
// DiagnosticErrorListener.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using Antlr4.Runtime;

namespace SharpSim.Parser.Grammar
{
    using Diagnostics;

    public class DiagnosticErrorListener : IAntlrErrorListener<IToken>
    {
        private IDiagnostics diag;
        private string filename;

        public DiagnosticErrorListener(IDiagnostics diagnostics, string filename)
        {
            this.diag = diagnostics;
            this.filename = filename;
        }

        public void SyntaxError(IRecognizer recognizer, Antlr4.Runtime.IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            diag.AddError(new DiagnosticLocation
                {
                    Filename = this.filename,
                    Line = line,
                    Column = charPositionInLine
                }, msg);
        }
    }
}

