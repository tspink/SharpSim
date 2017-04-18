//
// MLAStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public class MLAStatement : SSAStatement
	{
		public MLAStatement (TypedSSAOperand mlaBase, SSAOperand addend, SSAOperand shift)
		{
			if (mlaBase == null)
				throw new ArgumentNullException (nameof (mlaBase));

			if (addend == null)
				throw new ArgumentNullException (nameof (addend));

			if (shift == null)
				throw new ArgumentNullException (nameof (shift));

			this.Base = mlaBase;
			this.Addend = addend;
			this.Shift = shift;
		}

		public TypedSSAOperand Base { get; private set; }

		public SSAOperand Addend { get; private set; }

		public SSAOperand Shift { get; private set; }

		public override SSAType Type {
			get {
				return this.Base.Type;
			}
		}

		public override Fixedness Fixed {
			get {
				return this.Base.Fixed == Fixedness.AlwaysFixed &&
				this.Addend.Fixed == Fixedness.AlwaysFixed &&
				this.Shift.Fixed == Fixedness.AlwaysFixed ? Fixedness.AlwaysFixed : Fixedness.Dynamic;
			}
		}

		public override SSAStatement Clone ()
		{
			return new MLAStatement (this.Base.Clone () as TypedSSAOperand, this.Addend.Clone (), this.Shift.Clone ());
		}

		public override string ToString ()
		{
			return string.Format ("mla {0}, {1}, {2}", this.Base, this.Addend, this.Shift);
		}
	}
}
