using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;

namespace LASI.Algorithm.Thesauri
{
    public class AdverbThesaurus : ThesaurusBase
    {

        /// <summary>
        /// Initializes entity new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for nouns.</param>
        public AdverbThesaurus(string filePath)
            : base(filePath) {
            FilePath = filePath;
        }

        List<SynSet> allSets = new List<SynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            //throw new NotImplementedException();


            List<string> lines = new List<string>();

            using (StreamReader r = new StreamReader(FilePath)) {



                string line;

                for (int i = 0; i < 30; ++i) //stole this from Aluan
                {
                    r.ReadLine();
                }

                /*for (int i = 0; i < 5; i++)
                {
                    line = r.ReadLine();
                    //Console.WriteLine(line);
                    CreateSet(line);
                }*/
                //test 5 lines without having to wait


                while ((line = r.ReadLine()) != null) {

                    CreateSet(line);

                }









            }
        }

        void CreateSet(string line) {


            WordNetNounLex lexCategory = (WordNetNounLex) Int32.Parse(line.Substring(9, 2));

            String frontPart = line.Split('|', '!')[0];
            MatchCollection numbers = Regex.Matches(frontPart, @"(?<id>\d{8})");
            MatchCollection words = Regex.Matches(frontPart, @"(?<word>[A-Za-z_\-]{2,})");


            List<string> numbersList = numbers.Cast<Match>().Select(m => m.Value).Distinct().ToList();
            string id = numbersList[0];
            numbersList.Remove(id);
            List<string> wordList = words.Cast<Match>().Select(m => m.Value).Distinct().ToList();

            SynSet temp = new SynSet(id, wordList, numbersList, lexCategory);

            //SynSet temp = new SynSet(id, wordList, numbersList);


            allSets.Add(temp);

            /*foreach (string tester in numbersList){

                Console.WriteLine(tester);

           }*/
            //console view
        }

        public HashSet<string> SearchFor(string word) {

            //gets pointers of searched word
            var tempResults = from sn in allSets
                              where sn.SetWords.Contains(word)
                              select sn.SetPointers;
            var flatPointers = from R in tempResults
                               from r in R
                               select r;
            //gets words of searched word
            var tempWords = from sw in allSets
                            where sw.SetWords.Contains(word)
                            select sw.SetWords;
            HashSet<string> results = new HashSet<string>(
                from Q in tempWords
                from q in Q
                select q);


            //gets related words from above pointers
            foreach (var t in flatPointers) {

                foreach (SynSet s in allSets) {

                    if (t == s.SetID) {
                        results.Union(s.SetWords);
                    }

                }

            }

            return new HashSet<string>(results);


            //foreach (string tester in results)
            //{

            //    Console.WriteLine(tester);

            //}//console view
        }

        public override HashSet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }


        public override HashSet<string> this[Word search] {
            get {
                return this[search.Text];
            }
        }
    }
}
