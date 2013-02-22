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


//            var doc = TestTaggerHelper("Hello there! How are you? I am Working on a linguistic analysis project (for Dr. Hester) with a skilled, professional team of 6 people. They are Brittany, Dustin, Richard, Scott, and Erik.");
//            //doc.PrintByLinkage();
//            foreach (var p in doc.Words) {
//                print(p);
                
//            }
//// ParseThreaded();
            var doc = MakeDocumentFromTaggedFile(pathes[0]).Result;
            print(doc.Paragraphs.Count());
            print("\n");
            foreach (var p in doc.Phrases) {
                print(p);
                //print("\n");
            }

            // doc.Phrases.ToList().ForEach(p => print(p));
            //var categoryResults = ParseThreaded();
            //foreach (var item in categoryResults) {
            //    Console.WriteLine(item);
            //}
            StdIO.WaitForKey(ConsoleKey.Escape);


        }

        //private static string ParseSynch() {
        //    foreach (var p in pathes) {
        //        var doc=MakeDocumentFromTaggedFile(
        //    }
        //}

        private static Document TestTaggerHelper(string str) {
            var simpleTagger = new StringTagger(TaggingOption.TagAndAggregate);
            var tagged = simpleTagger.TagString(str);
           // print(tagged);
            //var XMLText = tagged.Replace('(', '<').Replace(')', '>');
            //var xmlfrag = new XmlDocument();
            //var xmlb = xmlfrag.CreateTextNode(XMLText);
            //var xmlw = XmlWriter.Create(@"C:\Users\Aluan\Desktop\xmlconverted.xml");
            //xmlw.WriteStartDocument(true);
            //xmlw.WriteElementString(xmlb.LocalName, xmlb.Value);
            var taggedParser = new TaggedFileParser(tagged);
            return taggedParser.ConstructDocument();
            //print(result);
        }

        public static IEnumerable<string> ParseThreaded() {

            return (from p in pathes.AsParallel()

                    let counts = (CountByTypeAndText(MakeDocumentFromTaggedFile(p).Result)).Result
                    from s in counts

                    select s);

        }



        private static async Task<Document> MakeDocumentFromTaggedFile(string filePath) {

            return await Task.Run(async () => await new TaggedFileParser(new TaggedFile(filePath)).GetDocumentAsync());


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