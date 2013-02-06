using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using SharpNLPTaggingModule;

namespace Scott_Experimentation
{
    class Program
    {
        static void Main(string[] args) {
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\TestSentences.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).GetParagraphs();
            var document = new Document(paragraphs);
            
           foreach (var p in document.Paragraphs)
            {
                foreach (var p1 in p.Sentences)
                {
                    Console.WriteLine(p1);
                }
            }

            /*var para2 = from p in document.Paragraphs select p;
             foreach (var p in para2) {
                 var sents = from sent in p.Sentences select sent;

                 foreach (var s in sents){
                     Console.WriteLine(s);
                 }
             }*/
           
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }
        }
    }
}
