//
// ArchException.cs
//
// Copyright (c) 2016 Tom Spink <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model
{
	public class ArchException
	{
		public ArchException(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			this.Name = name;
		}

		public string Name{ get; private set; }
	}
}

