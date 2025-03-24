
using mdl_parser.src.parser;
using mdl_parser.src.structs;
using mdl_parser.src.utilities;
using System.Reflection.PortableExecutable;

public class Program {
    public static void Main() {
        Parser par = new Parser();
        FileInfo file = new FileInfo("G:\\code\\C#\\mdl_parser\\test_data\\planet_blue_sun.mdl");
        //par.read_file(file);
        par.header = par.ReadStructFromFile<Header>(file, 0);
        par.WriteHeaderToFile();
        //  Utilities.WriteHeaderToFile(header, "file.bin");
    }
}