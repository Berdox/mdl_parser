using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Vec3 {
        public float X;
        public float Y;
        public float Z;
    }
}
