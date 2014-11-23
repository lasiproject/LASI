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

    internal sealed class AdjectiveLookup : WordNetLookup<Adjective>
    {
        /// <summary>
        /// Initializes a new instance of the AdjectiveThesaurus class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for adjectives.</param>
        public AdjectiveLookup(string path) {

            filePath = path;
        }

        ISet<AdjectiveSynSet> allSets = new HashSet<AdjectiveSynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        internal override void Load() {
            using (StreamReader reader = new StreamReader(filePath)) {
                foreach (var line in reader.ReadToEnd().SplitRemoveEmpty('\n').Skip(FILE_HEADER_LINE_COUNT)) {
                    try { allSets.Add(CreateSet(line)); }
                    catch (KeyNotFoundException e) {
                        Output.WriteLine("An error occured when Loading the {0}: {1}\r\n{2}", GetType().Name, e.Message, e.StackTrace);
                    }
                }
            }

        }
        private static AdjectiveSynSet CreateSet(string fileLine) {
            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from Match match in Regex.Matches(line, POINTER_REGEX)
                                 let split = match.Value.SplitRemoveEmpty(' ')
                                 where split.Count() > 1 && interSetMap.ContainsKey(split[0])
                                 select new SetReference(interSetMap[split[0]], int.Parse(split[1]));


            IEnumerable<string> words = from Match match in Regex.Matches(line, WORD_REGEX)
                                        select match.Value.Replace('_', '-');
            int id = int.Parse(line.Substring(0, 8), System.Globalization.CultureInfo.InvariantCulture);

            AdjectiveCategory lexCategory = (AdjectiveCategory)int.Parse(line.Substring(9, 2));
            return new AdjectiveSynSet(id, words, referencedSets, lexCategory);



        }

        private IImmutableSet<string> SearchFor(string search) {
            //gets words of searched word
            return allSets.AsParallel()
                .Where(set => set.ContainsWord(search))
                .SelectMany(set => set.Words)
                .ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
        }
        internal override IImmutableSet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }
        internal override IImmutableSet<string> this[Adjective search] {
            get {
                return this[search.Text];
            }
        }


        private string filePath;
        private const string POINTER_REGEX = @"\D{1,2}\s*\d{8}";
        private const string WORD_REGEX = @"(?<word>[A-Za-z_\-\']{3,})";

        /// <summary>
        /// Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adj files.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, Link> interSetMap = new Dictionary<string, Link> {
            {"!",Link.Antonym},
            {"&",Link.SimilarTo},
            {"<",Link.ParticipleOfVerb},
            {@"\", Link.Pertainym_pertains_to_noun},
            {"=",Link.Attribute},
            {"^",Link.AlsoSee},
            {";c", Link.DomainOfSynset_TOPIC},
            {";r", Link.DomainOfSynset_REGION},
            {";u", Link.DomainOfSynset_USAGE}
        };

    }

}


