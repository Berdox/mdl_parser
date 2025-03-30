using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs.bones {
	//'FROM: SourceEngineXXXX_source\public\studio.h

	//'struct mstudioquatinterpinfo_t
	//'{
	//'	DECLARE_BYTESWAP_DATADESC();
	//'	float			inv_tolerance;	// 1 / radian angle of trigger influence
	//'	Quaternion		trigger;	// angle to match
	//'	Vector			pos;		// new position
	//'	Quaternion		quat;		// new angle
	//'
	//'	mstudioquatinterpinfo_t(){}
	//'private:
	//'	// No copy constructors allowed
	//'	mstudioquatinterpinfo_t(const mstudioquatinterpinfo_t& vOther);
	//'};
    public class QuatInterpBoneInfoStruct
    {
        public double inverseToleranceAngle;  // 1 / radian angle of trigger influence
        public Vec4 trigger;      // Angle to match
        public Vec3 pos;              // New position
        public Vec4 quat;         // New angle

        public QuatInterpBoneInfoStruct() {
            trigger = new Vec4();
            pos = new Vec3();
            quat = new Vec4();
        }
    }
}
