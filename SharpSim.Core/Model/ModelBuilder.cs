//
// ADLBuilder.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model
{
    using Diagnostics;

    public class ModelBuilder
    {
        private IDiagnostics diag;

        public ModelBuilder(IDiagnostics diag)
        {
            this.diag = diag;
        }

        public bool TryBuild(IEnumerable<Model.AST.ArchFile> archFiles, out Architecture arch)
        {
            arch = null;

            string archName = null;
            foreach (var archFile in archFiles) {
                //var pp = new AST.Visitor.PrettyPrinterVisitor();
                //pp.VisitArchFile(archFile);

                if (archName == null) {
                    archName = archFile.Identifier.Identifier;
                } else if (archName != archFile.Identifier.Identifier) {
                    this.diag.AddError(
                        archFile.Identifier.Location.ToDiagnosticLocation(),
                        "Multiply defined architecture identifier.  Identifier already defined as {0}", archName);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(archName)) {
                this.diag.AddError(DiagnosticLocation.Empty, "Architecture name not declared");
                return false;
            }

            arch = new Architecture(archName);

            foreach (var archFile in archFiles) {
                foreach (var behaviour in archFile.Behaviours) {
                    arch.AddBehaviour(BuildBehaviour(behaviour));
                }
            }

            return true;
        }

        private Behaviour BuildBehaviour(AST.Behaviour behaviour)
        {
            var context = new SSA.SSAContext();
            var visitor = new SSA.SSAASTVisitor(context);
            visitor.VisitBehaviour(behaviour);

            return new Behaviour(behaviour.Name, context);
        }
    }
}

