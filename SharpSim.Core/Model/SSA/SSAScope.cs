//
// SSAScope.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model.SSA
{
    public class SSAScope
    {
        private Dictionary<string, SSASymbol> localSymbols = new Dictionary<string, SSASymbol>();

        internal SSAScope()
        {
            this.Parent = null;
        }

        public SSAScope(SSAScope parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            this.Parent = parent;
        }

        public SSAScope Parent{ get; private set; }

        public SSASymbol CreateSymbol(string name, SSAType type)
        {
            if (this.localSymbols.ContainsKey(name))
                throw new Exception("Symbol already in local scope");

            var symbol = new SSASymbol(name, type);
            localSymbols.Add(name, symbol);
            return symbol;
        }

        public SSASymbol ResolveSymbol(string name)
        {
            SSASymbol ret;
            if (localSymbols.TryGetValue(name, out ret))
                return ret;

            if (this.Parent == null)
                throw new Exception(string.Format("Symbol '{0}' not found in scope", name));
            
            return this.Parent.ResolveSymbol(name);
        }

        public bool InLocalScope(string name)
        {
            return localSymbols.ContainsKey(name);
        }

        public bool InAnyParentScope(string name)
        {
            var scope = this.Parent;
            while (scope != null) {
                if (scope.InLocalScope(name))
                    return true;
                scope = scope.Parent;
            }
            return false;
        }
    }
}
