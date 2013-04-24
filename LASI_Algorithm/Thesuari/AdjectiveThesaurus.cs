using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;


namespace LASI.Algorithm.Thesauri
{
    public class AdjectiveThesaurus : ThesaurusBase
    {

        /// <summary>
        /// Initializes a new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for nouns.</param>
        public AdjectiveThesaurus(string filePath)
            : base(filePath) {
            FilePath = filePath;
        }

        HashSet<SynSet> allSets = new HashSet<SynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            List<string> lines = new List<string>();

            using (StreamReader r = new StreamReader(FilePath)) {

                string line;

                for (int i = 0; i < 30; ++i) //stole this from Aluan
                {
                    r.ReadLine();
                }
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

            allSets.Add(temp);

        }
        public HashSet<string> SearchFor(string word) {


            //gets words of searched word
            var tempWords = from sw in allSets
                            where sw.SetWords.Contains(word)
                            select sw.SetWords;
            HashSet<string> results = new HashSet<string>(
                (from Q in tempWords
                 from q in Q
                 select q).Distinct());


            //gets pointers of searched word
            var tempResults = from sn in allSets
                              where sn.SetWords.Contains(word)
                              select sn.SetPointers;
            var flatPointers = from R in tempResults
                               from r in R
                               select r;
            //gets related words from above pointers

            foreach (var t in flatPointers) {
                foreach (SynSet s in allSets) {

                    if (t == s.SetID) {
                        results.Union(s.SetWords);
                    }

                }
            }
            return results;

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
