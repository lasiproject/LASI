using LASI.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.DataRepresentation;
namespace TaggedFileParserTest
{
    class Program
    {


        static void Main(string[] args) {

            ParagraphParseTest();





            //Keeps the console window open until the escape key is pressed
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }
        }

        private static string filePath = @".\..\..\..\..\..\TestDocs\Draft_Environmental_Assessment.tagged";



        private static void ParagraphParseTest() {

            TaggedFileParser testParser = new TaggedFileParser(filePath);
            var paras = testParser.GetParagraphs();
            foreach (var para in paras) {


                foreach (var sen in para.Sentences) {

                    foreach (var prs in sen.Phrases) {
                        foreach (var w in prs.Words) {
                            Console.Write(w.Text + " ");
                        }
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine("\n\nWord Count: {0}", (from P in paras
                                                      from S in P.Sentences
                                                      from W in S.Words
                                                      where !(W is LASI.DataRepresentation.Punctuator)
                                                      select W).Count());
            Console.WriteLine("\n\nParagraph Count: {0}", paras.Count());


        }



        private static void DisectParagraphTest() {

            TaggedFileParser testParser = new TaggedFileParser(filePath);
            var para = testParser.GetParagraphs().ElementAt(new Random().Next(100, 200));

            Console.Write('\t');
            foreach (var sen in para.Sentences) {

                foreach (var w in sen.Words) {
                    Console.Write(w.Text + " ");
                }
            }
            Console.WriteLine();
        }

        private static void PhraseParseTest() {

            TaggedFileParser testParser = new TaggedFileParser(filePath);
            var phrases = testParser.GetPhrases();


            var doc = new LASI.DataRepresentation.Document(from P in phrases
                                                  from W in P.Words
                                                  select W);

            doc.PrintByLinkage();
            var paragraphBreaks = from W in doc.Words
                                  where W is LASI.DataRepresentation.ParagraphBreak
                                  select W;
            Console.WriteLine("\n\nWord Count: {0}", doc.Words.Count);
            Console.WriteLine("\n\nParagraph Count: {0}", paragraphBreaks.Count());
            foreach (var paraBreak in paragraphBreaks) {
                Console.WriteLine(paraBreak.ID);
            }

        }

    }
}
