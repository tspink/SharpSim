//
// RegisterFile.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model
{
	public class RegisterFile
	{
		private List<RegisterDefinition> registerDefinitions = new List<RegisterDefinition>();

		public abstract class RegisterDefinition
		{
			protected RegisterDefinition(string name, SSA.SSAType type, int offset)
			{
				if (string.IsNullOrEmpty(name))
					throw new ArgumentNullException("name");

				if (type == null)
					throw new ArgumentNullException("type");

				if (offset < 0)
					throw new ArgumentOutOfRangeException("offset");

				this.Name = name;
				this.Type = type;
				this.Offset = offset;
			}

			public string Name{ get; private set; }

			public SSA.SSAType Type{ get; private set; }

			public int Offset{ get; private set; }

			public abstract int Size{ get; }
		}

		public class Register : RegisterDefinition
		{
			internal Register(string name, SSA.SSAType type, int offset, int width)
				: base(name, type, offset)
			{
				if (width < 1)
					throw new ArgumentOutOfRangeException("width");
				this.Width = width;
			}

			public int Width{ get; private set; }

			public override int Size {
				get {
					return this.Width;
				}
			}
		}

		public class RegisterBank : RegisterDefinition
		{
			internal RegisterBank(string name, SSA.SSAType type, int offset, int count, int width, int stride)
				: base(name, type, offset)
			{
				if (count < 1)
					throw new ArgumentOutOfRangeException(nameof(count));

				if (width < 1)
					throw new ArgumentOutOfRangeException(nameof(width));

				if (stride < 1)
					throw new ArgumentOutOfRangeException(nameof(stride));

				this.Width = width;
				this.Count = count;
				this.Stride = stride;
			}

			public int Width{ get; private set; }

			public int Count{ get; private set; }

			public int Stride{ get; private set; }

			public override int Size {
				get {
					return this.Stride * this.Count;
				}
			}

			public int OffsetOfRegister(int index)
			{
				if (index < 0 || index >= this.Count)
					throw new ArgumentOutOfRangeException("index");
				return this.Offset + (this.Stride * index);
			}
		}

		public class VectorRegisterBank:RegisterDefinition
		{
			internal VectorRegisterBank(string name, SSA.SSAType type, int offset, int arity, int count, int width, int stride)
				: base(name, type, offset)
			{
				if (arity < 1)
					throw new ArgumentOutOfRangeException(nameof(arity));
                
				if (count < 1)
					throw new ArgumentOutOfRangeException(nameof(count));

				if (width < 1)
					throw new ArgumentOutOfRangeException(nameof(width));

				if (stride < 1)
					throw new ArgumentOutOfRangeException(nameof(stride));

				this.Arity = arity;
				this.Width = width;
				this.Count = count;
				this.Stride = stride;
			}

			public int Arity{ get; private set; }

			public int Width{ get; private set; }

			public int Count{ get; private set; }

			public int Stride{ get; private set; }

			public override int Size {
				get {
					return this.Arity * this.Stride * this.Count;
				}
			}
		}

		public RegisterFile()
		{
		}

		public uint Size{ get; private set; }

		public void AddRegisterBank(string name, string type, int count, int width, int stride, int offset)
		{
			var bank = new RegisterBank(name, SSA.SSAType.FromString(null, type), offset, count, width, stride);
			this.registerDefinitions.Add(bank);
		}

		public RegisterBank GetRegisterBank(string name)
		{
			foreach (var def in this.registerDefinitions.OfType<RegisterBank>()) {
				if (def.Name == name)
					return def;
			}

			throw new Exception("Register bank does not exist");
		}

		public Register GetRegister(string name)
		{
			foreach (var def in this.registerDefinitions.OfType<Register>()) {
				if (def.Name == name)
					return def;
			}

			throw new Exception("Register does not exist");
		}

		public void AddVectorRegisterBank(string name, string type, int arity, int count, int width, int stride, int offset)
		{
			var vrb = new VectorRegisterBank(name, SSA.SSAType.FromString(null, type), offset, arity, count, width, stride);
			this.registerDefinitions.Add(vrb);
		}

		public void AddRegister(string name, string type, string tag, int width, int offset)
		{
			var reg = new Register(name, SSA.SSAType.FromString(null, type), offset, width);
			this.registerDefinitions.Add(reg);
		}
	}
}

