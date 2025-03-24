using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vec3 {
        public float X;
        public float Y;
        public float Z;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header {
        // Model format ID (e.g., "IDST" for 0x49 0x44 0x53 0x54)
        public int id;

        // Format version number (e.g., 48, represented as 0x30, 0x00, 0x00, 0x00)
        public int version;

        // Checksum for consistency, must match across related files (e.g., .phy, .vtx)
        public int checksum;

        // The internal name of the model, usually the model filename with padding
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public char[] name;

        // Total data size of the model file in bytes
        public int dataLength;

        // Position of the player's viewpoint relative to the model's origin
        public Vec3 eyeposition;

        // Position used for calculating ambient lighting and cubemap reflections
        public Vec3 illumposition;

        // The minimum corner of the model's bounding box (least X/Y/Z values)
        public Vec3 hull_min;

        // The maximum corner of the model's bounding box (opposite corner)
        public Vec3 hull_max;

        // Bounding box used for view culling (minimum coordinates)
        public Vec3 view_bbmin;

        // Bounding box used for view culling (maximum coordinates)
        public Vec3 view_bbmax;

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
        public int hitbox_count;

        // Offset to the first hitbox data section
        public int hitbox_offset;

        // Number of local animations in the model (e.g., idle, run, jump)
        public int localanim_count;

        // Offset to the local animations data section
        public int localanim_offset;

        // Number of local sequences in the model (e.g., movement states)
        public int localseq_count;

        // Offset to the local sequences data section
        public int localseq_offset;

        // Version of the activity list (for animation sequencing)
        public int activity_list_version;

        // Whether events have been indexed in the model
        public int events_indexed;

        // Number of textures used in the model
        public int texture_count;

        // Offset to the texture data section
        public int texture_offset;

        // Number of texture directories (directories storing texture files)
        public int texturedir_count;

        // Offset to the texture directory data section
        public int texturedir_offset;

        // Number of skin references (defines texture slots for skins)
        public int skin_reference_count;

        // Number of skin families (skin variations for the model)
        public int skin_family_count;

        // Index to the skin reference data
        public int skin_reference_index;

        // Number of body parts in the model (e.g., head, torso, legs)
        public int bodypart_count;

        // Offset to the body part data section
        public int bodypart_offset;

        // Number of attachment points (e.g., for props or accessories)
        public int attachment_count;

        // Offset to the attachment data section
        public int attachment_offset;

        // Number of local nodes (used for hierarchical structure)
        public int local_node_count;

        // Index to the local node data
        public int local_node_index;

        // Index to the local node names (null-terminated strings)
        public int local_node_name_index;

        // Number of flex descriptors (for facial animation controls)
        public int flex_desc_count;

        // Index to the flex descriptor data
        public int flex_desc_index;

        // Number of flex controllers (controls facial expression changes)
        public int flex_controller_count;

        // Index to the flex controller data
        public int flex_controller_index;

        // Number of flex rules (how the controllers affect the model)
        public int flex_rules_count;

        // Index to the flex rule data
        public int flex_rules_index;

        // Number of IK chains (inverse kinematics chains for bone movement)
        public int ikchain_count;

        // Index to the IK chain data
        public int ikchain_index;

        // Number of mouth sections for speech animation (can have more than one)
        public int mouths_count;

        // Index to the mouth data (defines sounds for speech animations)
        public int mouths_index;

        // Number of pose parameters (used for facial expressions)
        public int local_poseparm_count;

        // Index to the pose parameter data
        public int local_poseparm_index;

        // Index to the surface property value (like material type or surface interaction)
        public int surface_prop_index;

        // Index to key-value data (used for custom attributes, may reference a texture or other model data)
        public int key_value_index;

        // Count of key-value pairs in the model's key-value data
        public int key_value_count;

        // Number of IK lock data (locks for inverse kinematics chains)
        public int iklock_count;

        // Index to the IK lock data
        public int iklock_index;

        // Mass of the model in kilograms
        public float mass;

        // Flags indicating contents, defined in bspflags.h
        public int contents;

        // Count of included models for reusing sequences and animations
        public int include_model_count;

        // Index to the included model data
        public int include_model_index;

        // Virtual model placeholder for mutable-void* (usually a pointer)
        public int virtual_model;

        // Index to the animation block name data
        public int anim_blocks_name_index;

        // Count of animation blocks in the model
        public int anim_blocks_count;

        // Index to the animation block data
        public int anim_blocks_index;

        // Index to the model's animation block data (another placeholder for mutable-void*)
        public int anim_block_model;

        // Index to the bone table name (used for bone lookup)
        public int bone_table_name_index;

        // Index to the vertex base (for vertex data reference)
        public int vertex_base;

        // Index to the offset base (used for referencing different data segments)
        public int offset_base;

        // Directional light dot product (used for lighting calculations)
        public byte directional_dot_product;

        // Preferred level of detail (LOD) for the model
        public byte root_lod;

        // Number of allowed root LODs
        public byte num_allowed_root_lods;

        // Unused byte (likely reserved for future use)
        public byte unused0;

        // Unused integer (likely reserved for future use)
        public int unused1;

        // Number of flex controller UI entries (used for controlling flex parameters)
        public int flex_controller_ui_count;

        // Index to the flex controller UI entries
        public int flex_controller_ui_index;

        // Scaling factor for vertex animation fixed point representation
        public float vert_anim_fixed_point_scale;

        // Unused integer (likely reserved for future use)
        public int unused2;

        // Index to the second studio header data
        public int studio_hdr_2_index;

        // Unused integer (likely reserved for future use)
        public int unused3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Secondary_Header {
        public int srcbonetransform_count;
        public int srcbonetransform_index;

        public int illumpositionattachmentindex;

        public float flMaxEyeDeflection;    //  If set to 0, then equivalent to cos(30)

        // mstudiolinearbone_t
        public int linearbone_index;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public int unknown;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Texture {
        // Number of bytes past the beginning of this structure
        // where the first character of the texture name can be found.
        public int name_offset; // Offset for null-terminated string
        public int flags;

        public int used;        // Padding?
        public int unused;      // Padding.

        public int material;        // Placeholder for IMaterial
        public int client_material; // Placeholder for void*

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public int unused2; // Final padding
        // Struct is 64 bytes long
    }

}
