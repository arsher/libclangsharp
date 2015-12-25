using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DSerfozo.LibclangSharp.Example
{
    internal class Program
    {
        private static List<CxxClass> classes = new List<CxxClass>(); 

        private static void Main(string[] args)
        {
            var arr = new[] {"-x", "c++"};
            using (var index = new Index())
            {
                index.AddTranslationUnit(Path.Combine(Directory.GetCurrentDirectory(), "example.h"), arr);

                index.TranslationUnits
                    .ToList()
                    .ForEach(t => t.Cursor.VisitChildren(Visitor));
            }

            Console.WriteLine(classes.First().Name);
        }

        private static ChildVisitResult Visitor(Cursor cursor, Cursor parent)
        {
            if (cursor.Kind == CursorKind.ClassDecl)
            {
                var className = cursor.Spelling;
                var location = cursor.Location.FileName;
                if (classes.All(c => c.Name != className))
                {
                    var cxxClass = new CxxClass(className, location);
                    cursor.VisitChildren(MethodVisitor, cxxClass);
                    classes.Add(cxxClass);
                }
                return ChildVisitResult.Continue;
            }

            return ChildVisitResult.Recurse;
        }

        private static ChildVisitResult MethodVisitor(Cursor cursor, Cursor parent, CxxClass @class)
        {
            if (cursor.Kind == CursorKind.CXXMethod)
            {
                var methodName = cursor.Spelling;
                var resultType = cursor.ResultType.Spelling;
                var accessSpecifier = cursor.AccessSpecifier;
                var method = new CxxMethod(methodName, resultType, accessSpecifier);

                cursor.VisitChildren(ParameterVisitor, method);

                @class.Methods.Add(method);    
            }

            return ChildVisitResult.Continue;
        }

        private static ChildVisitResult ParameterVisitor(Cursor cursor, Cursor parent, CxxMethod @method)
        {
            if (cursor.Kind == CursorKind.ParmDecl)
            {
                @method.Parameters.Add(Tuple.Create(cursor.Type.Spelling, cursor.Spelling));
            }

            return ChildVisitResult.Continue;
        }
    }
}