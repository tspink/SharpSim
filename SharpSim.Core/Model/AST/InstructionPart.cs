//
// InstructionPart.cs
//
// Copyright (C) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
	public abstract class InstructionPart : ASTNode
	{
		public InstructionPart(ASTNode.ASTNodeLocation location)
			: base(location)
		{
		}

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitInstructionPart(this);
		}
	}

	public class MatchPart : InstructionPart
	{
		public MatchPart(ASTNode.ASTNodeLocation location, MatchExpression expression)
			: base(location)
		{
			if (expression == null)
				throw new ArgumentNullException(nameof(expression));
			
			this.Expression = expression;
		}

		public MatchExpression Expression{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitMatchPart(this);
		}
	}

	public class DisasmPart : InstructionPart
	{
		private List<DisasmStatement> statements = new List<DisasmStatement>();

		public DisasmPart(ASTNode.ASTNodeLocation location)
			: base(location)
		{
			
		}

		public void AddDisasmStatement(DisasmStatement stmt)
		{
			this.statements.Add(stmt);
		}

		public IEnumerable<DisasmStatement> Statements{ get { return this.statements.AsReadOnly(); } }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitDisasmPart(this);
		}
	}

	public class BehaviourPart:InstructionPart
	{
		private List<string> instantiationTypes = new List<string>();

		public BehaviourPart(ASTNode.ASTNodeLocation location, string name)
			: base(location)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			this.Name = name;
		}

		public BehaviourPart(ASTNode.ASTNodeLocation location, string name, IEnumerable<string> instantiationTypes, MatchExpression match)
			: base(location)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			this.Name = name;
			this.instantiationTypes.AddRange(instantiationTypes);
			this.InstantiationMatch = match;
		}

		public string Name{ get; private set; }

		public IEnumerable<string> InstantiationTypes{ get { return this.instantiationTypes.AsReadOnly(); } }

		public MatchExpression InstantiationMatch{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitBehaviourPart(this);
		}
	}
}

