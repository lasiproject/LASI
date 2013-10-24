using LASI.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LASI.Algorithm.ComparativeHeuristics
{
    using SetReference = System.Collections.Generic.KeyValuePair<NounSetRelationship, int>;
    using LASI.Algorithm.ComparativeHeuristics.Morphemization;
    internal sealed class NounLookup : IWordNetLookup<Noun>
    {
        private const int HEADER_LENGTH = 29;
        /// <summary>
        /// Initializes a new instance of the NounProvider class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for nouns.</param>
        public NounLookup(string path) {
            filePath = path;
            InitCategoryGroupsDictionary();
        }

        SortedDictionary<int, NounSynSet> data = new SortedDictionary<int, NounSynSet>();

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public void Load() {
            using (StreamReader reader = new StreamReader(filePath)) {
                for (int i = 0; i < HEADER_LENGTH; ++i) {
                    reader.ReadLine();
                }
                while (!reader.EndOfStream) {
                    InsertSetData(CreateSet(reader.ReadLine()));
                }
            }
        }
        public async System.Threading.Tasks.Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }
        private void InsertSetData(NounSynSet set) {
            data[set.Id] = set;
            lexicalGoups[set.LexicalCategory].Add(set);
        }

        private static NounSynSet CreateSet(string fileLine) {

            string line = fileLine.Substring(0, fileLine.IndexOf('|'));

            IEnumerable<SetReference> referencedSets =
                from match in Regex.Matches(line, POINTER_REGEX).Cast<Match>()
                let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                where split.Count() > 1 && IncludeReference(interSetRelationshipMap[split[0]])
                select new SetReference(interSetRelationshipMap[split[0]], Int32.Parse(split[1], System.Globalization.CultureInfo.InvariantCulture));


            IEnumerable<string> words = from Match match in Regex.Matches(line, WORD_REGEX)
                                        select match.Value.Replace('_', '-');

            int id = Int32.Parse(line.Substring(0, 8));

            Category lexCategory = (Category)Int32.Parse(line.Substring(9, 2));

            return new NounSynSet(id, words, referencedSets, lexCategory);
        }



        private ISet<string> SearchFor(string word) {
            var containingSet = data.Values.FirstOrDefault(set => set.Words.Contains(word));
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


        public ISet<string> AllNouns { get { return allNouns = allNouns ?? new SortedSet<string>(data.SelectMany(nss => nss.Value.Words)); } }

        public ISet<string> this[string search] {
            get {
                try {
                    return new HashSet<string>(SearchFor(NounMorpher.FindRoot(search)).SelectMany(syn => NounMorpher.GetLexicalForms(syn)).DefaultIfEmpty(search));
                }
                catch (AggregateException) { }
                catch (InvalidOperationException) { }
                return this[search];
            }
        }


        public ISet<string> this[Noun search] {
            get {
                return this[search.Text];
            }
        }


        private void SearchSubsets(NounSynSet containingSet, List<string> results, HashSet<NounSynSet> setsSearched) {
            results.AddRange(containingSet.Words);
            results.AddRange(containingSet[NounSetRelationship.HypERnym].Where(set => data.ContainsKey(set)).SelectMany(set => data[set].Words));
            setsSearched.Add(containingSet);
            foreach (var set in containingSet.ReferencedIndeces.Except(containingSet[NounSetRelationship.HypERnym]).Select(pointer => { NounSynSet result; data.TryGetValue(pointer, out result); return result; })) {
                if (set != null && set.LexicalCategory == containingSet.LexicalCategory && !setsSearched.Contains(set)) {
                    SearchSubsets(set, results, setsSearched);
                }
            }
        }



        private void InitCategoryGroupsDictionary() {
            foreach (Category e in Enum.GetValues(typeof(Category))) {
                lexicalGoups[e] = new List<NounSynSet>();
            }
        }

        private ConcurrentDictionary<Category, List<NounSynSet>> lexicalGoups = new ConcurrentDictionary<Category, List<NounSynSet>>();

        private string filePath;

        private SortedSet<string> allNouns;

        private static bool IncludeReference(NounSetRelationship referenceRelationship) {
            return
                referenceRelationship == NounSetRelationship.MemberOfThisDomain_REGION ||
                referenceRelationship == NounSetRelationship.MemberOfThisDomain_TOPIC ||
                referenceRelationship == NounSetRelationship.MemberOfThisDomain_USAGE ||
                referenceRelationship == NounSetRelationship.DomainOfSynset_REGION ||
                referenceRelationship == NounSetRelationship.DomainOfSynset_TOPIC ||
                referenceRelationship == NounSetRelationship.DomainOfSynset_USAGE ||
                referenceRelationship == NounSetRelationship.HypOnym ||
                referenceRelationship == NounSetRelationship.InstanceHypOnym ||
                referenceRelationship == NounSetRelationship.InstanceHypERnym ||
                referenceRelationship == NounSetRelationship.HypERnym;
        }

        private const string WORD_REGEX = @"(?<word>[A-Za-z_\-\']{3,})";


        private const string POINTER_REGEX = @"\D{1,2}\s*\d{8}";
        // Provides an indexed lookup between the values of the Noun enum and their corresponding string representation in WordNet data.noun files.
        private static readonly IReadOnlyDictionary<string, NounSetRelationship> interSetRelationshipMap = new Dictionary<string, NounSetRelationship>{ 
            { "!", NounSetRelationship.Antonym },
            { "@", NounSetRelationship.HypERnym },
            { "@i", NounSetRelationship.InstanceHypERnym },
            { "~", NounSetRelationship.HypOnym },
            { "~i", NounSetRelationship.InstanceHypOnym },
            { "#m", NounSetRelationship.MemberHolonym },
            { "#s", NounSetRelationship.SubstanceHolonym },
            { "#p", NounSetRelationship.PartHolonym },
            { "%m", NounSetRelationship.MemberMeronym },
            { "%s", NounSetRelationship.SubstanceMeronym },
            { "%p", NounSetRelationship.PartMeronym },
            { "=", NounSetRelationship.Attribute },
            { "+", NounSetRelationship.DerivationallyRelatedForm },
            { ";c", NounSetRelationship.DomainOfSynset_TOPIC },
            { "-c", NounSetRelationship.MemberOfThisDomain_TOPIC },
            { ";r", NounSetRelationship.DomainOfSynset_REGION },
            { "-r", NounSetRelationship.MemberOfThisDomain_REGION },
            { ";u", NounSetRelationship.DomainOfSynset_USAGE },
            { "-u", NounSetRelationship.MemberOfThisDomain_USAGE }
        };
        /// <summary>
        /// Defines the broad lexical categories assigned to Nouns in the WordNet system.
        /// </summary>
        public enum Category : byte
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
}
