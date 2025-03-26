using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using mdl_parser.src.structs;
using mdl_parser.src.utilities;

namespace mdl_parser.src.parser
{
    public class Parser
    {
        public mdlStructsData mdlData;
        BinaryReader reader;
        FileStream fileStream;

        public Parser(FileInfo file) {
            mdlData = new mdlStructsData();
            fileStream = file.OpenRead();  
            reader = new BinaryReader(fileStream);
        }

        public void Dispose() {
            reader?.Dispose();
            fileStream?.Dispose();
        }

        public void ReadMDLFile() {
            //OpenFile(file);
            HeaderFromFile();
            MayaStringFromFile();

            // writing to file
            WriteHeaderToFile();

            // printing Hex
            PrintHex();
        }

        public void OpenFile(FileInfo file) {
            if (file == null || !file.Exists) {
                throw new FileNotFoundException("File not found.", file?.FullName);
            }

            // Open FileStream and initialize BinaryReader
            fileStream = file.OpenRead();
            reader = new BinaryReader(fileStream);
        }

        public T ReadStructFromFile<T>(FileInfo file, long offset) where T : struct {
            int size = Marshal.SizeOf<T>();

            using (FileStream fs = file.OpenRead())
            using (BinaryReader reader = new BinaryReader(fs)) {
                // Check if the file is large enough for the struct after considering the offset
                if (fs.Length < offset + size)
                    throw new Exception("File is too small to contain the expected structure at the given offset.");

                // Move the reader's position to the given offset
                fs.Seek(offset, SeekOrigin.Begin);

                // Read the bytes from the given offset
                byte[] buffer = reader.ReadBytes(size);

                // Allocate unmanaged memory to hold the struct
                IntPtr ptr = Marshal.AllocHGlobal(size);
                try {
                    Marshal.Copy(buffer, 0, ptr, size);
                    return Marshal.PtrToStructure<T>(ptr);
                }
                finally {
                    Marshal.FreeHGlobal(ptr);
                }
            }
        }

        public void HeaderFromFile() {
            try {
                //using (FileStream fs = file.OpenRead())
                //using (BinaryReader reader = new BinaryReader(fs)) {

                    if(reader  == null) {
                        throw new InvalidOperationException("BinaryReader is not initialized.");
                    }

                    long fileSize = fileStream.Length;
                    int structSize = Marshal.SizeOf<Header>();

                    if (fileSize < structSize) {
                        Console.WriteLine("The file is too small to contain the Header struct.");
                        return;
                    }

                    // Sets the filestream to the beginning of the file
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);

                    mdlData.header.id = reader.ReadInt32();

                    mdlData.header.version = reader.ReadInt32();

                    mdlData.header.checksum = reader.ReadInt32();

                    mdlData.header.name_offset = reader.ReadInt32();
                    mdlData.header.name = reader.ReadChars(64);

                    mdlData.header.dataLength = reader.ReadInt32();

                    mdlData.header.eyeposition = new Vec3 { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() };

                    mdlData.header.illumposition = new Vec3 { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() };

                    mdlData.header.hull_min = new Vec3 { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() };
                    mdlData.header.hull_max = new Vec3 { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() };

                    mdlData.header.view_bounding_box_min = new Vec3 { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() };
                    mdlData.header.view_bounding_box_max = new Vec3 { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() };

                    mdlData.header.flags = reader.ReadInt32();

                    mdlData.header.bone_count = reader.ReadInt32();
                    mdlData.header.bone_offset = reader.ReadInt32();

                    mdlData.header.bone_controller_count = reader.ReadInt32();
                    mdlData.header.bone_controller_offset = reader.ReadInt32();

                    mdlData.header.hitbox_set_count = reader.ReadInt32();
                    mdlData.header.hitbox_set_offset = reader.ReadInt32();

                    mdlData.header.local_anim_count = reader.ReadInt32();
                    mdlData.header.local_anim_offset = reader.ReadInt32();

                    mdlData.header.local_seq_count = reader.ReadInt32();
                    mdlData.header.local_seq_offset = reader.ReadInt32();

                    mdlData.header.activity_list_version = reader.ReadInt32();

                    mdlData.header.events_indexed = reader.ReadInt32();

                    mdlData.header.texture_count = reader.ReadInt32();
                    mdlData.header.texture_offset = reader.ReadInt32();

                    mdlData.header.texture_path_count = reader.ReadInt32();
                    mdlData.header.texture_path_offset = reader.ReadInt32();

                    mdlData.header.skin_reference_count = reader.ReadInt32();
                    mdlData.header.skin_family_count = reader.ReadInt32();
                    mdlData.header.skin_family_offset = reader.ReadInt32();

                    mdlData.header.body_part_count = reader.ReadInt32();
                    mdlData.header.body_part_offset = reader.ReadInt32();

                    mdlData.header.local_attachment_count = reader.ReadInt32();
                    mdlData.header.local_attachment_offset = reader.ReadInt32();

                    mdlData.header.local_node_count = reader.ReadInt32();
                    mdlData.header.local_node_offset = reader.ReadInt32();
                    mdlData.header.local_node_name_offset = reader.ReadInt32();

                    mdlData.header.flex_desc_count = reader.ReadInt32();
                    mdlData.header.flex_desc_offset = reader.ReadInt32();

                    mdlData.header.flex_controller_count = reader.ReadInt32();
                    mdlData.header.flex_controller_offset = reader.ReadInt32();

                    mdlData.header.flex_rules_count = reader.ReadInt32();
                    mdlData.header.flex_rules_offset = reader.ReadInt32();

                    mdlData.header.ikchain_count = reader.ReadInt32();
                    mdlData.header.ikchain_offset = reader.ReadInt32();

                    mdlData.header.rui_count = reader.ReadInt32();
                    mdlData.header.rui_offset = reader.ReadInt32();

                    mdlData.header.local_pose_parm_count = reader.ReadInt32();
                    mdlData.header.local_pose_parm_offset = reader.ReadInt32();

                    mdlData.header.surface_prop_offset = reader.ReadInt32();
                    mdlData.header.surface_prop_name = reader.ReadInt32();

                    mdlData.header.key_value_offset = reader.ReadInt32();
                    mdlData.header.key_value_count = reader.ReadInt32();

                    mdlData.header.local_ik_auto_play_lock_count = reader.ReadInt32();
                    mdlData.header.local_ik_auto_play_lock_offset = reader.ReadInt32();

                    mdlData.header.mass = reader.ReadSingle();

                    mdlData.header.contents = reader.ReadInt32();

                    mdlData.header.include_model_count = reader.ReadInt32();
                    mdlData.header.include_model_offset = reader.ReadInt32();

                    mdlData.header.virtual_model = reader.ReadInt32();

                    mdlData.header.bone_table_by_name_offset = reader.ReadInt32();

                    mdlData.header.directional_light_dot_product = reader.ReadByte();

                    mdlData.header.root_lod = reader.ReadByte();
                    mdlData.header.allowed_root_lods_count = reader.ReadByte();

                    mdlData.header.unused0 = reader.ReadByte();

                    mdlData.header.fade_distance = reader.ReadSingle();

                    mdlData.header.flex_controller_ui_count = reader.ReadInt32();
                    mdlData.header.flex_controller_ui_offset = reader.ReadInt32();

                    mdlData.header.vertex_base = reader.ReadInt32();
                    mdlData.header.offset_base = reader.ReadInt32();

                    mdlData.header.maya_offset = reader.ReadInt32();

                    mdlData.header.source_bone_transform_count = reader.ReadInt32();
                    mdlData.header.source_bone_transform_offset = reader.ReadInt32();

                    mdlData.header.illum_position_attachment_offset = reader.ReadInt32();

                    mdlData.header.linear_bone_offset = reader.ReadInt32();

                    mdlData.header.bone_flex_driver_count = reader.ReadInt32();
                    mdlData.header.bone_flex_driver_offset = reader.ReadInt32();

                    mdlData.header.per_tri_collision_offset = reader.ReadInt32();
                    mdlData.header.per_tri_count1 = reader.ReadInt32();
                    mdlData.header.per_tri_count2 = reader.ReadInt32();
                    mdlData.header.per_tri_count3 = reader.ReadInt32();

                    mdlData.header.unk_string_offset = reader.ReadInt32();

                    mdlData.header.vtx_offset = reader.ReadInt32();
                    mdlData.header.vvd_offset = reader.ReadInt32();
                    mdlData.header.vvc_offset = reader.ReadInt32();
                    mdlData.header.phy_offset = reader.ReadInt32();

                    mdlData.header.vtx_size = reader.ReadInt32();
                    mdlData.header.vvd_size = reader.ReadInt32();
                    mdlData.header.vvc_size = reader.ReadInt32();
                    mdlData.header.phy_size = reader.ReadInt32();

                    mdlData.header.unk_section_offset = reader.ReadInt32();
                    mdlData.header.unk_section_count = reader.ReadInt32();

                    mdlData.header.unknown01 = reader.ReadInt32();
                    mdlData.header.unknown_offset_06 = reader.ReadInt32();

                    // Read reserved array
                    mdlData.header.reserved = new int[59];
                    for (int i = 0; i < 59; i++) {
                        mdlData.header.reserved[i] = reader.ReadInt32();
                    }

                    Console.WriteLine("Header successfully read.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void MayaStringFromFile() {
            if(mdlData.header.maya_offset > 0 && mdlData.header.maya_offset < mdlData.header.bone_offset) {
                reader.BaseStream.Seek(mdlData.header.maya_offset, SeekOrigin.Begin);
                mdlData.mayaStrings = new String(reader.ReadChars(mdlData.header.bone_offset - mdlData.header.maya_offset));
                Console.WriteLine("mayaStrings");
                Console.WriteLine(mdlData.mayaStrings);
            }
            Console.WriteLine("mayaStrings: empty");
            Console.WriteLine(mdlData.mayaStrings);
            //    If Me.theMdlFileData.mayaOffset > 0 And Me.theMdlFileData.mayaOffset<Me.theMdlFileData.boneOffset Then

            //    Dim fileOffsetStart As Long

            //    Dim fileOffsetEnd As Long


            //    Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.mayaOffset, SeekOrigin.Begin)

            //    fileOffsetStart = Me.theInputFileReader.BaseStream.Position


            //    theMdlFileData.theMayaStrings = Me.theInputFileReader.ReadChars(Me.theMdlFileData.boneOffset - Me.theMdlFileData.mayaOffset)


            //    fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1

            //    Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMayaStrings " + theMdlFileData.theMayaStrings)

            //End If
        }

        public void WriteHeaderToFile() {
            Utilities.WriteHeaderToFile(mdlData.header, "file.bin");
        }

        public void PrintHex() {
            var name = mdlData.header.name_offset;
            ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref mdlData.header.maya_offset, 1));
            Console.WriteLine("Memory (Hex) of {name}: " + BitConverter.ToString(bytes.ToArray()));
        }
    }
}
