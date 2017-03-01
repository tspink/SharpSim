//
// Decode.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST
{
	public class Decode : ASTNode
	{
		public Decode(ASTNode.ASTNodeLocation location, string isaName, string formatName, FunctionBody body)
			: base(location)
		{
			this.ISAName = isaName;
			this.FormatName = formatName;
			this.Body = body;
		}

		public string ISAName{ get; private set; }

		public string FormatName{ get; private set; }

		public FunctionBody Body{ get; private set; }

		public override void Accept(SharpSim.Model.AST.Visitor.IASTVisitor visitor)
		{
			throw new NotImplementedException();
		}
	}
}

