﻿//
// StoreRegisterStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public class StoreRegisterStatement : RegisterAccessStatement
	{
		public StoreRegisterStatement (SSAOperand value, SSAOperand fileOffset, SSAType registerType)
			: base (fileOffset, registerType)
		{
			this.Value = value;
		}

		public SSAOperand Value { get; private set; }

		public override SSAType Type {
			get {
				return SSAType.None;
			}
		}

		public override SSAStatement Clone ()
		{
			return new StoreRegisterStatement (this.Value.Clone (), this.FileOffset.Clone (), this.RegisterType);
		}

		public override string ToString ()
		{
			return string.Format ("str {0}, {1}", this.Value, this.FileOffset);
		}
	}
}

