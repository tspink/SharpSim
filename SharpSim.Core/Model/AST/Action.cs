//
// ActionNode.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
	public abstract class Action : ASTNode
	{
		private List<string> typeParameters = new List<string>();

		public Action(ASTNode.ASTNodeLocation location, string name)
			: base(location)
		{
			this.Name = name;
		}

		public string Name{ get; private set; }

		public IEnumerable<string> TypeParameters{ get { return this.typeParameters.AsReadOnly(); } }

		public bool HasTypeParameters{ get { return this.typeParameters.Count > 0; } }

		public void AddTypeParameter(string name)
		{
			this.typeParameters.Add(name);
		}

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitAction(this);
		}
	}
}

