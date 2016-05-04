//
// Program.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Generate
{
    using Diagnostics;
    using Model.AST;
    using Model.AST.Visitor;
    using Model.SSA;
    using Parser;

    internal static class MainClass
    {
        public static void Main(string[] args)
        {
            var diag = new ConsoleDiagnostics();

            bool ok = true;
            var archFiles = new List<ArchFile>();
            foreach (var arg in args) {
                Console.WriteLine("Parsing '{0}'...", arg);
                var parser = new FileParser(diag, arg);

                ArchFile af;
                if (!parser.TryParse(out af)) {
                    Console.WriteLine("Terminating due to errors in arch description '{0}'.", arg);

                    ok = false;
                    break;
                }

                archFiles.Add(af);
            }

            if (diag.HasErrors)
                return;

            var builder = new SharpSim.Model.ModelBuilder(diag);

            SharpSim.Model.Architecture arch;
            if (!builder.TryBuild(archFiles, out arch)) {
                Console.WriteLine("Terminating due to errors building architecture model.");
                return;
            }

            foreach (var behaviour in arch.Behaviours) {
                Console.WriteLine("Behaviour {0}", behaviour.Name);
                Console.WriteLine(behaviour.Action);
            }

            Console.WriteLine("Built Architecture '{0}'", arch.Name);
        }
    }
}
