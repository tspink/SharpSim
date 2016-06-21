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
		private Dictionary<string, Behaviour> behaviours = new Dictionary<string, Behaviour>();
		private List<Helper> helpers = new List<Helper>();

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

		public void AddBehaviour(Behaviour behaviour)
		{
			this.behaviours.Add(behaviour.Name, behaviour);
		}

		public Behaviour GetBehaviour(string name)
		{
			Behaviour behaviour;
			if (!this.behaviours.TryGetValue(name, out behaviour))
				throw new Exception(string.Format("Behaviour '{0}' does not exist", behaviour));
			return behaviour;
		}

		public void AddHelper(Helper helper)
		{
			this.helpers.Add(helper);
		}

		public IEnumerable<Behaviour> Behaviours{ get { return behaviours.Values; } }

		public IEnumerable<Helper> Helpers{ get { return helpers.AsReadOnly(); } }
	}
}

