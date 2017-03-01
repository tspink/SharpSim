//
// SSAOptimiser.cs
//
// Copyright (c) 2017 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using SharpSim.Diagnostics;

namespace SharpSim.Model.SSA.Optimiser
{
	public class SSAOptimiser
	{
		private IDiagnostics diagnostics;

		public SSAOptimiser (IDiagnostics diagnostics)
		{
			this.diagnostics = diagnostics;
		}

		public bool OptimiseAction (SSAAction action)
		{
			foreach (var pass in Passes (action)) {
				if (!pass.Run ()) {
					return false;
				}
			}

			return true;
		}

		private IEnumerable<OptimisationPass> Passes (SSAAction action)
		{
			yield return new InliningPass (this.diagnostics, action);
		}
	}
}
