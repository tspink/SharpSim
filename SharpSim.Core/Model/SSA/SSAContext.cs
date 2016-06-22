//
// SSAContext.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.SSA
{
	public class SSAContext
	{
		private Dictionary<string, SSAAction> actions = new Dictionary<string, SSAAction>();

		public SSAContext()
		{
			CreateBuiltins();
		}

		public SSAAction CreateAction(string name, SSAActionPrototype prototype)
		{
			if (this.actions.ContainsKey(name))
				throw new Exceptions.DuplicateActionException(name);
            
			var action = new SSAAction(this, name, prototype);
			actions.Add(action.Name, action);
			return action;
		}

		public SSAAction GetAction(string name)
		{
			SSAAction action;
			if (!actions.TryGetValue(name, out action))
				throw new Exceptions.NoSuchActionException(name);
			return action;
		}

		private void CreateBuiltins()
		{
			CreateAction("__builtin_update_zn_flags", new SSAActionPrototype(PrimitiveType.Void));
			CreateAction("__builtin_adc_flags", new SSAActionPrototype(PrimitiveType.UInt32));
		}
	}
}
