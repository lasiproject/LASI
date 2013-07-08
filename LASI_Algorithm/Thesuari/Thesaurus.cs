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
        #region Static Constructor

        static Thesaurus() {
            NounThesaurus = new NounThesaurus(nounThesaurusFilePath);
            VerbThesaurus = new VerbThesaurus(verbThesaurusFilePath);
            AdjectiveThesaurus = new AdjectiveThesaurus(adjectiveThesaurusFilePath);
            AdverbThesaurus = new AdverbThesaurus(adverbThesaurusFilePath);
        }

        #endregion

        #region Public Methods

        public static Task<string>[] GetTasksToLoadAllThesauri() {
            var Tasks = new List<Task<string>>();
            if (NounLoadingState == LoadingState.NotStarted)
                Tasks.Add(NounThesaurusLoadTask);
            if (VerbLoadingState == LoadingState.NotStarted)
                Tasks.Add(VerbThesaurusLoadTask);
            if (AdjectiveLoadingState == LoadingState.NotStarted)
                Tasks.Add(AdjectiveThesaurusLoadTask);
            if (AdverbLoadingState == LoadingState.NotStarted)
                Tasks.Add(AdverbThesaurusLoadTask);
            Tasks.Add(Task.Run(async () => {
                await LoadNameData();
                return "Loaded Name Data";
            }));
            return Tasks.ToArray();
        }



        public static Task<string> AdjectiveThesaurusLoadTask {
            get {
                return Task.Run(async () => {
                    AdjectiveLoadingState = LoadingState.InProgress;
                    await AdjectiveThesaurus.LoadAsync();
                    AdjectiveLoadingState = LoadingState.Finished;
                    return "Adjective Thesaurus Loaded";
                });
            }
        }

        public static Task<string> AdverbThesaurusLoadTask {
            get {
                return Task.Run(async () => {
                    AdverbLoadingState = LoadingState.InProgress;
                    await AdverbThesaurus.LoadAsync();
                    AdverbLoadingState = LoadingState.Finished;
                    return "Adverb Thesaurus Loaded";
                });
            }
        }

        public static Task<string> VerbThesaurusLoadTask {
            get {
                return Task.Run(async () => {
                    VerbLoadingState = LoadingState.InProgress;
                    await VerbThesaurus.LoadAsync();
                    VerbLoadingState = LoadingState.Finished;
                    return "Verb Thesaurus Loaded";
                });
            }
        }

        public static Task<string> NounThesaurusLoadTask {
            get {
                return Task.Run(async () => {
                    NounLoadingState = LoadingState.InProgress;
                    await NounThesaurus.LoadAsync();
                    NounLoadingState = LoadingState.Finished;
                    return "Noun Thesaurus Loaded";
                });

            }
        }

        public static IEnumerable<string> Lookup(Word word) {
            return InternalLookup(word as dynamic);
        }

        public static IEnumerable<string> LookupNoun(string nounText) {
            switch (NounLoadingState) {
                case LoadingState.Finished:
                    return cachedNounData.GetOrAdd(nounText, key => NounThesaurus[key]);
                case LoadingState.NotStarted:
                    NounThesaurusLoadTask.Wait();
                    return cachedNounData.GetOrAdd(nounText, key => NounThesaurus[key]);
                case LoadingState.InProgress:
                    throw new NounDataNotLoadedException("An attempt was made to access Noun data before loading could complete");
                default:
                    return Enumerable.Empty<string>();
            }
        }

        public static IEnumerable<string> LookupVerb(string verbText) {
            switch (VerbLoadingState) {
                case LoadingState.Finished:
                    return cachedVerbData.GetOrAdd(verbText, key => VerbThesaurus[key]);
                case LoadingState.NotStarted:
                    VerbThesaurusLoadTask.Wait();
                    return cachedVerbData.GetOrAdd(verbText, key => VerbThesaurus[key]);
                case LoadingState.InProgress:
                    throw new VerbDataNotLoadedException("An attempt was made to access Verb data before loading could complete");
                default:
                    return Enumerable.Empty<string>();
            }
        }

        public static IEnumerable<string> LookupAdjective(string adjectiveText) {
            switch (AdjectiveLoadingState) {
                case LoadingState.Finished:
                    return cachedAdjectiveData.GetOrAdd(adjectiveText, key => AdjectiveThesaurus[key]);
                case LoadingState.NotStarted:
                    AdjectiveThesaurusLoadTask.Wait();
                    return cachedAdjectiveData.GetOrAdd(adjectiveText, key => AdjectiveThesaurus[key]);
                case LoadingState.InProgress:
                    throw new AdjectiveDataNotLoadedException("An attempt was made to access Adjective data before loading could complete");
                default:
                    return Enumerable.Empty<string>();
            }
        }

        public static IEnumerable<string> LookupAdverb(string adverbText) {
            switch (AdverbLoadingState) {
                case LoadingState.Finished:
                    return cachedAdverbData.GetOrAdd(adverbText, key => AdverbThesaurus[key]);
                case LoadingState.NotStarted:
                    AdverbThesaurusLoadTask.Wait();
                    return cachedAdverbData.GetOrAdd(adverbText, key => AdverbThesaurus[key]);
                case LoadingState.InProgress:
                    throw new AdverbDataNotLoadedException("An attempt was made to access Adverb data before loading could complete");
                default:
                    return Enumerable.Empty<string>();
            }
        }

        /// <summary>
        /// Returns a value indicating wether the ProperNoun's text corresponds to a last name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a last name in the english language, false otherwise.</returns>
        public static bool IsLastName(this ProperNoun proper) {
            return lastNames.Contains(proper.Text);
        }
        /// <summary>
        /// Returns a value indicating wether the ProperNoun's text corresponds to a female first name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a female first name in the english language, false otherwise.</returns>
        public static bool IsFemaleName(this ProperNoun proper) {
            return femaleNames.Contains(proper.Text);
        }
        /// <summary>
        /// Returns a value indicating wether the ProperNoun's text corresponds to a male first name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a male first name in the english language, false otherwise.</returns>
        public static bool IsMaleName(this ProperNoun proper) {
            return maleNames.Contains(proper.Text);
        }

        /// <summary>
        /// Determines if two IEntity instances are similar.
        /// </summary>
        /// <param name="first">The first IEntity</param>
        /// <param name="second">The Second IEntity</param>
        /// <returns>True if the given IEntity instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// 1: <code>if ( Thesaurus.IsSimilarTo(e1, e2) ) { ... }
        /// 2: if ( e1.IsSimilarTo(e2) ) { ... }
        /// </code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this IEntity first, IEntity second) {
            if (first.Text.ToUpper() == second.Text.ToUpper()) {
                return true;
            }

            var n1 = first as Noun;
            var n2 = second as Noun;
            if (n1 != null && n2 != null) {
                return n1.IsSynonymFor(n2);
            }

            var np1 = first as NounPhrase;
            var np2 = second as NounPhrase;
            if (np1 != null && np2 != null) {
                return np1.IsSimilarTo(np2);
            }
            //need to add functionality to compare a Noun with a NounPhrase.


            var np = first as NounPhrase ?? second as NounPhrase;
            var n = first as Noun ?? second as Noun;
            if (n != null && np != null) {
                return n.IsSimilarTo(np);
            }
            return false;
        }

        public static bool IsSimilarTo(this Noun first, NounPhrase second) {
            var nouns = second.Words.GetNouns();
            return nouns.Count() == 1 && nouns.First().IsSynonymFor(first);
        }
        public static bool IsSimilarTo(this VerbPhrase first, Verb second) {
            return second.IsSimilarTo(first);
        }
        public static bool IsSimilarTo(this Verb first, VerbPhrase second) {
            var verbs = second.Words.GetVerbs();
            return verbs.Count() == 1 && verbs.First().IsSynonymFor(first);
        }

        /// <summary>
        /// Determines if two IVerbal instances are similar.
        /// </summary>
        /// <param name="first">The first IVerbal</param>
        /// <param name="second">The Second IVerbal</param>
        /// <returns>True if the given IVerbal instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// 1: <code>if ( Thesaurus.IsSimilarTo(v1, v2) ) { ... }
        /// 2: if ( v1.IsSimilarTo(v2) ) { ... }
        /// </code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this IVerbal first, IVerbal second) {

            //Compare literal text.
            if (first.Text.ToUpper() == second.Text.ToUpper()) {
                return true;
            }

            //If both are of type Verb check if syonymous
            var v1 = first as Verb;
            var v2 = second as Verb;
            if (v1 != null && v2 != null) {
                return v1.IsSynonymFor(v2);
            }

            //If both are of type VerbPhrase check for similarity
            var vp1 = first as VerbPhrase;
            var vp2 = second as VerbPhrase;
            if (vp1 != null && vp2 != null) {
                return vp1.IsSimilarTo(vp2);
            }

            //If one is of type Verb and the other is of Type VerbPhrase, test for similarirty.
            var vp = first as VerbPhrase ?? second as VerbPhrase;
            var v = first as Verb ?? second as Verb;
            if (v != null && vp != null) {
                return v.IsSimilarTo(vp);
            }

            return false;

        }
        /// <summary>
        /// Determines if two Noun instances are synonymous.
        /// </summary>
        /// <param name="word">The first Noun.</param>
        /// <param name="other">The second Noun</param>
        /// <returns>True if the Noun instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Noun word, Noun other) {
            return InternalLookup(word).Contains(other.Text);
        }
        /// <summary>
        /// Determines if two Verb instances are synonymous.
        /// </summary>
        /// <param name="word">The first Verb.</param>
        /// <param name="other">The second Verb</param>
        /// <returns>True if the Verb instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Verb word, Verb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        /// <summary>
        /// Determines if two Adjective instances are synonymous.
        /// </summary>
        /// <param name="word">The first Adjective.</param>
        /// <param name="other">The second Adjective</param>
        /// <returns>True if the Adjective instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Adjective word, Adjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        /// <summary>
        /// Determines if two Adverb instances are synonymous.
        /// </summary>
        /// <param name="word">The first Adverb.</param>
        /// <param name="other">The second Adverb</param>
        /// <returns>True if the Adverb instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Adverb word, Adverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        /// <summary>
        /// Determines if two Word instances are synonymous. Type checking is enforced.
        /// </summary>
        /// <param name="word">The first Word.</param>
        /// <param name="other">The second Word</param>
        /// <returns>True if the Word instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Word word, Word other) {

            return (word is Noun && other is Noun ||
                word is Verb && other is Verb ||
                word is Adverb && other is Adverb ||
                word is Adjective && other is Adjective
                ) && Lookup(other).Contains(word.Text);
        }


        /// <summary>
        /// Determines if two NounPhrases are similar.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The Second NounPhrase</param>
        /// <returns>True if the given NounPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// 1: <code>if ( Thesaurus.IsSimilarTo(vp1, vp2) ) { ... }
        /// 2: if ( vp1.IsSimilarTo(vp2) ) { ... }
        /// </code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this NounPhrase first, NounPhrase second) {

            return GetSimilarityRatio(first, second) > SIMILARITY_THRESHOLD;
        }

        /// <summary>
        /// Determines if two VerbPhrases are similar.
        /// </summary>
        /// <param name="first">The first VerbPhrase</param>
        /// <param name="second">The Second VerbPhrase</param>
        /// <returns>True if the given VerbPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// 1: <code>if ( Thesaurus.IsSimilarTo(vp1, vp2) ) { ... }
        /// 2: if ( vp1.IsSimilarTo(vp2) ) { ... }
        /// </code>
        /// Please prefer the second convention.
        /// </remarks>
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a double value indicating the degree of similarity between two NounPhrases.
        /// </summary>
        /// <param name="a">The first NounPhrase</param>
        /// <param name="b">The second NounPhrase</param>
        /// <returns>A double value indicating the degree of similarity between two NounPhrases.</returns>
        private static double GetSimilarityRatio(NounPhrase a, NounPhrase b) {
            NounPhrase outer = null;
            NounPhrase inner = null;
            double similarCount = 0.0d;

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
                        if (i.IsSimilarTo(o) || i.IsAliasFor(o))
                            similarCount += 0.7;
                    }
                    var scaleFactor = inner.Words.GetNouns().Count() * outer.Words.GetNouns().Count();
                    return (similarCount / scaleFactor == 0 ? 1 : scaleFactor);
                }

            }
            return 0d;
        }



        #region Internal Lookup Methods


        private static ISet<string> InternalLookup(Noun noun) {

            return cachedNounData.GetOrAdd(noun.Text, key => NounThesaurus[key]);
        }
        private static ISet<string> InternalLookup(Verb verb) {
            return cachedVerbData.GetOrAdd(verb.Text, key => VerbThesaurus[key]);
        }
        private static ISet<string> InternalLookup(Adverb adverb) {
            return cachedAdverbData.GetOrAdd(adverb.Text, key => AdverbThesaurus[key]);
        }
        private static ISet<string> InternalLookup(Adjective adjective) {
            return cachedAdjectiveData.GetOrAdd(adjective.Text, key => AdjectiveThesaurus[key]);
        }
        private static ISet<string> InternalLookup(Word word) {
            throw new NoSynonymLookupForTypeException(word);
        }
        private static async Task LoadNameData() {
            await Task.WhenAll(
                Task.Run(async () => {
                    using (var reader = new System.IO.StreamReader(lastNamesFilePath)) {
                        lastNames = new HashSet<string>((await reader.ReadToEndAsync()).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase);
                    }
                }),
                Task.Run(async () => {
                    using (var reader = new System.IO.StreamReader(femaleNamesFilePath)) {
                        femaleNames = new HashSet<string>((await reader.ReadToEndAsync()).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase);
                    }
                }),
                Task.Run(async () => {
                    using (var reader = new System.IO.StreamReader(maleNamesFilePath)) {
                        maleNames = new HashSet<string>((await reader.ReadToEndAsync()).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase);
                    }
                }));
        }
        #endregion

        #endregion

        #region Private Properties

        private static NounThesaurus NounThesaurus {
            get;
            set;
        }
        private static VerbThesaurus VerbThesaurus {
            get;
            set;
        }
        private static AdjectiveThesaurus AdjectiveThesaurus {
            get;
            set;
        }
        private static AdverbThesaurus AdverbThesaurus {
            get;
            set;
        }

        #endregion

        #region Private Fields

        //Loading states for specific data items
        private static LoadingState NounLoadingState = LoadingState.NotStarted;
        private static LoadingState VerbLoadingState = LoadingState.NotStarted;
        private static LoadingState AdjectiveLoadingState = LoadingState.NotStarted;
        private static LoadingState AdverbLoadingState = LoadingState.NotStarted;
        // Thesaurus File Paths
        private static readonly string nounThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        private static readonly string adverbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adv";
        private static readonly string adjectiveThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adj";
        // Name Data File Paths
        private static readonly string lastNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "last.txt";
        private static readonly string femaleNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "femalefirst.txt";
        private static readonly string maleNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "malefirst.txt";
        // Name Data Sets
        private static ISet<string> lastNames;
        private static ISet<string> femaleNames;
        private static ISet<string> maleNames;
        // Synonym Lookup Caches
        private static ConcurrentDictionary<string, ISet<string>> cachedNounData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.CurrentMax, 4096);
        private static ConcurrentDictionary<string, ISet<string>> cachedVerbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.CurrentMax, 4096);
        private static ConcurrentDictionary<string, ISet<string>> cachedAdjectiveData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.CurrentMax, 4096);
        private static ConcurrentDictionary<string, ISet<string>> cachedAdverbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.CurrentMax, 4096);
        private const double SIMILARITY_THRESHOLD = 0.6;

        #endregion

        #region Enumerations

        private enum LoadingState
        {
            NotStarted,
            InProgress,
            Finished
        }

        #endregion
    }
}


