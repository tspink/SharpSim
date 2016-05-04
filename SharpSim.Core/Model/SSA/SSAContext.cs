﻿//
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
            var action = new SSAAction(this, name, prototype);
            actions.Add(action.Name, action);
            return action;
        }

        public SSAAction GetAction(string name)
        {
            SSAAction action;
            if (!actions.TryGetValue(name, out action))
                throw new Exception(string.Format("Action '{0}' does not exist", name));
            return action;
        }

        private void CreateBuiltins()
        {
            CreateAction("__builtin_update_zn_flags", new SSAActionPrototype(PrimitiveType.Void));
            CreateAction("add_with_flags", new SSAActionPrototype(PrimitiveType.Void));
        }
    }
}
