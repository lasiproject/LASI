using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LASI.Core.Configuration;
using LASI.Utilities;

namespace LASI.Core.Heuristics.WordNet
{
    using Analysis.Heuristics.WordMorphing;
    using static Enumerable;
    using EventArgs = ResourceLoadEventArgs;
    using Link = NounLink;
    using SetReference = KeyValuePair<NounLink, int>;

    internal sealed class NounLookup : WordNetLookup<Noun>
    {
        /// <summary>
        /// Initializes a new instance of the NounProvider class. 
        /// </summary>
        /// <param name="path"> The path of the WordNet database file containing the synonym data for nouns. </param>
        public NounLookup(string path) => filePath = path;

        /// <summary>
        /// Parses the contents of the underlying WordNet database file. 
        /// </summary>
        internal override void Load()
        {
            var setsEnumerated = 0;
            //var setsSampled = 0;
            var indexedSynsets = LoadData()
                .Zip(Range(1, TotalLines)).With((line, i) => new { Set = CreateSet(line), LineNumber = i });
            try
            {
                foreach (var indexed in indexedSynsets)
                {
                    ++setsEnumerated;
                    setsById[indexed.Set.Id] = indexed.Set;
                    if (indexed.LineNumber % 821 == 0)
                    {
                        OnReport(new EventArgs($"Loaded Noun Data - Set: {indexed.LineNumber} / {TotalLines}", ProgressAmount * 2));
                    }
                }
            }
            catch (Exception e)
            {
                e.Log();
                throw;
            }
        }

        static NounSynset CreateSet(string rawSynset)
        {
            var line = rawSynset.Substring(0, rawSynset.IndexOf('|'));

            var referencedSets = from Match match in PointerRegex.Matches(line)
                                 let split = match.Value.SplitRemoveEmpty(' ')
                                 where split.Length > 1
                                 let linkKind = LinkMap[split[0]]
                                 where consideredSetLinks.Contains(linkKind)
                                 let referenced = int.Parse(split[1])
                                 select new SetReference(linkKind, referenced);
            return new NounSynset(
                id: int.Parse(line.Substring(0, 8)),
                words: from Match match in WordRegex.Matches(line)
                       select match.Value.Replace('_', ' '),
                category: (NounCategory)int.Parse(line.Substring(9, 2)),
                pointerRelationships: referencedSets
            );
        }

        IEnumerable<string> LoadData()
        {
            using (var reader = new StreamReader(path: filePath))
            {
                for (var i = 0; i < LinesInHeader; ++i)
                {
                    reader.ReadLine();
                }
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    yield return line;
                }
            }
        }

        IImmutableSet<string> SearchFor(string word)
        {
            var containingSets = setsById.Values.Where(set => set.ContainsWord(word));
            if (containingSets != null)
            {
                try
                {
                    var results = new List<string>();
                    foreach (var set in containingSets)
                    {
                        SearchSubsets(set, results, new HashSet<NounSynset>());
                    }
                    return results.ToImmutableHashSet(StringComparer);
                }
                catch (InvalidOperationException e)
                {
                    Logger.Log($"{e.Message} was thrown when attempting to get synonyms for word {word}: , containing set: {containingSets}");
                }
            }
            return ImmutableHashSet<string>.Empty;
        }

        void SearchSubsets(NounSynset containingSet, List<string> results, HashSet<NounSynset> setsSearched)
        {
            results.AddRange(containingSet.Words);
            results.AddRange(containingSet[Link.Hypernym].Where(setsById.ContainsKey).SelectMany(set => setsById[set].Words));
            setsSearched.Add(containingSet);
            foreach (var set in containingSet.ReferencedSets
                .Except(containingSet[Link.Hypernym])
                .Select(setsById.GetValueOrDefault)
                .NonNull())
            {
                if (set != null && set.Category == containingSet.Category && !setsSearched.Contains(set))
                {
                    SearchSubsets(set, results, setsSearched);
                }
            }
        }

        const double ProgressAmount = 100 / (211 * 100d);

        const int TotalLines = 82114;

        static readonly IImmutableSet<Link> consideredSetLinks = ImmutableHashSet.Create(
            Link.MemberOfThisDomain_REGION,
            Link.MemberOfThisDomain_TOPIC,
            Link.MemberOfThisDomain_USAGE,
            Link.DomainOfSynset_REGION,
            Link.DomainOfSynset_TOPIC,
            Link.DomainOfSynset_USAGE,
            Link.Hyponym,
            Link.InstanceHyponym,
            Link.InstanceHypernym,
            Link.Hypernym
        );

        // Provides an indexed lookup between the values of the Noun enum and their corresponding string representation in WordNet data.noun files.
        static readonly IReadOnlyDictionary<string, Link> LinkMap = new Dictionary<string, Link>
        {
            ["!"] = Link.Antonym,
            ["@"] = Link.Hypernym,
            ["@i"] = Link.InstanceHypernym,
            ["~"] = Link.Hyponym,
            ["~i"] = Link.InstanceHyponym,
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

        static readonly Regex PointerRegex = new Regex(@"\D{1,2}\s*\d{8}", RegexOptions.Compiled);

        static readonly Regex WordRegex = new Regex(@"(?<word>[A-Za-z_\-\']{3,})", RegexOptions.Compiled);

        string filePath;

        ConcurrentDictionary<int, NounSynset> setsById = new ConcurrentDictionary<int, NounSynset>(Concurrency.Max, TotalLines);

        internal override IImmutableSet<string> this[string search]
        {
            get
            {
                var morpher = new NounMorpher();
                try
                {
                    return SearchFor(morpher.FindRoot(search))
                        .SelectMany(morpher.GetLexicalForms)
                        .DefaultIfEmpty(search)
                        .ToImmutableHashSet();
                }
                catch (Exception e)
                {
                    e.Log();
                }
                return this[search];
            }
        }

        internal override IImmutableSet<string> this[Noun search] => this[search.Text];
    }
}
