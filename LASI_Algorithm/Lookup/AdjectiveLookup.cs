using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace LASI.Algorithm.Lookup
{
    using SetReference = System.Collections.Generic.KeyValuePair<AdjectiveSetRelationship, int>;
    internal class AdjectiveLookup : ISynonymLookup<Adjective>
    {
        protected const int HEADER_LENGTH = 29;

        /// <summary>
        /// Initializes a new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for adjectives.</param>
        public AdjectiveLookup(string path) {

            filePath = path;
        }

        HashSet<AdjectiveSynSet> allSets = new HashSet<AdjectiveSynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public void Load() {
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(filePath)) {


                for (int i = 0; i < HEADER_LENGTH; ++i) {
                    reader.ReadLine();
                }
                while (!reader.EndOfStream) {
                    var set = CreateSet(reader.ReadLine());
                    allSets.Add(set);
                }
            }
        }

        AdjectiveSynSet CreateSet(string fileLine) {



            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from match in Regex.Matches(line, @"\D{1,2}\s*\d{8}").Cast<Match>()
                                 let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                 where split.Count() > 1
                                 select new SetReference(relationMap[split[0]], Int32.Parse(split[1]));


            IEnumerable<string> words = from match in Regex.Matches(line, @"(?<word>[A-Za-z_\-\']{3,})").Cast<Match>()
                                        select match.Value.Replace('_', '-');
            int id = Int32.Parse(line.Substring(0, 8));

            AdjectiveCategory lexCategory = ( AdjectiveCategory )Int32.Parse(line.Substring(9, 2));
            return new AdjectiveSynSet(id, words, referencedSets, lexCategory);



        }
        private ISet<string> SearchFor(string word) {


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
        public ISet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }
        public ISet<string> this[Adjective search] {
            get {
                return this[search.Text];
            }
        }


        private static readonly LASI.Algorithm.Lookup.InterSetRelationshipManagement.AdjectivePointerSymbolMap relationMap =
            new LASI.Algorithm.Lookup.InterSetRelationshipManagement.AdjectivePointerSymbolMap();

        private string filePath;

        public async System.Threading.Tasks.Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }
    }
}
