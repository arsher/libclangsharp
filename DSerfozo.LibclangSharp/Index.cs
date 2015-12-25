using System;
using System.Collections.Generic;
using System.Linq;
using DSerfozo.LibclangSharp.Native;

namespace DSerfozo.LibclangSharp
{
    public sealed class Index : DisposableObject<Index>
    {
        private readonly IntPtr nativeIndex;
        private readonly List<TranslationUnit> translationUnits = new List<TranslationUnit>();

        public IEnumerable<TranslationUnit> TranslationUnits
        {
            get { return translationUnits.Where(t => !t.IsDisposed); }
        } 

        public Index() : base(DisposeCallback)
        {
            nativeIndex = Native.NativeMethods.clang_createIndex(0, 0);
        }

        public TranslationUnit AddTranslationUnit(string sourceFile, IEnumerable<string> commandLineArguments)
        {
            ThrowIfDisposed();

            IntPtr translationUnit;
            var args = commandLineArguments.ToArray();
            var errorCode = NativeMethods.clang_parseTranslationUnit2(nativeIndex, sourceFile, args, args.Length, new CXUnsavedFile[0],
                0, 0, out translationUnit);

            switch (errorCode)
            {
                case CXErrorCode.ASTReadError:
                    throw new ClangException("An AST deserialization error has occurred.");
                case CXErrorCode.Crashed:
                    throw new ClangException("libclang crashed while performing the requested operation.");
                case CXErrorCode.Failure:
                    throw new ClangException("A generic error code, no further details are available.");
                case CXErrorCode.InvalidArguments:
                    throw new ClangException("The function detected that the arguments violate the function contract.");
            }

            var result = new TranslationUnit(translationUnit);
            translationUnits.Add(result);

            return result;
        }

        private static void DisposeCallback(Index index, bool disposing)
        {
            if (disposing)
            {
                index.translationUnits.ForEach(t => t.Dispose());
                index.translationUnits.Clear();
            }

            if (!index.IsDisposed && index.nativeIndex != IntPtr.Zero)
            {
                Native.NativeMethods.clang_disposeIndex(index.nativeIndex);
            }
        }
    }
}
