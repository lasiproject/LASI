using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;


namespace LASI.Algorithm.Thesauri
{
    internal class AdjectiveThesaurus : ThesaurusBase
    {

        /// <summary>
        /// Initializes a new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for nouns.</param>
        public AdjectiveThesaurus(string filePath)
            : base(filePath) {
            FilePath = filePath;
        }

        HashSet<NounSynSet> allSets = new HashSet<NounSynSet>();

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

            WordNetNounCategory lexCategory = ( WordNetNounCategory )Int32.Parse(line.Substring(9, 2));

            String frontPart = line.Split('|', '!')[0];
            MatchCollection numbers = Regex.Matches(frontPart, @"(?<id>\d{8})");
            MatchCollection words = Regex.Matches(frontPart, @"(?<word>[A-Za-z_\-]{2,})");


            IEnumerable<int> pointers = numbers.Cast<Match>().Select(m => Int32.Parse(m.Value)).Distinct();
            int id = pointers.First();

            List<string> wordList = words.Cast<Match>().Select(m => m.Value).Distinct().ToList();

            NounSynSet temp = new NounSynSet(id, wordList, pointers, lexCategory);

            allSets.Add(temp);

        }
        public HashSet<string> SearchFor(string word) {


            //gets words of searched word
            var tempWords = from sw in allSets
                            where sw.Words.Contains(word)
                            select sw.Words;
            HashSet<string> results = new HashSet<string>(
                (from Q in tempWords
                 from q in Q
                 select q).Distinct());


            //gets pointers of searched word
            var tempResults = from sn in allSets
                              where sn.Words.Contains(word)
                              select sn.ReferencedIndexes;
            var flatPointers = from R in tempResults
                               from r in R
                               select r;
            //gets related words from above pointers

            foreach (var t in flatPointers) {
                foreach (NounSynSet s in allSets) {

                    if (t == s.ID) {
                        results.Union(s.Words);
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
