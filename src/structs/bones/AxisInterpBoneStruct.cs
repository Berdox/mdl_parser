using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs.bones {
    //'FROM: SourceEngineXXXX_source\public\studio.h
	//'struct mstudioaxisinterpbone_t
	//'{
	//'	DECLARE_BYTESWAP_DATADESC();
	//'	int				control;// local transformation of this bone used to calc 3 point blend
	//'	int				axis;	// axis to check
	//'	Vector			pos[6];	// X+, X-, Y+, Y-, Z+, Z-
	//'	Quaternion		quat[6];// X+, X-, Y+, Y-, Z+, Z-
	//'
	//'	mstudioaxisinterpbone_t(){}
	//'private:
	//'	// No copy constructors allowed
	//'	mstudioaxisinterpbone_t(const mstudioaxisinterpbone_t& vOther);
	//'};
    public class AxisInterpBoneStruct {

        public int control;  // Local transformation of this bone used to calculate 3-point blend
        public int axis;     // Axis to check
        public Vec3[] pos;  // X+, X-, Y+, Y-, Z+, Z-
        public Vec4[] quat; // X+, X-, Y+, Y-, Z+, Z-

        public AxisInterpBoneStruct() {
            pos = new Vec3[6];
            quat = new Vec4[6];

            for (int i = 0; i < pos.Length; i++) {
                pos[i] = new Vec3();
            }
            for (int i = 0; i < quat.Length; i++) {
                quat[i] = new Vec4();
            }
        }
    }

}
