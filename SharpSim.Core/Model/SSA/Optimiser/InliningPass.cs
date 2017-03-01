//
// InliningPass.cs
//
// Copyright (c) 2017 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using SharpSim.Diagnostics;

namespace SharpSim.Model.SSA.Optimiser
{
	public class InliningPass : OptimisationPass
	{
		public InliningPass (IDiagnostics diagnostics, SSAAction action) : base (diagnostics, action)
		{
		}

		public override bool Run ()
		{
			if (!this.Action.HasEntryBlock) {
				this.Diagnostics.AddError (DiagnosticLocation.Empty, $"Action {this.Action.Prototype} does not have an entry block");
				return false;
			}

			var blockStack = new Stack<SSABlock> ();
			blockStack.Push (this.Action.EntryBlock);

			while (blockStack.Count > 0) {
				var current = blockStack.Pop ();

				foreach (var stmt in current.Statements) {
					if (stmt is SSA.CallStatement) {
						if (((SSA.CallStatement)stmt).Action.Value.External) continue;

						Console.WriteLine ("Inlining Call: {0}", stmt);
					}
				}

				foreach (var targetBlock in current.TargetBlocks) {
					blockStack.Push (targetBlock);
				}
			}

			return true;
		}
	}
}
