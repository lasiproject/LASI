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
using LASI.Algorithm.Weighting;
using LASI.FileSystem.FileTypes;
namespace Aluan_Experimentation
{
    public class Program
    {
        static void Main(string[] args) {
            ThesaurusCMDLineTest();
            //   ParseAndCreate();


        }
        class Theme
        {
            public IEntity Subject {
                get;
                set;
            }
            public ITransitiveAction Verb {
                get;
                set;
            }
        }
        private static void ParseAndCreate() {

            TaggerUtil.TaggerOption = TaggingOption.NameFind;
            var str = TaggerUtil.TagString(new[]{
                @"Add one plus one."
            });

            print(str);
            TaggerUtil.TaggerOption = TaggingOption.TagAndAggregate;
            str = TaggerUtil.TagString(str);

            print(str);

            var document = TaggerUtil.TaggedToDoc(str);
            foreach (var S in CountByTypeAndText(document).Result) {
                print(S);
            }






            var actions = document.GetActions();

            var themes = from A in actions.WithSubject(
                             S => S.Weights[WeightKind.Individual].RawWeight > 10 &&
                             S.Text == "banana")
                         select new Theme {
                             Verb = A,
                             Subject = A.BoundSubject
                         };











            StdIO.WaitForKey(ConsoleKey.Escape);
        }



        private static async Task<Document> MakeDocumentFromTaggedFile(string filePath) {

            return await Task.Run(async () => await new TaggedFileParser(new TaggedFile(filePath)).LoadDocumentAsync());


        }

        private static async Task<IEnumerable<string>> CountByTypeAndText(Document document) {
            return await Task.Run(() => {
                var phrasePOSCounts = from R in document.Phrases
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

        static void ThesaurusCMDLineTest() {
            var verbLookUp = new LASI.Algorithm.Thesauri.VerbThesaurus(@"..\..\..\..\WordNetThesaurusData\data.verb");
            verbLookUp.Load();

            Console.Write("Enter a Verb:    ");
            var input = Console.ReadLine();
            while (input != "~") {

                try {
                    foreach (var v in verbLookUp[input]) {
                        Console.Write(v + ", ");
                    }
                } catch (KeyNotFoundException) {
                    Console.WriteLine(String.Format("No synonyms recognized for \"{0}\" : as verb", input));
                }
                Console.WriteLine();
                Console.Write("Enter a Verb:    ");
                input = Console.ReadLine();
            }
        }


        static Action<object> print = (o) => Console.WriteLine(o);
        static Action<object> printFile = (o) => {
            using (var writer = new StreamWriter(@"..\..\..\..\Desktop\taggingModeTesting.txt", true)) {
                writer.WriteLine(o);
            }
        };
    }
}