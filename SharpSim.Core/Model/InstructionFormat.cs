//
// InstructionFormat.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model
{
	public class InstructionFormat
	{
		private List<FormatChunk> chunks = new List<FormatChunk>();

		public abstract class FormatChunk
		{
			protected FormatChunk(int offset, int length)
			{
				if (offset < 0)
					throw new ArgumentOutOfRangeException("offset");

				if (length < 1)
					throw new ArgumentOutOfRangeException("length");

				this.Offset = offset;
				this.Length = length;
			}

			public int Offset{ get; private set; }

			public int Length{ get; private set; }

			internal bool IntersectsWith(int offset, int length)
			{
				// FIXME: TODO: Implement
				return false;
			}
		}

		public class InstructionField:FormatChunk
		{
			internal InstructionField(string name, int offset, int length)
				: base(offset, length)
			{
				if (string.IsNullOrEmpty(name))
					throw new ArgumentNullException("name");


				this.Name = name;
			}

			public string Name{ get; private set; }

			public bool Hidden{ get; set; }

			public bool SignExtend{ get; set; }
		}

		public class FormatConstraint:FormatChunk
		{
			internal FormatConstraint(int value, int offset, int length)
				: base(offset, length)
			{
				// TODO: Verify value fits in length
				this.Value = value;
			}

			public int Value{ get; private set; }
		}

		public InstructionFormat(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			this.Name = name;
		}

		public string Name{ get; private set; }

		public InstructionField AddField(string name, int offset, int length)
		{
			foreach (var existingChunk in this.chunks) {
				if (existingChunk.IntersectsWith(offset, length)) {
					throw new Exception(string.Format("Field would intersect with '{0}'.", existingChunk));
				}
			}

			var field = new InstructionField(name, offset, length);
			this.chunks.Add(field);

			return field;
		}

		public FormatConstraint AddConstraint(int value, int offset, int length)
		{
			foreach (var existingChunk in this.chunks) {
				if (existingChunk.IntersectsWith(offset, length)) {
					throw new Exception(string.Format("Constraint would intersect with '{0}'.", existingChunk));
				}
			}

			var constraint = new FormatConstraint(value, offset, length);
			this.chunks.Add(constraint);

			return constraint;
		}

		public IEnumerable<InstructionField> Fields{ get { return this.chunks.OfType<InstructionField>(); } }

		public IEnumerable<FormatConstraint> Constraints{ get { return this.chunks.OfType<FormatConstraint>(); } }
	}
}

