//
// SSAActionPrototype.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.SSA
{
    public class SSAActionPrototype
    {
        private List<SSAType> parameterTypes = new List<SSAType>();

        public SSAActionPrototype(SSAType returnType)
        {
            this.ReturnType = returnType;
        }

        public SSAType ReturnType{ get; private set; }

        public void AddParameter(SSAType parameterType)
        {
            if (parameterType == null)
                throw new ArgumentNullException("parameterType");
            parameterTypes.Add(parameterType);
        }

        public static SSAActionPrototype FromParameters(SSAType returnType, IEnumerable<SSAType> parameterTypes)
        {
            var prototype = new SSAActionPrototype(returnType);
            foreach (var type in parameterTypes) {
                prototype.AddParameter(type);
            }

            return prototype;
        }
    }
}

