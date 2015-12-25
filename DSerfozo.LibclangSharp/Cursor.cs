using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSerfozo.LibclangSharp.Native;

namespace DSerfozo.LibclangSharp
{
    public delegate ChildVisitResult CursorVisitor(Cursor cursor, Cursor parent);
    public delegate ChildVisitResult CursorVisitor<in T>(Cursor cursor, Cursor parent, T state);

    public sealed class Cursor
    {
        private readonly CXCursor nativeCursor;

        public CursorKind Kind
        {
            get { return NativeMethods.clang_getCursorKind(nativeCursor); }
        }

        public Type Type
        {
            get { return new Type(NativeMethods.clang_getCursorType(nativeCursor)); }
        }

        public Type ResultType
        {
            get
            {
                return new Type(NativeMethods.clang_getCursorResultType(nativeCursor));
            }
        }

        public SourceLocation Location
        {
            get { return new SourceLocation(NativeMethods.clang_getCursorLocation(nativeCursor)); }
        }

        public string Spelling
        {
            get { return NativeMethods.clang_getCursorSpelling(nativeCursor).ToManagedString(); }
        }

        public CXXAccessSpecifier AccessSpecifier
        {
            get { return NativeMethods.clang_getCXXAccessSpecifier(nativeCursor); }
        }

        public bool IsPureVirtual
        {
            get { return NativeMethods.clang_CXXMethod_isPureVirtual(nativeCursor) != 0; }
        }

        internal Cursor(CXCursor nativeCursor)
        {
            this.nativeCursor = nativeCursor;
        }

        public void VisitChildren<T>(CursorVisitor<T> visitor, T state)
        {
            NativeMethods.clang_visitChildren(nativeCursor,
                (cursor, parent, data) => visitor(new Cursor(cursor), new Cursor(parent), state), IntPtr.Zero);
        }

        public void VisitChildren(CursorVisitor visitor)
        {
            NativeMethods.clang_visitChildren(nativeCursor,
                (cursor, parent, data) => visitor(new Cursor(cursor), new Cursor(parent)), IntPtr.Zero);
        }
    }
}
