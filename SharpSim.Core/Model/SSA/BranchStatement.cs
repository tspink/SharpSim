//
// BranchStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public class BranchStatement : ControlFlowStatement
	{
		public enum BranchPredicate
		{
			IfZero,
			IfNonZero
		}

		public BranchStatement (BranchPredicate pred, SSAOperand cond, BlockOperand trueTarget, BlockOperand falseTarget)
		{
			this.Predicate = pred;
			this.Condition = cond;
			this.TrueTarget = trueTarget;
			this.FalseTarget = falseTarget;
		}

		public SSAOperand Condition { get; private set; }

		public BlockOperand TrueTarget { get; set; }

		public BlockOperand FalseTarget { get; set; }

		public BranchPredicate Predicate { get; private set; }

		public override Fixedness Fixed {
			get {
				return this.Condition.Fixed;
			}
		}

		public override System.Collections.Generic.IEnumerable<SSABlock> TargetBlocks {
			get {
				return new SSABlock [] { this.TrueTarget.Value, this.FalseTarget.Value };
			}
		}

		public override SSAStatement Clone ()
		{
			return new BranchStatement (
				this.Predicate,
				this.Condition.Clone (),
				this.TrueTarget.Clone () as BlockOperand,
				this.FalseTarget.Clone () as BlockOperand);
		}

		public override string ToString ()
		{
			return string.Format ("b{0} {1}, @{2}, @{3}",
				this.Predicate == BranchPredicate.IfZero ? "z" : "nz",
				this.Condition,
				this.TrueTarget.Value.Index,
				this.FalseTarget.Value.Index);
		}
	}
}

