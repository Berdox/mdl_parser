using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs.textures
{

    // mstudiotexture_t
    public class TextureHeader {

        public TextureHeader() {
            unused2 = new int[10];
        }

        // Number of bytes past the beginning of this structure
        // where the first character of the texture name can be found.
        public int name_offset; // Offset for null-terminated string
        public int flags;

        public int used;        // Padding?
        public int unused;      // Padding.

        public int material;        // Placeholder for IMaterial
        public int client_material; // Placeholder for void*

        public int[] unused2; // Final padding
        // Struct is 64 bytes long
    }
}
