using System;
using System.Runtime.InteropServices;

namespace DSerfozo.LibclangSharp.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CXSourceLocation
    {
        public readonly IntPtr ptr_data0;
        public readonly IntPtr ptr_data1;
        public readonly uint int_data;
    }
}
