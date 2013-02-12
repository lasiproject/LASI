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


namespace Aluan_Experimentation
{
    class Program
    {
        static void Main(string[] args) {

            StdIoUtil.WaitForKey(ConsoleKey.Escape);
        }

        private static void TestingDocParser() {
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Aluan\Desktop\411writtensummary2.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).GetParagraphs();
            var document = new Document(paragraphs);

            var phgrs = from p in document.Paragraphs
                        from sent in p.Sentences
                        from r in sent.Phrases
                        select r;

            var wordPOSCounts = from W in document.Words.AsParallel()
                                group W by new
                                {
                                    Type = W.GetType(),
                                    W.Text,
                                } into G
                                orderby G.Count()
                                select G;

            var phrasePOSCounts = from R in document.Phrases
                                  group R by new
                                  {
                                      Type = R.GetType(),
                                      R.Text
                                  } into G
                                  orderby G.Count()
                                  select G;

            foreach (var group in phrasePOSCounts) {
                Console.WriteLine("{0} : {1} count: {2}:", group.Key.Type.Name, group.Key.Text, group.Count());
            }

            Func<string, string> f = s => s.ToUpper();
            Func<string, string> g = s => s.Substring(0, 4);
            Func<string, string> h = s => s.ToLower();

            var MF = f.Compose(g, h, f);
            foreach (var S in new[] { 
                "Hello there!", 
                "How are you?", 
                "Would you like some cheese with that wine?" }) {
                Console.WriteLine(MF(S));
            }
        }

        static string UpperCaseString(string str) {
            return str.ToUpper();
        }

        static string Truncate(string str) {
            return str.Substring(0, 4);
        }

        void ThesaurusCMDLineTest() {

        }
    }
}
