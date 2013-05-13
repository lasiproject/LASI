using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;


namespace LASI.Algorithm.Thesauri
{
    public class NounThesaurus : ThesaurusBase
    {
        /// <summary>
        /// Initializes entity new instance of the NounProvider class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for nouns.</param>
        public NounThesaurus(string filePath)
            : base(filePath) {
            FilePath = filePath;
        }

        HashSet<SynSet> allSets = new HashSet<SynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            //throw new NotImplementedException();


            HashSet<string> lines = new HashSet<string>();

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

            //Aluan: This line gets extracts word category info I noticed was present in the DB files
            //Erik:  Gotcha, I'll try to decipher its meaning.

            WordNetNounLex lexCategory = (WordNetNounLex) Int32.Parse(line.Substring(9, 2));

            String frontPart = line.Split('|', '!')[0];
            MatchCollection numbers = Regex.Matches(frontPart, @"(?<id>\d{8})");
            MatchCollection words = Regex.Matches(frontPart, @"(?<word>[A-Za-z_\-]{3,})");


            List<string> numbersList = numbers.Cast<Match>().Select(m => m.Value).Distinct().ToList();
            string id = numbersList[0];
            numbersList.Remove(id);

            //somethin's amiss here.
            List<string> wordList = words.Cast<Match>().Select(m => m.Value).Distinct().ToList();

            SynSet temp = new SynSet(id, wordList, numbersList, lexCategory);

            //SynSet temp = new SynSet(id, wordList, numbersList);


            allSets.Add(temp);

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
        private NounConjugator conjugator = new NounConjugator(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "noun.exc");

    }
}
