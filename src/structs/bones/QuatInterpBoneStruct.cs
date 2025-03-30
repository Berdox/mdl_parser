using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs.bones {
	//'FROM: SourceEngineXXXX_source\public\studio.h

	//'struct mstudioquatinterpbone_t
	//'{
	//'	DECLARE_BYTESWAP_DATADESC();
	//'	int				control;// local transformation to check
	//'	int				numtriggers;
	//'	int				triggerindex;
	//'	inline mstudioquatinterpinfo_t *pTrigger( int i ) const { return  (mstudioquatinterpinfo_t *)(((byte *)this) + triggerindex) + i; };
	//'
	//'	mstudioquatinterpbone_t(){}
	//'private:
	//'	// No copy constructors allowed
	//'	mstudioquatinterpbone_t(const mstudioquatinterpbone_t& vOther);
	//'};
    public class QuatInterpBoneStruct
    {
        public int controlBoneIndex;  // Local transformation to check
        public int triggerCount;
        public int triggerOffset;

        public List<QuatInterpBoneInfoStruct> triggers;

        public QuatInterpBoneStruct() {
            triggers = new List<QuatInterpBoneInfoStruct>();
        }
    }
}
