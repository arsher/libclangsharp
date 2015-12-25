using System;
using System.Runtime.InteropServices;

namespace DSerfozo.LibclangSharp.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CXType
    {
        public TypeKind kind;
        public readonly IntPtr data0;
        public readonly IntPtr data1;
    }
}
