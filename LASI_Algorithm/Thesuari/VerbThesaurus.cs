using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    public class VerbThesaurus : Thesaurus
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym data for verbs.</param>
        public VerbThesaurus(string filePath = @"C:\Users\Aluan\Desktop\LASI\LASI_v1\WordNetThesaurusData\data.verb")
            : base(filePath) {
            FilePath = filePath;
            LoadingStatus = FileLoadingState.NotInitiated;
            AssociationData = new SortedList<string, SynonymSet>(13000);
        }

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            LoadingStatus = FileLoadingState.Initiated;
            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None, 10024, FileOptions.SequentialScan)) {
                var reader = new StreamReader(fileStream);
                //Discard file header
                for (int i = 0; i < HEADER_LENGTH; ++i)
                    reader.ReadLine();
                {
                    var fileLines = reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (
                    var data in fileLines) {
                        var synset = BuildSynset(data);

                        foreach (var word in synset.Members) {
                            try {
                                AssociationData.Add(word, synset);
                            } catch (ArgumentException) {
                                // Debug.WriteLine("previously defined: \n" + AssociationData[word]);
                                //throw new ArgumentException("previously defined: \n" + AssociationData[word].ToString(),ex);
                                //var previouslyDefined = 
                                SynonymSet temp = null;
                                AssociationData.TryGetValue(word, out temp);
                                if (temp != null)
                                    AssociationData[word] = new SynonymSet(AssociationData[word].ReferencedIndexes.Concat(synset.ReferencedIndexes), AssociationData[word].Members.Concat(synset.Members), synset.IndexCode);
                            }
                        }
                        AssociationData.Add(synset.IndexCode, synset);

                    }
                }
                LoadingStatus = FileLoadingState.Completed;
            }
        }

  

        /// <summary>
        /// Retrives the synonyms of the given verb as a collection of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public override IEnumerable<string> this[string search] {
            get {
                return (from M in AssociationData[search].Members
                        where M != search
                        select M).Concat(
                               from RI in AssociationData[search].ReferencedIndexes
                               from FM in AssociationData[RI].Members
                               select FM).Distinct().ToArray();
            }
        }

        /// <summary>
        /// Retrives the synonyms of the given verb as a collection of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public override IEnumerable<string> this[Word search] {
            get {
                return this[search.Text];
            }
        }


        private SynonymSet BuildSynset(string data) {

            data = Regex.Replace(data, @"([+]+|;c+)+[\s]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+", "");

            var refRgx = new Regex(@"[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+");

            var sep = data.Split(new[] { '!', '|' }, StringSplitOptions.RemoveEmptyEntries)[0];

            var setRefs = from Match M in refRgx.Matches(sep)
                          select M.Value;
            var elementRgx = new Regex(@"\b[A-Za-z-_]{2,}");

            var setElements = from Match WT in elementRgx.Matches(sep.Substring(17))
                              select WT.Value;

            return new SynonymSet(setRefs, setElements, setRefs.First());
        }

        const int HEADER_LENGTH = 30;
    }
}



