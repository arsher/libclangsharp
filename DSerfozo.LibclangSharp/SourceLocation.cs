using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSerfozo.LibclangSharp.Native;

namespace DSerfozo.LibclangSharp
{
    public sealed class SourceLocation
    {
        private readonly CXSourceLocation nativeSourceLocation;
        private readonly uint line;
        private readonly uint column;
        private readonly uint offset;
        private readonly string fileName;

        public uint Line
        {
            get { return line; }
        }

        public uint Column
        {
            get { return column; }
        }

        public uint Offset
        {
            get { return offset; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public bool IsInSystemHeader
        {
            get { return NativeMethods.clang_Location_isInSystemHeader(nativeSourceLocation) != 0; }
        }

        internal SourceLocation(CXSourceLocation nativeSourceLocation)
        {
            this.nativeSourceLocation = nativeSourceLocation;

            IntPtr file;
            NativeMethods.clang_getFileLocation(nativeSourceLocation, out file, out line, out column, out offset);
            fileName = NativeMethods.clang_getFileName(file).ToManagedString();
        }
    }
}
