using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LASI.Algorithm.Thesauri
{
    public class VerbThesaurus : Thesaurus
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for actions.</param>
        public VerbThesaurus(string filePath)
            : base(filePath) {
            FilePath = filePath;
            LoadingStatus = ThesaurusLoadingState.NotInitiated;
            AssociationData = new SortedList<string, SynonymSet>(25327);//Not a great practice, but the length of the file is fixed, making this a useful, but ugly optemization.
            cachedData = new SortedList<string, IEnumerable<string>>(32000);//Again this is ugly, but its fairly performant at the moment.
        }

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            LoadingStatus = ThesaurusLoadingState.Initiated;
            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None, 10024, FileOptions.SequentialScan)) {
                var reader = new StreamReader(fileStream);

                for (int i = 0; i < HEADER_LENGTH; ++i) {//Discard file header
                    reader.ReadLine();
                }

                var fileLines = reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in fileLines) {

                    var synset = BuildSynset(line);

                    foreach (var word in synset.Members) {
                        if (AssociationData.ContainsKey(word)) {
                            AssociationData[word] = new SynonymSet(
                                AssociationData[word].ReferencedIndexes.Concat(synset.ReferencedIndexes),
                                AssociationData[word].Members.Concat(synset.Members));
                        } else {
                            AssociationData.Add(word, synset);
                        }

                    }
                    AssociationData.Add(synset.IndexCode, synset);

                }
                LoadingStatus = ThesaurusLoadingState.Completed;
            }
        }



        /// <summary>
        /// Retrives the synonyms of the given verb as a collection of strings.
        /// </summary>
        /// <param name="search">The text of the verb to look for.</param>
        /// <returns>A collection of strings containing all of the synonyms of the given verb.</returns>
        public override IEnumerable<string> this[string search] {
            /*
             *   First, access the synset(d) which literally contain the search string text.
             *   Next, aggregate their external set keys and perform a lookup.
             *   Finally, merge the synsets yielded from these lookups aggregating their members as strings.
             * 
             */

            get {
                if (!cachedData.ContainsKey(search)) {
                    cachedData.Add(search, (from M in AssociationData[search].ReferencedIndexes
                                            select M into Temp
                                            join R in AssociationData on Temp equals R.Key into ReferencedSets
                                            from R in ReferencedSets.Distinct()
                                            from RM in R.Value.Members
                                            select RM).Distinct());
                }
                return cachedData[search];
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

            var setReferences = from Match M in refRgx.Matches(sep)
                                select M.Value;
            var elementRgx = new Regex(@"\b[A-Za-z-_]{2,}");

            var setElements = from Match ContainedWord in elementRgx.Matches(sep.Substring(17))
                              select ContainedWord.Value.Replace('_', '-');

            return new SynonymSet(setReferences, setElements);
        }

        private SortedList<String, IEnumerable<string>> cachedData;

        const int HEADER_LENGTH = 30;
    }
}



