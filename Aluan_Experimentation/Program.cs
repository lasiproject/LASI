using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using SharpNLPTaggingModule;
using System.IO;
using WebChart;
namespace Aluan_Experimentation
{
    class Program
    {
        static void Main(string[] args) {

            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Aluan\Desktop\intest1.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).GetParagraphs();
            var doc = new Document(paragraphs);

            

            foreach (var P in doc.Praragraphs) {
                var words = (from S in P.Sentences
                             from W in S.Phrases
                             select W).ToList();
                foreach (var w in words) {
                    Console.WriteLine(w);
                }
            }
         
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
            }
        }




        void ThesaurusCMDLineTest() {
            //var verbThesaurus = new VerbThesaurus();
            //verbThesaurus.Load();
            //while (true) {
            //    Console.Write("enter a word: ");
            //    var input = Console.ReadLine().Trim();
            //    if (input == "-1")
            //        break;
            //    foreach (var S in verbThesaurus[input])
            //        Console.WriteLine(S);}
        }
    }
}
