using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LASI.Core.Heuristics.WordNet
{
    using SetReference = KeyValuePair<NounLink, int>;
    using LASI.Core.Interop;
    using System.Collections.Immutable;
    using LASI.Utilities;

    internal sealed class NounLookup : WordNetLookup<Noun>
    {
        /// <summary>
        /// Initializes a new instance of the NounProvider class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for nouns.</param>
        public NounLookup(string path) {
            filePath = path;
            //InitCategoryGroupsDictionary();
        }

        ConcurrentDictionary<int, NounSynSet> setsById;

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        internal override void Load() {
            using (StreamReader reader = new StreamReader(filePath)) {
                for (int i = 0;
                i < HEADER_LENGTH;
                ++i) {
                    reader.ReadLine();
                }

                setsById = new ConcurrentDictionary<int, NounSynSet>(concurrencyLevel: Concurrency.Max, capacity: 90000);
                foreach (var item in from line in reader.ReadToEnd().SplitRemoveEmpty('\r', '\n').AsParallel()
                                     let set = CreateSet(line)
                                     select KeyValuePair.Create(set.Id, set)) {
                    setsById[item.Key] = item.Value;
                }
            }
        }

        private static NounSynSet CreateSet(string fileLine) {

            string line = fileLine.Substring(0, fileLine.IndexOf('|')).Replace('_', '-');

            IEnumerable<SetReference> referencedSets =
                from Match match in POINTER_REGEX.Matches(line)
                let split = match.Value.SplitRemoveEmpty(' ')
                where split.Count() > 1 && consideredSetLinks.Contains(linkMap[split[0]])
                select new SetReference(linkMap[split[0]], int.Parse(split[1], System.Globalization.CultureInfo.InvariantCulture));


            IEnumerable<string> words = from Match m in WORD_REGEX.Matches(line)
                                        select m.Value;

            int id = int.Parse(line.Substring(0, 8));

            NounCategory lexCategory = (NounCategory)int.Parse(line.Substring(9, 2));

            return new NounSynSet(id, words, referencedSets, lexCategory);
        }



        private ISet<string> SearchFor(string word) {
            var containingSet = setsById.Values.FirstOrDefault(set => set.Words.Contains(word));
            if (containingSet != null) {
                try {
                    List<string> results = new List<string>();
                    SearchSubsets(containingSet, results, new HashSet<NounSynSet>());
                    return new HashSet<string>(results);
                }
                catch (InvalidOperationException e) {
                    Output.WriteLine(string.Format("{0} was thrown when attempting to get synonyms for word {1}: , containing set: {2}", e, word, containingSet));

                }
            }
            return new HashSet<string>();
        }


        public ISet<string> AllNouns { get { return allNouns = allNouns ?? new HashSet<string>(setsById.SelectMany(nss => nss.Value.Words)); } }


        internal override ISet<string> this[string search] {

            get {
                var morpher = new NounMorpher();
                try {
                    return new HashSet<string>(SearchFor(morpher.FindRoot(search))
                        .SelectMany(syn => morpher.GetLexicalForms(syn))
                        .DefaultIfEmpty(search));
                }
                catch (AggregateException) { }
                catch (InvalidOperationException) { }
                return this[search];
            }
        }


        internal override ISet<string> this[Noun search] {
            get {
                return this[search.Text];
            }
        }


        private void SearchSubsets(NounSynSet containingSet, List<string> results, HashSet<NounSynSet> setsSearched) {
            results.AddRange(containingSet.Words);
            results.AddRange(containingSet[NounLink.HypERnym].Where(set => setsById.ContainsKey(set)).SelectMany(set => setsById[set].Words));
            setsSearched.Add(containingSet);
            foreach (var set in containingSet.ReferencedSetIds
                .Except(containingSet[NounLink.HypERnym])
                .Select(pointer => { NounSynSet result; setsById.TryGetValue(pointer, out result); return result; })) {
                if (set != null && set.Category == containingSet.Category && !setsSearched.Contains(set)) {
                    SearchSubsets(set, results, setsSearched);
                }
            }
        }

        private void InitCategoryGroupsDictionary() {
            foreach (NounCategory e in Enum.GetValues(typeof(NounCategory))) {
                lexicalGoups[e] = new List<NounSynSet>();
            }
        }

        private ConcurrentDictionary<NounCategory, List<NounSynSet>> lexicalGoups = new ConcurrentDictionary<NounCategory, List<NounSynSet>>();

        private string filePath;

        private HashSet<string> allNouns;

        private static readonly Regex WORD_REGEX = new Regex(@"(?<word>[A-Za-z_\-\']{3,})", RegexOptions.Compiled);
        private static readonly Regex POINTER_REGEX = new Regex(@"\D{1,2}\s*\d{8}", RegexOptions.Compiled);
        // Provides an indexed lookup between the values of the Noun enum and their corresponding string representation in WordNet data.noun files.
        private static readonly IReadOnlyDictionary<string, NounLink> linkMap = new Dictionary<string, NounLink> {
            { "!",  NounLink.Antonym },
            { "@",  NounLink.HypERnym },
            { "@i", NounLink.InstanceHypERnym },
            { "~",  NounLink.HypOnym },
            { "~i", NounLink.InstanceHypOnym },
            { "#m", NounLink.MemberHolonym },
            { "#s", NounLink.SubstanceHolonym },
            { "#p", NounLink.PartHolonym },
            { "%m", NounLink.MemberMeronym },
            { "%s", NounLink.SubstanceMeronym },
            { "%p", NounLink.PartMeronym },
            { "=",  NounLink.Attribute },
            { "+",  NounLink.DerivationallyRelatedForm },
            { ";c", NounLink.DomainOfSynset_TOPIC },
            { "-c", NounLink.MemberOfThisDomain_TOPIC },
            { ";r", NounLink.DomainOfSynset_REGION },
            { "-r", NounLink.MemberOfThisDomain_REGION },
            { ";u", NounLink.DomainOfSynset_USAGE },
            { "-u", NounLink.MemberOfThisDomain_USAGE }
        };
        private static readonly IImmutableSet<NounLink> consideredSetLinks = ImmutableHashSet.Create(
             NounLink.MemberOfThisDomain_REGION,
             NounLink.MemberOfThisDomain_TOPIC,
             NounLink.MemberOfThisDomain_USAGE,
             NounLink.DomainOfSynset_REGION,
             NounLink.DomainOfSynset_TOPIC,
             NounLink.DomainOfSynset_USAGE,
             NounLink.HypOnym,
             NounLink.InstanceHypOnym,
             NounLink.InstanceHypERnym,
             NounLink.HypERnym
        );


    }
    /// <summary>
    /// Defines the broad lexical categories assigned to Nouns in the WordNet system.
    /// </summary>
    public enum NounCategory : byte
    {
        /// <summary>
        /// Tops
        /// </summary>
        Tops = 3,
        /// <summary>
        /// Act
        /// </summary>
        Act,
        /// <summary>
        /// Animal
        /// </summary>
        Animal,
        /// <summary>
        /// Artifact
        /// </summary>
        Artifact,
        /// <summary>
        /// Attribute
        /// </summary>
        Attribute,
        /// <summary>
        /// Body
        /// </summary>
        Body,
        /// <summary>
        /// Cognition
        /// </summary>
        Cognition,
        /// <summary>
        /// Communication
        /// </summary>
        Communication,
        /// <summary>
        /// Event
        /// </summary>
        Event,
        /// <summary>
        /// Feeling
        /// </summary>
        Feeling,
        /// <summary>
        /// Food
        /// </summary>
        Food,
        /// <summary>
        /// Group
        /// </summary>
        Group,
        /// <summary>
        /// Location
        /// </summary>
        Location,
        /// <summary>
        /// Motive
        /// </summary>
        Motive,
        /// <summary>
        /// Object
        /// </summary>
        Object,
        /// <summary>
        /// Person
        /// </summary>
        Person,
        /// <summary>
        /// Phenomenon
        /// </summary>
        Phenomenon,
        /// <summary>
        /// Plant
        /// </summary>
        Plant,
        /// <summary>
        /// Possession
        /// </summary>
        Possession,
        /// <summary>
        /// Process
        /// </summary>
        Process,
        /// <summary>
        /// Quantity
        /// </summary>
        Quantity,
        /// <summary>
        /// Relation
        /// </summary>
        Relation,
        /// <summary>
        /// Shape
        /// </summary>
        Shape,
        /// <summary>
        /// State
        /// </summary>
        State,
        /// <summary>
        /// Substance
        /// </summary>
        Substance,
        /// <summary>
        /// Time
        /// </summary>
        Time,


    }
}
