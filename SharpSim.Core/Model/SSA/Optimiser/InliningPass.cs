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
						var callStatement = (SSA.CallStatement)stmt;

						// Can't inline external calls (obviously)
						if (callStatement.Action.Value.External) continue;

						if (InlineCall (callStatement, blockStack)) {
							break;
						}
					}
				}

				foreach (var targetBlock in current.TargetBlocks) {
					blockStack.Push (targetBlock);
				}
			}

			return true;
		}

		private bool InlineCall (SSA.CallStatement stmt, Stack<SSABlock> blockStack)
		{
			Console.WriteLine ("Inlining Call: {0}", stmt);

			// Split the block
			var bottom = stmt.Owner.Split (stmt);

			// Inline target action into top of bottom.
			var target = stmt.Action.Value;

			if (!target.HasEntryBlock)
				throw new Exception ("WHAT!?");

			var blockQueue = new Queue<SSABlock> ();
			blockQueue.Enqueue (target.EntryBlock);

			while (blockQueue.Count > 0) {
				var targetBlock = blockQueue.Dequeue ();

				foreach (var targetSuccessor in targetBlock.TargetBlocks) {
					blockQueue.Enqueue (targetSuccessor);
				}

				var newBlock = targetBlock.Clone (stmt.Owner.Owner);

				if (targetBlock == target.EntryBlock) {
					stmt.Owner.Last.Remove ();
					stmt.Owner.AddStatement (new JumpStatement (newBlock.AsOperand ()));
				}

			}

			return true;
		}
	}
}
