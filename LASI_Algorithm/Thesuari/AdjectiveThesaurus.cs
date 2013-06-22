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

        HashSet<AdjectiveSynSet> allSets = new HashSet<AdjectiveSynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(FilePath)) {


                for (int i = 0; i < HEADER_LENGTH; ++i) {
                    reader.ReadLine();
                }
                while (!reader.EndOfStream) {
                    var set = CreateSet(reader.ReadLine());
                    allSets.Add(set);
                }
            }
        }

        AdjectiveSynSet CreateSet(string line) {



            var setLine = line.Substring(0, line.IndexOf('|'));
            MatchCollection numbers = Regex.Matches(setLine, @"(?<id>\d{8})");



            var pointers = from match in Regex.Matches(line, @"\D{1,2}\s*\d{8}").Cast<Match>()
                           let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                           let pointer = split.Count() > 1 ?
                           new KeyValuePair<AdjectivePointerSymbol, int>(relationMap[split[0]], Int32.Parse(split[1])) :
                           new KeyValuePair<AdjectivePointerSymbol, int>(AdjectivePointerSymbol.UNDEFINED, Int32.Parse(split[0]))
                           select pointer;



            IEnumerable<string> localWords = from match in Regex.Matches(setLine, @"(?<word>[A-Za-z_\-\']{3,})").Cast<Match>()
                                             select match.Value.Replace('_', '-');
            int id = Int32.Parse(setLine.Substring(0, 8));

            WordNetAdjectiveCategory lexCategory = ( WordNetAdjectiveCategory )Int32.Parse(setLine.Substring(9, 2));
            return new AdjectiveSynSet(id, localWords, pointers, lexCategory);



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
            //var tempResults = from sn in allSets
            //                  where sn.Words.Contains(word)
            //                  select sn.ReferencedIndexes;
            //var flatPointers = from R in tempResults
            //                   from r in R
            //                   select r;
            //gets related words from above pointers

            //foreach (var t in flatPointers) {
            //    foreach (NounSynSet s in allSets) {

            //        if (t == s.ID) {
            //            results.Union(s.Words);
            //        }

            //    }
            //}
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


        private static readonly AdjectivePointerSymbolMap relationMap = new AdjectivePointerSymbolMap();
    }
}
