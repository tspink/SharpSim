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
            case "uint8":
                return PrimitiveType.UInt8;
            case "uint16":
                return PrimitiveType.UInt16;
            case "uint32":
                return PrimitiveType.UInt32;
            case "uint64":
                return PrimitiveType.UInt64;
            }

            throw new Exception(string.Format("Unrecognised type '{0}'", s));
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

        private PrimitiveType(PrimitiveKind kind)
        {
            this.Kind = kind;
        }

        public PrimitiveKind Kind{ get; private set; }
    }
}

