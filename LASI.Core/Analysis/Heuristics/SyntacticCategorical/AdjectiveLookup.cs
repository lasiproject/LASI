using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace LASI.Core.Heuristics.WordNet
{
    using SetReference = KeyValuePair<AdjectiveLink, int>;
    using Link = AdjectiveLink;
    using System.Collections.Immutable;
    using Utilities;
    using LASI.Core.Analysis.Heuristics.WordMorphing;
    using LASI.Core.Configuration;
    using System.Collections.Concurrent;

    internal sealed class AdjectiveLookup : WordNetLookup<Adjective>
    {
        /// <summary>
        /// Initializes a new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for adjectives.</param>
        public AdjectiveLookup(string path)
        {
            this.filePath = path;
            this.formGenerator = new AdjectiveMorpher();
        }


        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        internal override void Load()
        {
            using (var reader = new StreamReader(filePath))
            {
                foreach (var line in reader.ReadToEnd().SplitRemoveEmpty('\n').Skip(LinesInHeader))
                {
                    try
                    {
                        var set = CreateSet(line);
                        setsById[set.Id] = set;
                    }
                    catch (KeyNotFoundException e)
                    {
                        Logger.Log("An error occurred when Loading the {0}: {1}\r\n{2}", GetType().Name, e.Message, e.StackTrace);
                    }
                }
            }
        }
        private static AdjectiveSynset CreateSet(string fileLine)
        {
            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from Match match in Regex.Matches(line, PointerRegex)
                                 let split = match.Value.SplitRemoveEmpty(' ')
                                 where split.Length > 1 && interSetMap.ContainsKey(split[0])
                                 select new SetReference(interSetMap[split[0]], int.Parse(split[1]));


            var words = from Match match in Regex.Matches(line, WORD_REGEX1)
                                        select match.Value.Replace('_', ' ');
            var id = int.Parse(line.Substring(0, 8), System.Globalization.CultureInfo.InvariantCulture);

            var lexCategory = (AdjectiveCategory)int.Parse(line.Substring(9, 2));
            return new AdjectiveSynset(id, words, referencedSets, lexCategory);
        }

        private IImmutableSet<string> SearchFor(string search) =>
            setsById.AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max)
                .Where(pair => pair.Value.ContainsWord(search))
                .SelectMany(pair => pair.Value.Words.Concat(pair.Value.ReferencedSets.Select(id => setsById[id]).SelectMany(s => s.Words))
                .SelectMany(formGenerator.GetLexicalForms))
                .ToImmutableHashSet(System.StringComparer.OrdinalIgnoreCase);

        internal override IImmutableSet<string> this[string search] => SearchFor(search);
        public IEnumerable<string> SearchByAdverbId(int setId) => setsById[setId].Words.SelectMany(SearchFor);
        internal override IImmutableSet<string> this[Adjective search] => this[search.Text];

        private string filePath;
        private const string PointerRegex = @"\D{1,2}\s*\d{8}";
        private const string WordRegex = @"(?<word>[A-Za-z_\-\']{3,})";
        //ISet<AdjectiveSynset> allSets = new HashSet<AdjectiveSynset>();
        ConcurrentDictionary<int, AdjectiveSynset> setsById = new ConcurrentDictionary<int, AdjectiveSynset>(concurrencyLevel: 8, capacity: 18154);
        /// <summary>
        /// Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adj files.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, Link> interSetMap = new Dictionary<string, Link>
        {
            ["!"] = Link.Antonym,
            ["&"] = Link.SimilarTo,
            ["<"] = Link.ParticipleOfVerb,
            [@"\"] = Link.Pertainym_pertains_to_noun,
            ["="] = Link.Attribute,
            ["^"] = Link.AlsoSee,
            [";c"] = Link.DomainOfSynset_TOPIC,
            [";r"] = Link.DomainOfSynset_REGION,
            [";u"] = Link.DomainOfSynset_USAGE
        };
        private readonly AdjectiveMorpher formGenerator;

        public static string WORD_REGEX1 => WordRegex;
    }
}


