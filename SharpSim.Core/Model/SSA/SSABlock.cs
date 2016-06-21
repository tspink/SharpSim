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
		private List<SSAStatement> statements = new List<SSAStatement>();
		private bool hasControlFlowStatement = false;

		public SSABlock(SSAAction owner)
		{
			if (owner == null)
				throw new ArgumentNullException("owner");

			this.Owner = owner;
			this.Fixed = SSAStatement.Fixedness.Unknown;
		}

		public uint Index{ get; internal set; }

		public SSAAction Owner{ get; private set; }

		public SSAStatement.Fixedness Fixed{ get; set; }

		public void AddStatement(SSAStatement statement)
		{
			if (statement == null)
				throw new ArgumentNullException("statement");

			if (statement.Owner != null)
				throw new Exception("Statement already has an owner");
            
			if (hasControlFlowStatement)
				throw new InvalidOperationException("Block contains control-flow statement");

			statement.Index = this.Owner.GetUniqueStatementIndex();
			statement.Owner = this;

			statements.Add(statement);

			if (statement is ControlFlowStatement)
				hasControlFlowStatement = true;
		}

		public SSAStatement this [int index] {
			get {
				return statements[index];
			}
		}

		public SSAStatement Last{ get { return this.statements.LastOrDefault(); } }

		public IEnumerable<SSABlock> TargetBlocks {
			get {
				ControlFlowStatement controlFlow = this.Last as ControlFlowStatement;
				if (controlFlow != null)
					return controlFlow.TargetBlocks;
				else
					return new SSABlock[0];
			}
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.AppendLine(string.Format("SSA Block {0}:", this.Index));
			foreach (var stmt in statements) {

				if (stmt.Type == SSAType.None) {
					builder.AppendFormat("[{0}:{1:000}] {2}",
						stmt.Fixed == SSAStatement.Fixedness.AlwaysFixed ? 'F' : 'D',
						stmt.Index,
						stmt);
				} else {
					builder.AppendFormat("[{0}:{1:000}] {2} {3}",
						stmt.Fixed == SSAStatement.Fixedness.AlwaysFixed ? 'F' : 'D',
						stmt.Index,
						stmt.Type,
						stmt);
				}

				builder.AppendLine();
			}

			return builder.ToString();
		}
	}
}

