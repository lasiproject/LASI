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
        private static readonly string nounThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        private static readonly string adverbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adv";
        private static readonly string adjectiveThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adj";
        static Thesaurus() {
            NounProvider = new NounThesaurus(nounThesaurusFilePath);
            VerbProvider = new VerbThesaurus(verbThesaurusFilePath);
            AdverbProvider = new AdverbThesaurus(adverbThesaurusFilePath);
            AdjectiveProvider = new AdjectiveThesaurus(adjectiveThesaurusFilePath);
        }
        public static void LoadAll() {
            var sw = Stopwatch.StartNew();
            NounProvider.Load();
            VerbProvider.Load();
            AdverbProvider.Load();
            AdjectiveProvider.Load();
            sw.Stop();
            Output.WriteLine("Sync thesaurus loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }
        public static async Task LoadAllAsync() {

            await LoadAllTaskLevelParallelTest();

        }

        private static async Task LoadAllParallelLinqTest() {
            var sw = Stopwatch.StartNew();
            await Task.Run(() => new ThesaurusBase[] { NounProvider, VerbProvider, AdverbProvider, AdjectiveProvider }.AsParallel().ForAll(t => t.Load()));
            Output.WriteLine("Async  PLINQ thesaurus loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }

        private static async Task LoadAllTaskLevelParallelTest() {
            var sw = Stopwatch.StartNew();
            await Task.WhenAll(
               Task.Run(async () => {
                   var s = Stopwatch.StartNew();
                   await NounProvider.LoadAsync();
                   s.Stop();
                   Output.WriteLine("NounThesausus Loaded in{0}", s.ElapsedMilliseconds);
               }), Task.Run(async () => {
                   var s = Stopwatch.StartNew();
                   await AdverbProvider.LoadAsync();
                   s.Stop();
                   Output.WriteLine("AdverbThesausus Loaded in{0}", s.ElapsedMilliseconds);
               }), Task.Run(async () => {
                   var s = Stopwatch.StartNew();
                   await AdjectiveProvider.LoadAsync();
                   s.Stop();
                   Output.WriteLine("AdjectiveThesausus Loaded in{0}", s.ElapsedMilliseconds);
               }), Task.Run(async () => {
                   var s = Stopwatch.StartNew();
                   await VerbProvider.LoadAsync();
                   s.Stop();
                   Output.WriteLine("VerbThesausus Loaded in{0}", s.ElapsedMilliseconds);
               }));

            sw.Stop();
            Output.WriteLine("Async TPLI loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }
        public static IEnumerable<string> Lookup(Word word) {
            return InternalLookup(word as dynamic);
        }
        public static IEnumerable<string> InternalLookup(Verb verb) {
            if (!cachedVerbData.ContainsKey(verb.Text))
                cachedVerbData[verb.Text] = VerbProvider[verb];
            return cachedVerbData[verb.Text] ?? new List<string>();
        }
        public static IEnumerable<string> InternalLookup(Noun noun) {
            if (!cachedNounData.ContainsKey(noun.Text))
                cachedNounData[noun.Text] = NounProvider[noun];
            return cachedNounData[noun.Text];
        }
        public static IEnumerable<string> InternalLookup(Adverb verb) {
            return AdverbProvider[verb];
        }
        public static IEnumerable<string> InternalLookup(Adjective verb) {
            return AdjectiveProvider[verb];
        }
        public static IEnumerable<string> InternalLookup(Word word) {
            throw new NoSynonymLookupForTypeException(word) {
            };
        }
        public static NounThesaurus NounProvider {
            get;
            private set;
        }
        public static VerbThesaurus VerbProvider {
            get;
            private set;
        }
        public static AdjectiveThesaurus AdjectiveProvider {
            get;
            private set;
        }
        public static AdverbThesaurus AdverbProvider {
            get;
            private set;
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
        /// This takes two noun componentPhrases and determines if they are similar.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool IsSimilarTo(this NounPhrase lhs, NounPhrase rhs) {

            return getSimilarityRatio(lhs, rhs) > 0.6;
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
                }
                catch (NullReferenceException) {
                    return false;
                }
            }

            return result;
        }

        /// <summary>
        /// Determine if two noun componentPhrases are similar
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static double getSimilarityRatio(NounPhrase a, NounPhrase b) {
            NounPhrase outer = null;
            NounPhrase inner = null;
            double similarCount = 0;

            if (a.Words.Count() >= b.Words.Count()) {
                outer = a;
                inner = b;
            }
            else {
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
            }
            else
                return 1;

        }

        private static ConcurrentDictionary<string, IEnumerable<string>> cachedNounData = new ConcurrentDictionary<string, IEnumerable<string>>();
        private static ConcurrentDictionary<string, IEnumerable<string>> cachedVerbData = new ConcurrentDictionary<string, IEnumerable<string>>();

    }
}


