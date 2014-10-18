using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.Heuristics.WordNet
{
    using SetReference = KeyValuePair<VerbLink, int>;
    using LinkType = VerbLink;
    using Interop;
    using System.Reactive.Linq;

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
                sets.ToObservable()
                    .Sample(TimeSpan.FromMilliseconds(1))
                    .Subscribe(set => OnReport(new ResourceLoadEventArgs("Processed Verb Synset " + set.Id, 0)));

                OnReport(new ResourceLoadEventArgs("Mapping Sets", 0));
                foreach (var set in sets) { LinkSynset(set); }
                OnReport(new ResourceLoadEventArgs("Loaded", 0));
            }
        }


        private static VerbSynSet CreateSet(string setLine) {
            var line = setLine.Substring(0, setLine.IndexOf('|')).Replace('_', '-');

            var referencedSets =
                from Match m in RELATIONSHIP_REGEX.Matches(line)
                let segments = m.Value.SplitRemoveEmpty(' ')
                where segments.Length >= 3
                select new SetReference(interSetMap[segments[0]], int.Parse(segments[1]));

            var words = from Match m in WORD_REGEX.Matches(line.Substring(17)) select m.Value;
            var id = int.Parse(line.Substring(0, 8));
            var category = (VerbCategory)int.Parse(line.Substring(9, 2));
            return new VerbSynSet(id, words, referencedSets, category);
        }


        private void LinkSynset(VerbSynSet set) {
            setsById[set.Id] = set;
            foreach (var word in set.Words) {
                VerbSynSet extantSet;
                if (setsByWord.TryGetValue(word, out extantSet)) {
                    extantSet.Words.UnionWith(set.Words);
                    extantSet.ReferencedSetIds.UnionWith(set.ReferencedSetIds);
                } else {
                    setsByWord[word] = set;
                }
            }
        }
        private ISet<string> SearchFor(string search) {
            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var verbRoots = VerbMorpher.FindRoots(search);
            result.UnionWith(new HashSet<string>(verbRoots.AsParallel().SelectMany(root => {
                VerbSynSet containingSet;
                setsByWord.TryGetValue(root, out containingSet);
                containingSet = containingSet ?? setsByWord.Where(set => set.Value.ContainsWord(root)).Select(kv => kv.Value).FirstOrDefault();
                return containingSet == null ? new[] { search } :
                    containingSet[LinkType.Verb_Group, LinkType.Hypernym]
                         .SelectMany(id => { VerbSynSet set; return setsById.TryGetValue(id, out set) ? set[LinkType.Verb_Group, LinkType.Hypernym] : Enumerable.Empty<int>(); })
                         .Select(id => { VerbSynSet referenced; return setsById.TryGetValue(id, out referenced) ? referenced : null; })
                         .Where(set => set != null)
                         .SelectMany(set => set.Words.SelectMany(w => VerbMorpher.GetConjugations(w)))
                         .Append(root);
            })));
            return result;
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
        private ConcurrentDictionary<int, VerbSynSet> setsById = new ConcurrentDictionary<int, VerbSynSet>(
            concurrencyLevel: Concurrency.Max,
            capacity: 30000
        );
        private ConcurrentDictionary<string, VerbSynSet> setsByWord = new ConcurrentDictionary<string, VerbSynSet>(
            concurrencyLevel: Concurrency.Max,
            capacity: 30000,
            comparer: StringComparer.OrdinalIgnoreCase
        );
        /// <summary>
        /// The regular expression describes a string which
        /// starts at a word(in the regex sense of word) boundary: \b
        /// consisting of any combination of alpha, underscore, and minus(dash), that is at least 2 characters in length: [A-Za-z-_]{2,}
        /// </summary>
        private static readonly Regex WORD_REGEX = new Regex(@"\b[A-Za-z-_]{2,}", RegexOptions.Compiled);
        /// <summary>
        /// The regular expression describes a string which 
        /// starts with at least one but not more than two non-digit characters (matches the pointer symbol): \D{1,2}
        /// followed by 0 or more whitespace characters: \s*
        /// followed by 8 decimal digits (matches the related set id): [\d]{8}
        /// followed by a single space: [\s]
        /// followed by any single character (matches the syntactic category of the relationship n, v, a, s, r): .
        /// followed by a single space: [\s]
        /// ends with the sequence 0000 (indicates that the source/target relationship between to the set is semantic as opposed to lexical): [0]{4,}
        /// </summary>
        private static readonly Regex RELATIONSHIP_REGEX = new Regex(@"\D{1,2}\s*[\d]{8}[\s].[\s][0]{4,}", RegexOptions.Compiled);

        /// <summary>
        /// The number of in the WordNet file data.verb which contains the textual Synset data for verbs.
        /// </summary>
        private const uint SET_COUNT = 13797;
        // Provides an indexed lookup between the values of the VerbPointerSymbol enum and their corresponding string representation in WordNet data.verb files.
        private static readonly IReadOnlyDictionary<string, LinkType> interSetMap = new Dictionary<string, LinkType> {
            { "!", LinkType.Antonym },
            { "@", LinkType.Hypernym },
            { "~", LinkType.Hyponym },
            { "*", LinkType.Entailment },
            { ">", LinkType.Cause },
            { "^", LinkType.AlsoSee },
            { "$", LinkType.Verb_Group },
            { "+", LinkType.DerivationallyRelatedForm },
            { ";c", LinkType.DomainOfSynset_TOPIC },
            { ";r", LinkType.DomainOfSynset_REGION },
            { ";u", LinkType.DomainOfSynset_USAGE }
        };
    }
    
}