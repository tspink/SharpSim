//
// DuplicateActionException.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA.Exceptions
{
	public class DuplicateActionException : SSAException
	{
		public DuplicateActionException(SSAActionPrototype prototype)
			: base(string.Format("The action '{0}' already exists", prototype))
		{
		}
	}
}

