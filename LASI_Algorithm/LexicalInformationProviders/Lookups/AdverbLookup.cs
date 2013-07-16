using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;

namespace LASI.Algorithm.LexicalInformationProviders.Lookups
{
    using SetReference = System.Collections.Generic.KeyValuePair<AdverbSetRelationship, int>;
    internal sealed class AdverbLookup : IWordNetLookup<Adverb>
    {
        private const int HEADER_LENGTH = 29;
        /// <summary>
        /// Initializes a new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for adverbs.</param>
        public AdverbLookup(string path) {
            filePath = path;
        }

        List<AdverbSynSet> allSets = new List<AdverbSynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public void Load() {
            using (StreamReader reader = new StreamReader(filePath)) {

                foreach (var line in reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(HEADER_LENGTH)) {
                    allSets.Add(CreateSet(line));
                }

            }
        }

        AdverbSynSet CreateSet(string fileLine) {


            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from match in Regex.Matches(line, pointerRegex).Cast<Match>()
                                 let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                 where split.Count() > 1
                                 select new SetReference(relationMap[split[0]], Int32.Parse(split[1]));

            IEnumerable<string> words = from match in Regex.Matches(line, wordRegex).Cast<Match>()
                                        select match.Value.Replace('_', '-');

            int id = Int32.Parse(line.Substring(0, 8));

            AdverbCategory lexCategory = ( AdverbCategory )Int32.Parse(line.Substring(9, 2));
            return new AdverbSynSet(id, words, referencedSets, lexCategory);


        }

        private const string pointerRegex = @"\D{1,2}\s*\d{8}";
        private const string wordRegex = @"(?<word>[A-Za-z_\-\']{3,})";
        private ISet<string> SearchFor(string word) {

            //gets pointers of searched word
            //var tempResults = from sn in allSets
            //                  where sn.Words.Contains(word)
            //                  select sn.ReferencedIndexes;
            //var flatPointers = from R in tempResults
            //                   from r in R
            //                   select r;
            ////gets words of searched word
            //var tempWords = from sw in allSets
            //                where sw.Words.Contains(word)
            //                select sw.Words;
            HashSet<string> results = new HashSet<string>();
            //from Q in tempWords
            //from q in Q
            //select q);


            ////gets related words from above pointers
            //foreach (var t in flatPointers) {

            //    foreach (NounSynSet s in allSets) {

            //        if (t == s.ID) {
            //            results.Union(s.Words);
            //        }

            //    }

            //}

            return new HashSet<string>(results);


            //foreach (string tester in results)
            //{

            //    Console.WriteLine(tester);

            //}//console view
        }

        public ISet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }


        public ISet<string> this[Adverb search] {
            get {
                return this[search.Text];
            }
        }

        private static readonly LASI.Algorithm.LexicalInformationProviders.InterSetRelationshipManagement.AdverbPointerSymbolMap relationMap =
            new LASI.Algorithm.LexicalInformationProviders.InterSetRelationshipManagement.AdverbPointerSymbolMap();

        private string filePath;

        public async System.Threading.Tasks.Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }
    }
}
