using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LASI.Utilities;

namespace LASI.Core.Heuristics
{
    using SetReference = KeyValuePair<AdverbSetLink, int>;
    internal sealed class AdverbLookup : WordNetLookup<Adverb>
    {
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
        internal override void Load() {
            using (StreamReader reader = new StreamReader(filePath)) {

                foreach (var line in reader.ReadToEnd().SplitRemoveEmpty('\n').Skip(HEADER_LENGTH)) {
                    try { allSets.Add(CreateSet(line)); }
                    catch (KeyNotFoundException) { }
                }

            }
        }


        static AdverbSynSet CreateSet(string fileLine) {


            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from match in Regex.Matches(line, pointerRegex).Cast<Match>()
                                 let split = match.Value.SplitRemoveEmpty(' ')
                                 where split.Count() > 1 && interSetMap.ContainsKey(split[0])
                                 select new SetReference(interSetMap[split[0]], Int32.Parse(split[1]));

            IEnumerable<string> words = from match in Regex.Matches(line, wordRegex).Cast<Match>()
                                        select match.Value.Replace('_', '-');

            int id = Int32.Parse(line.Substring(0, 8));
            return new AdverbSynSet(id, words, referencedSets, AdverbCategory.All);
        }

        private ISet<string> SearchFor(string search) {
            return (from containingSet in allSets
                    where containingSet.ContainsWord(search)
                    from linkedSet in allSets
                    where containingSet.DirectlyReferences(linkedSet)
                    from word in linkedSet.Words
                    select word).ToHashSet(StringComparer.OrdinalIgnoreCase);


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
        }

        internal override ISet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }


        internal override ISet<string> this[Adverb search] {
            get {
                return this[search.Text];
            }
        }

        private string filePath;



        private const string pointerRegex = @"\D{1,2}\s*\d{8}";
        private const string wordRegex = @"(?<word>[A-Za-z_\-\']{3,})";
        // Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adv files.
        private static readonly IReadOnlyDictionary<string, AdverbSetLink> interSetMap = new Dictionary<string, AdverbSetLink> {
            {"!", AdverbSetLink.Antonym },
            {@"\", AdverbSetLink.DerivedFromAdjective },
            {";c", AdverbSetLink.DomainOfSynset_TOPIC },
            {";r", AdverbSetLink.DomainOfSynset_REGION },
            {";u", AdverbSetLink.DomainOfSynset_USAGE }
        };
    }
    /// <summary>
    /// Defines the broad lexical categories assigned to Adverbs in the WordNet system.
    /// </summary>
    public enum AdverbCategory : byte
    {
        /// <summary>
        /// All adverbs have the same category. This value is simply included for completeness.
        /// </summary>
        All = 2

    }
}

