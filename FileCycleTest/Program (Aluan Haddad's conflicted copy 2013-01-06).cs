using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
namespace FileCycleTest
{
    class Program
    {
        static void Main(string[] args) {
            var docfile = new DocFile(args[0]);
            DocToDocXConverter converter = new DocToDocXConverter(docfile);
            NewMethod(converter);
            Console.ReadKey();
        }

        private static async Task NewMethod(DocToDocXConverter converter) {
            var docxfile = await converter.ConvertFileAsync();
            //var docxfile = converter.ConvertedFile;
            DocxToTextConverter dxttxt = new DocxToTextConverter(docxfile);
            var textfile = dxttxt.ConvertFile();
            SharpNLPTaggingModule.SharpNLPTagger tagger = new SharpNLPTaggingModule.SharpNLPTagger(TaggingOption.TagAndAggregate, textfile.FullPath);
            Console.WriteLine(tagger.OutputFilePath);
        }
    }
}
