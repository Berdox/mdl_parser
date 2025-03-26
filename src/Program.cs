
using mdl_parser.src.parser;
using mdl_parser.src.structs;
using mdl_parser.src.utilities;
using System.Reflection.PortableExecutable;

public class Program {
    public static void Main() {
        FileInfo file = new FileInfo("G:\\code\\C#\\mdl_parser\\test_data\\crane_wallrun.mdl");
        Parser par = new Parser(file);
        par.ReadMDLFile();
    }
}