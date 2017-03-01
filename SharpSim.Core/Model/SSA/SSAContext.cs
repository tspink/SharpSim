//
// SSAContext.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model.SSA
{
	public class SSAContext
	{
		private List<SSAAction> actions = new List<SSAAction> ();

		public SSAContext ()
		{
			CreateBuiltins ();
		}

		public SSAAction CreateAction (SSAActionPrototype prototype, bool external = false)
		{
			foreach (var action in this.actions) {
				if (action.Prototype == prototype) {
					throw new Exceptions.DuplicateActionException (prototype);
				}
			}

			var newAction = new SSAAction (this, prototype, external);
			this.actions.Add (newAction);
			return newAction;
		}

		public bool HasAction (SSAActionPrototype prototype)
		{
			foreach (var candidateAction in this.actions) {
				if (candidateAction.Prototype.Equals (prototype)) {
					return true;
				}
			}
			return false;
		}

		public bool TryGetAction (SSAActionPrototype prototype, out SSAAction action, bool partial)
		{
			foreach (var candidateAction in this.actions) {
				if (candidateAction.Prototype.Equivalent (prototype, partial)) {
					action = candidateAction;
					return true;
				}
			}

			action = null;
			return false;
		}

		public SSAAction GetAction (SSAActionPrototype prototype, bool partial = false)
		{
			SSAAction action;
			if (!TryGetAction (prototype, out action, partial))
				throw new Exceptions.NoSuchActionException (prototype);
			return action;
		}

		public IEnumerable<SSAAction> Actions {
			get {
				return this.actions.AsReadOnly ();
			}
		}

		private void CreateBuiltins ()
		{
			CreateAction (
				SSAActionPrototype.FromParameters (
					PrimitiveType.Void, "__builtin_update_zn_flags", new SSA.SSAType [] { PrimitiveType.UInt64 }
				), true
			);

			CreateAction (
				SSAActionPrototype.FromParameters (
					PrimitiveType.Void, "__builtin_update_zn_flags", new SSA.SSAType [] { PrimitiveType.UInt32 }
				), true
			);

			CreateAction (
				SSAActionPrototype.FromParameters (
					PrimitiveType.Void, "trap"
				), true
			);
		}
	}
}
