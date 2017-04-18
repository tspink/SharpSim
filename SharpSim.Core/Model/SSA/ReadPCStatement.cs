//
// ReadPCStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public class ReadPCStatement : SSAStatement
	{
		public override Fixedness Fixed {
			get {
				return Fixedness.Dynamic;
			}
		}

		public override SSAType Type {
			get {
				// XXX: TODO: HACK HACK HACK
				return PrimitiveType.UInt64;
			}
		}

		public override SSAStatement Clone ()
		{
			return new ReadPCStatement ();
		}

		public override string ToString ()
		{
			return "rpc";
		}
	}
}

