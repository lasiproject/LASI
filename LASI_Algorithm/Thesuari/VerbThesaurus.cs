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
                    foreach (var root in conjugator.TryExtractRoot(search)) {
                        try {
                            return new HashSet<string>((from refIndex in AssociationData[root].ReferencedIndexes //Get all set reference indeces stored directly within the set containing the search word 

                                                        from referencedSet in AssociationData.Values
                                                        group referencedSet by referencedSet.ReferencedIndexes.Contains(refIndex) && (!lexRestrict || referencedSet.LexName == AssociationData[root].LexName)
                                                            into referencedSet
                                                            where referencedSet.Key
                                                            select referencedSet into referencedSets
                                                            //The result of our group join contains all referenced sets
                                                            from R in referencedSets
                                                            from word in R.Words                           //Now aggregate all words directly contained within the group
                                                            select word into words                                   //concatanting them with their various morphs
                                                            select new string[] { words }.Concat(conjugator.TryComputeConjugations(words)) into withConjugations
                                                            from word in withConjugations                                       //Now simply remove any duplicates
                                                            select word));
                        }
                        catch (KeyNotFoundException) {
                        }
                        catch (ArgumentOutOfRangeException) {
                        }


                    }
                    return new HashSet<string>();
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

        //private ConcurrentDictionary<int, VerbThesaurusSynSet> allSynonymSets = new ConcurrentDictionary<int, VerbThesaurusSynSet>();

        const int HEADER_LENGTH = 30;
        private VerbConjugator conjugator = new VerbConjugator(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc");


        private bool lexRestrict;
    }



}