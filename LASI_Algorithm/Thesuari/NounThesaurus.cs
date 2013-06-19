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

            using (StreamReader r = new StreamReader(FilePath)) {




                for (int i = 0; i < 29; ++i) //stole this from Aluan
                {
                    r.ReadLine();
                }
                while (!r.EndOfStream) {

                    CreateSet(r.ReadLine());
                }
            }


        }


        Dictionary<WordNetNounCategory, List<NounSynSet>> lexicalGoups = new Dictionary<WordNetNounCategory, List<NounSynSet>>
        {
            { WordNetNounCategory.Entity, new List<NounSynSet>() },
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


        internal IEnumerable<string> GetAllWordStrings() {

            return from set in allSets
                   from word in set.Words
                   select word;

        }

        void CreateSet(string line) {

            //Aluan: This line gets extracts word category info I noticed was present in the DB files
            //Erik:  Gotcha, I'll try to decipher its meaning.

            line = line.Substring(0, line.IndexOf('|'));


            MatchCollection numbers = Regex.Matches(line, @"\D{1,2}\s*\d{8}");

            var relationshipsToKeep = new HashSet<NounPointerSymbol> {
                NounPointerSymbol.Memberofthisdomain_REGION,
                NounPointerSymbol.Memberofthisdomain_TOPIC,
                NounPointerSymbol.Memberofthisdomain_USAGE,
                NounPointerSymbol.Domainofsynset_REGION,
                NounPointerSymbol.Domainofsynset_TOPIC,
                NounPointerSymbol.Domainofsynset_USAGE, 
                NounPointerSymbol.Hyponym, 
                NounPointerSymbol.InstanceHyponym
            };
            IEnumerable<KeyValuePair<NounPointerSymbol, int>> kindedPointers =
                from match in numbers.Cast<Match>()
                let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                let pointer = split.Count() > 1 ? new KeyValuePair<NounPointerSymbol, int>(RelationMap[split[0]], Int32.Parse(split[1])) : new KeyValuePair<NounPointerSymbol, int>(NounPointerSymbol.UNDEFINED, Int32.Parse(split[0]))

                where relationshipsToKeep.Contains(pointer.Key)
                select pointer;


            MatchCollection words = Regex.Matches(line, @"(?<word>[A-Za-z_\-]{3,})");
            //somethin'subject amiss here.
            IEnumerable<string> members = from match in words.Cast<Match>()
                                          select match.Value.Replace('_', '-').ToLower();

            int id = Int32.Parse(line.Substring(0, 8));

            WordNetNounCategory lexCategory = ( WordNetNounCategory )Int32.Parse(line.Substring(9, 2));
            NounSynSet set = new NounSynSet(id, members, kindedPointers, lexCategory);

            allSets.Add(set);
            try {
                data.Add(id, set);
            }
            catch (Exception) {
            }
            lexicalGoups[lexCategory].Add(set);
        }




        public HashSet<string> SearchFor(string word) {

            var containingSet = allSets.FirstOrDefault(set => set.Words.Contains(word));
            if (containingSet == null)
                return new HashSet<string>();
            var lexname = containingSet.LexName;
            List<string> results = new List<string>();
            SearchSubsets(containingSet, results, new HashSet<NounSynSet> {
            }, lexname);
            return new HashSet<string>(results.Distinct(StringComparer.OrdinalIgnoreCase));
        }

        private void SearchSubsets(NounSynSet containingSet, List<string> results, HashSet<NounSynSet> setsSearched, WordNetNounCategory lexname) {
            results.AddRange(containingSet.Words);
            if (!setsSearched.Contains(containingSet)) {

                setsSearched.Add(containingSet);
                foreach (var set in containingSet.ReferencedIndexes.Select(p => {
                    NounSynSet set;
                    data.TryGetValue(p, out set);
                    return set;
                })) {


                    if (set != null && !setsSearched.Contains(set) && set.LexName == lexname) {
                        SearchSubsets(set, results, setsSearched, lexname);

                    }


                }
            }
        }



        public override HashSet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }


        public override HashSet<string> this[Word search] {
            get {
                return this[search.Text];
            }
        }
        private static readonly NounPointerSymbolMap RelationMap = new NounPointerSymbolMap();
    }
}
