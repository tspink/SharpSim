//
// SSAType.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public abstract class SSAType
    {
        public SSAType()
        {
        }

        public static SSAType FromString(string s)
        {
            switch (s) {
            case "void":
                return PrimitiveType.Void;
            case "bool":
                return PrimitiveType.Boolean;
            case "u8":
                return PrimitiveType.UInt8;
            case "u16":
                return PrimitiveType.UInt16;
            case "u32":
                return PrimitiveType.UInt32;
            case "u64":
                return PrimitiveType.UInt64;
            case "s8":
                return PrimitiveType.SInt8;
            case "s16":
                return PrimitiveType.SInt16;
            case "s32":
                return PrimitiveType.SInt32;
            case "s64":
                return PrimitiveType.SInt64;
            }

            throw new Exception(string.Format("Unrecognised type '{0}'", s));
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
        SInt64
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
}

