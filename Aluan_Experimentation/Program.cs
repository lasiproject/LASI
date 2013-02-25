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
using LASI.Algorithm.Heuristics;
using System.Xml;
namespace Aluan_Experimentation
{
    public class Program
    {
        static void Main(string[] args) {
            //ThesaurusCMDLineTest();

            TaggerUtil.TaggerOption = TaggingOption.NameFind;
            var str = TaggerUtil.TagString(new[]{
                @"Hello there!",
                "How are you?",
                "I am working on a linguistic analysis project with a skilled, professional team of 6 people.",
                "They are Brittany, Dustin, Richard, Scott, and Erik.",
                "We all work together here at Dominion.",
                "Dustin is working on determing the relationships between nouns and verbs.",
            "Brittany is working on the user interface.",
            });

            print(str);
            TaggerUtil.TaggerOption = TaggingOption.TagAndAggregate;
            str = TaggerUtil.TagString(str);

            printFile(str);

            var document = TaggerUtil.TaggedToDoc(str);
            foreach (var S in CountByTypeAndText(document).Result) {
                print(S);
            }
            StdIO.WaitForKey(ConsoleKey.Escape);


        }


        public static IEnumerable<string> ParseThreaded() {

            return (from p in pathes.AsParallel()

                    let counts = (CountByTypeAndText(MakeDocumentFromTaggedFile(p).Result)).Result
                    from s in counts

                    select s);

        }



        private static async Task<Document> MakeDocumentFromTaggedFile(string filePath) {

            return await Task.Run(async () => await new TaggedFileParser(new TaggedFile(filePath)).LoadDocumentAsync());


        }

        private static async Task<IEnumerable<string>> CountByTypeAndText(Document document) {
            return await Task.Run(() => {
                var phrasePOSCounts = from R in document.Phrases
                                      group R by new {
                                          Type = R.GetType(),
                                          R.Text
                                      } into G
                                      orderby G.Count()
                                      select G;
                return from g in phrasePOSCounts
                       select String.Format("{0} : \"{1}\"; with count: {2}:", g.Key.Type.Name, g.Key.Text, g.Count());


            });
        }

        void ThesaurusCMDLineTest() {
            var verbLookUp = new VerbThesaurus();
            verbLookUp.Load();
            Console.Write("Enter a Verb:    ");
            var input = Console.ReadLine();
            while (input != "~") {


                foreach (var v in verbLookUp[input]) {
                    Console.Write(v + ", ");
                }
                Console.WriteLine();
                Console.Write("Enter a Verb:    ");
                input = Console.ReadLine();
            }
        }
        private static string[] pathes = new[]{

 @"C:\Users\Aluan\Desktop\411writtensummary.tagged",

            
          };

        static Action<object> print = (o) => Console.WriteLine(o);
        static Action<object> printFile = (o) => {
            using (var writer = new StreamWriter(@"C:\Users\Aluan\Desktop\taggingModeTesting.txt", true)) {
                writer.WriteLine(o);
            }
        };
    }
}