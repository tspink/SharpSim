﻿//
// VariableDeclarationNode.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
	public class VariableDeclaration : Expression
	{
		public VariableDeclaration(ASTNode.ASTNodeLocation location, bool exported, string type, string name)
			: this(location, exported, type, name, null)
		{
		}

		public VariableDeclaration(ASTNode.ASTNodeLocation location, bool exported, string type, string name, Expression assignment)
			: base(location)
		{
			this.Exported = exported;
			this.Type = type;
			this.Name = name;
			this.Assignment = assignment;
		}

		public bool Exported{ get; private set; }

		public string Type{ get; private set; }

		public string Name{ get; private set; }

		public Expression Assignment{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitVariableDeclaration(this);
		}
	}
}

