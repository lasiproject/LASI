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



            var document1 = TaggerUtil.LoadTextFile(new TextFile(@"C:\Users\Aluan\Desktop\weight2.txt"));
            var document2 = TaggerUtil.LoadTextFile(new TextFile(@"C:\Users\Aluan\Desktop\weight3.txt"));

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

            foreach (Word w in d.Words) {
                w.FrequencyCurrent = d.Words.Count(wordCompare => wordCompare.Text == w.Text);

                Console.WriteLine("Word : " + w + "Count: " + w.FrequencyCurrent);
            }




        }

    }


    /// <summary>
    /// Attempts to show how functions which take other functions, such as lambda expressions, as parameters make use of those functions.
    /// Contains several example functions which take other functions as parameters.
    /// These types of functions are called higher order functions, because they take other functions, allowing them to abstract over common activities such as counting.
    /// 
    /// 
    /// Count is a kind of pattern which can roughly be described by the following two scenarios:
    /// 
    /// Scenario 1.
    /// 
    ///         Given some sequence of elements    
    ///                 ex: think vector, set, sequence, iterable, enumerable
    /// 
    ///         Visit each element in said sequence, making a note of its existence in a tally
    ///                 
    /// Scenario 2.
    /// 
    ///         Given some sequence of element    
    ///             
    ///         select one or more criteria which can be relevantly tested for on any element in said sequence
    ///                 
    ///         Visit each element in said sequence, only making a note of its existence, if it matches the criteria specified
    /// 
    ///         
    /// </summary>
    internal static class FunctionsTakingFunctionAsAgrumentsExamples
    {
        /// <summary>
        /// Scenario 1.
        /// </summary>
        static int Count(this IEnumerable<Word> someWords) {
            int cnt = 0;
            foreach (var word in someWords) {
                cnt++;
            }
            return cnt;
        }
        /// <summary>
        /// Scenario 2.
        /// </summary>
        static int Count(this IEnumerable<Word> someWords, Func<Word, bool> comparisonFunction) {
            int cnt = 0;
            foreach (var word in someWords) {
                if (comparisonFunction(word)) {
                    cnt++;
                }
            }
            return cnt;
        }


        /// <summary>
        /// Example with data
        /// </summary>
        static void DrugsAreInteresting() {


            var someDrugs = new[] {
                new { Name = "LSD", Category = "Hallucinogen" },
                new { Name = "Aderal", Category = "Amphetamine" },
                new { Name = "Vyvanse", Category = "Amphetamine" },                    
                new { Name = "Zoloft", Category = "SSRI" } 
            };

            var someGuy = new {
                FirName = "John",
                LastName = "Smith",
                DrugPreferences = new[] {
                    new { Category = "Amphetamine", PreferenceLevel = 1 },
                    new { Category = "Hallucinogen", PreferenceLevel = 2} 
                },
                GetHigh = new Func<dynamic, dynamic>(drug => "Some Guy is now high on " + drug.Name)
            };

            var defaultStates = new[] { "sober", "moody" };

            var drugsSomeGuyWillLike =
                someDrugs
                .Where(drug => someGuy.DrugPreferences
                    .Any(dr => dr.Category == drug.Category));

            var canGetHighToday = drugsSomeGuyWillLike.Count() > 0;


        }
    }
}








