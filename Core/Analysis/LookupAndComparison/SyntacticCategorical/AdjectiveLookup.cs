using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace LASI.Core.Heuristics.WordNet
{
    using SetReference = KeyValuePair<AdjectiveLink, int>;
    internal sealed class AdjectiveLookup : WordNetLookup<Adjective, AdjectiveLink>
    {
        /// <summary>
        /// Initializes a new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for adjectives.</param>
        public AdjectiveLookup(string path) {

            filePath = path;
        }

        ISet<AdjectiveSynSet> allSets = new HashSet<AdjectiveSynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        internal override void Load() {
            using (StreamReader reader = new StreamReader(filePath)) {
                foreach (var line in reader.ReadToEnd().SplitRemoveEmpty('\n').Skip(HEADER_LENGTH)) {
                    try { allSets.Add(CreateSet(line)); }
                    catch (KeyNotFoundException e) {
                        Output.WriteLine("An error occured when Loading the {0}: {1}\r\n{2}", GetType().Name, e.Message, e.StackTrace);
                    }
                }
            }

        }
        private AdjectiveSynSet CreateSet(string fileLine) {
            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = ParseReferencedSets(line, segments => new SetReference(InterSetMap[segments[0]], int.Parse(segments[1])));

            IEnumerable<string> words = from match in Regex.Matches(line, wordRegex).Cast<Match>()
                                        select match.Value.Replace('_', '-');
            int id = int.Parse(line.Substring(0, 8), System.Globalization.CultureInfo.InvariantCulture);

            AdjectiveCategory lexCategory = (AdjectiveCategory)int.Parse(line.Substring(9, 2));
            return new AdjectiveSynSet(id, words, referencedSets, lexCategory);



        }




        private ISet<string> SearchFor(string search) {
            //gets words of searched word
            return allSets.AsParallel()
                .Where(set => set.ContainsWord(search))
                .SelectMany(set => set.Words)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
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
        private static readonly IReadOnlyDictionary<string, AdjectiveLink> interSetMap = new Dictionary<string, AdjectiveLink> {
            {"!",AdjectiveLink.Antonym},
            {"&",AdjectiveLink.SimilarTo},
            {"<",AdjectiveLink.ParticipleOfVerb},
            {@"\", AdjectiveLink.Pertainym_pertains_to_noun},
            {"=",AdjectiveLink.Attribute},
            {"^",AdjectiveLink.AlsoSee},
            {";c", AdjectiveLink.DomainOfSynset_TOPIC},
            {";r", AdjectiveLink.DomainOfSynset_REGION},
            {";u", AdjectiveLink.DomainOfSynset_USAGE}
        };

        protected override IReadOnlyDictionary<string, AdjectiveLink> InterSetMap {
            get { return interSetMap; }
        }
    }
    /// <summary>
    /// Defines the broad lexical categories assigned to Adjectives in the WordNet system.
    /// </summary>
    public enum AdjectiveCategory : byte
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


