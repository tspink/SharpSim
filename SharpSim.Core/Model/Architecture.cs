//
// Architecture.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model
{
	public class Architecture
	{
		private Dictionary<string, ISA> isas = new Dictionary<string, ISA>();
		private List<ArchException> exceptions = new List<ArchException>();

		public Architecture(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			this.Name = name;

			this.RegisterFile = new RegisterFile();
		}

		public string Name{ get; private set; }

		public RegisterFile RegisterFile{ get; private set; }

		public ISA GetOrCreateISA(string name)
		{
			ISA isa;
			if (!this.isas.TryGetValue(name, out isa)) {
				isa = new ISA(this, name);
				this.isas.Add(name, isa);
			}

			return isa;
		}

		public ISA GetISA(string name)
		{
			ISA isa;
			if (!this.isas.TryGetValue(name, out isa))
				throw new Exception(string.Format("ISA '{0}' does not exist", name));
			return isa;
		}

		public IEnumerable<ISA> ISAs{ get { return this.isas.Values; } }

		public ArchException CreateException(string name)
		{
			var exp = new ArchException(name);
			this.exceptions.Add(exp);

			// TODO: Duplicates
			return exp;
		}

		public IEnumerable<ArchException> Exceptions{ get { return exceptions.AsReadOnly(); } }
	}
}

