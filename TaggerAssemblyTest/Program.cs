using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TaggerAssemblyTest
{
    class Program
    {
        static void Main(string[] args) {

            LASI.FileSystem.FileManager.Initialize("test1");
            Console.WriteLine("enter Source path: ");
            var inputFile = @"C:\Users\Aluan\Desktop\random fema data.txt";
            var tagger = new SharpNLPTaggingModule.SharpNLPTagger(TaggingOption.TagAndAggregate, inputFile);
            tagger.ProcessFile();
            var outputFile = tagger.OutputFilePath;

        }


    }
}
