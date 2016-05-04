//
// Architecture.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model
{
    public class Architecture
    {
        private List<Behaviour> behaviours = new List<Behaviour>();
        private List<Helper> helpers = new List<Helper>();

        public Architecture(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.Name = name;
        }

        public string Name{ get; private set; }

        public void AddBehaviour(Behaviour behaviour)
        {
            this.behaviours.Add(behaviour);
        }

        public void AddHelper(Helper helper)
        {
            this.helpers.Add(helper);
        }

        public IEnumerable<Behaviour> Behaviours{ get { return behaviours.AsReadOnly(); } }

        public IEnumerable<Helper> Helpers{ get { return helpers.AsReadOnly(); } }
    }
}

