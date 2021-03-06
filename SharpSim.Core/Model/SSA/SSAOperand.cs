﻿//
// SSAOperand.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
	public abstract class SSAOperand
	{
		public abstract SSAStatement.Fixedness Fixed { get; }

		public abstract SSAOperand Clone ();

		public override string ToString ()
		{
			return "?";
		}
	}

	public abstract class TypedSSAOperand : SSAOperand
	{
		public TypedSSAOperand (SSAType type)
		{
			if (type == null)
				throw new ArgumentNullException ("type");
			this.Type = type;
		}

		public SSAType Type { get; private set; }

		public override string ToString ()
		{
			return string.Format ("{0} ?", this.Type);
		}
	}

	public abstract class SSAValueOperand<T> : SSAOperand
	{
		public SSAValueOperand (T value)
		{
			this.Value = value;
		}

		public T Value { get; private set; }

		public override string ToString ()
		{
			return string.Format ("?{0}", Value);
		}
	}

	public abstract class TypedSSAValueOperand<T> : TypedSSAOperand
	{
		public TypedSSAValueOperand (SSAType type, T value)
			: base (type)
		{
			this.Value = value;
		}

		public T Value { get; private set; }

		public override string ToString ()
		{
			return string.Format ("{0} ?{1}", this.Type, Value);
		}
	}

	public class StatementOperand : TypedSSAValueOperand<SSAStatement>
	{
		public StatementOperand (SSAType type, SSAStatement stmt)
			: base (type, stmt)
		{
			if (stmt == null)
				throw new ArgumentNullException ("stmt");
		}

		public override SSAStatement.Fixedness Fixed {
			get {
				return this.Value.Fixed;
			}
		}

		public override SSAOperand Clone ()
		{
			return new StatementOperand (this.Type, this.Value);
		}

		public override string ToString ()
		{
			return string.Format ("{0} %{1}", this.Type, this.Value.Index);
		}
	}

	public class BlockOperand : SSAValueOperand<SSABlock>
	{
		public BlockOperand (SSABlock block)
			: base (block)
		{
			if (block == null)
				throw new ArgumentNullException ("block");
		}

		public override SSAStatement.Fixedness Fixed {
			get {
				return this.Value.Fixed;
			}
		}

		public override SSAOperand Clone ()
		{
			return new BlockOperand (this.Value);
		}

		public override string ToString ()
		{
			return string.Format ("@{0}", this.Value.Index);
		}
	}

	public class SymbolOperand : SSAValueOperand<SSASymbol>
	{
		public SymbolOperand (SSASymbol symbol)
			: base (symbol)
		{
		}

		public override SSAStatement.Fixedness Fixed {
			get { return this.Value.Fixedness; }
		}

		public override SSAOperand Clone ()
		{
			return new SymbolOperand (this.Value);
		}

		public override string ToString ()
		{
			return string.Format ("${0}", this.Value.Name);
		}
	}

	public class IntegerOperand : TypedSSAValueOperand<long>
	{
		public IntegerOperand (PrimitiveType type, long val)
			: base (type, val)
		{
		}

		public override SSAStatement.Fixedness Fixed {
			get {
				return SSAStatement.Fixedness.AlwaysFixed;
			}
		}

		public override SSAOperand Clone ()
		{
			return new IntegerOperand (this.Type as PrimitiveType, this.Value);
		}

		public override string ToString ()
		{
			return string.Format ("{0} #{1}", this.Type, this.Value);
		}
	}

	public class DoubleOperand : TypedSSAValueOperand<double>
	{
		public DoubleOperand (PrimitiveType type, double val)
			: base (type, val)
		{
		}

		public override SSAStatement.Fixedness Fixed {
			get {
				return SSAStatement.Fixedness.AlwaysFixed;
			}
		}

		public override SSAOperand Clone ()
		{
			return new DoubleOperand (this.Type as PrimitiveType, this.Value);
		}

		public override string ToString ()
		{
			return string.Format ("{0} #{1}", this.Type, this.Value);
		}
	}

	public class ActionOperand : SSAValueOperand<SSAAction>
	{
		public ActionOperand (SSAAction action)
			: base (action)
		{
		}

		public override SSAStatement.Fixedness Fixed {
			get {
				throw new NotImplementedException ();
			}
		}

		public override SSAOperand Clone ()
		{
			return new ActionOperand (this.Value);
		}

		public override string ToString ()
		{
			return string.Format ("@{0}", this.Value.Prototype);
		}
	}

	public static class SSAOperandExtensions
	{
		public static BlockOperand AsOperand (this SSABlock block)
		{
			return new BlockOperand (block);
		}

		public static StatementOperand AsOperand (this SSAStatement stmt)
		{
			return new StatementOperand (stmt.Type, stmt);
		}

		public static SymbolOperand AsOperand (this SSASymbol sym)
		{
			return new SymbolOperand (sym);
		}

		public static IntegerOperand AsOperand (this long cv)
		{
			return new IntegerOperand (PrimitiveType.UInt64, cv);
		}

		public static IntegerOperand AsOperand (this int cv)
		{
			return new IntegerOperand (PrimitiveType.UInt32, (long)cv);
		}

		public static DoubleOperand AsOperand (this double cv)
		{
			return new DoubleOperand (PrimitiveType.Float64, cv);
		}

		public static ActionOperand AsOperand (this SSAAction action)
		{
			return new ActionOperand (action);
		}
	}
}
