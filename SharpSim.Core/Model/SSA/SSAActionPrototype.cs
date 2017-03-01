//
// SSAActionPrototype.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model.SSA
{
	public class SSAActionPrototype
	{
		private List<SSAType> parameterTypes = new List<SSAType> ();
		private List<SSAType> typeParameterTypes = new List<SSAType> ();

		public SSAActionPrototype (SSAType returnType, string name)
		{
			this.Name = name;
			this.ReturnType = returnType;
		}

		public string Name { get; private set; }

		public SSAType ReturnType { get; private set; }

		public void AddParameter (SSAType parameterType)
		{
			if (parameterType == null)
				throw new ArgumentNullException ("parameterType");
			parameterTypes.Add (parameterType);
		}

		public void AddTypeParameter (SSAType typeParameterType)
		{
			if (typeParameterType == null)
				throw new ArgumentNullException ("typeParameterType");

			typeParameterTypes.Add (typeParameterType);
		}

		public IEnumerable<SSAType> TypeParameters { get { return this.typeParameterTypes.AsReadOnly (); } }

		public bool IsGenericPrototype {
			get { return this.typeParameterTypes.Any (t => t is SSATypeParameter); }
		}

		public static SSAActionPrototype FromParameters (
			SSAType returnType,
			string name,
			IEnumerable<SSAType> parameterTypes = null,
			IEnumerable<SSAType> typeParameterTypes = null)
		{
			var prototype = new SSAActionPrototype (returnType, name);

			if (parameterTypes != null) {
				foreach (var type in parameterTypes) {
					prototype.AddParameter (type);
				}
			}

			if (typeParameterTypes != null) {
				foreach (var type in typeParameterTypes) {
					prototype.AddTypeParameter (type);
				}
			}

			return prototype;
		}

		public override int GetHashCode ()
		{
			return this.Name.GetHashCode ();
		}

		public bool Equivalent (SSAActionPrototype other, bool partial)
		{
			if (other.Name != this.Name)
				return false;

			if (other.typeParameterTypes.Count != this.typeParameterTypes.Count)
				return false;

			if (other.parameterTypes.Count != this.parameterTypes.Count)
				return false;

			if (!partial) {
				for (int i = 0; i < other.typeParameterTypes.Count; i++) {
					if (other.typeParameterTypes [i] != this.typeParameterTypes [i])
						return false;
				}
			}

			for (int i = 0; i < other.parameterTypes.Count; i++) {
				if (other.parameterTypes [i] != this.parameterTypes [i])
					return false;
			}

			return true;
		}

		public override bool Equals (object obj)
		{
			if (!(obj is SSAActionPrototype))
				return false;

			var other = (SSAActionPrototype)obj;

			return Equivalent (other, false);
		}

		public override string ToString ()
		{
			var typeParams = string.Join (", ", this.typeParameterTypes.Select (t => t.ToString ()));
			var @params = string.Join (", ", this.parameterTypes.Select (t => t.ToString ()));

			return $"{this.ReturnType} {this.Name}<{typeParams}>({@params})";
		}
	}
}

