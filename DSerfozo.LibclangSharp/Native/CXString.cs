using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DSerfozo.LibclangSharp.Native
{
    internal struct CXString
    {
#pragma warning disable 649
        public readonly IntPtr data;
        public readonly uint private_flags;
#pragma warning restore 649

        public string ToManagedString()
        {
            var result = Marshal.PtrToStringAnsi(data);
            NativeMethods.clang_disposeString(this);
            return result;
        }
    }
}
