using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LASI.Algorithm.Thesauri
{
    internal class VerbThesaurus : ThesaurusBase
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class.
        ///<param name="constrainByCategory"></param>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for verbals.</param>
        /// </summary>
        public VerbThesaurus(string filePath)
            : base(filePath) {
            FilePath = filePath;
            AssociationData = new ConcurrentDictionary<string, VerbSynSet>();
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

            data = data.Substring(0, data.IndexOf('|'));

            var extractedIndeces = new Regex(@"\D{1,2}\s*[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+");

            var pointers = from Match M in extractedIndeces.Matches(data)
                           let split = M.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                           let pointer = split.Count() > 1 ?
                           new KeyValuePair<VerbPointerSymbol, int>(RelationMap[split[0]], Int32.Parse(split[1])) :
                           new KeyValuePair<VerbPointerSymbol, int>(VerbPointerSymbol.UNDEFINED, Int32.Parse(split[0]))
                           select pointer;
            var elementRgx = new Regex(@"\b[A-Za-z-_]{2,}");

            var wordMembers = from Match ContainedWord in elementRgx.Matches(data.Substring(17))
                              select ContainedWord.Value.Replace('_', '-').ToLower();
            var id = Int32.Parse(data.Substring(0, 8));
            var WNlexNameCode = ( WordNetVerbCategory )Int32.Parse(data.Substring(9, 2));
            return new VerbSynSet(id, wordMembers, pointers, WNlexNameCode);
        }




        private void LinkSynset(VerbSynSet synset) {
            foreach (var word in synset.Words) {
                if (AssociationData.ContainsKey(word)) {
                    AssociationData[word] = new VerbSynSet(
                        AssociationData[word].ID,
                        AssociationData[word].Words.Concat(synset.Words),
                        AssociationData[word].RelatedOnPointerSymbol.Concat(synset.RelatedOnPointerSymbol)
                        .SelectMany(grouping => grouping.Select(pointer => new KeyValuePair<VerbPointerSymbol, int>(grouping.Key, pointer))),
                         AssociationData[word].LexName);
                }
                else {
                    AssociationData.Add(word, synset);
                }

            }
        }



        /// <summary>
        /// Retrives the synonyms of the given verb as a collection of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public override ISet<string> this[string search] {
            get {
                try {
                    foreach (var root in from root in VerbConjugator.FindRoot(search)
                                         where AssociationData.ContainsKey(root)
                                         select root) {
                        return new HashSet<string>(
                            from refIndex in AssociationData[root].ReferencedIndexes
                            from referencedSet in AssociationData.Values
                            //.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                            where referencedSet.ReferencedIndexes.Contains(refIndex)
                            where referencedSet.LexName == AssociationData[root].LexName
                            from word in referencedSet.Words
                            //.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                            let withConjugations = new string[] { word }.Concat(VerbConjugator.GetConjugations(word))
                            from w in withConjugations
                            select w);
                    }
                }
                catch (ArgumentOutOfRangeException) {
                }
                catch (KeyNotFoundException) {
                }
                catch (IndexOutOfRangeException) {
                }
                return new HashSet<string>();
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

        private static VerbPointerSymbolMap RelationMap = new VerbPointerSymbolMap();
    }



}