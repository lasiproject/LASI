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
    public class VerbThesaurus : ThesaurusBase
    {
        /// <summary>
        /// Initializes a new instance of the VerbProvider class.
        ///<param name="constrainByCategory"></param>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym line for actions.</param>
        /// </summary>
        public VerbThesaurus(string filePath, bool constrainByCategory = false)
            : base(filePath) {
            FilePath = filePath;
            AssociationData = new ConcurrentDictionary<string, SynonymSet>();//Not a great practice, but the length of the file is fixed, making this a useful, but ugly optemization.
            //cachedData = new ConcurrentDictionary<string, HashSet<string>>();//Again this is ugly, but its fairly performant at the moment.
            lexRestrict = constrainByCategory;
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
                        ParseLine(line);
                    }
                }
            }
        }

        private void ParseLine(string line) {
            var synset = BuildSynset(line);

            LinkSynset(synset);
            AssociationData.Add(synset.IndexCode, synset);
            //Console.Write("V ");
        }

        private void LinkSynset(SynonymSet synset) {
            foreach (var word in synset.Members) {
                if (AssociationData.ContainsKey(word)) {
                    AssociationData[word] = new SynonymSet(
                        AssociationData[word].ReferencedIndexes.Concat(synset.ReferencedIndexes),
                        AssociationData[word].Members.Concat(synset.Members), AssociationData[word].LexName);
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
        /// <returns>a collection of strings containing all of the synonyms of the given verb.</returns>
        public override HashSet<string> this[string search] {
            get {
                try {
                    foreach (var root in conjugator.TryExtractRoot(search)) {
                        try {

                            return new HashSet<string>((from REF in AssociationData[root].ReferencedIndexes //Get all set reference indeces stored directly within 
                                                        select new {                                        //The synset indexed by the word
                                                            ind = REF,                                      //Store the LexName for restrictive comparison if enabled
                                                            lex = lexRestrict ? AssociationData[root].LexName : WordNetVerbLex.ARBITRARY
                                                        } into Temp
                                                        join REF in AssociationData on new {                //Now group join all synsets in the entire 
                                                            Temp.ind,                                       //thesaurus which reference the set above 
                                                            Temp.lex
                                                        } equals new {
                                                            ind = REF.Key,
                                                            lex = lexRestrict ? REF.Value.LexName : WordNetVerbLex.ARBITRARY
                                                        } into ReferencedSets                                //The result of our group join contains all referenced sets
                                                        from R in ReferencedSets.Distinct()
                                                        from RM in R.Value.Members                           //Now aggregate all words directly contained within the group
                                                        select RM into RMG                                   //concatanting them with their various morphs
                                                        select new string[] { RMG }.Concat(conjugator.TryComputeConjugations(RMG)) into CJRM
                                                        from C in CJRM                                       //Now simply remove any duplicates
                                                        select C));
                        }
                        catch (KeyNotFoundException) {
                        }
                        catch (ArgumentOutOfRangeException) {
                        }


                    }
                    return null;
                }
                catch (ArgumentOutOfRangeException) {
                }
                catch (KeyNotFoundException) {
                }
                catch (IndexOutOfRangeException) {
                }


                return null;
            }
        }






        /// <summary>
        /// Retrives the synonyms of the given verb as a collection of strings.
        /// </summary>
        /// <param name="search">An instance of Verb</param>
        /// <returns>a collection of strings containing all of the synonyms of the given verb.</returns>
        public override HashSet<string> this[Word search] {
            get {
                return this[search.Text];
            }
        }


        private SynonymSet BuildSynset(string data) {

            var WNlexNameCode = (WordNetVerbLex)Int32.Parse(data.Substring(9, 2));

            data = Regex.Replace(data, @"([+]+|;c+)+[\s]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+", "");

            var setIDsReferenced = new Regex(@"[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+");

            var sep = data.Split(new[] { '!', '|' }, StringSplitOptions.RemoveEmptyEntries)[0];

            var setReferences = from Match M in setIDsReferenced.Matches(sep)
                                select M.Value;
            var elementRgx = new Regex(@"\b[A-Za-z-_]{2,}");

            var setElements = from Match ContainedWord in elementRgx.Matches(sep.Substring(17))
                              select ContainedWord.Value.Replace('_', '-');

            return new SynonymSet(setReferences, setElements, WNlexNameCode);
        }

        //private ConcurrentDictionary<String, HashSet<string>> cachedData;

        const int HEADER_LENGTH = 30;
        private VerbConjugator conjugator = new VerbConjugator(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc");


        private bool lexRestrict;
    }



}



//}
//    //try {
//    return cachedData.ContainsKey(root) ? cachedData[root] : null;
//    //} catch (KeyNotFoundException ex) {
//    //    //  Debug.WriteLine("No entry present in VerbThesaurus for {0}\n{1}", root, ex.Message);
//    //}
//}
//return null;