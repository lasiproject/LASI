using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace LASI.Algorithm.ComparativeHeuristics
{
    using SetReference = System.Collections.Generic.KeyValuePair<AdjectiveSetRelationship, int>;
    internal sealed class AdjectiveLookup : IWordNetLookup<Adjective>
    {
        private const int HEADER_LENGTH = 29;

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

            using (StreamReader reader = new StreamReader(filePath)) {
                foreach (var line in reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(HEADER_LENGTH)) {
                    try { allSets.Add(CreateSet(line)); }
                    catch (KeyNotFoundException) { }
                }
            }

        }

        static AdjectiveSynSet CreateSet(string fileLine) {



            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from match in Regex.Matches(line, pointerRegex).Cast<Match>()
                                 let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                 where split.Count() > 1
                                 select new SetReference(interSetRelationshipMap[split[0]], Int32.Parse(split[1]));


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


        private string filePath;

        public async System.Threading.Tasks.Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }

        // Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adj files.
        private static readonly IReadOnlyDictionary<string, AdjectiveSetRelationship> interSetRelationshipMap = new Dictionary<string, AdjectiveSetRelationship> {
            { "!", AdjectiveSetRelationship. Antonym }, 
            { "&", AdjectiveSetRelationship.SimilarTo},
            { "<", AdjectiveSetRelationship.ParticipleOfVerb},
            { @"\", AdjectiveSetRelationship.Pertainym_pertains_to_noun},
            { "=", AdjectiveSetRelationship.Attribute},
            { "^", AdjectiveSetRelationship.AlsoSee },
            { ";c", AdjectiveSetRelationship.DomainOfSynset_TOPIC },
            { ";r", AdjectiveSetRelationship.DomainOfSynset_REGION },
            { ";u", AdjectiveSetRelationship.DomainOfSynset_USAGE}
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

