using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using LASI.Core.Interop;
using LASI.Utilities;

namespace LASI.Core.Heuristics.WordNet
{
    using Enumerable;
    using EventArgs = ResourceLoadEventArgs;
    using Link = NounLink;
    using SetReference = KeyValuePair<NounLink, int>;

    internal sealed class NounLookup : WordNetLookup<Noun>
    {
        /// <summary>
        /// Initializes a new instance of the NounProvider class.
        /// </summary>
        /// <param name="path">
        /// The path of the WordNet database file containing the synonym data for nouns.
        /// </param>
        public NounLookup(string path) {
            filePath = path;

            //InitCategoryGroupsDictionary();
        }

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        internal override void Load() {
            var indexedSynsets = LoadData()
                .Zip(Range(1, TOTAL_LINES), (line, i) => new { Set = CreateSet(line), LineNumber = i });
            try {
                indexedSynsets.ToObservable()
                    .Throttle(TimeSpan.FromMilliseconds(40))
                    .Subscribe(
                        onNext: e => OnReport(new EventArgs("Loaded Noun Data - Set: \{e.LineNumber} / \{TOTAL_LINES}", 10 * (e.LineNumber / TOTAL_LINES))),
                        onCompleted: () => OnReport(new EventArgs("Noun Data Loaded", 0)),
                        onError: e => {
                            e.LogIfDebug();
                        });
                foreach (var set in from e in indexedSynsets select e.Set) {
                    setsById[set.Id] = set;
                }
            } catch (Exception e) {
                e.LogIfDebug();
                throw;
            }
        }

        private static NounSynSet CreateSet(string fileLine) {
            string line = fileLine.Substring(0, fileLine.IndexOf('|')).Replace('_', '-');

            var referencedSets = from Match match in POINTER_REGEX.Matches(line)
                                 let split = match.Value.SplitRemoveEmpty(' ')
                                 where split.Length > 1 && consideredSetLinks.Contains(linkMap[split[0]])
                                 select new SetReference(
                                    key: linkMap[split[0]],
                                    value: int.Parse(split[1])
                                 );

            return new NounSynSet(
                id: int.Parse(line.Substring(0, 8)),
                words: from Match m in WORD_REGEX.Matches(line) select m.Value,
                category: (NounCategory)int.Parse(line.Substring(9, 2)),
                pointerRelationships: referencedSets
            );
        }

        private IEnumerable<string> LoadData() {
            using (var reader = new StreamReader(File.Open(path: filePath, mode: FileMode.Open, access: FileAccess.Read))) {
                for (int i = 0; i < FILE_HEADER_LINE_COUNT; ++i) {
                    reader.ReadLine();
                }
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine()) {
                    yield return line;
                }
            }
        }

        private IImmutableSet<string> SearchFor(string word) {
            var containingSet = setsById.Values.FirstOrDefault(set => set.ContainsWord(word));
            if (containingSet != null) {
                try {
                    List<string> results = new List<string>();
                    SearchSubsets(containingSet, results, new HashSet<NounSynSet>());
                    return results.ToImmutableHashSet(IgnoreCase);
                } catch (InvalidOperationException e) {
                    Output.WriteLine(string.Format("{0} was thrown when attempting to get synonyms for word {1}: , containing set: {2}", e, word, containingSet));
                }
            }
            return ImmutableHashSet<string>.Empty;
        }

        private void SearchSubsets(NounSynSet containingSet, List<string> results, HashSet<NounSynSet> setsSearched) {
            results.AddRange(containingSet.Words);
            results.AddRange(containingSet[Link.HypERnym].Where(set => setsById.ContainsKey(set)).SelectMany(set => setsById[set].Words));
            setsSearched.Add(containingSet);
            foreach (var set in containingSet.ReferencedSet
                .Except(containingSet[Link.HypERnym])
                .Select(pointer => { NounSynSet result; setsById.TryGetValue(pointer, out result); return result; })) {
                if (set != null && set.Category == containingSet.Category && !setsSearched.Contains(set)) {
                    SearchSubsets(set, results, setsSearched);
                }
            }
        }

        internal override IImmutableSet<string> this[string search] {
            get {
                var morpher = new NounMorpher();
                try {
                    return SearchFor(morpher.FindRoot(search))
                        .SelectMany(morpher.GetLexicalForms)
                        .DefaultIfEmpty(search)
                        .ToImmutableHashSet();
                } catch (AggregateException e) { e.LogIfDebug(); } catch (InvalidOperationException e) { e.LogIfDebug(); }
                return this[search];
            }
        }

        internal override IImmutableSet<string> this[Noun search] {
            get { return this[search.Text]; }
        }

        private const int TOTAL_LINES = 82114;

        private static readonly IImmutableSet<Link> consideredSetLinks = ImmutableHashSet.Create(
             Link.MemberOfThisDomain_REGION,
             Link.MemberOfThisDomain_TOPIC,
             Link.MemberOfThisDomain_USAGE,
             Link.DomainOfSynset_REGION,
             Link.DomainOfSynset_TOPIC,
             Link.DomainOfSynset_USAGE,
             Link.HypOnym,
             Link.InstanceHypOnym,
             Link.InstanceHypERnym,
             Link.HypERnym
        );

        // Provides an indexed lookup between the values of the Noun enum and their corresponding
        // string representation in WordNet data.noun files.
        private static readonly IReadOnlyDictionary<string, Link> linkMap = new Dictionary<string, Link>
        {
            ["!"] = Link.Antonym,
            ["@"] = Link.HypERnym,
            ["@i"] = Link.InstanceHypERnym,
            ["~"] = Link.HypOnym,
            ["~i"] = Link.InstanceHypOnym,
            ["#m"] = Link.MemberHolonym,
            ["#s"] = Link.SubstanceHolonym,
            ["#p"] = Link.PartHolonym,
            ["%m"] = Link.MemberMeronym,
            ["%s"] = Link.SubstanceMeronym,
            ["%p"] = Link.PartMeronym,
            ["="] = Link.Attribute,
            ["+"] = Link.DerivationallyRelatedForm,
            [";c"] = Link.DomainOfSynset_TOPIC,
            ["-c"] = Link.MemberOfThisDomain_TOPIC,
            [";r"] = Link.DomainOfSynset_REGION,
            ["-r"] = Link.MemberOfThisDomain_REGION,
            [";u"] = Link.DomainOfSynset_USAGE,
            ["-u"] = Link.MemberOfThisDomain_USAGE
        };

        private static readonly Regex POINTER_REGEX = new Regex(@"\D{1,2}\s*\d{8}", RegexOptions.Compiled);
        private static readonly Regex WORD_REGEX = new Regex(@"(?<word>[A-Za-z_\-\']{3,})", RegexOptions.Compiled);
        private string filePath;
        private ConcurrentDictionary<NounCategory, List<NounSynSet>> lexicalGoups = new ConcurrentDictionary<NounCategory, List<NounSynSet>>();
        private ConcurrentDictionary<int, NounSynSet> setsById = new ConcurrentDictionary<int, NounSynSet>(Concurrency.Max, TOTAL_LINES);
    }
}