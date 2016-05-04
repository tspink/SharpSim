//
// InstructionFormat.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model
{
    public class InstructionFormat
    {
        private List<InstructionField> fields = new List<InstructionField>();

        public class InstructionField
        {
            internal InstructionField(string name, int offset, int length)
            {
                this.Name = name;
                this.Offset = offset;
                this.Length = length;
            }

            public int Offset{ get; private set; }

            public int Length{ get; private set; }

            public string Name{ get; private set; }

            public bool Hidden{ get; set; }

            public bool SignExtend{ get; set; }
        }

        public InstructionFormat()
        {
        }

        public void AddField(string name, int offset, int length)
        {
            this.fields.Add(new InstructionField(name, offset, length));
        }
    }
}

