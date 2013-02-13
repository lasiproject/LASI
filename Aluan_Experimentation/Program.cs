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
using LASI.Algorithm.IEnumerableExtensions;


namespace Aluan_Experimentation
{
    class Program
    {
        static void Main(string[] args) {

            //Word myword = new PastTenseVerb("congregated");
            //Verb myVerb = myword as dynamic;
            //Console.WriteLine(myVerb != null ? myVerb.Text : "conversion failed");
            TestingDocParser();
            //List<Word> myList = new Word[] { 
            //    new GenericPluralNoun("people"),
            //    new PresentTenseVerb("congregate"), 
            //    new Pronoun("there") }.ToList();


            //foreach (Word w in myList) {
            //    ToMostDerrived(w as dynamic);
            //    Console.WriteLine(w);
            //}


            StdIoUtil.WaitForKey(ConsoleKey.Escape);
        }

        static Verb ToMostDerrived(Verb v) {
            Console.WriteLine("verb mthd called");
            return v;
        }

        static Noun ToMostDerrived(Noun n) {
            Console.WriteLine("noun mthd called");
            return n;
        }

        static Pronoun ToMostDerrived(Pronoun p) {
            Console.WriteLine("pronoun mthd called");
            return p;
        }


        private static void TestingDocParser() {
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Aluan\Desktop\411writtensummary2.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).GetParagraphs();
            var document = new Document(paragraphs);


            var myPhrases = from p in document.Paragraphs
                            from sent in p.Sentences
                            from r in sent.Phrases
                            select r;

            var verbPhrases = myPhrases.GetVerbPhrases();
            var verbPhrasesWithSubjectLASI = verbPhrases.WithSubject((NounPhrase N) => N.Text == "LASI");

            //var wordPOSCounts = from W in document.Words.AsParallel()
            //                    group W by new {
            //                        Type = W.GetType(),
            //                        W.Text,
            //                    } into G
            //                    orderby G.Count()
            //                    select G;

            var phrasePOSCounts = from R in document.Phrases
                                  group R by new {
                                      Type = R.GetType(),
                                      R.Text
                                  } into G
                                  orderby G.Count()
                                  select G;

            var counts = from phrase in document.Phrases
                         group phrase by phrase.Type;
            foreach (var grp in counts) {
                Console.WriteLine("Category: {0}, count: {1}", grp.Key, grp.Count());
                var textGroupsInTypeCategory = from phrase in grp
                                               group phrase by phrase.Text;
                foreach (var innerGrp in textGroupsInTypeCategory) {
                    Console.WriteLine("Text: {0}, count: {1}", innerGrp.Key, innerGrp.Count());
                }
            }


            //foreach (var group in phrasePOSCounts) {
            //    Console.WriteLine("{0} : {1} count: {2}:", group.Key.Type.Name, group.Key.Text, group.Count());
            //}

            //Func<string, string> f = s => s.ToUpper();
            //Func<string, string> g = s => s.Substring(0, 4);
            //Func<string, string> h = s => s.ToLower();

            //var MF = f.Compose(g, h, f);
            //foreach (var S in new[] { 
            //    "Hello there!", 
            //    "How are you?", 
            //    "Would you like some cheese with that wine?" }) {
            //    Console.WriteLine(MF(S));
            //}
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
