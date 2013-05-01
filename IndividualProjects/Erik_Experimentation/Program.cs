using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using SharpNLPTaggingModule;
using System.IO;
using System.Xml;
using LASI.FileSystem.FileTypes;
using LASI.Algorithm.Binding;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Algorithm.Weighting;
using LASI.Algorithm.Thesauri;
using System.Text.RegularExpressions;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm.DocumentConstructs;


namespace Erik_Experimentation
{
    class Program
    {

        static void Main(string[] args) {

            //var nounTest2 = new NounThesaurus(@"..\..\..\..\WordNetThesaurusData\data.noun");
            //nounTest2.Load();
            //string key = "man";
            //HashSet<string> synonyms2;
            //synonyms2 = nounTest2.SearchFor(key);
            //foreach (var s in synonyms2)
            //{
            //    Console.WriteLine(s);
            //}

            //var adjTest = new AdjectiveThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adj");
            //adjTest.Load();
            //string key = "able";
            //adjTest.SearchFor(key);

            //var advTest = new AdverbThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adv");
            //advTest.Load();
            //string key = "vastly";
            //advTest.SearchFor(key);



            var document1 = TaggerUtil.LoadTextFile(new TextFile(@"C:\Users\CynosureEPR\Desktop\weight2.txt"));
            var document2 = TaggerUtil.LoadTextFile(new TextFile(@"C:\Users\CynosureEPR\Desktop\weight3.txt"));

            var documents = new Document[] { document1, document2 };


            foreach (var d in documents) {
                InverseDocumentFrequency(d);

            }





            //Keeps the console window open until the escape key is pressed
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }





        }

        private static void InverseDocumentFrequency(Document d) {


            //IEnumerable<int> allWordCounts = from searchWord in d.Words
            //                                 select d.Words.Count(wordToCompare => wordToCompare.Text == searchWord.Text);

            foreach (Word w in d.Words)
            {
                w.FrequencyCurrent = d.Words.Count(wordCompare => wordCompare.Text == w.Text);

                Console.WriteLine("Word : " + w + "Count: " + w.FrequencyCurrent);
            }




        }
        //static int Count(this IEnumerable<Word> someWords, Func<Word, bool> comparisonFunction) {
        //    int cnt = 0;
        //    foreach (var word in someWords) {
        //        if (comparisonFunction(word)) {
        //            cnt++;
        //        }
        //    }
        //    return cnt;
        //}




    }



}


