//
// Behaviour.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSim.Model.SSA
{
	public class SSABlock
	{
		private List<SSAStatement> statements = new List<SSAStatement> ();

		public SSABlock (SSAAction owner)
		{
			if (owner == null)
				throw new ArgumentNullException ("owner");

			this.Owner = owner;
			this.Fixed = SSAStatement.Fixedness.Unknown;
		}

		public uint Index { get; internal set; }

		public SSAAction Owner { get; private set; }

		public SSAStatement.Fixedness Fixed { get; set; }

		public void AddStatement (SSAStatement statement)
		{
			if (statement == null)
				throw new ArgumentNullException ("statement");

			if (statement.Owner != null)
				throw new Exception ("Statement already has an owner");

			if (this.HasControlFlowStatement)
				throw new InvalidOperationException ("Block contains control-flow statement");

			statement.Index = this.Owner.GetUniqueStatementIndex ();
			statement.Owner = this;

			statements.Add (statement);
		}

		public void RemoveStatement (SSAStatement statement)
		{
			if (statement == null)
				throw new ArgumentNullException (nameof (statement));

			if (statement.Owner != this)
				throw new Exception ("Statement not owned by this block");

			statements.Remove (statement);
		}

		public bool HasControlFlowStatement {
			get {
				return this.Last is ControlFlowStatement;
			}
		}

		public SSAStatement this [int index] {
			get {
				return statements [index];
			}
		}

		public IEnumerable<SSAStatement> Statements {
			get {
				return this.statements.AsReadOnly ();
			}
		}

		public SSAStatement Last { get { return this.statements.LastOrDefault (); } }

		public IEnumerable<SSABlock> TargetBlocks {
			get {
				ControlFlowStatement controlFlow = this.Last as ControlFlowStatement;
				if (controlFlow != null)
					return controlFlow.TargetBlocks;
				else
					return new SSABlock [0];
			}
		}

		public SSABlock Clone (SSAAction targetAction)
		{
			var block = targetAction.CreateBlock ();

			foreach (var stmt in statements) {
				block.AddStatement (stmt.Clone ());
			}

			return block;
		}

		public SSABlock Split (SSAStatement stmt)
		{
			if (stmt.Owner != this)
				throw new InvalidOperationException ("Statement not owned by this block");

			SSABlock bottom = this.Owner.CreateBlock ();

			var removeList = new List<SSAStatement> ();

			bool moving = false;
			for (int i = 0; i < this.statements.Count; i++) {
				var childStmt = this.statements [i];

				if (moving) {
					removeList.Add (childStmt);

					childStmt.Owner = null;
					bottom.AddStatement (childStmt);
				} else {

					if (childStmt == stmt) {
						moving = true;
						removeList.Add (childStmt);
					}
				}
			}

			foreach (var s in removeList) {
				this.statements.Remove (s);
			}

			this.AddStatement (new SSA.JumpStatement (bottom.AsOperand ()));

			return bottom;
		}

		public override string ToString ()
		{
			var builder = new StringBuilder ();

			builder.AppendLine (string.Format ("SSA Block {0}:", this.Index));
			foreach (var stmt in statements) {

				if (stmt.Type == SSAType.None) {
					builder.AppendFormat ("[{0}:{1:000}] {2}",
						stmt.Fixed == SSAStatement.Fixedness.AlwaysFixed ? 'F' : 'D',
						stmt.Index,
						stmt);
				} else {
					builder.AppendFormat ("[{0}:{1:000}] {2} {3}",
						stmt.Fixed == SSAStatement.Fixedness.AlwaysFixed ? 'F' : 'D',
						stmt.Index,
						stmt.Type,
						stmt);
				}

				builder.AppendLine ();
			}

			return builder.ToString ();
		}
	}
}

