using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LASI.Core.Heuristics.WordNet
{
    using SetReference = KeyValuePair<AdverbLink, int>;
    using Link = AdverbLink;
    using System.Collections.Immutable;
    using LASI.Utilities;
    using LASI.Core.Analysis.Heuristics.WordMorphing;

    internal sealed class AdverbLookup : WordNetLookup<Adverb>
    {
        /// <summary>
        /// Initializes a new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for adverbs.</param>
        /// <param name="adjectiveLookup">
        /// The an instance of <code>WordNetLookup&lt;Adjective&gt;</code> to be queried for related adjectives.
        /// </param>
        public AdverbLookup(string path, WordNetLookup<Adjective> adjectiveLookup)
        {
            filePath = path;
            this.adjectiveLookup = adjectiveLookup;
        }

        HashSet<AdverbSynset> allSets = new HashSet<AdverbSynset>();
        System.Collections.Concurrent.ConcurrentDictionary<int, AdverbSynset> setsById = new System.Collections.Concurrent.ConcurrentDictionary<int, WordNet.AdverbSynset>(8, 100000);
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
                        var set = CreateSet(line); setsById[set.Id] = set;
                    }
                    catch (KeyNotFoundException e)
                    {
                        Logger.Log($"An error occured when Loading the {GetType().Name}: {e.Message}\r\n{e.StackTrace}");
                        throw;
                    }
                }
            }
        }


        private static AdverbSynset CreateSet(string fileLine)
        {

            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from Match match in Regex.Matches(line, pointerRegex)
                                 let split = match.Value.SplitRemoveEmpty(' ')
                                 where split.Count() > 1 && interSetMap.ContainsKey(split[0])
                                 select new SetReference(interSetMap[split[0]], int.Parse(split[1]));

            var words = from Match match in Regex.Matches(line, wordRegex)
                        select match.Value.Replace('_', ' ');

            var id = int.Parse(line.Substring(0, 8));

            return new AdverbSynset(id, words, referencedSets, AdverbCategory.All);
        }

        private IImmutableSet<string> SearchFor(string search)
        {
            var containingSet = setsById.First(entry => entry.Value.ContainsWord(search));
            //var root = conjugator.FindRoot(search);
            return ImmutableHashSet.CreateRange(StringComparer,
                from setId in containingSet.Value.ReferencedSets
                let relatedAdjectives = (adjectiveLookup as AdjectiveLookup).SearchByAdverbId(setId)

                from word in relatedAdjectives
                from form in conjugator.GetLexicalForms(word)
                select form).Add(search);
        }

        internal override IImmutableSet<string> this[string search] => SearchFor(search);

        internal override IImmutableSet<string> this[Adverb search] => this[search.Text];

        AdverbMorpher conjugator = new AdverbMorpher();
        private string filePath;
        private const string pointerRegex = @"\D{1,2}\s*\d{8}";
        private const string wordRegex = @"(?<word>[A-Za-z_\-\']{3,})";

        /// <summary>
        /// Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adv files.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, Link> interSetMap = new Dictionary<string, Link>
        {
            ["!"] = Link.Antonym,
            [@"\"] = Link.DerivedFromAdjective,
            [";c"] = Link.DomainOfSynset_TOPIC,
            [";r"] = Link.DomainOfSynset_REGION,
            [";u"] = Link.DomainOfSynset_USAGE
        };
        private readonly WordNetLookup<Adjective> adjectiveLookup;
    }

}

