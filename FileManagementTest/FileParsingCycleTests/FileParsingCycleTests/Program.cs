using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.FileSystem;
using System.Diagnostics;
namespace FileParsingCycleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            LegacyMSWordDoc docInput = new LegacyMSWordDoc(@"C:\Users\Aluan\Desktop\411writtensummary2.doc");
            DocToDocXConverter converter = new DocToDocXConverter(docInput);
            ModernMSWordDoc docxOutput = converter.ConvertDocument();
            Console.WriteLine("Converted    {0} \nTo   {1}", docInput.FInfo.FileNameWithExt, docxOutput.FInfo.FileNameWithExt);
            DocxToTextConverter textConverter = new DocxToTextConverter(docxOutput);
            textConverter.Convert();
            AsciiTextFile extracted = new AsciiTextFile(textConverter.DestinationInfo.FullPathAndExt);


        }
    }
}
