using System;
using System.Runtime.InteropServices;

namespace DSerfozo.LibclangSharp.Native
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate ChildVisitResult CXCursorVisitor(CXCursor cursor, CXCursor parent, IntPtr data);

    internal static class NativeMethods
    {
        private const string LibName = "libclang";

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr clang_createIndex(int excludeDeclarationsFromPch, int displayDiagnostics);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clang_disposeIndex(IntPtr index);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CXErrorCode clang_parseTranslationUnit2(IntPtr index, [MarshalAs(UnmanagedType.LPStr)] string sourceFilename, string[] commandLineArgs, int numCommandLineArgs, CXUnsavedFile[] unsavedFiles, uint numUnsavedFiles, uint @options, out IntPtr translationUnit);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clang_disposeTranslationUnit(IntPtr translationUnit);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CXCursor clang_getTranslationUnitCursor(IntPtr translationUnit);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern uint clang_visitChildren(CXCursor parent, CXCursorVisitor visitor, IntPtr clientData);

        [DllImport(LibName,  CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CursorKind clang_getCursorKind(CXCursor cursor);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clang_disposeString(CXString @string);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CXString clang_getCursorSpelling(CXCursor @param0);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CXType clang_getCursorResultType(CXCursor cursor);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CXString clang_getTypeSpelling(CXType type);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CXType clang_getCursorType(CXCursor cursor);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern AccessSpecifier clang_getCXXAccessSpecifier(CXCursor @param0);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern uint clang_CXXMethod_isPureVirtual(CXCursor cursor);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CXSourceLocation clang_getCursorLocation(CXCursor cursor);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clang_getFileLocation(CXSourceLocation location, out IntPtr file, out uint line, out uint column, out uint offset);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern CXString clang_getFileName(IntPtr file);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int clang_Location_isInSystemHeader(CXSourceLocation @location);
    }
}
