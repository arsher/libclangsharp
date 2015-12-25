using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSerfozo.LibclangSharp.Native;

namespace DSerfozo.LibclangSharp
{
    public sealed class Type
    {
        private readonly CXType nativeType;

        public string Spelling
        {
            get { return NativeMethods.clang_getTypeSpelling(nativeType).ToManagedString(); }
        }

        internal Type(CXType nativeType)
        {
            this.nativeType = nativeType;
        }
    }
}
