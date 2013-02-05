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

namespace Aluan_Experimentation
{
    class Program
    {
        static void Main(string[] args) {

            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Aluan\Desktop\intest1.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).GetParagraphs();
            var document = new Document(paragraphs);

            var para2 = from p in document.Paragraphs
                        select p;
            foreach (var p in para2) {
                var sents = from sent in p.Sentences
                            select sent;

                foreach (var s in sents) {
                    Console.WriteLine(s);
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
