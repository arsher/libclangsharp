using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSerfozo.LibclangSharp
{
    public sealed class ClangException : Exception
    {
        public ClangException(string message) : base(message)
        {
        }

        public ClangException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
