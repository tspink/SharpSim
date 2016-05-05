//
// NoSuchActionException.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.SSA.Exceptions
{
    public class NoSuchActionException : SSAException
    {
        public NoSuchActionException(string action) : base(string.Format("Action '{0}' does not exist", action))
        {
        }
    }
}

