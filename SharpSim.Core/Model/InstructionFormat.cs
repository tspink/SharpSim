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
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException("name");

                if (offset < 0)
                    throw new ArgumentOutOfRangeException("offset");

                if (length < 1)
                    throw new ArgumentOutOfRangeException("length");

                this.Name = name;
                this.Offset = offset;
                this.Length = length;
            }

            public int Offset{ get; private set; }

            public int Length{ get; private set; }

            public string Name{ get; private set; }

            public bool Hidden{ get; set; }

            public bool SignExtend{ get; set; }

            internal bool IntersectsWith(int offset, int length)
            {
                return false;
            }
        }

        public InstructionFormat(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.Name = name;
        }

        public string Name{ get; private set; }

        public InstructionField AddField(string name, int offset, int length)
        {
            foreach (var existingField in this.fields) {
                if (existingField.IntersectsWith(offset, length))
                    throw new Exception(string.Format("Field would intersect with '{0}'.", existingField.Name));
            }

            var field = new InstructionField(name, offset, length);
            this.fields.Add(field);

            return field;
        }

        public IEnumerable<InstructionField> Fields{ get { return this.fields.AsReadOnly(); } }
    }
}

