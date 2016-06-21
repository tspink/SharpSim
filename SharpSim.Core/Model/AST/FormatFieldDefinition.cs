//
// FormatFieldDefinition.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
	public abstract class FormatFieldDefinition : ASTNode
	{
		public FormatFieldDefinition(ASTNode.ASTNodeLocation location, int width)
			: base(location)
		{
			this.Width = width;
		}

		public int Width{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitFormatFieldDefinition(this);
		}
	}

	public class NamedFormatFieldDefinition : FormatFieldDefinition
	{
		public NamedFormatFieldDefinition(ASTNode.ASTNodeLocation location, int width, string name)
			: base(location, width)
		{
			this.Name = name;
		}

		public string Name{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitNamedFormatFieldDefinition(this);
		}
	}

	public class ConstrainedFormatFieldDefinition:FormatFieldDefinition
	{
		public ConstrainedFormatFieldDefinition(ASTNode.ASTNodeLocation location, int width, int value)
			: base(location, width)
		{
			this.Value = value;
		}

		public int Value{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			visitor.VisitConstrainedFormatFieldDefinition(this);
		}
	}
}

