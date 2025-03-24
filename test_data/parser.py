import struct

def read_header(filename, output_file):
    with open(filename, "rb") as f:
        data = f.read()
    
    header_format = "4s I I 64s I 9I I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I 2I B B B B I I I I I I B B B B I I I I I I"
    
    values = struct.unpack(header_format, data[:struct.calcsize(header_format)])
    
    keys = [
        "id", "version", "checksum", "name", "dataLength", "eyeposition_x", "eyeposition_y", "eyeposition_z",
        "illumposition_x", "illumposition_y", "illumposition_z", "hull_min_x", "hull_min_y", "hull_min_z",
        "hull_max_x", "hull_max_y", "hull_max_z", "view_bbmin_x", "view_bbmin_y", "view_bbmin_z",
        "view_bbmax_x", "view_bbmax_y", "view_bbmax_z", "flags", "bone_count", "bone_offset",
        "bone_controller_count", "bone_controller_offset", "hitbox_count", "hitbox_offset", "localanim_count",
        "localanim_offset", "localseq_count", "localseq_offset", "activity_list_version", "events_indexed",
        "texture_count", "texture_offset", "texturedir_count", "texturedir_offset", "skin_reference_count",
        "skin_family_count", "skin_reference_index", "bodypart_count", "bodypart_offset", "attachment_count",
        "attachment_offset", "local_node_count", "local_node_index", "local_node_name_index", "flex_desc_count",
        "flex_desc_index", "flex_controller_count", "flex_controller_index", "flex_rules_count", "flex_rules_index",
        "ikchain_count", "ikchain_index", "mouths_count", "mouths_index", "local_poseparm_count", "local_poseparm_index",
        "surface_prop_index", "key_value_index", "key_value_count", "iklock_count", "iklock_index", "mass",
        "contents", "include_model_count", "include_model_index", "virtual_model", "anim_blocks_name_index",
        "anim_blocks_count", "anim_blocks_index", "anim_block_model", "bone_table_name_index", "vertex_base",
        "offset_base", "directional_dot_product", "root_lod", "num_allowed_root_lods", "unused0", "unused1",
        "flex_controller_ui_count", "flex_controller_ui_index", "vert_anim_fixed_point_scale", "unused2",
        "studio_hdr_2_index", "unused3"
    ]
    
    with open(output_file, "w") as out:
        for key, value in zip(keys, values):
            if isinstance(value, bytes):
                value = " ".join(f"{b:02X}" for b in value)
            elif isinstance(value, float):
                hex_value = struct.unpack('<I', struct.pack('<f', value))[0]
                value = " ".join(f"{(hex_value >> (i * 8)) & 0xFF:02X}" for i in range(4))
            else:  # Ensure all integers are stored as 4 bytes (32-bit little-endian)
                value = " ".join(f"{(value >> (i * 8)) & 0xFF:02X}" for i in range(4))
            out.write(f"{key}: {value}\n")

# Example usage
read_header("planet_blue_sun.mdl", "header_output4.txt")
