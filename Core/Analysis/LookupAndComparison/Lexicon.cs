using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LASI.Core.Heuristics.WordNet;
using LASI.Core.Reporting;

namespace LASI.Core.Heuristics
{
    using System.Collections.Immutable;
    using Utilities;

    /// <summary>
    /// Provides Comprehensive static facilities for Synonym Identification, Word and Phrase
    /// Comparison, Gender Stratification, and Named Entity Recognition.
    /// </summary>
    public static partial class Lexicon
    {
        #region Public Methods
        /// <summary>
        /// Clears the cache of Adjective synonym data.
        /// </summary>
        public static void ClearAdjectiveCache() => cachedAdjectiveData.Clear();

        /// <summary>
        /// Clears the cache of Adverb synonym data.
        /// </summary>
        public static void ClearAdverbCache() => cachedAdverbData.Clear();

        /// <summary>
        /// Clears all cached adjective
        /// </summary>
        public static void ClearAllCachedSynonymData() {
            ClearAdjectiveCache();
            ClearVerbCache();
            ClearAdverbCache();
            ClearAdjectiveCache();
        }

        /// <summary>
        /// Clears the cache of Noun synonym data.
        /// </summary>
        public static void ClearNounCache() => cachedNounData.Clear();

        /// <summary>
        /// Clears the cache of Verb synonym data.
        /// </summary>
        public static void ClearVerbCache() => cachedVerbData.Clear();

        /// <summary>
        /// Returns a NameGender value indicating the likely gender of the entity.
        /// </summary>
        /// <param name="entity">
        /// The entity whose gender to lookup.
        /// </param>
        /// <returns>
        /// A NameGender value indicating the likely gender of the entity.
        /// </returns>
        public static Gender GetGender(this IEntity entity) {
            return entity.Match()
                    .Case((ISimpleGendered p) => p.Gender)
                    .Case((IReferencer p) => GetGender(p))
                    .Case((NounPhrase n) => DetermineNounPhraseGender(n))
                    .Case((CommonNoun n) => Gender.Neutral)
                    .Case((IEntity e) => (
                        from referener in e.Referencers
                        let gendered = referener as ISimpleGendered
                        let gender = gendered != null ? gendered.Gender : default(Gender)
                        group gender by gender into byGender
                        orderby byGender.Count() descending
                        select byGender.Key).DefaultIfEmpty().First(), when: e => e.Referencers.Any())
                    .Result();
        }

        /// <summary>
        /// Returns the synonyms for the provided Noun.
        /// </summary>
        /// <param name="noun">
        /// The Noun to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Noun.
        /// </returns>
        public static IImmutableSet<string> GetSynonyms(this Noun noun) {
            return cachedNounData.GetOrAdd(noun.Text, key => NounLookup[key]);
        }

        /// <summary>
        /// Returns the synonyms for the provided Verb.
        /// </summary>
        /// <param name="verb">
        /// The Verb to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Verb.
        /// </returns>
        public static IImmutableSet<string> GetSynonyms(this Verb verb) {
            return cachedVerbData.GetOrAdd(verb.Text, key => VerbLookup[key]);
        }

        /// <summary>
        /// Returns the synonyms for the provided Adjective.
        /// </summary>
        /// <param name="adjective">
        /// The Adjective to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Adjective.
        /// </returns>
        public static IImmutableSet<string> GetSynonyms(this Adjective adjective) {
            return cachedAdjectiveData.GetOrAdd(adjective.Text, key => AdjectiveLookup[key]);
        }

        /// <summary>
        /// Returns the synonyms for the provided Adverb.
        /// </summary>
        /// <param name="adverb">
        /// The Adverb to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Adverb.
        /// </returns>
        public static IImmutableSet<string> GetSynonyms(this Adverb adverb) {
            return cachedAdverbData.GetOrAdd(adverb.Text, key => AdverbLookup[key]);
        }

        /// <summary>
        /// Determines if two Noun instances are synonymous.
        /// </summary>
        /// <param name="first">
        /// The first Noun.
        /// </param>
        /// <param name="second">
        /// The second Noun
        /// </param>
        /// <returns>
        /// <c>true</c> if the Noun instances are synonymous; otherwise, <c>false</c>.
        /// </returns> 
        public static bool IsSynonymFor(this Noun first, Noun second) => first?.GetSynonyms().Contains(second?.Text) ?? false;

        /// <summary>
        /// Determines if two Verb instances are synonymous.
        /// </summary>
        /// <param name="first">
        /// The first Verb.
        /// </param>
        /// <param name="second">
        /// The second Verb
        /// </param>
        /// <returns>
        /// <c>true</c> if the Verb instances are synonymous; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSynonymFor(this Verb first, Verb second) => first?.GetSynonyms().Contains(second?.Text) ?? false;

        /// <summary>
        /// Determines if two Adjective instances are synonymous.
        /// </summary>
        /// <param name="first">
        /// The first Adjective.
        /// </param>
        /// <param name="second">
        /// The second Adjective
        /// </param>
        /// <returns>
        /// <c>true</c> if the Adjective instances are synonymous; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSynonymFor(this Adjective first, Adjective second) => first?.GetSynonyms().Contains(second?.Text) ?? false;

        /// <summary>
        /// Determines if two Adverb instances are synonymous.
        /// </summary>
        /// <param name="first">
        /// The first Adverb.
        /// </param>
        /// <param name="second">
        /// The second Adverb
        /// </param>
        /// <returns>
        /// <c>true</c> if the Adverb instances are synonymous; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSynonymFor(this Adverb first, Adverb second) => first?.GetSynonyms().Contains(second?.Text) ?? false;

        #endregion Public Methods

        #region Synonym Lookup Methods
        #endregion Synonym Lookup Methods

        #region Internal Synonym Lookup Methods

        #endregion

        private static WordNetLookup<TWord> LazyLoad<TWord>(WordNetLookup<TWord> wordNetLookup) where TWord : Word {
            var resourceName = typeof(TWord).Name + " Association Map";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            wordNetLookup.ProgressChanged += ResourceLoading;
            wordNetLookup.Load();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 1 / 5f) { ElapsedMiliseconds = timer.ElapsedMilliseconds });
            return wordNetLookup;
        }

        #region Properties

        /// <summary>
        /// Raised when a data set resource finishes loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoaded = delegate
        { };

        /// <summary>
        /// Raised when a resource starts loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoading = delegate
        { };

        /// <summary>
        /// Gets the sequence of strings corresponding to all nouns in the Scrabble Dictionary data source.
        /// </summary>
        public static IEnumerable<string> ScrabbleDictionary => scrabbleDictionary.Value;

        #endregion

        #region Events
        #endregion

        private static NameProvider NameData => names.Value;

        private static WordNetLookup<Adjective> AdjectiveLookup => adjectiveLookup.Value;

        private static WordNetLookup<Adverb> AdverbLookup => adverbLookup.Value;

        private static WordNetLookup<Noun> NounLookup => nounLookup.Value;

        private static WordNetLookup<Verb> VerbLookup => verbLookup.Value;

        #region Private Fields

        // Resource Data File Paths

        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ration of a
        /// similarity comparison is &gt;= the threshold, then the similarity comparison result will
        /// be considered as True in a boolean expression context.
        /// </summary>
        internal const double SIMILARITY_THRESHOLD = 0.6;

        private static Lazy<WordNetLookup<Adjective>> adjectiveLookup =
                    new Lazy<WordNetLookup<Adjective>>(() => LazyLoad(new AdjectiveLookup(Paths.WordNet.Adjective)), true);

        private static Lazy<WordNetLookup<Adverb>> adverbLookup =
                    new Lazy<WordNetLookup<Adverb>>(() => LazyLoad(new AdverbLookup(Paths.WordNet.Adverb)), true);

        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedAdjectiveData = new ConcurrentDictionary<string, IImmutableSet<string>>(
                    concurrencyLevel: Concurrency.Max,
                    capacity: 40960
                );

        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedAdverbData = new ConcurrentDictionary<string, IImmutableSet<string>>(
                    concurrencyLevel: Concurrency.Max,
                    capacity: 40960
                );

        // Synonym LexicalLookup Caches
        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedNounData = new ConcurrentDictionary<string, IImmutableSet<string>>(
            concurrencyLevel: Concurrency.Max,
            capacity: 40960
        );

        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedVerbData = new ConcurrentDictionary<string, IImmutableSet<string>>(
                    concurrencyLevel: Concurrency.Max,
                    capacity: 40960
                );

        private static Lazy<NameProvider> names = new Lazy<NameProvider>(() => {
            var resourceName = "Name Data";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var val = new NameProvider();
            val.Load();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0)
            {
                ElapsedMiliseconds = timer.ElapsedMilliseconds
            });
            return val;
        }, true);

        // scrabble dictionary Internal Lookups
        private static Lazy<WordNetLookup<Noun>> nounLookup =
            new Lazy<WordNetLookup<Noun>>(() => LazyLoad(new NounLookup(Paths.WordNet.Noun)), true);

        private static Lazy<ISet<string>> scrabbleDictionary = new Lazy<ISet<string>>(() => {
            var resourceName = "Scrabble Dictionary";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            System.Diagnostics.Stopwatch timer;
            var words = FunctionExtensions.InvokeWithTimer(() => {
                using (var reader = new StreamReader(Paths.ScrabbleDict)) {
                    return reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                             .Select(s => s.ToLower())
                             .Except(NameData.AllNames, IgnoreCase)
                             .ToImmutableHashSet(IgnoreCase);
                }
            }, out timer);
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedMiliseconds = timer.ElapsedMilliseconds });
            return words;
        }, true);

        private static Lazy<WordNetLookup<Verb>> verbLookup =
                    new Lazy<WordNetLookup<Verb>>(() => LazyLoad(new VerbLookup(Paths.WordNet.Verb)), true);

        #region Name Lookup Methods
        #endregion

        #endregion
    }
}