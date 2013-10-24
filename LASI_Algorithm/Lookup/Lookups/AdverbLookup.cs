using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LASI.Algorithm.ComparativeHeuristics
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

        HashSet<AdverbSynSet> allSets = new HashSet<AdverbSynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public void Load() {
            using (StreamReader reader = new StreamReader(filePath)) {

                foreach (var line in reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(HEADER_LENGTH)) {
                    try { allSets.Add(CreateSet(line)); }
                    catch (KeyNotFoundException) { }
                }

            }
        }

        static AdverbSynSet CreateSet(string fileLine) {


            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from match in Regex.Matches(line, pointerRegex).Cast<Match>()
                                 let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                 where split.Count() > 1
                                 select new SetReference(interSetRelationshipMap[split[0]], Int32.Parse(split[1]));

            IEnumerable<string> words = from match in Regex.Matches(line, wordRegex).Cast<Match>()
                                        select match.Value.Replace('_', '-');

            int id = Int32.Parse(line.Substring(0, 8));

            Category lexCategory = (Category)Int32.Parse(line.Substring(9, 2));
            return new AdverbSynSet(id, words, referencedSets, lexCategory);


        }

        private const string pointerRegex = @"\D{1,2}\s*\d{8}";
        private const string wordRegex = @"(?<word>[A-Za-z_\-\']{3,})";
        private ISet<string> SearchFor(string word) {

            //gets pointers of searched word
            //var tempResults = from sn in allSets
            //                  where sn.Words.Contains(word)
            //                  select sn.ReferencedIndexes;
            ////var flatPointers = from R in tempResults
            ////                   from r in R
            ////                   select r;
            //////gets words of searched word
            ////var tempWords = from sw in allSets
            ////                where sw.Words.Contains(word)
            ////                select sw.Words;
            //HashSet<string> results = new HashSet<string>();
            ////from Q in tempWords
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

            //return new HashSet<string>(results);
            throw new NotImplementedException();
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

        private string filePath;

        public async System.Threading.Tasks.Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }


        // Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adv files.
        private static readonly IReadOnlyDictionary<string, AdverbSetRelationship> interSetRelationshipMap = new Dictionary<string, AdverbSetRelationship> {
            { "!", AdverbSetRelationship. Antonym }, 
            { @"\", AdverbSetRelationship.DerivedFromAdjective},
            { ";c", AdverbSetRelationship.DomainOfSynset_TOPIC },
            { ";r", AdverbSetRelationship.DomainOfSynset_REGION },
            { ";u", AdverbSetRelationship.DomainOfSynset_USAGE}
        };
        /// <summary>
        /// Defines the broad lexical categories assigned to Adverbs in the WordNet system.
        /// </summary>
        public enum Category : byte
        {
            /// <summary>
            /// All adverbs have the same category. This value is simply included for completeness.
            /// </summary>
            All = 2
        }
    }
}

