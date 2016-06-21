//
// DisasmStatement.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.AST
{
	public abstract class DisasmStatement : ASTNode
	{
		public DisasmStatement(ASTNode.ASTNodeLocation location)
			: base(location)
		{
		}

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitDisasmStatement(this);
		}
	}

	public class DisasmAppend : DisasmStatement
	{
		public DisasmAppend(ASTNode.ASTNodeLocation location, string format)
			: base(location)
		{
			if (string.IsNullOrEmpty(format))
				throw new ArgumentNullException(nameof(format));

			this.Format = format;
		}

		public string Format{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitDisasmAppend(this);
		}
	}

	public class DisasmWhere : DisasmStatement
	{
		private List<DisasmStatement> statements = new List<DisasmStatement>();

		public DisasmWhere(ASTNode.ASTNodeLocation location, MatchExpression constraint)
			: base(location)
		{
			this.Constraint = constraint;
		}

		public MatchExpression Constraint{ get; private set; }

		public void AddStatement(DisasmStatement stmt)
		{
			this.statements.Add(stmt);
		}

		public IEnumerable<DisasmStatement> Statements{ get { return this.statements.AsReadOnly(); } }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitDisasmWhere(this);
		}
	}
}

