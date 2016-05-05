//
// SSAException.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA.Exceptions
{
    public abstract class SSAException : Exception
    {
        public SSAException()
        {
        }

        public SSAException(string message) : base(message)
        {
        }
    }
}

