//
// ISA.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model
{
    public class ISA
    {
        private Dictionary<string, InstructionFormat> formats = new Dictionary<string, InstructionFormat>();

        public ISA(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            
            this.Name = name;
        }

        public string Name{ get; private set; }

        public InstructionFormat CreateInstructionFormat(string name)
        {
            var format = new InstructionFormat(name);
            formats.Add(name, format);
            return format;
        }

        public InstructionFormat GetInstructionFormat(string name)
        {
            InstructionFormat format;
            if (!this.formats.TryGetValue(name, out format))
                throw new Exception(string.Format("Instruction format '{0}' does not exist.", name));

            return format;
        }
    }
}

