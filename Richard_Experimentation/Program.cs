using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using SharpNLPTaggingModule;

namespace Richard_Experimentation
{
    class Program
    { static void Main(string[] args) {
            
            TagExampleFile();

            //Keeps the console window open until the escape key is pressed
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }
        }


        static void TagExampleFile() {

                                                                            //Put your windows account name here
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Richard\Desktop\intest1.txt");
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

            }
         }
    }
}
