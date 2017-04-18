//
// SSAStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public abstract class SSAStatement
	{
		public enum Fixedness
		{
			Unknown,
			AlwaysFixed,
			SometimesFixed,
			Dynamic
		}

		public uint Index { get; internal set; }

		public SSABlock Owner { get; internal set; }

		public abstract SSAType Type { get; }

		public abstract Fixedness Fixed { get; }

		public abstract SSAStatement Clone ();

		public void Remove ()
		{
			if (this.Owner == null)
				throw new InvalidOperationException ("Statement not owned by block");

			this.Owner.RemoveStatement (this);
		}
	}
}

