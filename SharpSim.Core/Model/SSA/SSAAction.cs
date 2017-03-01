//
// SSAAction.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model.SSA
{
	public class SSAAction
	{
		private List<SSABlock> blocks = new List<SSABlock> ();
		private uint nextStatementIndex = 0;
		private uint nextBlockIndex = 0;

		public SSAAction (SSAContext owner, SSAActionPrototype prototype, bool external)
		{
			if (owner == null)
				throw new ArgumentNullException (nameof (owner));

			if (prototype == null)
				throw new ArgumentNullException (nameof (prototype));

			this.Owner = owner;
			this.Prototype = prototype;
			this.External = external;
		}

		public SSAContext Owner { get; private set; }

		public SSAActionPrototype Prototype { get; private set; }

		public bool External { get; private set; }

		public SSABlock CreateBlock ()
		{
			if (this.External)
				throw new InvalidOperationException ("Cannot create a block in an external action");

			var block = new SSABlock (this);
			block.Index = GetUniqueBlockIndex ();

			this.blocks.Add (block);
			return block;
		}

		public SSABlock CreateEntryBlock ()
		{
			return this.EntryBlock = CreateBlock ();
		}

		public SSABlock EntryBlock { get; private set; }

		public bool HasEntryBlock {
			get {
				return this.EntryBlock != null;
			}
		}

		public SSAType GetNamedTypeParameter (string name)
		{
			return this.Prototype.TypeParameters.First ();
		}

		internal uint GetUniqueStatementIndex ()
		{
			uint ret = this.nextStatementIndex;
			this.nextStatementIndex++;
			return ret;
		}

		internal uint GetUniqueBlockIndex ()
		{
			uint ret = this.nextBlockIndex;
			this.nextBlockIndex++;
			return ret;
		}

		public override string ToString ()
		{
			var builder = new System.Text.StringBuilder ();
			builder.AppendLine (string.Format ("SSA Action {0}:", this.Prototype));

			foreach (var block in this.blocks) {
				builder.AppendLine (block.ToString ());
			}

			return builder.ToString ();
		}
	}
}

