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
        //ConcurrentDictionary<int, HashSet<SynSet>> cachedSetTraverals = new ConcurrentDictionary<int, HashSet<SynSet>>();
        HashSet<SynSet> allSets = new HashSet<SynSet>();
        Dictionary<int, SynSet> data = new Dictionary<int, SynSet>();
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


        Dictionary<WordNetNounCategory, List<SynSet>> lexicalGoups = new Dictionary<WordNetNounCategory, List<SynSet>>
        {
            { WordNetNounCategory.Entity, new List<SynSet>() },
            { WordNetNounCategory.Act,new List<SynSet>() },
            { WordNetNounCategory.Animal,new List<SynSet>() },
            { WordNetNounCategory.Artifact,new List<SynSet>() },
            { WordNetNounCategory.Attribute,new List<SynSet>() },
            { WordNetNounCategory.Body,new List<SynSet>() },
            { WordNetNounCategory.Cognition,new List<SynSet>() },
            { WordNetNounCategory.Communication,new List<SynSet>() },
            { WordNetNounCategory.Event,new List<SynSet>() },
            { WordNetNounCategory.Feeling,new List<SynSet>() },
            { WordNetNounCategory.Food,new List<SynSet>() },
            { WordNetNounCategory.Group,new List<SynSet>() },
            { WordNetNounCategory.Location,new List<SynSet>() },
            { WordNetNounCategory.Motive,new List<SynSet>() },
            { WordNetNounCategory.Object,new List<SynSet>() },
            { WordNetNounCategory.Person,new List<SynSet>() },
            { WordNetNounCategory.Phenomenon,new List<SynSet>() },
            { WordNetNounCategory.Plant,new List<SynSet>() },
            { WordNetNounCategory.Possession,new List<SynSet>() },
            { WordNetNounCategory.Process,new List<SynSet>() },
            { WordNetNounCategory.Quantity,new List<SynSet>() },
            { WordNetNounCategory.Relation,new List<SynSet>() },
            { WordNetNounCategory.Shape,new List<SynSet>() },
            { WordNetNounCategory.State,new List<SynSet>() },
            { WordNetNounCategory.Substance,new List<SynSet>() },
            { WordNetNounCategory.Time,new List<SynSet>() }
        };


        internal IEnumerable<string> GetAllWordStrings() {

            return from set in allSets
                   from word in set.Words
                   select word;

        }

        void CreateSet(string line) {

            //Aluan: This line gets extracts wd category info I noticed was present in the DB files
            //Erik:  Gotcha, I'll try to decipher its meaning.

            line = line.Substring(0, line.IndexOf('|'));


            MatchCollection numbers = Regex.Matches(line, @"\D{1,2}\s*\d{8}");

            var relationshipsToKeep = new HashSet<PointerKind> {
                PointerKind.Memberofthisdomain_REGION,
                PointerKind.Memberofthisdomain_TOPIC,
                PointerKind.Memberofthisdomain_USAGE,
                PointerKind.Domainofsynset_REGION,
                PointerKind.Domainofsynset_TOPIC,
                PointerKind.Domainofsynset_USAGE, 
                PointerKind.Hyponym, 
                PointerKind.InstanceHyponym
            };
            IEnumerable<KeyValuePair<PointerKind, int>> kindedPointers =
                from match in numbers.Cast<Match>()
                let split = match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                let pointer = split.Count() > 1 ? new KeyValuePair<PointerKind, int>(RelationMap[split[0]], Int32.Parse(split[1])) : new KeyValuePair<PointerKind, int>(PointerKind.UNKNOWN, Int32.Parse(split[0]))

                where relationshipsToKeep.Contains(pointer.Key)
                select pointer;


            MatchCollection words = Regex.Matches(line, @"(?<word>[A-Za-z_\-]{3,})");
            //somethin's amiss here.
            IEnumerable<string> members = from match in words.Cast<Match>()
                                          select match.Value.Replace('_', '-').ToLower();

            int id = Int32.Parse(line.Substring(0, 8));

            WordNetNounCategory lexCategory = ( WordNetNounCategory )Int32.Parse(line.Substring(9, 2));
            SynSet set = new SynSet(id, members, kindedPointers, lexCategory);

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
            SearchSubsets(containingSet, results, new HashSet<SynSet> {
            }, lexname);
            return new HashSet<string>(results.Distinct(StringComparer.OrdinalIgnoreCase));
        }

        private void SearchSubsets(SynSet containingSet, List<string> results, HashSet<SynSet> setsSearched, WordNetNounCategory lexname) {
            results.AddRange(containingSet.Words);
            if (!setsSearched.Contains(containingSet)) {

                setsSearched.Add(containingSet);
                foreach (var set in containingSet.Pointers.Select(p => {
                    SynSet set;
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
        private static readonly NounRelationshipTranslator RelationMap = new NounRelationshipTranslator();
    }
}
