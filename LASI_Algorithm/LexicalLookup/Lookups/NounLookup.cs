using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LASI.Utilities;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;


namespace LASI.Algorithm.LexicalLookup.Lookups
{
    using SetReference = System.Collections.Generic.KeyValuePair<NounSetRelationship, int>;
    using LASI.Algorithm.LexicalLookup.InterSetRelationshipManagement;
    internal sealed class NounLookup : IWordNetLookup<Noun>
    {
        private const int HEADER_LENGTH = 29;
        /// <summary>
        /// Initializes a new instance of the NounProvider class.
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the synonym data for nouns.</param>
        public NounLookup(string path) {
            filePath = path;
        }
        //ConcurrentDictionary<int, HashSet<NounSynSet>> cachedSetTraverals = new ConcurrentDictionary<int, HashSet<NounSynSet>>();
        HashSet<NounSynSet> allSets = new HashSet<NounSynSet>();
        Dictionary<int, NounSynSet> data = new Dictionary<int, NounSynSet>();
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

        private void InsertSetData(NounSynSet set) {
            allSets.Add(set);
            data.Add(set.ID, set);
            lexicalGoups[set.LexName].Add(set);
        }

        private NounSynSet CreateSet(string fileLine) {

            string line = fileLine.Substring(0, fileLine.IndexOf('|'));

            IEnumerable<SetReference> referencedSets =
                from match in Regex.Matches(line, pointerRegex).Cast<Match>()
                let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                where split.Count() > 1 && IncludeReference(RelationshipMap[split[0]])
                select new SetReference(RelationshipMap[split[0]], Int32.Parse(split[1]));


            IEnumerable<string> words = from Match match in Regex.Matches(line, wordRegex)
                                        select match.Value.Replace('_', '-');

            int id = Int32.Parse(line.Substring(0, 8));

            NounCategory lexCategory = (NounCategory)Int32.Parse(line.Substring(9, 2));

            return new NounSynSet(id, words, referencedSets, lexCategory);
        }

        private const string wordRegex = @"(?<word>[A-Za-z_\-\']{3,})";


        private const string pointerRegex = @"\D{1,2}\s*\d{8}";





        private ISet<string> SearchFor(string word) {
            var containingSet = allSets.FirstOrDefault(set => set.Words.Contains(word));
            if (containingSet != null) {
                try {
                    List<string> results = new List<string>();
                    SearchSubsets(containingSet, results, new HashSet<NounSynSet>());
                    return new HashSet<string>(results);
                }
                catch (InvalidOperationException e) {
                    Output.WriteLine(string.Format("{0} was thrown when attempting to get synonyms for word {1}: , containing set: {2}", e, word, containingSet));

                    return new HashSet<string>();
                }
            }
            else {
                return new HashSet<string>();
            }
        }

        private void SearchSubsets(NounSynSet containingSet, List<string> results, HashSet<NounSynSet> setsSearched) {
            results.AddRange(containingSet.Words);
            results.AddRange(containingSet[NounSetRelationship.HypERnym].Where(set => data.ContainsKey(set)).SelectMany(set => data[set].Words));
            setsSearched.Add(containingSet);
            foreach (var set in containingSet.ReferencedIndexes.Except(containingSet[NounSetRelationship.HypERnym]).Select(pointer => data.ContainsKey(pointer) ? data[pointer] : null)) {
                if (set != null && set.LexName == containingSet.LexName && !setsSearched.Contains(set)) {
                    SearchSubsets(set, results, setsSearched);
                }
            }
        }

        public ISet<string> this[string search] {
            get {
                try {
                    return new HashSet<string>(SearchFor(NounConjugator.FindRoot(search)).SelectMany(syn => NounConjugator.GetLexicalForms(syn)));
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

        private Dictionary<NounCategory, List<NounSynSet>> lexicalGoups = new Dictionary<NounCategory, List<NounSynSet>>
        {
            { NounCategory.Tops, new List<NounSynSet>() },
            { NounCategory.Act,new List<NounSynSet>() },
            { NounCategory.Animal,new List<NounSynSet>() },
            { NounCategory.Artifact,new List<NounSynSet>() },
            { NounCategory.Attribute,new List<NounSynSet>() },
            { NounCategory.Body,new List<NounSynSet>() },
            { NounCategory.Cognition,new List<NounSynSet>() },
            { NounCategory.Communication,new List<NounSynSet>() },
            { NounCategory.Event,new List<NounSynSet>() },
            { NounCategory.Feeling,new List<NounSynSet>() },
            { NounCategory.Food,new List<NounSynSet>() },
            { NounCategory.Group,new List<NounSynSet>() },
            { NounCategory.Location,new List<NounSynSet>() },
            { NounCategory.Motive,new List<NounSynSet>() },
            { NounCategory.Object,new List<NounSynSet>() },
            { NounCategory.Person,new List<NounSynSet>() },
            { NounCategory.Phenomenon,new List<NounSynSet>() },
            { NounCategory.Plant,new List<NounSynSet>() },
            { NounCategory.Possession,new List<NounSynSet>() },
            { NounCategory.Process,new List<NounSynSet>() },
            { NounCategory.Quantity,new List<NounSynSet>() },
            { NounCategory.Relation,new List<NounSynSet>() },
            { NounCategory.Shape,new List<NounSynSet>() },
            { NounCategory.State,new List<NounSynSet>() },
            { NounCategory.Substance,new List<NounSynSet>() },
            { NounCategory.Time,new List<NounSynSet>() }
        };

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

        private static readonly NounPointerSymbolMap RelationshipMap = new NounPointerSymbolMap();

        private string filePath;

        public async System.Threading.Tasks.Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }


    }
}
