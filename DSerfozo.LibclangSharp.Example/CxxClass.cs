using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSerfozo.LibclangSharp.Example
{
    public class CxxClass
    {
        public string Name { get; private set; }

        public string Location { get; private set; }

        public List<CxxMethod> Methods { get; } = new List<CxxMethod>();

        public CxxClass(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }
}
