using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using LASI.Utilities;
using System.Dynamic;
using System.Collections.Concurrent;

namespace LASI.Algorithm.Thesauri
{
    public static class Thesaurus
    {

        #region Public Methods



        public static IEnumerable<string> Lookup(Word word) {
            return InternalLookup(word as dynamic);
        }

        public static IEnumerable<string> LookupNoun(string nounText, WordNetNounCategory wordNetNounLex) {
            return NounThesaurus[nounText, wordNetNounLex];
        }

        public static IEnumerable<string> LookupVerb(string verbText) {
            return VerbThesaurus[verbText];
        }

        public static IEnumerable<string> LookAdjective(string adjectiveText) {
            return AdjectiveThesaurus[adjectiveText];
        }

        public static IEnumerable<string> LookAdverb(string adverbText) {
            return AdjectiveThesaurus[adverbText];
        }

        public static bool IsSynonymFor(this Word word, Word other) {
            if (word == null || other == null) {
                return false;
            }
            return (
                word is Noun && other is Noun ||
                word is Verb && other is Verb ||
                word is Adverb && other is Adverb ||
                word is Adjective && other is Adjective
                )
                && Lookup(other).Contains(word.Text);
        }


        /// <summary>
        /// Determines if two NounPhrases are similar.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The Second NounPhrase</param>
        /// <returns>True if the given NounPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// 1: if ( Thesaurus.IsSimilarTo(np1, np2) ) { ... }
        /// 2: if ( np1.IsSimilarTo(np2) ) { ... }
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this NounPhrase first, NounPhrase second) {
            return GetSimilarityRatio(first, second) > 0.6;
        }

        public static bool IsSimilarTo(this VerbPhrase lhs, VerbPhrase rhs) {
            //Look into refining this
            List<Verb> leftHandVerbs = lhs.Words.GetVerbs().ToList();
            List<Verb> rightHandVerbs = rhs.Words.GetVerbs().ToList();

            bool result = leftHandVerbs.Count == rightHandVerbs.Count;

            if (result) {
                try {
                    for (var i = 0; i < leftHandVerbs.Count; ++i) {
                        result &= leftHandVerbs[i].IsSynonymFor(rightHandVerbs[i]);
                    }
                } catch (NullReferenceException) {
                    return false;
                }
            }

            return result;
        }



        /// <summary>
        /// Returns a double value indicating the degree of similarity between two NounPhrases.
        /// </summary>
        /// <param name="a">The first NounPhrase</param>
        /// <param name="b">The second NounPhrase</param>
        /// <returns>A double value indicating the degree of similarity between two NounPhrases.</returns>
        public static double GetSimilarityRatio(NounPhrase a, NounPhrase b) {
            NounPhrase outer = null;
            NounPhrase inner = null;
            double similarCount = 0;

            if (a.Words.Count() >= b.Words.Count()) {
                outer = a;
                inner = b;
            } else {
                outer = b;
                inner = a;
            }

            if ((outer.Words.GetNouns().Count() != 0) && (inner.Words.GetNouns().Count() != 0)) {
                foreach (var o in outer.Words.GetNouns()) {
                    foreach (var i in inner.Words.GetNouns()) {
                        if (i.IsSynonymFor(o))
                            similarCount += 0.7;
                        else if (i.Text == o.Text)
                            similarCount++;
                    }
                }

                return (similarCount / (inner.Words.GetNouns().Count() * outer.Words.GetNouns().Count()));
            } else
                return 1;

        }

        #endregion

        #region Internal Lookup Methods

        private static IEnumerable<string> InternalLookup(Verb verb) {
            if (!cachedVerbData.ContainsKey(verb.Text))
                cachedVerbData[verb.Text] = VerbThesaurus[verb];
            return cachedVerbData[verb.Text] ?? new List<string>();
        }
        private static IEnumerable<string> InternalLookup(Noun noun) {
            if (!cachedNounData.ContainsKey(noun.Text))
                cachedNounData[noun.Text] = NounThesaurus[noun];
            return cachedNounData[noun.Text];
        }
        private static IEnumerable<string> InternalLookup(Adverb verb) {
            return AdverbThesaurus[verb];
        }
        private static IEnumerable<string> InternalLookup(Adjective verb) {
            return AdjectiveThesaurus[verb];
        }
        private static IEnumerable<string> InternalLookup(Word word) {
            throw new NoSynonymLookupForTypeException(word);
        }

        #endregion

        #region Lazy Initialization of Thesaurus Instances

        private static readonly Lazy<NounThesaurus> nounThesaurus = new Lazy<NounThesaurus>(() => {
            var t = new NounThesaurus(nounThesaurusFilePath);
            t.Load();
            return t;
        }, true);
        private static readonly Lazy<VerbThesaurus> verbThesaurus = new Lazy<VerbThesaurus>(() => {
            var t = new VerbThesaurus(verbThesaurusFilePath);
            t.Load();
            return t;
        }, true);
        private static readonly Lazy<AdjectiveThesaurus> adjectiveThesaurus = new Lazy<AdjectiveThesaurus>(() => {
            var t = new AdjectiveThesaurus(adjectiveThesaurusFilePath);
            t.Load();
            return t;
        }, true);
        private static readonly Lazy<AdverbThesaurus> adverbThesaurus = new Lazy<AdverbThesaurus>(() => {
            var t = new AdverbThesaurus(adverbThesaurusFilePath);
            t.Load();
            return t;
        }, true);

        #endregion

        #region Private Properties

        private static NounThesaurus NounThesaurus {
            get {
                return nounThesaurus.Value;
            }
        }
        private static VerbThesaurus VerbThesaurus {
            get {
                return verbThesaurus.Value;
            }
        }
        private static AdjectiveThesaurus AdjectiveThesaurus {
            get {
                return adjectiveThesaurus.Value;
            }
        }
        private static AdverbThesaurus AdverbThesaurus {
            get {
                return adverbThesaurus.Value;
            }
        }

        #endregion

        #region Private Methods

        private static async Task LoadAllParallelLinqTest() {
            var sw = Stopwatch.StartNew();
            await Task.Run(() => new ThesaurusBase[] { NounThesaurus, VerbThesaurus, AdverbThesaurus, AdjectiveThesaurus }.AsParallel().ForAll(t => t.Load()));
            Output.WriteLine("Async PLINQ thesaurus loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }

        private static async Task LoadAllTaskLevelParallelTest() {
            var sw = Stopwatch.StartNew();
            await Task.WhenAll(
               Task.Run(async () => {
                   var s = Stopwatch.StartNew();
                   await NounThesaurus.LoadAsync();
                   s.Stop();
                   Output.WriteLine("NounThesausus Loaded in{0}", s.ElapsedMilliseconds);
               }), Task.Run(async () => {
                   var s = Stopwatch.StartNew();
                   await AdverbThesaurus.LoadAsync();
                   s.Stop();
                   Output.WriteLine("AdverbThesausus Loaded in{0}", s.ElapsedMilliseconds);
               }), Task.Run(async () => {
                   var s = Stopwatch.StartNew();
                   await AdjectiveThesaurus.LoadAsync();
                   s.Stop();
                   Output.WriteLine("AdjectiveThesausus Loaded in{0}", s.ElapsedMilliseconds);
               }), Task.Run(async () => {
                   var s = Stopwatch.StartNew();
                   await VerbThesaurus.LoadAsync();
                   s.Stop();
                   Output.WriteLine("VerbThesausus Loaded in{0}", s.ElapsedMilliseconds);
               }));

            sw.Stop();
            Output.WriteLine("Async TPLI loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }

        #endregion

        #region Fields
        // Thesaurus File Paths
        private static readonly string nounThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        private static readonly string adverbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adv";
        private static readonly string adjectiveThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adj";
        // Synonym Lookup Caches
        private static ConcurrentDictionary<string, IEnumerable<string>> cachedNounData = new ConcurrentDictionary<string, IEnumerable<string>>();
        private static ConcurrentDictionary<string, IEnumerable<string>> cachedVerbData = new ConcurrentDictionary<string, IEnumerable<string>>();
        private static ConcurrentDictionary<string, IEnumerable<string>> cachedAdjectiveData = new ConcurrentDictionary<string, IEnumerable<string>>();
        private static ConcurrentDictionary<string, IEnumerable<string>> cachedAdverbData = new ConcurrentDictionary<string, IEnumerable<string>>();

        #endregion


    }
}


