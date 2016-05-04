//
// ADLBuilder.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

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

            var context = new SSA.SSAContext();

            // Load ISAs
            foreach (var archFile in archFiles) {
                foreach (var isaBlock in archFile.ISABlocks) {
                    Console.WriteLine("Building ISA {0}", isaBlock.Name);
                    var isa = arch.CreateISA(isaBlock.Name);

                    foreach (var format in isaBlock.FormatDefinitions) {
                        Console.WriteLine("Building Format {0}", format.Name);
                        var instructionFormat = isa.CreateInstructionFormat(format.Name);

                        int currentOffset = 0;
                        foreach (var field in format.FieldDefinitions) {
                            Console.WriteLine("Adding Field {0}", field.Name);
                            instructionFormat.AddField(field.Name, currentOffset, field.Width);
                            currentOffset += field.Width;
                        }
                    }
                }
            }

            // Load instruction formats

            // Load helper prototypes
            foreach (var archFile in archFiles) {
                foreach (var helper in archFile.Helpers) {
                    context.CreateAction(helper.Name, GeneratePrototype(helper));
                }
            }

            // Create helpers
            foreach (var archFile in archFiles) {
                foreach (var helper in archFile.Helpers) {
                    arch.AddHelper(BuildHelper(context, helper));
                }
            }

            // Create behaviours
            foreach (var archFile in archFiles) {
                foreach (var behaviour in archFile.Behaviours) {
                    arch.AddBehaviour(BuildBehaviour(context, arch, behaviour));
                }
            }

            return true;
        }

        private Helper BuildHelper(SSA.SSAContext context, AST.Helper helper)
        {
            var action = context.GetAction(helper.Name);
            var visitor = new SSA.SSAASTVisitor(action, null);
            visitor.VisitHelper(helper);

            return new Helper(helper.Name, action);
        }

        private Behaviour BuildBehaviour(SSA.SSAContext context, Architecture arch, AST.Behaviour behaviour)
        {
            var action = context.CreateAction(behaviour.Name, GeneratePrototype(behaviour));
            var visitor = new SSA.SSAASTVisitor(action, arch.GetISA(behaviour.ISAName).GetInstructionFormat(behaviour.FormatName));
            visitor.VisitBehaviour(behaviour);

            return new Behaviour(behaviour.Name, action);
        }

        private SSA.SSAActionPrototype GeneratePrototype(AST.Helper helper)
        {
            return SSA.SSAActionPrototype.FromParameters(
                SSA.SSAType.FromString(helper.ReturnType),
                helper.Parameters.Select(p => SSA.SSAType.FromString(p.Type)));
        }

        private SSA.SSAActionPrototype GeneratePrototype(AST.Behaviour behaviour)
        {
            return new SSA.SSAActionPrototype(SSA.PrimitiveType.Void);
        }

        private InstructionFormat GetInstructionFormat(Architecture arch, string fqn)
        {
            var parts = fqn.Split('.');
            if (parts.Length != 2)
                throw new Exception("Invalid name for instruction format");

            var isa = arch.GetISA(parts[0]);
            return isa.GetInstructionFormat(parts[1]);
        }
    }
}

