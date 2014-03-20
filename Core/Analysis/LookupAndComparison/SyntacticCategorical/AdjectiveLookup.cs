using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace LASI.Core.Heuristics
{
    using SetReference = System.Collections.Generic.KeyValuePair<AdjectiveSetLink, int>;
    internal sealed class AdjectiveLookup : WordNetLookup<Adjective>
    {


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
        internal override void Load() {

            using (StreamReader reader = new StreamReader(filePath)) {
                foreach (var line in reader.ReadToEnd().SplitRemoveEmpty('\n').Skip(HEADER_LENGTH)) {
                    try { allSets.Add(CreateSet(line)); }
                    catch (KeyNotFoundException) { }
                }
            }

        }

        static AdjectiveSynSet CreateSet(string fileLine) {



            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from match in Regex.Matches(line, pointerRegex).Cast<Match>()
                                 let split = match.Value.SplitRemoveEmpty(' ')
                                 where split.Count() > 1 && interSetMap.ContainsKey(split[0])
                                 select new SetReference(interSetMap[split[0]], Int32.Parse(split[1]));


            IEnumerable<string> words = from match in Regex.Matches(line, wordRegex).Cast<Match>()
                                        select match.Value.Replace('_', '-');
            int id = Int32.Parse(line.Substring(0, 8), System.Globalization.CultureInfo.InvariantCulture);

            Category lexCategory = (Category)Int32.Parse(line.Substring(9, 2));
            return new AdjectiveSynSet(id, words, referencedSets, lexCategory);



        }
        private const string pointerRegex = @"\D{1,2}\s*\d{8}";
        private const string wordRegex = @"(?<word>[A-Za-z_\-\']{3,})";
        private ISet<string> SearchFor(string word) {


            //gets words of searched word
            var tempWords = from sw in allSets
                            where sw.Words.Contains(word)
                            select sw.Words;
            HashSet<string> results = new HashSet<string>(
                (from Q in tempWords
                 from q in Q
                 select q).Distinct());


            return results;

        }
        internal override ISet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }
        internal override ISet<string> this[Adjective search] {
            get {
                return this[search.Text];
            }
        }


        private string filePath;



        // Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adj files.
        private static readonly IReadOnlyDictionary<string, AdjectiveSetLink> interSetMap = new Dictionary<string, AdjectiveSetLink> {
            { "!", AdjectiveSetLink. Antonym }, 
            { "&", AdjectiveSetLink.SimilarTo},
            { "<", AdjectiveSetLink.ParticipleOfVerb},
            { @"\", AdjectiveSetLink.Pertainym_pertains_to_noun},
            { "=", AdjectiveSetLink.Attribute},
            { "^", AdjectiveSetLink.AlsoSee },
            { ";c", AdjectiveSetLink.DomainOfSynset_TOPIC },
            { ";r", AdjectiveSetLink.DomainOfSynset_REGION },
            { ";u", AdjectiveSetLink.DomainOfSynset_USAGE}
        };
        /// <summary>
        /// Defines the broad lexical categories assigned to Adjectives in the WordNet system.
        /// </summary>
        public enum Category : byte
        {
            /// <summary>
            /// all adjective clusters
            /// </summary>
            All = 0,
            /// <summary>
            /// relational adjectives (pertainyms)
            /// </summary>
            Pert = 1,
            /// <summary>
            /// participial adjectives
            /// </summary>
            PPL = 44,
        }
    }

}

