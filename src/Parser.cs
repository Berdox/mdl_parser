using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using mdl_parser.src.structs;
using mdl_parser.src.utilities;

namespace mdl_parser.src.parser
{
    class Parser
    {
        public Header header;

        public T ReadStructFromFile<T>(FileInfo file, long offset) where T : struct {
            int size = Marshal.SizeOf<T>();

            using (FileStream fs = file.OpenRead())
            using (BinaryReader reader = new BinaryReader(fs)) {
                // Check if the file is large enough for the struct after considering the offset
                if (fs.Length < offset + size)
                    throw new Exception("File is too small to contain the expected structure at the given offset.");

                // Move the reader's position to the given offset
                fs.Seek(offset, SeekOrigin.Begin);

                // Read the bytes from the given offset
                byte[] buffer = reader.ReadBytes(size);

                // Allocate unmanaged memory to hold the struct
                IntPtr ptr = Marshal.AllocHGlobal(size);
                try {
                    Marshal.Copy(buffer, 0, ptr, size);
                    return Marshal.PtrToStructure<T>(ptr);
                }
                finally {
                    Marshal.FreeHGlobal(ptr);
                }
            }
        }

        public void WriteHeaderToFile() {
            Utilities.WriteHeaderToFile(header, "file.bin");
        }
    }
}
