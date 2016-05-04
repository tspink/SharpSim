//
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
        public abstract SSAStatement.Fixedness Fixed{ get; }

        public override string ToString()
        {
            return "?";
        }
    }

    public abstract class SSAValueOperand<T> : SSAOperand
    {
        public SSAValueOperand(T value)
        {
            this.Value = value;
        }

        public T Value{ get; private set; }

        public override string ToString()
        {
            return string.Format("?{0}", Value);
        }
    }

    public class StatementOperand : SSAValueOperand<SSAStatement>
    {
        public StatementOperand(SSAStatement stmt) : base(stmt)
        {
            if (stmt == null)
                throw new ArgumentNullException("stmt");
        }

        public override SSAStatement.Fixedness Fixed
        {
            get {
                return this.Value.Fixed;
            }
        }

        public override string ToString()
        {
            return string.Format("%{0}", this.Value.Index);
        }
    }

    public class BlockOperand : SSAValueOperand<SSABlock>
    {
        public BlockOperand(SSABlock block) : base(block)
        {
            if (block == null)
                throw new ArgumentNullException("block");
        }

        public override SSAStatement.Fixedness Fixed
        {
            get {
                return this.Value.Fixed;
            }
        }

        public override string ToString()
        {
            return string.Format("@{0}", this.Value.Index);
        }
    }

    public class SymbolOperand : SSAValueOperand<string>
    {
        public SymbolOperand(string symbol) : base(symbol)
        {
        }

        public override SSAStatement.Fixedness Fixed
        {
            get {
                return SSAStatement.Fixedness.AlwaysFixed;
            }
        }

        public override string ToString()
        {
            return string.Format("${0}", this.Value);
        }
    }

    public class IntegerOperand : SSAValueOperand<long>
    {
        public IntegerOperand(long val) : base(val)
        {
        }

        public override SSAStatement.Fixedness Fixed
        {
            get {
                return SSAStatement.Fixedness.AlwaysFixed;
            }
        }

        public override string ToString()
        {
            return string.Format("#{0}", this.Value);
        }
    }

    public static class SSAOperandExtensions
    {
        public static BlockOperand AsOperand(this SSABlock block)
        {
            return new BlockOperand(block);
        }

        public static StatementOperand AsOperand(this SSAStatement stmt)
        {
            return new StatementOperand(stmt);
        }

        public static SymbolOperand AsOperand(this string sym)
        {
            return new SymbolOperand(sym);
        }

        public static IntegerOperand AsOperand(this long cv)
        {
            return new IntegerOperand(cv);
        }

        public static IntegerOperand AsOperand(this int cv)
        {
            return new IntegerOperand((long)cv);
        }
    }
}

