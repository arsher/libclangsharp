using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DSerfozo.LibclangSharp.Native
{
    internal struct CXString
    {
        public readonly IntPtr data;
        public readonly uint private_flags;

        public string ToManagedString()
        {
            var result = Marshal.PtrToStringAnsi(data);
            NativeMethods.clang_disposeString(this);
            return result;
        }
    }
}
