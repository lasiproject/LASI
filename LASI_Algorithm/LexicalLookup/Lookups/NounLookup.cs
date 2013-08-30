using LASI.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LASI.Algorithm.Lookup.InterSetRelationshipManagement;

namespace LASI.Algorithm.Lookup
{
    using SetReference = System.Collections.Generic.KeyValuePair<NounSetRelationship, int>;
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

        HashSet<NounSynSet> allSets = new HashSet<NounSynSet>();

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
            allSets.Add(set);
            data[set.ID] = set;
            lexicalGoups[set.LexName].Add(set);
        }

        private NounSynSet CreateSet(string fileLine) {

            string line = fileLine.Substring(0, fileLine.IndexOf('|'));

            IEnumerable<SetReference> referencedSets =
                from match in Regex.Matches(line, POINTER_REGEX).Cast<Match>()
                let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                where split.Count() > 1 && IncludeReference(RelationshipMap[split[0]])
                select new SetReference(RelationshipMap[split[0]], Int32.Parse(split[1]));


            IEnumerable<string> words = from Match match in Regex.Matches(line, WORD_REGEX)
                                        select match.Value.Replace('_', '-');

            int id = Int32.Parse(line.Substring(0, 8));

            NounCategory lexCategory = (NounCategory)Int32.Parse(line.Substring(9, 2));

            return new NounSynSet(id, words, referencedSets, lexCategory);
        }



        private ISet<string> SearchFor(string word) {
            var containingSet = data.Values.FirstOrDefault(set => set.Words.Contains(word));
            if (containingSet != null) {
                try {
                    List<string> results = new List<string>();
                    SearchSubsets(containingSet, results, new HashSet<NounSynSet>());
                    return new HashSet<string>(results);
                } catch (InvalidOperationException e) {
                    Output.WriteLine(string.Format("{0} was thrown when attempting to get synonyms for word {1}: , containing set: {2}", e, word, containingSet));

                }
            }
            return new HashSet<string>();
        }


        public ISet<string> this[string search] {
            get {
                try {
                    return new HashSet<string>(SearchFor(NounConjugator.FindRoot(search)).SelectMany(syn => NounConjugator.GetLexicalForms(syn)));
                } catch (AggregateException) { } catch (InvalidOperationException) { }
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
            foreach (var set in containingSet.ReferencedIndexes.Except(containingSet[NounSetRelationship.HypERnym]).Select(pointer => data.ContainsKey(pointer) ? data[pointer] : null)) {
                if (set != null && set.LexName == containingSet.LexName && !setsSearched.Contains(set)) {
                    SearchSubsets(set, results, setsSearched);
                }
            }
        }
        private ConcurrentDictionary<NounCategory, List<NounSynSet>> lexicalGoups = new ConcurrentDictionary<NounCategory, List<NounSynSet>>();
        private void InitCategoryGroupsDictionary() {
            foreach (NounCategory e in Enum.GetValues(typeof(NounCategory))) {
                lexicalGoups[e] = new List<NounSynSet>();
            }
        }



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



        private const string WORD_REGEX = @"(?<word>[A-Za-z_\-\']{3,})";


        private const string POINTER_REGEX = @"\D{1,2}\s*\d{8}";

    }
}
