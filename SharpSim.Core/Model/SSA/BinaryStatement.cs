//
// BinaryStatement.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA
{
    public abstract class BinaryStatement : SSAStatement
    {
        public BinaryStatement(TypedSSAOperand lhs, TypedSSAOperand rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException("lhs");

            if (rhs == null)
                throw new ArgumentNullException("rhs");
            
            this.LHS = lhs;
            this.RHS = rhs;
        }

        public TypedSSAOperand LHS{ get; private set; }

        public TypedSSAOperand RHS{ get; private set; }

        public override Fixedness Fixed
        {
            get {
                if (LHS.Fixed == Fixedness.AlwaysFixed && RHS.Fixed == Fixedness.AlwaysFixed)
                    return Fixedness.AlwaysFixed;
                else
                    return Fixedness.Dynamic;
            }
        }
    }
}

