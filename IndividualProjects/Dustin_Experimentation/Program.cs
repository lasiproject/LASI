using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Utilities;
using SharpNLPTaggingModule;
using System.IO;
using LASI.Algorithm.DocumentConstructs;
namespace Dustin_Experimentation
{ //this is a comment 
    class Program
    {
        static void Main(string[] args) {
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Dustin\Downloads\example.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).LoadParagraphs();
            var document = new Document(paragraphs);
            //StreamWriter file = new StreamWriter(@"C:\Users\Dustin\Downloads\411output.txt");

            foreach (var i in document.Sentences)
            {
                foreach (var j in i.Words)
                {
                    Console.WriteLine(j);
                }
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey())
            {
                Console.WriteLine("Press escape to exit");
            }
        }
    }
}
