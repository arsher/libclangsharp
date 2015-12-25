using System.Runtime.InteropServices;

namespace DSerfozo.LibclangSharp.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct CXUnsavedFile
    {
        public string Filename;
        public string Contents;
        public ulong Length;
    }
}
