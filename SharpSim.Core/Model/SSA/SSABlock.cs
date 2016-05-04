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

        public SSABlock()
        {
            this.Fixed = SSAStatement.Fixedness.Unknown;
        }

        public uint Index{ get; internal set; }

        public SSAContext Owner{ get; internal set; }

        public SSAStatement.Fixedness Fixed{ get; set; }

        public void AddStatement(SSAStatement statement)
        {
            if (this.Owner == null)
                throw new InvalidOperationException("Block not owned by context");
            
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

        public SSAStatement this [int index]
        {
            get {
                return statements[index];
            }
        }

        public SSAStatement Last{ get { return this.statements.LastOrDefault(); } }

        public IEnumerable<SSABlock> TargetBlocks
        {
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
                builder.AppendFormat("[{0}:{1:000}] {2}", stmt.Fixed == SSAStatement.Fixedness.AlwaysFixed ? 'F' : 'D', stmt.Index, stmt);
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}

