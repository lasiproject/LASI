using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LASI.Utilities;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;


namespace LASI.Algorithm.Thesauri
{
    internal class NounThesaurus : ThesaurusBase
    {
        /// <summary>
        /// Initializes a new instance of the NounProvider class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for nouns.</param>
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

        NounSynSet CreateSet(string line) {


            var setLine = line.Substring(0, line.IndexOf('|'));

            IEnumerable<KeyValuePair<NounPointerSymbol, int>> kindedPointers =
                from match in Regex.Matches(setLine, @"\D{1,2}\s*\d{8}").Cast<Match>()
                let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                let pointer = split.Count() > 1 ?
                new KeyValuePair<NounPointerSymbol, int>(RelationMap[split[0]], Int32.Parse(split[1])) :
                new KeyValuePair<NounPointerSymbol, int>(NounPointerSymbol.UNDEFINED, Int32.Parse(split[0]))
                where relationshipsToKeep.Contains(pointer.Key)
                select pointer;

            IEnumerable<string> localWords = from match in Regex.Matches(setLine, @"(?<word>[A-Za-z_\-\']{3,})").Cast<Match>()
                                             select match.Value.Replace('_', '-');

            int id = Int32.Parse(setLine.Substring(0, 8));

            WordNetNounCategory lexCategory = ( WordNetNounCategory )Int32.Parse(setLine.Substring(9, 2));

            return new NounSynSet(id, localWords, kindedPointers, lexCategory);

        }




        private ISet<string> SearchFor(string word) {

            var containingSet = allSets.FirstOrDefault(set => set.Words.Contains(word));
            if (containingSet == null)
                return new HashSet<string>();
            var lexname = containingSet.LexName;
            List<string> results = new List<string>();

            SearchSubsets(containingSet, results, new HashSet<NounSynSet>(), lexname);
            return new HashSet<string>(results);
        }

        private void SearchSubsets(NounSynSet containingSet, List<string> results, HashSet<NounSynSet> setsSearched, WordNetNounCategory lexname) {
            results.AddRange(containingSet.Words);
            results.AddRange(from set in containingSet[NounPointerSymbol.HypERnym]
                             where data.ContainsKey(set)
                             from w in data[set].Words
                             select w);
            if (!setsSearched.Contains(containingSet)) {

                setsSearched.Add(containingSet);
                foreach (var set in containingSet.ReferencedIndexes.Except(containingSet[NounPointerSymbol.HypERnym]).Select(p =>
                  data.ContainsKey(p) ? data[p] : null)) {
                    if (set != null && set.LexName == lexname && !setsSearched.Contains(set)) {
                        SearchSubsets(set, results, setsSearched, lexname);
                    }


                }
            }
        }


        public override ISet<string> this[string search] {
            get {

                return SearchFor(NounConjugator.FindRoot(search)).SelectMany(syn => NounConjugator.GetLexicalForms(syn)).ToSet();
            }
        }


        public override ISet<string> this[Word search] {
            get {
                return this[search.Text];
            }
        }

        private Dictionary<WordNetNounCategory, List<NounSynSet>> lexicalGoups = new Dictionary<WordNetNounCategory, List<NounSynSet>>
        {
            { WordNetNounCategory.Tops, new List<NounSynSet>() },
            { WordNetNounCategory.Act,new List<NounSynSet>() },
            { WordNetNounCategory.Animal,new List<NounSynSet>() },
            { WordNetNounCategory.Artifact,new List<NounSynSet>() },
            { WordNetNounCategory.Attribute,new List<NounSynSet>() },
            { WordNetNounCategory.Body,new List<NounSynSet>() },
            { WordNetNounCategory.Cognition,new List<NounSynSet>() },
            { WordNetNounCategory.Communication,new List<NounSynSet>() },
            { WordNetNounCategory.Event,new List<NounSynSet>() },
            { WordNetNounCategory.Feeling,new List<NounSynSet>() },
            { WordNetNounCategory.Food,new List<NounSynSet>() },
            { WordNetNounCategory.Group,new List<NounSynSet>() },
            { WordNetNounCategory.Location,new List<NounSynSet>() },
            { WordNetNounCategory.Motive,new List<NounSynSet>() },
            { WordNetNounCategory.Object,new List<NounSynSet>() },
            { WordNetNounCategory.Person,new List<NounSynSet>() },
            { WordNetNounCategory.Phenomenon,new List<NounSynSet>() },
            { WordNetNounCategory.Plant,new List<NounSynSet>() },
            { WordNetNounCategory.Possession,new List<NounSynSet>() },
            { WordNetNounCategory.Process,new List<NounSynSet>() },
            { WordNetNounCategory.Quantity,new List<NounSynSet>() },
            { WordNetNounCategory.Relation,new List<NounSynSet>() },
            { WordNetNounCategory.Shape,new List<NounSynSet>() },
            { WordNetNounCategory.State,new List<NounSynSet>() },
            { WordNetNounCategory.Substance,new List<NounSynSet>() },
            { WordNetNounCategory.Time,new List<NounSynSet>() }
        };

        private HashSet<NounPointerSymbol> relationshipsToKeep = new HashSet<NounPointerSymbol> {
            NounPointerSymbol.MemberOfThisDomain_REGION,
            NounPointerSymbol.MemberOfThisDomain_TOPIC,
            NounPointerSymbol.MemberOfThisDomain_USAGE,
            NounPointerSymbol.DomainOfSynset_REGION,
            NounPointerSymbol.DomainOfSynset_TOPIC,
            NounPointerSymbol.DomainOfSynset_USAGE, 
            NounPointerSymbol.HypOnym, 
            NounPointerSymbol.InstanceHypOnym, 
            NounPointerSymbol.InstanceHypERnym,
            NounPointerSymbol.HypERnym,
        };

        private static readonly NounPointerSymbolMap RelationMap = new NounPointerSymbolMap();
    }
}
