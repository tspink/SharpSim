//
// CastStatement.cs
//
// Copyright (c) 2017 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
namespace SharpSim.Model.SSA
{
	public class CastStatement : SSAStatement
	{
		private SSAType targetType;

		public enum CastType
		{
			ZeroExtend,
			SignExtend,
			Truncate,
			BitCast
		}

		public CastStatement (SSAOperand input, SSAType targetType)
		{
			this.Input = input;
			this.targetType = targetType;
		}

		public SSAOperand Input { get; private set; }

		public override Fixedness Fixed {
			get {
				return this.Input.Fixed;
			}
		}

		public override SSAType Type {
			get { return targetType; }
		}

		public override string ToString ()
		{
			return $"cast {this.Input}";
		}
	}
}
