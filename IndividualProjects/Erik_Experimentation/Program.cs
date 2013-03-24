using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using SharpNLPTaggingModule;
using System.IO;
using System.Xml;
using LASI.FileSystem.FileTypes;
using LASI.Algorithm.Binding;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Algorithm.Weighting;
using LASI.Algorithm.Thesuari;


namespace Erik_Experimentation
{
    class Program
    {
       
        static void Main(string[] args)
        {

                   //var nounTest = new LASI.Algorithm.Thesuari.NounThesaurus(@"..\..\..\..\WordNetThesaurusData\data.noun");
                   //nounTest.Load();
                   //string key = "walk";
                   //nounTest.SearchFor(key);




                    //Keeps the console window open until the escape key is pressed
                    //Console.WriteLine("Press escape to exit");
                    //for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                        //Console.WriteLine("Press escape to exit");
                    //}

            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\CynosureEPR\Desktop\test.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).LoadParagraphs();
            var document = new Document(paragraphs);
            StreamWriter file = new StreamWriter(@"C:\Users\CynosureEPR\Desktop\output.txt");

            foreach (var i in document.Phrases)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey())
            {
                Console.WriteLine("Press escape to exit");
            }
        }
    }
}
