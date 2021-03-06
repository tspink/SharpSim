﻿//
// LoadRegisterStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public class LoadRegisterStatement : RegisterAccessStatement
	{
		public LoadRegisterStatement (SSAOperand fileOffset, SSAType registerType) : base (fileOffset, registerType)
		{
		}

		public override SSAType Type {
			get {
				return this.RegisterType;
			}
		}

		public override SSAStatement Clone ()
		{
			return new LoadRegisterStatement (this.FileOffset.Clone (), this.RegisterType);
		}

		public override string ToString ()
		{
			return string.Format ("ldr {0}", this.FileOffset);
		}
	}
}

