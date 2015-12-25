using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DSerfozo.LibclangSharp.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CXCursor
    {
        public readonly CursorKind @kind;
        public readonly int xdata;
        public readonly IntPtr data0;
        public readonly IntPtr data1;
        public readonly IntPtr data2;
    }
}
