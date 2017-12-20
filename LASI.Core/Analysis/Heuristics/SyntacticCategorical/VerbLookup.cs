using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LASI.Utilities;

namespace LASI.Core.Heuristics.WordNet
{
    using System.Collections.Immutable;
    using System.Reactive.Linq;
    using Analysis.Heuristics.WordMorphing;
    using Configuration;
    using static Enumerable;
    using EventArgs = ResourceLoadEventArgs;
    using LinkType = VerbLink;
    using SetReference = KeyValuePair<VerbLink, int>;

    internal sealed class VerbLookup : WordNetLookup<Verb>
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for verbals.</param>
        public VerbLookup(string path) => filePath = path;

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        internal override void Load()
        {
            OnReport(new EventArgs("Parsing File", 0));
            OnReport(new EventArgs("Mapping Verb Sets", 0));
            foreach (var (line, index) in LoadData())
            {
                var set = CreateSet(line);
                LinkSynset(set);
                if (index % progressModulus == 0)
                {
                    OnReport(new EventArgs(string.Format(progressFormat, index), progressAmmount));
                }
            }
            OnReport(new EventArgs("Mapped Verb Sets", 1));
        }

        private IEnumerable<(string line, int index)> LoadData()
        {
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                for (var i = 0; i < LinesInHeader; ++i)
                {
                    reader.ReadLine();
                }
                for (var (line, lineNumber) = (reader.ReadLine(), 0); line != null; (line, lineNumber) = (reader.ReadLine(), lineNumber + 1))
                {
                    yield return (line, lineNumber);
                }
            }
        }

        private static VerbSynset CreateSet(string setLine)
        {
            var line = setLine.Substring(0, setLine.IndexOf('|'));

            var referencedSets =
                from Match m in RelationshipRegex.Matches(line)
                let segments = m.Value.SplitRemoveEmpty(' ')
                where segments.Length > 1
                select new SetReference(InterSetMap[segments[0]], int.Parse(segments[1]));

            var words = from Match m in WordRegex.Matches(line)
                        select m.Value.Replace('_', ' ');
            var id = int.Parse(line.Substring(0, 8));
            var category = (VerbCategory)int.Parse(line.Substring(9, 2));
            return new VerbSynset(id, words, referencedSets, category);
        }

        private void LinkSynset(VerbSynset set)
        {
            setsById[set.Id] = set;
            foreach (var word in set.Words)
            {
                setsByWord.AddOrUpdate(
                    key: word,
                    addValue: set,
                    updateValueFactory: (key, value) => new VerbSynset(value.Id,
                        value.Words.Union(set.Words),
                        value.RelatedSetIdsByRelationKind.Union(set.RelatedSetIdsByRelationKind).SelectMany(x => x.Select(e => new SetReference(x.Key, e))),
                        value.Category)
                    );
            }
        }

        private IImmutableSet<string> SearchFor(string search)
        {
            var setBuilder = ImmutableHashSet.CreateBuilder(System.StringComparer.OrdinalIgnoreCase);
            var verbRoots = VerbMorpher.FindRoots(search);
            setBuilder.UnionWith(verbRoots.AsParallel().SelectMany(root =>
            {
                VerbSynset containingSet;
                setsByWord.TryGetValue(root, out containingSet);
                containingSet = containingSet ?? setsByWord.Where(set => set.Value.ContainsWord(root)).Select(kv => kv.Value).FirstOrDefault();
                return (from setId in containingSet?[TraversedLinks]
                        let outerSet = setsById.GetValueOrDefault(setId)
                        where outerSet != null
                        let set = outerSet[TraversedLinks]
                        //.SelectMany(id => setsById.GetValueOrDefault(id) is VerbSynset set ? set[TraversedLinks] : Empty<int>())
                        select setsById.GetValueOrDefault(setId) into set
                        where set != null
                        from word in set.Words
                        from conjugation in VerbMorpher.GetConjugations(word)
                        select conjugation
                        ).Concat(VerbMorpher.GetConjugations(root))
                    ?? new[] { search };
            }));
            return setBuilder.ToImmutable();
        }

        /// <summary>
        /// Retrieves the synonyms of the given verb as an ISet of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        internal override IImmutableSet<string> this[string search] => SearchFor(search);

        /// <summary>
        /// Retrieves the synonyms of the given Verb as an ISet of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>A collection of strings containing all of the synonyms of the given Verb.</returns>
        internal override IImmutableSet<string> this[Verb search] => this[search.Text];

        private const int TOTAL_LINES = 13766;

        /// <summary>
        /// A report will be propagated for every 1 in 138 sets roughly 100 updates will take place.
        /// </summary>
        const int progressModulus = 138;

        const double progressAmmount = 100 / (100d * progressModulus);
        const string progressFormat = "Mapping Verb Set {0} / 13766";

        private readonly string filePath;

        private ConcurrentDictionary<int, VerbSynset> setsById = new ConcurrentDictionary<int, VerbSynset>(
            concurrencyLevel: Concurrency.Max,
            capacity: 30000
        );

        private ConcurrentDictionary<string, VerbSynset> setsByWord = new ConcurrentDictionary<string, VerbSynset>(
            concurrencyLevel: Concurrency.Max,
            capacity: 30000,
            comparer: System.StringComparer.OrdinalIgnoreCase
        );

        private static readonly Regex WordRegex = new Regex(@"\b[A-Za-z-_]{2,}", RegexOptions.Compiled);

        private static readonly Regex RelationshipRegex = new Regex(@"\D{1,2}\s*[\d]{8}[\s].[\s][0]{4,}", RegexOptions.Compiled);

        private const uint SetCount = 13797;

        private static readonly LinkType[] TraversedLinks =
        {
             LinkType.Hypernym,
            //LinkType.Hyponym,
            LinkType.AlsoSee,
            LinkType.Verb_Group,
            LinkType.DerivationallyRelatedForm,
            LinkType. Entailment,
            LinkType.Cause,
            LinkType.DomainOfSynset_USAGE
        };

        // Provides an indexed lookup between the values of the VerbPointerSymbol enumerations and their corresponding string representation in WordNet data.verb files.
        private static readonly IReadOnlyDictionary<string, LinkType> InterSetMap = new Dictionary<string, LinkType>
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
