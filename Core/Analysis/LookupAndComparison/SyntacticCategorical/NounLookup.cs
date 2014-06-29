using LASI.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LASI.Core.Heuristics
{
    using SetReference = KeyValuePair<NounSetLink, int>;
    using Morphemization;
    using LASI.Core.Interop;

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
                                     select new KeyValuePair<int, NounSynSet>(set.Id, set)) {
                    setsById[item.Key] = item.Value;
                }
            }
        }

        private static NounSynSet CreateSet(string fileLine) {

            string line = fileLine.Substring(0, fileLine.IndexOf('|')).Replace('_', '-');

            IEnumerable<SetReference> referencedSets =
                from Match match in POINTER_REGEX.Matches(line)
                let split = match.Value.SplitRemoveEmpty(' ')
                where split.Count() > 1 && IncludeReference(interSetMap[split[0]])
                select new SetReference(interSetMap[split[0]], int.Parse(split[1], System.Globalization.CultureInfo.InvariantCulture));


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
            results.AddRange(containingSet[NounSetLink.HypERnym].Where(set => setsById.ContainsKey(set)).SelectMany(set => setsById[set].Words));
            setsSearched.Add(containingSet);
            foreach (var set in containingSet.ReferencedSetIds
                .Except(containingSet[NounSetLink.HypERnym])
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

        private static bool IncludeReference(NounSetLink referenceRelationship) {
            return
                referenceRelationship == NounSetLink.MemberOfThisDomain_REGION ||
                referenceRelationship == NounSetLink.MemberOfThisDomain_TOPIC ||
                referenceRelationship == NounSetLink.MemberOfThisDomain_USAGE ||
                referenceRelationship == NounSetLink.DomainOfSynset_REGION ||
                referenceRelationship == NounSetLink.DomainOfSynset_TOPIC ||
                referenceRelationship == NounSetLink.DomainOfSynset_USAGE ||
                referenceRelationship == NounSetLink.HypOnym ||
                referenceRelationship == NounSetLink.InstanceHypOnym ||
                referenceRelationship == NounSetLink.InstanceHypERnym ||
                referenceRelationship == NounSetLink.HypERnym;
        }



        private static readonly Regex WORD_REGEX = new Regex(@"(?<word>[A-Za-z_\-\']{3,})", RegexOptions.Compiled);
        private static readonly Regex POINTER_REGEX = new Regex(@"\D{1,2}\s*\d{8}", RegexOptions.Compiled);
        // Provides an indexed lookup between the values of the Noun enum and their corresponding string representation in WordNet data.noun files.
        private static readonly IReadOnlyDictionary<string, NounSetLink> interSetMap = new Dictionary<string, NounSetLink> {
            {"!",   NounSetLink.Antonym },
            {"@", NounSetLink.HypERnym },
            {"@i", NounSetLink.InstanceHypERnym},
            {"~", NounSetLink.HypOnym},
            {"~i", NounSetLink.InstanceHypOnym},
            {"#m", NounSetLink.MemberHolonym},
            {"#s", NounSetLink.SubstanceHolonym},
            {"#p", NounSetLink.PartHolonym},
            {"%m", NounSetLink.MemberMeronym},
            {"%s", NounSetLink.SubstanceMeronym},
            {"%p", NounSetLink.PartMeronym},
            {"=", NounSetLink.Attribute},
            {"+", NounSetLink.DerivationallyRelatedForm},
            {";c", NounSetLink.DomainOfSynset_TOPIC},
            {"-c", NounSetLink.MemberOfThisDomain_TOPIC},
            {";r", NounSetLink.DomainOfSynset_REGION},
            {"-r", NounSetLink.MemberOfThisDomain_REGION},
            {";u", NounSetLink.DomainOfSynset_USAGE},
            {"-u", NounSetLink.MemberOfThisDomain_USAGE}
        };
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
