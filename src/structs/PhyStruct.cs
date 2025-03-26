using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs
{
    // FROM: SourceEngine2007\src_main\public\phyfile.h
    // typedef struct phyheader_s
    // {
    //	int		size;
    //	int		id;
    //	int		solidCount;
    //	long	checkSum;	// checksum of source .mdl file
    // } phyheader_t;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PhyHeader {
        int size;
        int id;
        int solidCount;
        int checkSum;	// checksum of source .mdl file
    }
}
