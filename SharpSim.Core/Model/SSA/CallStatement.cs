//
// CallStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model.SSA
{
    public class CallStatement : SSAStatement
    {
        private List<SSAOperand> arguments = new List<SSAOperand>();

        public CallStatement(string function)
        {
            this.Function = function;
        }

        public string Function{ get; private set; }

        public override Fixedness Fixed
        {
            get {
                return this.arguments.All(a => a.Fixed == Fixedness.AlwaysFixed) ? Fixedness.AlwaysFixed : Fixedness.Dynamic;
            }
        }

        public void AddArgument(SSAOperand arg)
        {
            this.arguments.Add(arg);
        }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            builder.AppendFormat("call @{0}", this.Function);

            if (this.arguments.Count > 0) {
                builder.Append(", ");
                builder.Append(string.Join(", ", this.arguments.Select(a => a.ToString())));
            }

            return builder.ToString();
        }
    }
}

