﻿//
// Behaviour.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model
{
    public class Behaviour : ModelAction
    {
        public Behaviour(string name, SSA.SSAAction action) : base(name, action)
        {
        }
    }
}

