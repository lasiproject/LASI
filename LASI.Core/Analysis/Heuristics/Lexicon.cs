using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LASI.Core.Heuristics.WordNet;
using System.Collections.Immutable;
using LASI.Core.Configuration;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Provides Comprehensive static facilities for Synonym Identification, Word and Phrase
    /// Comparison, Gender Stratification, and Named Entity Recognition.
    /// </summary>
    public static partial class Lexicon
    {
        #region Public Methods
        /// <summary>
        /// Clears all synonym caches
        /// </summary>
        public static void ClearAllCachedSynonymData()
        {
            ClearNounCache();
            ClearVerbCache();
            ClearAdjectiveCache();
            ClearAdverbCache();
        }

        /// <summary>
        /// Clears the cache of Adjective synonym data.
        /// </summary>
        public static void ClearAdjectiveCache() => cachedAdjectiveData.Clear();

        /// <summary>
        /// Clears the cache of Adverb synonym data.
        /// </summary>
        public static void ClearAdverbCache() => cachedAdverbData.Clear();

        /// <summary>
        /// Clears the cache of Noun synonym data.
        /// </summary>
        public static void ClearNounCache() => cachedNounData.Clear();

        /// <summary>
        /// Clears the cache of Verb synonym data.
        /// </summary>
        public static void ClearVerbCache() => cachedVerbData.Clear();

        /// <summary>
        /// Returns the synonyms for the provided Noun.
        /// </summary>
        /// <param name="noun">
        /// The Noun to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Noun.
        /// </returns>
        public static IEnumerable<string> GetSynonyms(this Noun noun) => cachedNounData.GetOrAdd(noun.Text, key => NounLookup[key]);

        /// <summary>
        /// Returns the synonyms for the provided Verb.
        /// </summary>
        /// <param name="verb">
        /// The Verb to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Verb.
        /// </returns>
        public static IEnumerable<string> GetSynonyms(this Verb verb) => cachedVerbData.GetOrAdd(verb.Text, key => VerbLookup[key]);


        /// <summary>
        /// Returns the synonyms for the provided Adjective.
        /// </summary>
        /// <param name="adjective">
        /// The Adjective to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Adjective.
        /// </returns>
        public static IEnumerable<string> GetSynonyms(this Adjective adjective) => cachedAdjectiveData.GetOrAdd(adjective.Text, key => AdjectiveLookup[key]);


        /// <summary>
        /// Returns the synonyms for the provided Adverb.
        /// </summary>
        /// <param name="adverb">
        /// The Adverb to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Adverb.
        /// </returns>
        public static IEnumerable<string> GetSynonyms(this Adverb adverb) => cachedAdverbData.GetOrAdd(adverb.Text, key => AdverbLookup[key]);

        #endregion Public Methods

        private static WordNetLookup<TWord> LazyLoad<TWord>(WordNetLookup<TWord> wordNetLookup) where TWord : Word
        {
            var resourceName = typeof(TWord).Name + " Association Map";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedMiliseconds = 0 });
            System.Diagnostics.Stopwatch timer;
            wordNetLookup.ProgressChanged += ResourceLoading;
            Action load = wordNetLookup.Load;
            load.WithTimer(out timer)();
            ResourceLoaded(wordNetLookup, new ResourceLoadEventArgs(resourceName, 1 / 5f)
            {
                ElapsedMiliseconds = timer.ElapsedMilliseconds
            });
            return wordNetLookup;
        }

        #region Properties

        /// <summary>
        /// Raised when a data set resource finishes loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoaded = delegate { };

        /// <summary>
        /// Raised when a resource starts loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoading = delegate { };

        /// <summary>
        /// Gets the sequence of strings corresponding to all nouns in the Scrabble Dictionary data source.
        /// </summary>
        public static IEnumerable<string> ScrabbleDictionary => scrabbleDictionary.Value;

        #endregion

        #region Events
        #endregion

        private static NameProvider NameData => nameData.Value;

        private static WordNetLookup<Adjective> AdjectiveLookup => lazyAdjectiveLookup.Value;

        private static WordNetLookup<Adverb> AdverbLookup => lazyAdverbLookup.Value;

        private static WordNetLookup<Noun> NounLookup => lazyNounLookup.Value;

        private static WordNetLookup<Verb> VerbLookup => lazyVerbLookup.Value;

        #region Private Fields

        // Resource Data File Paths

        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ratio of a
        /// similarity comparison is &gt;= the threshold, then the similarity comparison result will
        /// be considered as True in a boolean expression context.
        /// </summary>
        internal const double SimilarityThreshold = 0.6;

        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedAdjectiveData =
            new ConcurrentDictionary<string, IImmutableSet<string>>(
                concurrencyLevel: Concurrency.Max,
                capacity: 40960
            );

        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedAdverbData =
            new ConcurrentDictionary<string, IImmutableSet<string>>(
                concurrencyLevel: Concurrency.Max,
                capacity: 40960
            );

        // Synonym LexicalLookup Caches
        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedNounData =
            new ConcurrentDictionary<string, IImmutableSet<string>>(
                concurrencyLevel: Concurrency.Max,
                capacity: 40960
            );

        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedVerbData =
            new ConcurrentDictionary<string, IImmutableSet<string>>(
                concurrencyLevel: Concurrency.Max,
                capacity: 40960
            );

        private static Lazy<NameProvider> nameData = new Lazy<NameProvider>(() =>
        {
            var resourceName = "Name Data";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var nameProvider = new NameProvider();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0)
            {
                ElapsedMiliseconds = timer.ElapsedMilliseconds
            });
            return nameProvider;
        }, isThreadSafe: true);

        // scrabble dictionary Internal Lookups
        private static Lazy<IEnumerable<string>> scrabbleDictionary = new Lazy<IEnumerable<string>>(() =>
        {
            var resourceName = "Scrabble Dictionary";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            System.Diagnostics.Stopwatch timer;

            Func<IImmutableSet<string>> loadWords = () => File.ReadAllText(Paths.ScrabbleDict)
                .SplitRemoveEmpty('\r', '\n')
                .Select(s => s.ToLower())
                .Except(NameData.AllNames, OrdinalIgnoreCase)
                .ToImmutableHashSet(OrdinalIgnoreCase);
            var words = loadWords.WithTimer(out timer)();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedMiliseconds = timer.ElapsedMilliseconds });
            return words;
        }, isThreadSafe: true);

        private static Lazy<WordNetLookup<Noun>> lazyNounLookup =
            new Lazy<WordNetLookup<Noun>>(() => LazyLoad(new NounLookup(Paths.WordNet.Noun)), isThreadSafe: true);

        private static Lazy<WordNetLookup<Verb>> lazyVerbLookup =
            new Lazy<WordNetLookup<Verb>>(() => LazyLoad(new VerbLookup(Paths.WordNet.Verb)), isThreadSafe: true);

        private static Lazy<WordNetLookup<Adjective>> lazyAdjectiveLookup =
            new Lazy<WordNetLookup<Adjective>>(() => LazyLoad(new AdjectiveLookup(Paths.WordNet.Adjective)), isThreadSafe: true);

        private static Lazy<WordNetLookup<Adverb>> lazyAdverbLookup =
            new Lazy<WordNetLookup<Adverb>>(() => LazyLoad(new AdverbLookup(Paths.WordNet.Adverb, AdjectiveLookup)), isThreadSafe: true);

        #endregion
        private static readonly StringComparer OrdinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
    }
}