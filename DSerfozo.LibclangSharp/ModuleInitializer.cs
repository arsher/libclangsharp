using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using DSerfozo.LibclangSharp.Properties;


/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
    static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        const string libNameFormat = "DSerfozo.LibclangSharp.Resources.libclang_{0}";
        const string libClangPath = @"libclang\libclang.dll";

        var libPath = Path.Combine(Directory.GetCurrentDirectory(), libClangPath);
        var libDirectory = Path.GetDirectoryName(libPath);

        if (NeedsExtract(libPath))
        {
            Directory.CreateDirectory(libDirectory);

            var libStreamName = string.Format(libNameFormat, Environment.Is64BitProcess ? "x64" : "x86");

            using (var dllStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(libStreamName))
            using (var deflate = new DeflateStream(dllStream, CompressionMode.Decompress))
            using (var output = File.Create(libPath))
            {
                deflate.CopyTo(output);
            }
        }

        var module = LoadLibrary(libPath);
        if (module == IntPtr.Zero)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }

    private static bool NeedsExtract(string libPath)
    {
        var result = true;
        var expectedHash = GetExpectedHash();
        if (File.Exists(libPath))
        {
            using (var file = File.OpenRead(libPath))
            using (var md5 = MD5.Create())
            {
                var existingHash = Convert.ToBase64String(md5.ComputeHash(file));
                result = existingHash != expectedHash;
            }
        }
        return result;
    }

    private static string GetExpectedHash()
    {
        string expectedHash;
        if (Environment.Is64BitProcess)
        {
            expectedHash = Resources.libclang_x64_checksum;
        }
        else
        {
            expectedHash = Resources.libclang_x86_checksum;
        }
        return expectedHash;
    }
}