﻿using System;
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
            TestTagParser(@"C:\Users\Aluan\Desktop\411writtensummary2.txt");


            StdIoUtil.WaitForKey(ConsoleKey.Escape);
        }




        private static void TestTagParser(string filePath) {
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, filePath);
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).GetParagraphs();
            var document = new Document(paragraphs);


            var myPhrases = from p in document.Paragraphs
                            from sent in p.Sentences
                            from r in sent.Phrases
                            select r;
            PerformPhraseTypeAndTextCounts(document);



        }

        private static void PerformPhraseTypeAndTextCounts(Document document) {
            var phrasePOSCounts = from R in document.Phrases
                                  group R by new {
                                      Type = R.GetType(),
                                      R.Text
                                  } into G
                                  orderby G.Count()
                                  select G;
            foreach (var group in phrasePOSCounts) {
                Console.WriteLine("{0} : \"{1}\"; with count: {2}:", group.Key.Type.Name, group.Key.Text, group.Count());
            }
        }

        void ThesaurusCMDLineTest() {

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
    }
}
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


//var counts = from phrase in document.phrases
//             group phrase by phrase.type;
//foreach (var grp in counts) {
//    console.writeline("category: {0}, count: {1}", grp.key, grp.count());
//    var textgroupsintypecategory = from phrase in grp
//                                   group phrase by phrase.text;
//    foreach (var innergrp in textgroupsintypecategory) {
//        console.writeline("text: {0}, count: {1}", innergrp.key, innergrp.count());
//    }
//}


//var verbPhrases = myPhrases.GetVerbPhrases();
//var verbPhrasesWithSubjectLASI = verbPhrases.WithSubject((NounPhrase N) => N.Text == "LASI");

//var wordPOSCounts = from W in document.Words.AsParallel()
//                    group W by new {
//                        Type = W.GetType(),
//                        W.Text,
//                    } into G
//                    orderby G.Count()
//                    select G;
