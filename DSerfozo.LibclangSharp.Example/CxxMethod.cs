using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSerfozo.LibclangSharp.Example
{
    public class CxxMethod
    {
        private readonly string name;
        private readonly string resultType;
        private readonly AccessSpecifier accessSpecifier;

        public string Name => name;

        public string ResultType => resultType;

        public AccessSpecifier AccessSpecifier => accessSpecifier;

        public List<Tuple<string, string>> Parameters { get; } = new List<Tuple<string, string>>();

        public CxxMethod(string name, string resultType, AccessSpecifier accessSpecifier)
        {
            this.name = name;
            this.resultType = resultType;
            this.accessSpecifier = accessSpecifier;
        }

        public override int GetHashCode()
        {
            var p = (Parameters.Count > 0 ? Parameters.Select(i => i.Item1 + " " + i.Item2).Aggregate((i, j) => i + ", " + j) : "");
            return new { Name, ResultType, p }.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p = (CxxMethod)obj;
            return Name == p.Name && ResultType == p.ResultType && Parameters.SequenceEqual(p.Parameters);
        }
    }
}
