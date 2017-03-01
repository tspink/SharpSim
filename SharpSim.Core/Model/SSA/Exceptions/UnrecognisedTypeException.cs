//
// UnrecognisedTypeException.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA.Exceptions
{
	public class UnrecognisedTypeException : SSAException
	{
		public UnrecognisedTypeException(string type)
			: base(string.Format("Unrecognised type '{0}'", type))
		{
			this.TypeName = type;
		}

		public string TypeName{ get; private set; }
	}
}

