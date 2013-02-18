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
    class Program
    {
        static void Main(string[] args) {
            ThesaurusCMDLineTest();
            //ParseThreaded();
            StdIO.WaitForKey(ConsoleKey.Escape);
        }

        private static void ParseThreaded() {

            var tasks = (from p in pathes.AsParallel()
                         select CountByTypeAndText(MakeDocumentFromTaggedFile(p).Result));
            foreach (var t in tasks)
                t.Wait();
        }

        private static async Task<Document> MakeDocumentFromTaggedFile(string filePath) {

            return await Task.Run(async () => await new TaggedFileParser(filePath).GetDocumentAsync());


        }

        private static async Task CountByTypeAndText(Document document) {
            await Task.Run(() => {
                var phrasePOSCounts = from R in document.Phrases.AsParallel()
                                      // where !(R is IPrepositional || R is IConjunctive || R is ParticlePhrase || R is IEntityReferencer)
                                      group R by new
                                      {
                                          Type = R.GetType(),
                                          R.Text
                                      } into G
                                      orderby G.Count()
                                      select G;
                foreach (var group in phrasePOSCounts) {
                    Console.WriteLine("{0} : \"{1}\"; with count: {2}:", group.Key.Type.Name, group.Key.Text, group.Count());


                }
            });
        }

      static  void ThesaurusCMDLineTest() {
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
        private static string[] pathes = new[]{@"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (10).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (11).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (12).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (13).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (14).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (15).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (16).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (17).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (18).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (19).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (2).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (20).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (21).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (22).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (3).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (4).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (5).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (6).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (7).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (8).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy (9).tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment - Copy.tagged",
 @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment.tagged",

            
          };
    }
}