//
// SSAType.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.SSA
{
	public abstract class SSAType
	{
		private static bool typesInitialised = false;
		private static Dictionary<string, SSAType> primitiveTypeMapping = new Dictionary<string, SSAType>();

		public static readonly SSAType None = new NoneType();

		private class NoneType:SSAType
		{
			internal NoneType()
			{
			}
		}

		public SSAType()
		{
		}

		private static void InitialisePrimitiveTypes()
		{
			if (typesInitialised)
				return;

			typesInitialised = true;

			primitiveTypeMapping.Add("void", PrimitiveType.Void);
			primitiveTypeMapping.Add("bool", PrimitiveType.Boolean);

			primitiveTypeMapping.Add("u8", PrimitiveType.UInt8);
			primitiveTypeMapping.Add("u16", PrimitiveType.UInt16);
			primitiveTypeMapping.Add("u32", PrimitiveType.UInt32);
			primitiveTypeMapping.Add("u64", PrimitiveType.UInt64);

			primitiveTypeMapping.Add("s8", PrimitiveType.SInt8);
			primitiveTypeMapping.Add("s16", PrimitiveType.SInt16);
			primitiveTypeMapping.Add("s32", PrimitiveType.SInt32);
			primitiveTypeMapping.Add("s64", PrimitiveType.SInt64);

			primitiveTypeMapping.Add("single", PrimitiveType.Single);
			primitiveTypeMapping.Add("float", PrimitiveType.Double);
			primitiveTypeMapping.Add("double", PrimitiveType.Double);
		}

		public static SSAType FromString(SSAAction action, string s, bool refType)
		{
			if (!refType)
				return FromString(action, s);
			else
				return FromString(action, s).CreateReferenceType();
		}

		public static bool TryFromString(SSAAction action, string s, out SSAType type)
		{
			InitialisePrimitiveTypes();

			if (primitiveTypeMapping.TryGetValue(s, out type))
				return true;

			if (action != null) {
				type = action.GetNamedTypeParameter(s);
				if (type != null)
					return true;
			}

			type = null;
			return false;
		}

		public static SSAType FromString(SSAAction action, string s)
		{
			SSAType type;
			if (!TryFromString(action, s, out type))
				throw new Exceptions.UnrecognisedTypeException(s);

			return type;
		}

		public ReferenceType CreateReferenceType()
		{
			return new ReferenceType(this);
		}
	}

	public enum PrimitiveKind
	{
		Void,
		Boolean,
		UInt8,
		UInt16,
		UInt32,
		UInt64,
		SInt8,
		SInt16,
		SInt32,
		SInt64,
		Single,
		Double
	}

	public class PrimitiveType : SSAType
	{
		public static readonly PrimitiveType Void = new PrimitiveType(PrimitiveKind.Void);
		public static readonly PrimitiveType Boolean = new PrimitiveType(PrimitiveKind.Boolean);
		public static readonly PrimitiveType UInt8 = new PrimitiveType(PrimitiveKind.UInt8);
		public static readonly PrimitiveType UInt16 = new PrimitiveType(PrimitiveKind.UInt16);
		public static readonly PrimitiveType UInt32 = new PrimitiveType(PrimitiveKind.UInt32);
		public static readonly PrimitiveType UInt64 = new PrimitiveType(PrimitiveKind.UInt64);
		public static readonly PrimitiveType SInt8 = new PrimitiveType(PrimitiveKind.SInt8);
		public static readonly PrimitiveType SInt16 = new PrimitiveType(PrimitiveKind.SInt16);
		public static readonly PrimitiveType SInt32 = new PrimitiveType(PrimitiveKind.SInt32);
		public static readonly PrimitiveType SInt64 = new PrimitiveType(PrimitiveKind.SInt64);
		public static readonly PrimitiveType Single = new PrimitiveType(PrimitiveKind.Single);
		public static readonly PrimitiveType Double = new PrimitiveType(PrimitiveKind.Double);

		private PrimitiveType(PrimitiveKind kind)
		{
			this.Kind = kind;
		}

		public PrimitiveKind Kind{ get; private set; }

		public override string ToString()
		{
			switch (this.Kind) {
			case PrimitiveKind.Boolean:
				return "u1";
			case PrimitiveKind.UInt8:
				return "u8";
			case PrimitiveKind.UInt16:
				return "u16";
			case PrimitiveKind.UInt32:
				return "u32";
			case PrimitiveKind.UInt64:
				return "u64";
			case PrimitiveKind.SInt8:
				return "s8";
			case PrimitiveKind.SInt16:
				return "s16";
			case PrimitiveKind.SInt32:
				return "s32";
			case PrimitiveKind.SInt64:
				return "s64";
			case PrimitiveKind.Single:
				return "single";
			case PrimitiveKind.Double:
				return "double";
			case PrimitiveKind.Void:
				return "void";
			default:
				return "?";
			}
		}
	}

	public class ReferenceType:SSAType
	{
		public ReferenceType(SSAType underlyingType)
		{
			if (underlyingType == null)
				throw new ArgumentNullException(nameof(underlyingType));
			this.UnderlyingType = underlyingType;
		}

		public SSAType UnderlyingType{ get; private set; }

		public override string ToString()
		{
			return "&" + this.UnderlyingType.ToString();
		}
	}

	public class ExceptionType : SSAType
	{
		public static readonly ExceptionType Type = new SharpSim.Model.SSA.ExceptionType();

		private ExceptionType()
		{
		}

		public override string ToString()
		{
			return string.Format("[ExceptionType]");
		}
	}

	public class SSATypeParameter : SSAType
	{
		public SSATypeParameter(string name)
		{
			this.Name = name;
		}

		public string Name{ get; private set; }

		public override string ToString()
		{
			return this.Name;
		}
	}
}

