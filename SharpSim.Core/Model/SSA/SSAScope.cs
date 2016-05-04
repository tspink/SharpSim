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
        private List<SSAStatement> statements = new List<SSAStatement>();

        public SSAScope()
        {
        }

        public void AddStatement(SSAStatement stmt)
        {
            if (stmt == null)
                throw new ArgumentNullException("stmt");

            this.statements.Add(stmt);
        }

        public SSAStatement First{ get { return this.statements.FirstOrDefault(); } }

        public SSAStatement Last{ get { return this.statements.LastOrDefault(); } }
    }
}
