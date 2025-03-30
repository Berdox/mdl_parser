using mdl_parser.src.structs.textures;
using mdl_parser.src.structs.bones;
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
        public List<BoneStruct> bones;
        public TextureHeader texture;
        public PhyHeader phy_header;
        public string mayaStrings;

        public mdlStructsData() {
            header = new Header();
            sec_header = new Secondary_Header();
            bones = new List<BoneStruct>();
            texture = new TextureHeader();
            phy_header = new PhyHeader();
            mayaStrings = "";
        }

    }
}
