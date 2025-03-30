using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs.bones {
	//'FROM: SourceEngineXXXX_source\public\studio.h
	//'struct mstudiojigglebone_t
	//'{
	//'	DECLARE_BYTESWAP_DATADESC();

	//'	int				flags;

	//'	// general params
	//'	float			length;					// how from from bone base, along bone, is tip
	//'	float			tipMass;

	//'	// flexible params
	//'	float			yawStiffness;
	//'	float			yawDamping;	
	//'	float			pitchStiffness;
	//'	float			pitchDamping;	
	//'	float			alongStiffness;
	//'	float			alongDamping;	

	//'	// angle constraint
	//'	float			angleLimit;				// maximum deflection of tip in radians

	//'	// yaw constraint
	//'	float			minYaw;					// in radians
	//'	float			maxYaw;					// in radians
	//'	float			yawFriction;
	//'	float			yawBounce;

	//'	// pitch constraint
	//'	float			minPitch;				// in radians
	//'	float			maxPitch;				// in radians
	//'	float			pitchFriction;
	//'	float			pitchBounce;

	//'	// base spring
	//'	float			baseMass;
	//'	float			baseStiffness;
	//'	float			baseDamping;	
	//'	float			baseMinLeft;
	//'	float			baseMaxLeft;
	//'	float			baseLeftFriction;
	//'	float			baseMinUp;
	//'	float			baseMaxUp;
	//'	float			baseUpFriction;
	//'	float			baseMinForward;
	//'	float			baseMaxForward;
	//'	float			baseForwardFriction;

	//'FROM: https://github.com/ValveSoftware/source-sdk-2013/blob/master/mp/src/public/studio.h
	//'	// boing
	//'	float			boingImpactSpeed;
	//'	float			boingImpactAngle;
	//'	float			boingDampingRate;
	//'	float			boingFrequency;
	//'	float			boingAmplitude;

	//'private:
	//'	// No copy constructors allowed
	//'	//mstudiojigglebone_t(const mstudiojigglebone_t& vOther);
	//'};
    public class JiggleBoneStruct
    {
        public int flags;

        public double length;

        public double tipMass;

        public double yaw_stiffness;
        public double yaw_damping;

        public double pitch_stiffness;
        public double pitch_damping;

        public double along_stiffness;
        public double along_damping;

        public double angle_limit;

        public double min_yaw;
        public double max_yaw;

        public double yaw_friction;
        public double yaw_bounce;

        public double min_pitch;
        public double max_pitch;

        public double pitch_friction;
        public double pitch_bounce;

        public double base_mass;
        public double base_stiffness;
        public double base_damping;
        public double base_min_left;
        public double base_max_left;
        public double base_left_friction;
        public double base_min_up;
        public double base_max_up;
        public double base_up_friction;
        public double base_min_forward;
        public double base_max_forward;
        public double base_forward_friction;

        // These fields are present in models compiled with Source SDK Base 2013 MP and SP.
        public double boing_impact_speed;
        public double boing_impact_angle;
        public double boing_damping_rate;
        public double boing_frequency;
        public double boing_amplitude;

        // Flags values
        public const int JIGGLE_IS_FLEXIBLE = 0x01;
        public const int JIGGLE_IS_RIGID = 0x02;
        public const int JIGGLE_HAS_YAW_CONSTRAINT = 0x04;
        public const int JIGGLE_HAS_PITCH_CONSTRAINT = 0x08;
        public const int JIGGLE_HAS_ANGLE_CONSTRAINT = 0x10;
        public const int JIGGLE_HAS_LENGTH_CONSTRAINT = 0x20;
        public const int JIGGLE_HAS_BASE_SPRING = 0x40;
        public const int JIGGLE_IS_BOING = 0x80;

    }
}
