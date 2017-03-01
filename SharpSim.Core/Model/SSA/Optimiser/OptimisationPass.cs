//
// OptimisationPass.cs
//
// Copyright (c) 2017 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
using SharpSim.Diagnostics;

namespace SharpSim.Model.SSA.Optimiser
{
	public abstract class OptimisationPass
	{
		public OptimisationPass (IDiagnostics diagnostics, SSAAction action)
		{
			this.Diagnostics = diagnostics;
			this.Action = action;
		}

		protected IDiagnostics Diagnostics { get; private set; }
		protected SSAAction Action { get; private set; }

		public abstract bool Run ();
	}
}
