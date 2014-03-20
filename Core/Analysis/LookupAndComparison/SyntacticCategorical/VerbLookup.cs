using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.Heuristics
{
    using LASI.Core.Heuristics.Morphemization;
    using SetReference = System.Collections.Generic.KeyValuePair<VerbSetLink, int>;

    internal sealed class VerbLookup : WordNetLookup<Verb>
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class. 
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for verbals.</param>
        public VerbLookup(string path) {
            filePath = path;
        }
        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary> 
        internal override void Load() {
            using (var reader = new StreamReader(filePath)) {
                OnReport(new ResourceLoadEventArgs("Parsing File", 0));
                var sets = reader.ReadToEnd().SplitRemoveEmpty('\n')
                    .Skip(HEADER_LENGTH)
                    .AsParallel()
                    .Select(line => CreateSet(line));
                OnReport(new ResourceLoadEventArgs("Mapping Sets", 0));

                foreach (var set in sets)
                    LinkSynset(set);
                OnReport(new ResourceLoadEventArgs("Loaded", 0));

            }
        }


        private static VerbSynSet CreateSet(string fileLine) {
            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from Match M in POINTER_REGEX.Matches(line)
                                 let split = M.Value.SplitRemoveEmpty(' ')
                                 where split.Count() > 1
                                 select new SetReference(interSetMap[split[0]], Int32.Parse(split[1]));

            var words = from Match ContainedWord in WORD_REGEX.Matches(line.Substring(17))
                        select ContainedWord.Value.Replace('_', '-').ToLower();
            var id = Int32.Parse(line.Substring(0, 8));
            var lexCategory = (Category)Int32.Parse(line.Substring(9, 2));
            return new VerbSynSet(id, words, referencedSets, lexCategory);
        }


        private void LinkSynset(VerbSynSet synset) {
            setsBySetID[synset.Id] = synset;
            foreach (var word in synset.Words) {
                data[word] = data.ContainsKey(word) ?
                new VerbSynSet(data[word].Id,
                    data[word].Words.Concat(synset.Words),
                    data[word].RelatedSetsByRelationKind
                    .Concat(synset.RelatedSetsByRelationKind)
                    .SelectMany(grouping => grouping.Select(pointer => new SetReference(grouping.Key, pointer))), synset.LexicalCategory)

            : data[word] = synset;
            }
        }

        private ISet<string> SearchFor(string search) {

            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var verbRoots = VerbMorpher.FindRoots(search);

            result.UnionWith(verbRoots.AsParallel().SelectMany(root => {

                VerbSynSet containingSet;
                data.TryGetValue(root, out containingSet);

                containingSet = containingSet ??
                data.Where(kv => kv.Value.Words.Contains(root)).Select(kv => kv.Value).FirstOrDefault();

                return containingSet != null ?
                    containingSet.ReferencedIndeces
                         .SelectMany(id => { VerbSynSet temp; return setsBySetID.TryGetValue(id, out temp) ? temp.ReferencedIndeces : Enumerable.Empty<int>(); }).Select(id => { VerbSynSet temp; return setsBySetID.TryGetValue(id, out temp) ? temp : null; })
                         .Where(set => set != null)
                         .Where(set => set.LexicalCategory == containingSet.LexicalCategory)
                         .SelectMany(set => set.Words.SelectMany(w => VerbMorpher.GetConjugations(w)))
                         .Append(root) : new[] { search };
            }));
            return result;

            //return new HashSet<string>(new[] { search }, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Retrives the synonyms of the given verb as an ISet of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        internal override ISet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }

        /// <summary>
        /// Retrives the synonyms of the given Verb as an ISet of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>A collection of strings containing all of the synonyms of the given Verb.</returns>
        internal override ISet<string> this[Verb search] {
            get {
                return this[search.Text];
            }
        }
        private string filePath;
        private ConcurrentDictionary<int, VerbSynSet> setsBySetID = new ConcurrentDictionary<int, VerbSynSet>(concurrencyLevel: 100, capacity: 30000);
        private ConcurrentDictionary<string, VerbSynSet> data = new ConcurrentDictionary<string, VerbSynSet>(concurrencyLevel: 100, capacity: 30000);
        private static readonly Regex WORD_REGEX = new Regex(@"\b[A-Za-z-_]{2,}", RegexOptions.Compiled);
        private static readonly Regex POINTER_REGEX = new Regex(@"\D{1,2}\s*[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+", RegexOptions.Compiled);

        // Provides an indexed lookup between the values of the VerbPointerSymbol enum and their corresponding string representation in WordNet data.verb files.
        private static readonly IReadOnlyDictionary<string, VerbSetLink> interSetMap = new Dictionary<string, VerbSetLink> {
            { "!", VerbSetLink. Antonym }, 
            { "@", VerbSetLink.Hypernym },
            { "~", VerbSetLink.Hyponym },
            { "*", VerbSetLink.Entailment },
            { ">", VerbSetLink.Cause },
            { "^", VerbSetLink. AlsoSee },
            { "$", VerbSetLink.Verb_Group },
            { "+", VerbSetLink.DerivationallyRelatedForm },
            { ";c", VerbSetLink.DomainOfSynset_TOPIC },
            { ";r", VerbSetLink.DomainOfSynset_REGION },
            { ";u", VerbSetLink.DomainOfSynset_USAGE}
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