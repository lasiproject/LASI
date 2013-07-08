using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Algorithm.Thesauri
{
    using SetReference = System.Collections.Generic.KeyValuePair<VerbSetRelationship, int>;
    internal class VerbThesaurus : SynonymLookup
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class.
        ///<param name="constrainByCategory"></param>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for verbals.</param>
        /// </summary>
        public VerbThesaurus(string filePath)
            : base(filePath) {
            verbData = new ConcurrentDictionary<string, VerbSynSet>();
        }



        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None, 10024, FileOptions.SequentialScan)) {
                using (var reader = new StreamReader(fileStream)) {
                    for (int i = 0; i < HEADER_LENGTH; ++i) {//Discard file header
                        reader.ReadLine();
                    }
                    var fileLines = reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in fileLines) {
                        var set = CreateSet(line);
                        LinkSynset(set);
                    }
                }
            }
        }

        private VerbSynSet CreateSet(string data) {

            var setLine = data.Substring(0, data.IndexOf('|'));

            var extractedIndeces = new Regex(@"\D{1,2}\s*[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+");

            var referencedSets = from Match M in extractedIndeces.Matches(setLine)
                                 let split = M.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                 where split.Count() > 1
                                 select new SetReference(RelationMap[split[0]], Int32.Parse(split[1]));

            var elementRgx = new Regex(@"\b[A-Za-z-_]{2,}");

            var words = from Match ContainedWord in elementRgx.Matches(setLine.Substring(17))
                        select ContainedWord.Value.Replace('_', '-').ToLower();
            var id = Int32.Parse(setLine.Substring(0, 8));
            var lexCategory = ( VerbCategory )Int32.Parse(setLine.Substring(9, 2));
            return new VerbSynSet(id, words, referencedSets, lexCategory);
        }




        private void LinkSynset(VerbSynSet synset) {
            foreach (var word in synset.Words) {
                if (verbData.ContainsKey(word)) {
                    verbData[word] = new VerbSynSet(
                        verbData[word].ID,
                        verbData[word].Words.Concat(synset.Words),
                        verbData[word].RelatedOnPointerSymbol.Concat(synset.RelatedOnPointerSymbol)
                        .SelectMany(grouping => grouping.Select(pointer => new SetReference(grouping.Key, pointer))), verbData[word].LexName);
                }
                else {
                    verbData.Add(word, synset);
                }

            }
        }

        private ISet<string> SearchFor(string search) {
            try {
                List<string> results = new List<string>();
                var root = VerbConjugator.FindRoot(search);
                if (verbData.ContainsKey(root)) {
                    results.AddRange(
                        from refIndex in verbData[root].ReferencedIndexes
                        from referencedSet in verbData.Values
                        where referencedSet.ReferencedIndexes.Contains(refIndex)
                        where referencedSet.LexName == verbData[root].LexName
                        from word in referencedSet.Words

                        let withConjugations = new string[] { word }.Concat(VerbConjugator.GetConjugations(word))
                        from w in withConjugations
                        select w);
                }
                return results.ToSet();
            }
            catch (ArgumentOutOfRangeException) {
            }
            catch (IndexOutOfRangeException) {
            }
            catch (KeyNotFoundException) {
            }
            return new HashSet<string>();
        }

        /// <summary>
        /// Retrives the synonyms of the given verb as a collection of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public override ISet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }








        /// <summary>
        /// Retrives the synonyms of the given Verb as a collection of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>A collection of strings containing all of the synonyms of the given Verb.</returns>
        public override ISet<string> this[Word search] {
            get {
                return this[search.Text];
            }
        }



        private IDictionary<string, VerbSynSet> verbData;
        private static LASI.Algorithm.Thesauri.InterSetRelationshipManagement.VerbPointerSymbolMap RelationMap =
            new LASI.Algorithm.Thesauri.InterSetRelationshipManagement.VerbPointerSymbolMap();
    }



}