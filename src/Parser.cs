using System.Runtime.InteropServices;
using mdl_parser.src.logging;
using mdl_parser.src.structs;
using mdl_parser.src.utilities;
using mdl_parser.src.structs.bones;

namespace mdl_parser.src.parser
{
    public class Parser
    {
        public mdlStructsData mdlData;
        public BinaryReader reader;
        private FileStream fileStream;

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
            Logger.Info("MDL Parser started");
            ReadHeader();
            ReadMayaString();

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

        public string GetStringAtOffset(long startOffset, long offset) {
            string aString = "";

            if (offset > 0) {
                // Save the current file position
                long inputFileStreamPosition = reader.BaseStream.Position;

                // Seek to the offset
                reader.BaseStream.Seek(startOffset + offset, SeekOrigin.Begin);
                long fileOffsetStart = reader.BaseStream.Position;

                // Read a null-terminated string
                aString = ReadNullTerminatedString();

                // Get the end offset
                long fileOffsetEnd = reader.BaseStream.Position - 1;

                // Restore the original file position
                reader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
            }

            return aString;
        }

        private string ReadNullTerminatedString() {
            List<char> chars = new List<char>();
            char ch;

            while ((ch = reader.ReadChar()) != '\0')  // Read until null terminator
            {
                chars.Add(ch);
            }

            return new string(chars.ToArray());
        }



        public void ReadHeader() {
            try {
                if(reader  == null) {
                    throw new InvalidOperationException("BinaryReader is not initialized.");
                }

                long fileSize = fileStream.Length;
                int structSize = Marshal.SizeOf<Header>();

                if (fileSize < structSize) {
                    Console.WriteLine("The file is too small to contain the Header struct.");
                    return;
                }
                long fileOffsetStart = 0;
                long fileOffsetEnd = 0;

                // Sets the filestream to the beginning of the file
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                fileOffsetStart = reader.BaseStream.Position;
                    
                mdlData.header.id = reader.ReadChars(4);

                mdlData.header.version = reader.ReadInt32();

                mdlData.header.checksum = reader.ReadInt32();

                mdlData.header.name_offset = reader.ReadInt32();

                mdlData.header.name_by_offset = GetStringAtOffset(0, mdlData.header.name_offset);
                mdlData.header.name = reader.ReadChars(64);

                if(mdlData.header.name_by_offset == "") {
                    mdlData.header.model_name = new String(mdlData.header.name);
                } else {
                    mdlData.header.model_name = mdlData.header.name_by_offset;
                }

                mdlData.header.dataLength = reader.ReadInt32();
                mdlData.header.actual_file_length = reader.BaseStream.Length;

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

                //FROM: StudioMdl for MDL48 and MDL49
                //#define MAXSTUDIOHITBOXSETNAME 64
                if (mdlData.header.hitbox_set_count > 64) {
                    mdlData.header.hitbox_set_count = 64;
                }

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

                //'FROM: StudioMdl for MDL48 and MDL49
                //'EXTERN char *g_xnodename[100];
                //EXTERN Int g_xnode[100][100];
                if (mdlData.header.local_node_count > 100) {
                    mdlData.header.local_node_count = 100;
                }

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

                //mdlData.header.surface_prop_name = new String(reader.ReadChars(mdlData.header.bone_offset - mdlData.header.maya_offset));
               // Me.theMdlFileData.theSurfacePropName = Me.GetStringAtOffset(0, Me.theMdlFileData.surfacePropOffset, "theSurfacePropName")
                //mdlData.header.surface_prop_name = reader.ReadInt32();

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

        public void ReadMayaString() {
            if(mdlData.header.maya_offset > 0 && mdlData.header.maya_offset < mdlData.header.bone_offset) {
                reader.BaseStream.Seek(mdlData.header.maya_offset, SeekOrigin.Begin);
                mdlData.mayaStrings = new String(reader.ReadChars(mdlData.header.bone_offset - mdlData.header.maya_offset));
                Console.WriteLine("mayaStrings");
                Console.WriteLine(mdlData.mayaStrings);
            }
            Console.WriteLine("mayaStrings: empty");
            Console.WriteLine(mdlData.mayaStrings);
        }

        public void ReadBones() {
            if (mdlData.header.bone_count > 0) {
                long boneInputFileStreamPosition;
                long inputFileStreamPosition;
                long fileOffsetStart;
                long fileOffsetEnd;
                long fileOffsetStart2;
                long fileOffsetEnd2;

                try {
                    reader.BaseStream.Seek(mdlData.header.bone_offset, SeekOrigin.Begin);
                    //fileOffsetStart = this.theInputFileReader.BaseStream.Position;

                    //this.theMdlFileData.theBones = new List<SourceMdlBone53>(this.theMdlFileData.boneCount);
                    //for (int i = 0; i < this.theMdlFileData.boneCount; i++) {
                    //    boneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
                    //    BoneStruct aBone = new BoneStruct();

                    //    aBone.nameOffset = this.theInputFileReader.ReadInt32();
                    //    aBone.parentBoneIndex = this.theInputFileReader.ReadInt32();

                    //    for (int j = 0; j < aBone.boneControllerIndex.Length; j++) {
                    //        aBone.boneControllerIndex[j] = this.theInputFileReader.ReadInt32();
                    //    }

                    //    aBone.position = new SourceVector();
                    //    aBone.position.x = this.theInputFileReader.ReadSingle();
                    //    aBone.position.y = this.theInputFileReader.ReadSingle();
                    //    aBone.position.z = this.theInputFileReader.ReadSingle();

                    //    aBone.quat = new SourceQuaternion();
                    //    aBone.quat.x = this.theInputFileReader.ReadSingle();
                    //    aBone.quat.y = this.theInputFileReader.ReadSingle();
                    //    aBone.quat.z = this.theInputFileReader.ReadSingle();
                    //    aBone.quat.w = this.theInputFileReader.ReadSingle();

                    //    aBone.rotation = new SourceVector();
                    //    aBone.rotation.x = this.theInputFileReader.ReadSingle();
                    //    aBone.rotation.y = this.theInputFileReader.ReadSingle();
                    //    aBone.rotation.z = this.theInputFileReader.ReadSingle();

                    //    aBone.unkVector = new SourceVector();
                    //    aBone.unkVector.x = this.theInputFileReader.ReadSingle();
                    //    aBone.unkVector.y = this.theInputFileReader.ReadSingle();
                    //    aBone.unkVector.z = this.theInputFileReader.ReadSingle();

                    //    aBone.positionScale = new SourceVector();
                    //    aBone.positionScale.x = this.theInputFileReader.ReadSingle();
                    //    aBone.positionScale.y = this.theInputFileReader.ReadSingle();
                    //    aBone.positionScale.z = this.theInputFileReader.ReadSingle();

                    //    aBone.rotationScale = new SourceVector();
                    //    aBone.rotationScale.x = this.theInputFileReader.ReadSingle();
                    //    aBone.rotationScale.y = this.theInputFileReader.ReadSingle();
                    //    aBone.rotationScale.z = this.theInputFileReader.ReadSingle();

                    //    aBone.unkVector1 = new SourceVector();
                    //    aBone.unkVector1.x = this.theInputFileReader.ReadSingle();
                    //    aBone.unkVector1.y = this.theInputFileReader.ReadSingle();
                    //    aBone.unkVector1.z = this.theInputFileReader.ReadSingle();

                    //    aBone.poseToBoneColumn0 = new SourceVector();
                    //    aBone.poseToBoneColumn1 = new SourceVector();
                    //    aBone.poseToBoneColumn2 = new SourceVector();
                    //    aBone.poseToBoneColumn3 = new SourceVector();

                    //    aBone.poseToBoneColumn0.x = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn1.x = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn2.x = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn3.x = this.theInputFileReader.ReadSingle();

                    //    aBone.poseToBoneColumn0.y = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn1.y = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn2.y = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn3.y = this.theInputFileReader.ReadSingle();

                    //    aBone.poseToBoneColumn0.z = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn1.z = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn2.z = this.theInputFileReader.ReadSingle();
                    //    aBone.poseToBoneColumn3.z = this.theInputFileReader.ReadSingle();

                    //    aBone.qAlignment = new SourceQuaternion();
                    //    aBone.qAlignment.x = this.theInputFileReader.ReadSingle();
                    //    aBone.qAlignment.y = this.theInputFileReader.ReadSingle();
                    //    aBone.qAlignment.z = this.theInputFileReader.ReadSingle();
                    //    aBone.qAlignment.w = this.theInputFileReader.ReadSingle();

                    //    aBone.flags = this.theInputFileReader.ReadInt32();
                    //    aBone.proceduralRuleType = this.theInputFileReader.ReadInt32();
                    //    aBone.proceduralRuleOffset = this.theInputFileReader.ReadInt32();
                    //    aBone.physicsBoneIndex = this.theInputFileReader.ReadInt32();
                    //    aBone.surfacePropNameOffset = this.theInputFileReader.ReadInt32();
                    //    aBone.contents = this.theInputFileReader.ReadInt32();
                    //    aBone.surfacepropLookup = this.theInputFileReader.ReadInt32();
                    //    aBone.unk1 = this.theInputFileReader.ReadInt32();

                    //    for (int k = 0; k < 7; k++) {
                    //        aBone.unused[k] = this.theInputFileReader.ReadInt32();
                    //    }

                    //    this.theMdlFileData.theBones.Add(aBone);

                    //    inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
                    //    aBone.theName = GetStringAtOffset(boneInputFileStreamPosition, aBone.nameOffset, "aBone.theName");

                    //    if (aBone.proceduralRuleOffset != 0) {
                    //        if (aBone.proceduralRuleType == SourceMdlBone53.STUDIO_PROC_AXISINTERP)
                    //            ReadAxisInterpBone(boneInputFileStreamPosition, aBone);
                    //        else if (aBone.proceduralRuleType == SourceMdlBone53.STUDIO_PROC_QUATINTERP)
                    //            ReadQuatInterpBone(boneInputFileStreamPosition, aBone);
                    //        else if (aBone.proceduralRuleType == SourceMdlBone53.STUDIO_PROC_JIGGLE)
                    //            ReadJiggleBone(boneInputFileStreamPosition, aBone);
                    //    }

                    //    this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
                    //}

                    //fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
                    //this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, $"theMdlFileData.theBones {this.theMdlFileData.theBones.Count}");
                }
                catch (Exception ex) {
                    int debug = 4242;
                }
            }
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
