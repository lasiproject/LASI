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
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for actions.</param>
        /// </summary>
        public VerbThesaurus(string filePath, bool constrainByCategory = false)
            : base(filePath)
        {
            FilePath = filePath;
            AssociationData = new ConcurrentDictionary<string, VerbThesaurusSynSet>();
            lexRestrict = constrainByCategory;
        }



        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load()
        {
            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None, 10024, FileOptions.SequentialScan)) {
                using (var reader = new StreamReader(fileStream)) {
                    for (int i = 0; i < HEADER_LENGTH; ++i) {//Discard file header
                        reader.ReadLine();
                    }
                    var fileLines = reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in fileLines) {
                        ParseLineAndAddToSets(line);
                    }
                }
            }
        }

        private void ParseLineAndAddToSets(string line)
        {
            var synset = BuildSynset(line);

            LinkSynset(synset);
            //AssociationData.Add(synset.Index, synset);
        }

        private void LinkSynset(VerbThesaurusSynSet synset)
        {
            foreach (var word in synset.Words) {
                if (AssociationData.ContainsKey(word)) {
                    AssociationData[word] = new VerbThesaurusSynSet(
                        AssociationData[word].ReferencedIndexes.Concat(synset.ReferencedIndexes),
                        AssociationData[word].Words.Concat(synset.Words), AssociationData[word].LexName);
                } else {
                    AssociationData.Add(word, synset);
                }

            }
        }



        /// <summary>
        /// Retrives the synonyms of the given verb as a collection of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public override HashSet<string> this[string search]
        {
            get
            {
                try {
                    foreach (var root in from root in conjugator.TryExtractRoot(search)
                                         where AssociationData.ContainsKey(root)
                                         select root) {
                        return new HashSet<string>(
                            (from refIndex in AssociationData[root].ReferencedIndexes
                             from referencedSet in AssociationData.Values.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                             where referencedSet.ReferencedIndexes.Contains(refIndex)
                             where (!lexRestrict || referencedSet.LexName == AssociationData[root].LexName)
                             from word in referencedSet.Words.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                             let withConjugations = new string[] { word }.Concat(conjugator.TryComputeConjugations(word))
                             from w in withConjugations
                             select w));
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
        /// Retrives the synonyms of the given verb as a collection of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public override HashSet<string> this[Word search]
        {
            get
            {
                return this[search.Text];
            }
        }


        private VerbThesaurusSynSet BuildSynset(string data)
        {

            var WNlexNameCode = ( WordNetVerbCategory )Int32.Parse(data.Substring(9, 2));

            data = Regex.Replace(data, @"([+]+|;c+)+[\s]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+", "");

            var extractedIndeces = new Regex(@"[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+");

            var sep = data.Split(new[] { '!', '|' }, StringSplitOptions.RemoveEmptyEntries)[0];

            var referencedIndeces = from Match M in extractedIndeces.Matches(sep)
                                    select Int32.Parse(M.Value);
            var elementRgx = new Regex(@"\b[A-Za-z-_]{2,}");

            var setElements = from Match ContainedWord in elementRgx.Matches(sep.Substring(17))
                              select ContainedWord.Value.Replace('_', '-');

            return new VerbThesaurusSynSet(referencedIndeces, setElements, WNlexNameCode);
        }


        const int HEADER_LENGTH = 30;
        private VerbConjugator conjugator = new VerbConjugator(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc");


        private bool lexRestrict;
    }



}