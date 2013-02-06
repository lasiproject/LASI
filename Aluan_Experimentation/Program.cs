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

            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Aluan\Desktop\411writtensummary2.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).GetParagraphs();
            var document = new Document(paragraphs);




            var phgrs = from p in document.Paragraphs
                        from sent in p.Sentences
                        from r in sent.Phrases
                        select r;


            //foreach (var r in phgrs) {
            //    Console.WriteLine(r);
            //}


            var wordPOSCounts = from W in document.Words.AsParallel()
                                group W by new {
                                    Type = W.GetType(),
                                    W.Text,
                                } into G
                                orderby G.Count()
                                select G;

            var phrasePOSCounts = from R in document.Phrases
                                  group R by new {
                                      Type = R.GetType(),
                                      R.Text
                                  } into G
                                  orderby G.Count()
                                  select G;

            foreach (var group in phrasePOSCounts) {
                Console.WriteLine("{0} : {1} count: {2}:", group.Key.Type.Name, group.Key.Text, group.Count());
            }
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
            }
        }




        void ThesaurusCMDLineTest() {

        }
    }
}
