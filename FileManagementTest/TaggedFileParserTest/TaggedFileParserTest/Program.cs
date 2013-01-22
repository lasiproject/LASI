using LASI.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TaggedFileParserTest
{
    class Program
    {
        static void Main(string[] args) {

            //PhraseParseTest();
            ParagraphParseTest();
            //DisectParagraphTest();

            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
            }
        }

        private static void PhraseParseTest() {
            string filePath = @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment.tagged";
            TaggedFileParser testParser = new TaggedFileParser(filePath);
            var phrases = testParser.GetPhrases();


            var doc = new LASI.Algorithm.Document(from P in phrases
                                                  from W in P.Words
                                                  select W);

            doc.PrintByLinkage();
            var paragraphBreaks = from W in doc.Words
                                  where W is LASI.Algorithm.ParagraphBreak
                                  select W;
            Console.WriteLine("\n\nWord Count: {0}", doc.Words.Count);
            Console.WriteLine("\n\nParagraph Count: {0}", paragraphBreaks.Count());
            foreach (var paraBreak in paragraphBreaks) {
                Console.WriteLine(paraBreak.ID);
            }

        }

        private static void ParagraphParseTest() {
            string filePath = @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment.tagged";
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
                                                      where !(W is LASI.Algorithm.Punctuator)
                                                      select W).Count());
            Console.WriteLine("\n\nParagraph Count: {0}", paras.Count());


        }
        private static void DisectParagraphTest() {
            string filePath = @"C:\Users\Aluan\Desktop\Draft_Environmental_Assessment.tagged";
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

        //Console.WriteLine("\n\nWord Count: {0}", (
        //                                          from S in para.Sentences
        //                                          from W in S.Words
        //                                          where !(W is LASI.Algorithm.Punctuator)
        //                                          select W).Count());




    }
}
