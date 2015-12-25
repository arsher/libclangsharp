using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSerfozo.LibclangSharp.Native
{
    internal enum CXErrorCode : uint
    {
        Success = 0,
        Failure = 1,
        Crashed = 2,
        InvalidArguments = 3,
        ASTReadError = 4,
    }
}
