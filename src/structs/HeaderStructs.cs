using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs
{
    
    // struct studiohdr_t
    public class Header {

        public Header() {
            id = new char[4];
            name = new char[64];
            reserved = new int[59];
        }

        // Model format ID (e.g., "IDST" for 0x49 0x44 0x53 0x54)
        public char[] id;

        // Format version number (e.g., 48, represented as 0x30, 0x00, 0x00, 0x00)
        public int version;

        public int fileSize;

        // Checksum for consistency, must match across related files (e.g., .phy, .vtx)
        public int checksum;

        // The name may need an offset to find the name of model
        public int name_offset;

        // The internal name of the model, usually the model filename with padding
        public char[] name;

        public string name_by_offset;

        public string model_name;

        // Total data size of the model file in bytes
        public int dataLength;

        public long actual_file_length;

        // Position of the player's viewpoint relative to the model's origin
        public Vec3 eyeposition;

        // Position used for calculating ambient lighting and cubemap reflections
        public Vec3 illumposition;

        // The minimum corner of the model's bounding box (least X/Y/Z values)
        public Vec3 hull_min;

        // The maximum corner of the model's bounding box (opposite corner)
        public Vec3 hull_max;

        // Bounding box used for view culling (minimum coordinates)
        public Vec3 view_bounding_box_min;

        // Bounding box used for view culling (maximum coordinates)
        public Vec3 view_bounding_box_max;

        // Binary flags for model properties, stored in little-endian order
        public int flags;

        // Number of bones in the model (used for skeletal animation)
        public int bone_count;

        // Offset to the first bone data section
        public int bone_offset;

        // Number of bone controllers (used to control bone movement)
        public int bone_controller_count;

        // Offset to the first bone controller data section
        public int bone_controller_offset;

        // Number of hitboxes for collision detection in the model
        public int hitbox_set_count;

        // Offset to the first hitbox data section
        public int hitbox_set_offset;

        // Number of local animations in the model (e.g., idle, run, jump)
        public int local_anim_count;

        // Offset to the local animations data section
        public int local_anim_offset;

        // Number of local sequences in the model (e.g., movement states)
        public int local_seq_count;

        // Offset to the local sequences data section
        public int local_seq_offset;

        // Version of the activity list (for animation sequencing)
        public int activity_list_version;

        // Whether events have been indexed in the model
        public int events_indexed;

        // Number of textures used in the model
        public int texture_count;

        // Offset to the texture data section
        public int texture_offset;

        // Number of texture directories (directories storing texture files)
        public int texture_path_count;

        // Offset to the texture directory data section
        public int texture_path_offset;

        // Number of skin references (defines texture slots for skins)
        public int skin_reference_count;

        // Number of skin families (skin variations for the model)
        public int skin_family_count;

        // Index to the skin reference data
        public int skin_family_offset;

        // Number of body parts in the model (e.g., head, torso, legs)
        public int body_part_count;

        // Offset to the body part data section
        public int body_part_offset;

        // Number of attachment points (e.g., for props or accessories)
        public int local_attachment_count;

        // Offset to the attachment data section
        public int local_attachment_offset;

        // Number of local nodes (used for hierarchical structure)
        public int local_node_count;

        // Index to the local node data
        public int local_node_offset;

        // Index to the local node names (null-terminated strings)
        public int local_node_name_offset;

        // Number of flex descriptors (for facial animation controls)
        public int flex_desc_count;

        // Index to the flex descriptor data
        public int flex_desc_offset;

        // Number of flex controllers (controls facial expression changes)
        public int flex_controller_count;

        // Index to the flex controller data
        public int flex_controller_offset;

        // Number of flex rules (how the controllers affect the model)
        public int flex_rules_count;

        // Index to the flex rule data
        public int flex_rules_offset;

        // Number of IK chains (inverse kinematics chains for bone movement)
        public int ikchain_count;

        // Index to the IK chain data
        public int ikchain_offset;

        // Number of mouth sections for speech animation (can have more than one)
        public int rui_count;

        // Index to the mouth data (defines sounds for speech animations)
        public int rui_offset;

        // Number of pose parameters (used for facial expressions)
        public int local_pose_parm_count;

        // Index to the pose parameter data
        public int local_pose_parm_offset;

        // Index to the surface property value (like material type or surface interaction)
        public int surface_prop_offset;

        // Index to the surface property value (like material type or surface interaction)
        public string surface_prop_name;

        // Index to key-value data (used for custom attributes, may reference a texture or other model data)
        public int key_value_offset;

        // Count of key-value pairs in the model's key-value data
        public int key_value_count;

        // Number of IK lock data (locks for inverse kinematics chains)
        public int local_ik_auto_play_lock_count;

        // Index to the IK lock data
        public int local_ik_auto_play_lock_offset;

        // Mass of the model in kilograms
        public float mass;

        // Flags indicating contents, defined in bspflags.h
        public int contents;

        // Count of included models for reusing sequences and animations
        public int include_model_count;

        // Index to the included model data
        public int include_model_offset;

        // Virtual model placeholder for mutable-void* (usually a pointer)
        public int virtual_model;

        // Index to the bone table name (used for bone lookup)
        public int bone_table_by_name_offset;

        // Directional light dot product (used for lighting calculations)
        public byte directional_light_dot_product;

        // Preferred level of detail (LOD) for the model
        public byte root_lod;

        // Number of allowed root LODs
        public byte allowed_root_lods_count;

        // Unused byte (likely reserved for future use)
        public byte unused0;

        // zero out if version < 47
        public float fade_distance;

        // Number of flex controller UI entries (used for controlling flex parameters)
        public int flex_controller_ui_count;

        // Index to the flex controller UI entries
        public int flex_controller_ui_offset;

        // Index to the vertex base (for vertex data reference)
        public int vertex_base;

        // Index to the offset base (used for referencing different data segments)
        public int offset_base;

        public int maya_offset;

        public int source_bone_transform_count;

        public int source_bone_transform_offset;

        public int illum_position_attachment_offset;

        public int linear_bone_offset;

        public int bone_flex_driver_count;

        public int bone_flex_driver_offset;

        // High detail collision mesh for static props.
        public int per_tri_collision_offset;

        public int per_tri_count1;

        public int per_tri_count2;

        public int per_tri_count3;

        // When used the string is normally "Titan", only observed on models for animation.
        public int unk_string_offset;

        public int vtx_offset;

        public int vvd_offset;

        public int vvc_offset;

        public int phy_offset;

        public int vtx_size;

        public int vvd_size;

        public int vvc_size;

        public int phy_size;

        public int unk_section_offset;

        // Only used if PHY has a solid count of "1"
        public int unk_section_count;

        public int unknown01;

        public int unknown_offset_06;

        //Nothing observed past here, however the first in may be a count for unknownOffset06
        public int[] reserved;

    }

    // studiohdr2_t
    public class Secondary_Header {

        public Secondary_Header() {
            unknown = new int[64];
        }

        public int srcbonetransform_count;
        public int srcbonetransform_index;

        public int illumpositionattachmentindex;

        public float flMaxEyeDeflection;    //  If set to 0, then equivalent to cos(30)

        // mstudiolinearbone_t
        public int linearbone_index;

        public int[] unknown;
    }

}
