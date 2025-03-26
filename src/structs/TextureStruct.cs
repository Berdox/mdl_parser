using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    // mstudiotexture_t
    public class TextureHeader {
        // Number of bytes past the beginning of this structure
        // where the first character of the texture name can be found.
        public int name_offset; // Offset for null-terminated string
        public int flags;

        public int used;        // Padding?
        public int unused;      // Padding.

        public int material;        // Placeholder for IMaterial
        public int client_material; // Placeholder for void*

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public int unused2; // Final padding
        // Struct is 64 bytes long
    }
}
