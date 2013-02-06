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
                var phgrs = from sent in p.Sentences
                            from r in sent.Phrases
                            select r;

                foreach (var r in phgrs) {
                    Console.WriteLine(r);
                }
                var wordstack = new Stack<Word>();
            }
            var POSCounts = from W in document.Words
                            group W by new
                            {
                                Type = W.GetType(),
                                W.Text,
                            };


            foreach (var group in POSCounts) {
                Console.WriteLine("Type: {0} : {1}:", group.Key.Type, group.Key.Text, group.Count());
            }
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
            }
        }




        void ThesaurusCMDLineTest() {

        }
    }
}
