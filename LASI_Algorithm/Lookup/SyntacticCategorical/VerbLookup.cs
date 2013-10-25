using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Algorithm.ComparativeHeuristics
{
    using LASI.Algorithm.ComparativeHeuristics.Morphemization;
    using SetReference = System.Collections.Generic.KeyValuePair<VerbSetRelationship, int>;

    internal sealed class VerbLookup : IWordNetLookup<Verb>
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class. 
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the sysnonym data for verbals.</param>
        public VerbLookup(string path) {
            filePath = path;
        }
        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary> 
        public void Load() {
            using (var reader = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 10024, FileOptions.SequentialScan))) {
                var fileLines = reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(HEADER_LENGTH);
                foreach (var line in fileLines) {
                    LinkSynset(CreateSet(line));
                }
                reader.BaseStream.Dispose();
            }
        }
        public async Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }

        private static VerbSynSet CreateSet(string fileLine) { 
            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from Match M in Regex.Matches(line, POINTER_REGEX)
                                 let split = M.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                 where split.Count() > 1
                                 select new SetReference(interSetRelationshipMap[split[0]], Int32.Parse(split[1]));

            var words = from Match ContainedWord in Regex.Matches(line.Substring(17), WORD_REGEX)
                        select ContainedWord.Value.Replace('_', '-').ToLower();
            var id = Int32.Parse(line.Substring(0, 8));
            var lexCategory = (Category)Int32.Parse(line.Substring(9, 2));
            return new VerbSynSet(id, words, referencedSets, lexCategory);
        }


        private void LinkSynset(VerbSynSet synset) {
            setsBySetID[synset.Id] = synset;
            foreach (var word in synset.Words) {
                if (data.ContainsKey(word)) {
                    var newSet = new VerbSynSet(
                        data[word].Id,
                        data[word].Words.Concat(synset.Words),
                        data[word].RelatedSetsByRelationKind
                            .Concat(synset.RelatedSetsByRelationKind)
                            .SelectMany(grouping => grouping.Select(pointer => new SetReference(grouping.Key, pointer))), data[word].LexicalCategory);
                    data[word] = newSet;
                } else {
                    data[word] = synset;
                }
            }
        }

        private ISet<string> SearchFor(string search) {
            try {
                var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var verbRoots = VerbMorpher.FindRoots(search);
                VerbSynSet containingSet = null;
                foreach (var root in verbRoots) {
                    VerbSynSet tryGetVal;
                    if (data.TryGetValue(root, out tryGetVal)) { containingSet = tryGetVal; } else {
                        try {
                            containingSet = data.First(kv => kv.Value.Words.Contains(root)).Value;
                        }
                        catch (InvalidOperationException) { }
                    }
                    result.UnionWith(containingSet != null ?
                        containingSet.ReferencedIndeces
                             .SelectMany(id => { VerbSynSet temp; return setsBySetID.TryGetValue(id, out temp) ? temp.ReferencedIndeces : Enumerable.Empty<int>(); })
                             .Select(s => { VerbSynSet temp; return setsBySetID.TryGetValue(s, out temp) ? temp : null; })
                             .Where(s => s != null)
                             .Where(s => s.LexicalCategory == containingSet.LexicalCategory)
                             .SelectMany(s => s.Words.SelectMany(w => VerbMorpher.GetConjugations(w))).Append(root) : new[] { search });
                }
                return result;
            }
            catch (KeyNotFoundException) {
            }
            return new HashSet<string>(new[] { search }, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Retrives the synonyms of the given verb as an ISet of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public ISet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }

        /// <summary>
        /// Retrives the synonyms of the given Verb as an ISet of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>A collection of strings containing all of the synonyms of the given Verb.</returns>
        public ISet<string> this[Verb search] {
            get {
                return this[search.Text];
            }
        }
        private string filePath;
        private ConcurrentDictionary<int, VerbSynSet> setsBySetID = new ConcurrentDictionary<int, VerbSynSet>();
        private ConcurrentDictionary<string, VerbSynSet> data = new ConcurrentDictionary<string, VerbSynSet>();
        private const string WORD_REGEX = @"\b[A-Za-z-_]{2,}";
        private const string POINTER_REGEX = @"\D{1,2}\s*[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+";
        private const int HEADER_LENGTH = 29;

        // Provides an indexed lookup between the values of the VerbPointerSymbol enum and their corresponding string representation in WordNet data.verb files.
        private static readonly IReadOnlyDictionary<string, VerbSetRelationship> interSetRelationshipMap = new Dictionary<string, VerbSetRelationship> {
            { "!", VerbSetRelationship. Antonym }, 
            { "@", VerbSetRelationship.Hypernym },
            { "~", VerbSetRelationship.Hyponym },
            { "*", VerbSetRelationship.Entailment },
            { ">", VerbSetRelationship.Cause },
            { "^", VerbSetRelationship. AlsoSee },
            { "$", VerbSetRelationship.Verb_Group },
            { "+", VerbSetRelationship.DerivationallyRelatedForm },
            { ";c", VerbSetRelationship.DomainOfSynset_TOPIC },
            { ";r", VerbSetRelationship.DomainOfSynset_REGION },
            { ";u", VerbSetRelationship.DomainOfSynset_USAGE}
        };
        /// <summary>
        /// Defines the broad lexical categories assigned to Verbs in the WordNet system.
        /// </summary>
        public enum Category : byte
        {
            /// <summary>
            /// Body
            /// </summary>
            Body = 29,
            /// <summary>
            /// Cognition
            /// </summary>
            Cognition,
            /// <summary>
            /// Communication
            /// </summary>
            Communication,
            /// <summary>
            /// Competition
            /// </summary>
            Competition,
            /// <summary>
            /// Consumption
            /// </summary>
            Consumption,
            /// <summary>
            /// Contact
            /// </summary>
            Contact,
            /// <summary>
            /// Creation
            /// </summary>
            Creation,
            /// <summary>
            /// Emotion
            /// </summary>
            Emotion,
            /// <summary>
            /// Motion
            /// </summary>
            Motion,
            /// <summary>
            /// Perception
            /// </summary>
            Perception,
            /// <summary>
            /// Possession
            /// </summary>
            Possession,
            /// <summary>
            /// Social
            /// </summary>
            Social,
            /// <summary>
            /// Stative
            /// </summary>
            Stative,
            /// <summary>
            /// Weather
            /// </summary>
            Weather,
        }
    }
}