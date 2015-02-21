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
    using InteropBindings;
    using System.Reactive.Linq;
    using System.Collections.Immutable;
    using EventArgs = ResourceLoadEventArgs;
    using LASI.Core.Analysis.Heuristics.WordMorphing;

    internal sealed class VerbLookup : WordNetLookup<Verb>
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class. 
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for verbals.</param>
        public VerbLookup(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary> 
        internal override void Load()
        {
            //using (var reader = new StreamReader(filePath)) {
            OnReport(new EventArgs("Parsing File", 0));
            OnReport(new EventArgs("Mapping Verb Sets", 0));
            foreach (var indexed in LoadData()
                    //.AsParallel()
                    //.Select((line, i) => new { Line = line, Index = i })

                    )
            {
                var set = CreateSet(indexed.Item1);
                LinkSynset(set);
                if (indexed.Item2 % PROGRESS_MODULUS == 0)
                {
                    OnReport(new EventArgs(string.Format(PROGRESS_FORMAT, indexed.Item2), PROGRESS_AMOUNT));
                }
            }
            OnReport(new EventArgs("Mapped Verb Sets", 1));
            //}
        }

        private IEnumerable<Tuple<string, int>> LoadData()
        {
            using (var reader = new StreamReader(File.Open(path: filePath, mode: FileMode.Open, access: FileAccess.Read)))
            {
                for (int i = 0; i < FILE_HEADER_LINE_COUNT; ++i)
                {
                    reader.ReadLine();
                }
                int lineNumber = 0;
                for (var line = reader.ReadLine(); line != null; ++lineNumber, line = reader.ReadLine())
                {
                    yield return Tuple.Create(line, lineNumber);
                }
            }
        }
        private static VerbSynSet CreateSet(string setLine)
        {
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


        private void LinkSynset(VerbSynSet set)
        {
            setsById[set.Id] = set;
            foreach (var word in set.Words)
            {
                VerbSynSet extantSet;
                if (setsByWord.TryGetValue(word, out extantSet))
                {
                    extantSet.Words.UnionWith(set.Words);
                    extantSet.ReferencedSet.UnionWith(set.ReferencedSet);
                }
                else
                {
                    setsByWord[word] = set;
                }
            }
        }
        private IImmutableSet<string> SearchFor(string search)
        {
            var setBuilder = ImmutableHashSet.CreateBuilder(StringComparer.OrdinalIgnoreCase);
            var verbRoots = VerbMorpher.FindRoots(search);
            setBuilder.UnionWith(verbRoots.AsParallel().SelectMany(root =>
            {
                VerbSynSet containingSet;
                setsByWord.TryGetValue(root, out containingSet);
                containingSet = containingSet ?? setsByWord.Where(set => set.Value.ContainsWord(root)).Select(kv => kv.Value).FirstOrDefault();
                return containingSet == null ? new[] { search } :
                    containingSet[LinkType.Verb_Group, LinkType.Hypernym]
                         .SelectMany(id => { VerbSynSet set; return setsById.TryGetValue(id, out set) ? set[LinkType.Verb_Group, LinkType.Hypernym] : Enumerable.Empty<int>(); })
                         .Select(id => { VerbSynSet referenced; return setsById.TryGetValue(id, out referenced) ? referenced : null; })
                         .Where(set => set != null)
                         .SelectMany(set => set.Words.SelectMany(w => VerbMorpher.GetConjugations(w)))
                         .Concat(VerbMorpher.GetConjugations(root));
            }));
            return setBuilder.ToImmutable();
        }

        /// <summary>
        /// Retrieves the synonyms of the given verb as an ISet of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        internal override IImmutableSet<string> this[string search]
        {
            get
            {
                return SearchFor(search);
            }
        }

        /// <summary>
        /// Retrieves the synonyms of the given Verb as an ISet of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>A collection of strings containing all of the synonyms of the given Verb.</returns>
        internal override IImmutableSet<string> this[Verb search]
        {
            get
            {
                return this[search.Text];
            }
        }
        private const int TOTAL_LINES = 13766;
        /// <summary>
        /// A report will be propagated for every 1 in 138 sets rougly 100 updates will take place.
        /// </summary>
        private const int PROGRESS_MODULUS = 138;
        private const double PROGRESS_AMOUNT = 100 / (100d * PROGRESS_MODULUS);
        private const string PROGRESS_FORMAT = "Mapping Verb Set {0} / 13766";

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
        private static readonly IReadOnlyDictionary<string, LinkType> interSetMap = new Dictionary<string, LinkType>
        {
            ["!"] = LinkType.Antonym,
            ["@"] = LinkType.Hypernym,
            ["~"] = LinkType.Hyponym,
            ["*"] = LinkType.Entailment,
            [">"] = LinkType.Cause,
            ["^"] = LinkType.AlsoSee,
            ["$"] = LinkType.Verb_Group,
            ["+"] = LinkType.DerivationallyRelatedForm,
            [";c"] = LinkType.DomainOfSynset_TOPIC,
            [";r"] = LinkType.DomainOfSynset_REGION,
            [";u"] = LinkType.DomainOfSynset_USAGE
        };
    }

}