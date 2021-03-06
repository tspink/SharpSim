﻿//
// ArchFile.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
	public class ArchFile : ASTNode
	{
		private List<ISABlock> isaBlocks = new List<ISABlock>();
		private List<RegisterSpace> registerSpaces = new List<RegisterSpace>();
		private List<Behaviour> behaviours = new List<Behaviour>();
		private List<Helper> helpers = new List<Helper>();
		private List<ExceptionDeclaration> exceptions = new List<ExceptionDeclaration>();

		public ArchFile(ASTNode.ASTNodeLocation location, ArchIdentifier ident)
			: base(location)
		{
			if (ident == null)
				throw new ArgumentNullException(nameof(ident));
			this.Identifier = ident;
		}

		public ArchIdentifier Identifier{ get; private set; }

		public IEnumerable<ISABlock> ISABlocks{ get { return this.isaBlocks.AsReadOnly(); } }

		public void AddISABlock(ISABlock isa)
		{
			this.isaBlocks.Add(isa);
		}

		public IEnumerable<RegisterSpace> RegisterSpaces{ get { return this.registerSpaces.AsReadOnly(); } }

		public void AddRegisterSpace(RegisterSpace regspace)
		{
			this.registerSpaces.Add(regspace);
		}

		public IEnumerable<Behaviour> Behaviours{ get { return this.behaviours.AsReadOnly(); } }

		public void AddBehaviour(Behaviour behaviour)
		{
			this.behaviours.Add(behaviour);
		}

		public IEnumerable<Helper> Helpers{ get { return this.helpers.AsReadOnly(); } }

		public void AddHelper(Helper helper)
		{
			this.helpers.Add(helper);
		}

		public IEnumerable<ExceptionDeclaration> Exceptions{ get { return this.exceptions.AsReadOnly(); } }

		public void AddExceptionDeclaration(ExceptionDeclaration exception)
		{
			this.exceptions.Add(exception);
		}

		public override void Accept(Visitor.IASTVisitor visitor)
		{
			visitor.VisitArchFile(this);
		}
	}
}
