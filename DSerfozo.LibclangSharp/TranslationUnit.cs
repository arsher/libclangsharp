using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using DSerfozo.LibclangSharp.Native;

namespace DSerfozo.LibclangSharp
{
    public sealed class TranslationUnit : DisposableObject
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

        internal TranslationUnit(IntPtr nativeTranslationUnit) : base()
        {
            this.nativeTranslationUnit = nativeTranslationUnit;
        }

        protected override void DisposeInternal(bool disposing)
        {
            if (nativeTranslationUnit != IntPtr.Zero)
            {
                NativeMethods.clang_disposeTranslationUnit(nativeTranslationUnit);
            }
        }
    }
}
