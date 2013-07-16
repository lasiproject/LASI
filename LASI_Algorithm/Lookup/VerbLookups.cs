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

namespace LASI.Algorithm.Lookup
{
    using SetReference = System.Collections.Generic.KeyValuePair<VerbSetRelationship, int>;
    internal class VerbLookups : ISynonymLookup<Verb>
    {

        protected const int HEADER_LENGTH = 29;
        Dictionary<int, VerbSynSet> setsBySetID = new Dictionary<int, VerbSynSet>();
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class. 
        /// </summary>
        /// <param name="path">The path of the WordNet database file containing the sysnonym data for verbals.</param>
        public VerbLookups(string path) {
            filePath = path;
            verbData = new ConcurrentDictionary<string, VerbSynSet>();
        }



        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public void Load() {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 10024, FileOptions.SequentialScan)) {
                using (var reader = new StreamReader(fileStream)) {
                    var fileLines = reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(HEADER_LENGTH);
                    foreach (var line in fileLines) {
                        var set = CreateSet(line);
                        LinkSynset(set);
                    }
                }
            }
        }

        private VerbSynSet CreateSet(string fileLine) {
            var line = fileLine.Substring(0, fileLine.IndexOf('|'));

            var referencedSets = from Match M in Regex.Matches(line, @"\D{1,2}\s*[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+")
                                 let split = M.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                 where split.Count() > 1
                                 select new SetReference(RelationMap[split[0]], Int32.Parse(split[1]));

            var words = from Match ContainedWord in Regex.Matches(line.Substring(17), @"\b[A-Za-z-_]{2,}")
                        select ContainedWord.Value.Replace('_', '-').ToLower();
            var id = Int32.Parse(line.Substring(0, 8));
            var lexCategory = ( VerbCategory )Int32.Parse(line.Substring(9, 2));
            return new VerbSynSet(id, words, referencedSets, lexCategory);
        }

        private void LinkSynset(VerbSynSet synset) {
            setsBySetID[synset.ID] = synset;
            foreach (var word in synset.Words) {
                if (verbData.ContainsKey(word)) {
                    verbData[word] = new VerbSynSet(
                        verbData[word].ID,
                        verbData[word].Words.Concat(synset.Words),
                        verbData[word].RelatedOnPointerSymbol.Concat(synset.RelatedOnPointerSymbol)
                        .SelectMany(grouping => grouping.Select(pointer => new SetReference(grouping.Key, pointer))), verbData[word].LexName);
                }
                else {
                    verbData[word] = synset;
                }
            }
        }

        private ISet<string> SearchFor(string search) {
            try {
                VerbSynSet containingSet;
                return new HashSet<string>(verbData.TryGetValue(VerbConjugator.FindRoot(search), out containingSet) ?
                         from refIndex in containingSet.ReferencedIndexes
                         from referencedSet in setsBySetID[refIndex].ReferencedIndexes select setsBySetID[referencedSet] into referenced
                         where referenced.LexName == containingSet.LexName
                         from word in referenced.Words
                         let withConjugations = Enumerable.Repeat(word, 1).Concat(VerbConjugator.GetConjugations(word))
                         from w in withConjugations
                         select w : Enumerable.Repeat(search, 1), StringComparer.OrdinalIgnoreCase);
            }
            catch (ArgumentOutOfRangeException) {
            }
            catch (IndexOutOfRangeException) {
            }
            catch (KeyNotFoundException) {
            }
            return Enumerable.Repeat(search, 1).ToSet(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Retrives the synonyms of the given verb as an ISet of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public ISet<string> this[string search] {
            get {
                return SearchFor(search);
            }
        }

        /// <summary>
        /// Retrives the synonyms of the given Verb as an ISet of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>A collection of strings containing all of the synonyms of the given Verb.</returns>
        public ISet<string> this[Verb search] {
            get {
                return this[search.Text];
            }
        }



        private IDictionary<string, VerbSynSet> verbData;
        private static LASI.Algorithm.Lookup.InterSetRelationshipManagement.VerbPointerSymbolMap RelationMap =
            new LASI.Algorithm.Lookup.InterSetRelationshipManagement.VerbPointerSymbolMap();

        private string filePath;

        public async Task LoadAsync() {
            await System.Threading.Tasks.Task.Run(() => Load());
        }
    }



}