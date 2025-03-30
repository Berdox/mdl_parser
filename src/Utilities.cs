using mdl_parser.src.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.utilities {
    public static class Utilities
    {
        public static void PrintHex<T>(T value) where T : unmanaged {
            string name = typeof(T).Name;  // Automatically use the type name
            ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref value, 1));
            Console.WriteLine("Memory (Hex) of {name}: " + BitConverter.ToString(bytes.ToArray()));
        }

        //public static void WriteHeaderToFile(Header header, string fileName) {
        //    // Get the size of the struct
        //    int size = Marshal.SizeOf(typeof(Header));

        //    // Allocate memory for the struct and copy its contents
        //    IntPtr ptr = Marshal.AllocHGlobal(size);
        //    try {
        //        // Marshal the struct to memory
        //        Marshal.StructureToPtr(header, ptr, false);

        //        // Create a byte array of the correct size
        //        byte[] bytes = new byte[size];

        //        // Copy the contents from the unmanaged memory into the byte array
        //        Marshal.Copy(ptr, bytes, 0, size);

        //        // Write the byte array to a file
        //        File.WriteAllBytes(fileName, bytes);
        //        Console.WriteLine($"Binary struct written to {fileName}");
        //    }
        //    finally {
        //        // Free the allocated memory
        //        Marshal.FreeHGlobal(ptr);
        //    }
        //}
    }
}
