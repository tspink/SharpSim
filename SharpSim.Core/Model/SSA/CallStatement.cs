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
		private List<SSAOperand> arguments = new List<SSAOperand> ();

		public CallStatement (ActionOperand action)
		{
			this.Action = action;
		}

		public ActionOperand Action { get; private set; }

		public override Fixedness Fixed {
			get {
				return this.arguments.All (a => a.Fixed == Fixedness.AlwaysFixed) && this.Action.Value.Prototype.ReturnType == PrimitiveType.Void ? Fixedness.AlwaysFixed : Fixedness.Dynamic;
			}
		}

		public override SSAType Type {
			get {
				return this.Action.Value.Prototype.ReturnType;
			}
		}

		public void AddArgument (SSAOperand arg)
		{
			this.arguments.Add (arg);
		}

		public override string ToString ()
		{
			var builder = new System.Text.StringBuilder ();

			builder.AppendFormat ("call {0}", this.Action);

			if (this.arguments.Count > 0) {
				builder.Append (", ");
				builder.Append (string.Join (", ", this.arguments.Select (a => a.ToString ())));
			}

			return builder.ToString ();
		}
	}
}

