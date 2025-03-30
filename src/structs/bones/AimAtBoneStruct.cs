using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs.bones {
	//'struct mstudioaimatbone_t
	//'{
	//'	DECLARE_BYTESWAP_DATADESC();
	//'
	//'	int				parent;
	//'	int				aim;		// Might be bone or attach
	//'	Vector			aimvector;
	//'	Vector			upvector;
	//'	Vector			basepos;
	//'
	//'	mstudioaimatbone_t() {}
	//'private:
	//'	// No copy constructors allowed
	//'	mstudioaimatbone_t(const mstudioaimatbone_t& vOther);
	//'};
    public class AimAtBoneStruct
    {
        public int parentBoneIndex;
        public int aimBoneOrAttachmentIndex;

        public Vec3 aim;
        public Vec3 up;
        public Vec3 basePos;

        public AimAtBoneStruct() {
            aim = new Vec3();
            up = new Vec3();
            basePos = new Vec3();
        }
    }
}
