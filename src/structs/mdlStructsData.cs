using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs
{
    public class mdlStructsData
    {

        public Header header;
        public Secondary_Header sec_header;
        public TextureHeader texture;
        public PhyHeader phy_header;
        public string mayaStrings;

        public mdlStructsData() {
            header = new Header();
            sec_header = new Secondary_Header();
            texture = new TextureHeader();
            phy_header = new PhyHeader();
            mayaStrings = "";
        }

    }
}
