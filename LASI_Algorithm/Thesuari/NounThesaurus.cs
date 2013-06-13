using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace LASI.Algorithm.Thesauri
{
    internal class NounThesaurus : ThesaurusBase
    {
        /// <summary>
        /// Initializes a new instance of the NounProvider class.
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

            //Aluan: This line gets extracts wd category info I noticed was present in the DB files
            //Erik:  Gotcha, I'll try to decipher its meaning.

            WordNetNounCategory lexCategory = (WordNetNounCategory)Int32.Parse(line.Substring(9, 2));
            String frontPart = line.Split('|', '!')[0];
            MatchCollection numbers = Regex.Matches(frontPart, @"(?<id>\d{8})");
            MatchCollection words = Regex.Matches(frontPart, @"(?<word>[A-Za-z_\-]{3,})");


            IEnumerable<int> pointers = numbers.Cast<Match>().Select(m => Int32.Parse(m.Value)).Distinct();
            int id = pointers.First();


            //somethin's amiss here.
            List<string> wordList = words.Cast<Match>().Select(m => m.Value).Distinct().ToList();

            SynSet temp = new SynSet(id, wordList, pointers, lexCategory);

            //SynSet temp = new SynSet(id, wordList, pointers);


            allSets.Add(temp);

            /*foreach (string tester in pointers){

                Console.WriteLine(tester);

           }*/
            //console view
        }

        public HashSet<string> SearchFor(string word) {
            HashSet<string> ResultSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            HashSet<int> PointerSet = new HashSet<int>();
            ResultSet.Add(word);
            foreach (SynSet set in from set in allSets
                                   where set.Words.Contains(word)
                                   select set) {


                PointerSet.UnionWith(set.Pointers);
                RecursiveSearch(ResultSet, PointerSet);
                ResultSet.UnionWith(set.Words);
            }
            return ResultSet;
        }

        private void RecursiveSearch(HashSet<string> ResultSet, HashSet<int> PointerSet) {
            foreach (SynSet subset in from subset in allSets
                                      where PointerSet.Contains(subset.ID)
                                      select subset) {

                PointerSet.UnionWith(subset.Pointers);
                RecursiveSearch(ResultSet, subset.Pointers);

                ResultSet.UnionWith(subset.Words);

            }



        }

        //private void searchPointers(HashSet<string> ResultSet, HashSet<int> PointerSet) {
        //    foreach (var p in PointerSet) {
        //        if (PointerSet.Add(p)) {
        //            foreach (SynSet subset in from subset in allSets
        //                                      where subset.ID == p
        //                                      select subset) {
        //                foreach (var w in subset.Words) {
        //                    ResultSet.Add(w);

        //                }
        //                searchPointers(ResultSet, subset.Pointers);
        //            }
        //        }
        //    }
        //}

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
