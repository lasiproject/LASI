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

namespace Aluan_Experimentation
{
    public class Program
    {
        static void Main(string[] args) {
            //ThesaurusCMDLineTest();


            //TestTaggerHelper();
            var categoryResults = ParseThreaded();

            foreach (var item in categoryResults) {
                Console.WriteLine(item);
            }
            StdIO.WaitForKey(ConsoleKey.Escape);


        }

        private static void TestTaggerHelper() {
            var simpleTag = new StringTagger(TaggingOption.TagAndAggregate);
            var result = simpleTag.TagString("Hello I am Working a linguistic analysis project with 5 other people");
            print(result);
        }

        public static IEnumerable<string> ParseThreaded() {

            return (from p in pathes.AsParallel()

                    let counts = (CountByTypeAndText(MakeDocumentFromTaggedFile(p).Result)).Result
                    from s in counts

                    select s);

        }



        private static async Task<Document> MakeDocumentFromTaggedFile(string filePath) {

            return await Task.Run(async () => await new TaggedFileParser(filePath).GetDocumentAsync());


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
    }
}