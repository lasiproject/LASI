using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LASI.Utilities;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;


namespace LASI.Algorithm.Lookup
{
    using SetReference = System.Collections.Generic.KeyValuePair<NounSetRelationship, int>;
    internal class NounThesaurus : SynonymLookup
    {
        /// <summary>
        /// Initializes a new instance of the NounProvider class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the synonym data for nouns.</param>
        public NounThesaurus(string filePath)
            : base(filePath) {
            FilePath = filePath;

        }
        //ConcurrentDictionary<int, HashSet<NounSynSet>> cachedSetTraverals = new ConcurrentDictionary<int, HashSet<NounSynSet>>();
        HashSet<NounSynSet> allSets = new HashSet<NounSynSet>();
        Dictionary<int, NounSynSet> data = new Dictionary<int, NounSynSet>();
        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {

            using (StreamReader reader = new StreamReader(FilePath)) {
                for (int i = 0; i < HEADER_LENGTH; ++i) {
                    reader.ReadLine();
                }
                while (!reader.EndOfStream) {
                    var set = CreateSet(reader.ReadLine());
                    InsertSetData(set);
                }
            }


        }

        private void InsertSetData(NounSynSet set) {
            allSets.Add(set);
            data.Add(set.ID, set);
            lexicalGoups[set.LexName].Add(set);
        }

        private NounSynSet CreateSet(string line) {


            var setLine = line.Substring(0, line.IndexOf('|'));

            var referencedSets = from match in Regex.Matches(setLine, @"\D{1,2}\s*\d{8}").Cast<Match>()
                                 let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                 where split.Count() > 1
                                 let pointer = new SetReference(RelationshipMap[split[0]], Int32.Parse(split[1]))
                                 where IncludeReference(pointer.Key)
                                 select pointer;

            IEnumerable<string> words = from match in Regex.Matches(setLine, @"(?<word>[A-Za-z_\-\']{3,})").Cast<Match>()
                                        select match.Value.Replace('_', '-');

            int id = Int32.Parse(setLine.Substring(0, 8));

            NounCategory lexCategory = ( NounCategory )Int32.Parse(setLine.Substring(9, 2));

            return new NounSynSet(id, words, referencedSets, lexCategory);

        }




        private ISet<string> SearchFor(string word) {

            var containingSet = allSets.FirstOrDefault(set => set.Words.Contains(word));
            if (containingSet == null)
                return new HashSet<string>();
            var lexname = containingSet.LexName;
            List<string> results = new List<string>();
            try {
                SearchSubsets(containingSet, results, new HashSet<NounSynSet>(), lexname);
            }
            catch (InvalidOperationException e) {
                Output.WriteLine(string.Format("InvalidOperationException {0} was thrown in attempting to search for synonyms. Search word {1}: , containing set: {2}", e, word, containingSet));
            }
            return new HashSet<string>(results);
        }

        private void SearchSubsets(NounSynSet containingSet, List<string> results, HashSet<NounSynSet> setsSearched, NounCategory lexname) {
            results.AddRange(containingSet.Words);
            results.AddRange(from set in containingSet[NounSetRelationship.HypERnym]
                             where data.ContainsKey(set)
                             from w in data[set].Words
                             select w);
            if (!setsSearched.Contains(containingSet)) {

                setsSearched.Add(containingSet);
                foreach (var set in containingSet.ReferencedIndexes.Except(containingSet[NounSetRelationship.HypERnym]).Select(p =>
                  data.ContainsKey(p) ? data[p] : null)) {
                    if (set != null && set.LexName == lexname && !setsSearched.Contains(set)) {
                        SearchSubsets(set, results, setsSearched, lexname);
                    }


                }
            }
        }


        public override ISet<string> this[string search] {
            get {

                return (SearchFor(NounConjugator.FindRoot(search)).SelectMany(syn => NounConjugator.GetLexicalForms(syn))).ToSet();
            }
        }


        public override ISet<string> this[Word search] {
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

        private static readonly LASI.Algorithm.Lookup.InterSetRelationshipManagement.NounPointerSymbolMap RelationshipMap =
            new LASI.Algorithm.Lookup.InterSetRelationshipManagement.NounPointerSymbolMap();
    }
}
