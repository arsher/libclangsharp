using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using DSerfozo.LibclangSharp.Native;

namespace DSerfozo.LibclangSharp
{
    public sealed class TranslationUnit : DisposableObject<TranslationUnit>
    {
        private readonly IntPtr nativeTranslationUnit;
        private Cursor cursor;

        public Cursor Cursor
        {
            get
            {
                if (cursor == null)
                {
                    cursor = new Cursor(NativeMethods.clang_getTranslationUnitCursor(nativeTranslationUnit));
                }

                return cursor;
            }
        }

        internal TranslationUnit(IntPtr nativeTranslationUnit) : base(DisposeCallback)
        {
            this.nativeTranslationUnit = nativeTranslationUnit;
        }

        private static void DisposeCallback(TranslationUnit obj, bool disposing)
        {
            if (!obj.IsDisposed && obj.nativeTranslationUnit != IntPtr.Zero)
            {
                NativeMethods.clang_disposeTranslationUnit(obj.nativeTranslationUnit);
            }
        }
    }
}
