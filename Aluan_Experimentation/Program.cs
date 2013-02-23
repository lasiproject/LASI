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
            var str = TaggerUtil.TaggedStringFromString(new[]{@"Hello there! How are you? I am Working on a 
                                        linguistic analysis project with a skilled, 
                                        professional team of 6 people. They are Brittany, Dustin, Richard, Scott, and Erik. We all work at ODU."
            });
            TaggerUtil.TaggerOption = TaggingOption.TagAndAggregate;
            str = TaggerUtil.TaggedStringFromString(str);
            printfl(str);
            //TaggerUtil.TaggerOption = TaggingOption.ExperimentalClauseNesting;
            //printfl(TaggerUtil.TaggedStringFromString(str));
            //foreach (var p in doc.Phrases) {
            //    print(p);

            //}
            //var document2 = MakeDocumentFromTaggedFile(pathes[0]).Result;
            //foreach (var p in document2.Phrases)
            //    print(p);


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
                var phrasePOSCounts = from R in document.Phrases.AsParallel()
                                      group R by new
                                      {
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
        static Action<object> printfl = (o) => {
            using (var writer = new StreamWriter(@"C:\Users\Aluan\Desktop\taggingModeTesting.txt", true)) {
                writer.WriteLine(o);
            }
        };
    }
}