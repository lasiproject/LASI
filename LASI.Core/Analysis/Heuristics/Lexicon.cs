using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using LASI.Core.Configuration;
using LASI.Core.Heuristics.WordNet;
using LASI.Utilities;
using static System.StringComparer;
using ConcurrentSetDictionary = System.Collections.Concurrent.ConcurrentDictionary<string, System.Collections.Immutable.IImmutableSet<string>>;

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
            cachedAdjectiveData.Clear();
            cachedAdverbData.Clear();
            cachedNounData.Clear();
            cachedVerbData.Clear();
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
        public static IEnumerable<string> GetSynonyms(this Noun noun) => GetSynonymsCore(noun, cachedNounData, NounLookup);

        /// <summary>
        /// Returns the synonyms for the provided Verb.
        /// </summary>
        /// <param name="verb">
        /// The Verb to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Verb.
        /// </returns>
        public static IEnumerable<string> GetSynonyms(this Verb verb) => GetSynonymsCore(verb, cachedVerbData, VerbLookup);

        /// <summary>
        /// Returns the synonyms for the provided Adjective.
        /// </summary>
        /// <param name="adjective">
        /// The Adjective to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Adjective.
        /// </returns>
        public static IEnumerable<string> GetSynonyms(this Adjective adjective) => GetSynonymsCore(adjective, cachedAdjectiveData, AdjectiveLookup);


        /// <summary>
        /// Returns the synonyms for the provided Adverb.
        /// </summary>
        /// <param name="adverb">
        /// The Adverb to lookup.
        /// </param>
        /// <returns>
        /// The synonyms for the provided Adverb.
        /// </returns>
        public static IEnumerable<string> GetSynonyms(this Adverb adverb) => GetSynonymsCore(adverb, cachedAdverbData, AdverbLookup);


        static IImmutableSet<string> GetSynonymsCore<TWord>(TWord word, ConcurrentSetDictionary cache, WordNetLookup<TWord> lookup) where TWord : Word => cache.GetOrAdd(word.Text, key => lookup[key]);

        #endregion Public Methods

        static WordNetLookup<TWord> LazyLoad<TWord>(WordNetLookup<TWord> wordNetLookup) where TWord : Word
        {
            var resourceName = typeof(TWord).Name + " Association Map";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0)
            {
                ElapsedMiliseconds = 0
            });
            System.Diagnostics.Stopwatch timer;
            wordNetLookup.ProgressChanged += ResourceLoading;
            Action load = wordNetLookup.Load;
            load.WithTimer(out timer)();
            ResourceLoaded(wordNetLookup, new ResourceLoadEventArgs(resourceName, increment: 1 / 5f)
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
        /// The sequence of strings corresponding to all nouns in the Scrabble Dictionary data source.
        /// </summary>
        public static IEnumerable<string> ScrabbleDictionary => scrabbleDictionary.Value;

        #endregion

        #region Events
        #endregion

        static NameProvider NameData => nameData.Value;

        static WordNetLookup<Adjective> AdjectiveLookup => lazyAdjectiveLookup.Value;

        static WordNetLookup<Adverb> AdverbLookup => lazyAdverbLookup.Value;

        static WordNetLookup<Noun> NounLookup => lazyNounLookup.Value;

        static WordNetLookup<Verb> VerbLookup => lazyVerbLookup.Value;

        #region Private Fields

        // Resource Data File Paths

        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ratio of a
        /// similarity comparison is &gt;= the threshold, then the similarity comparison result will
        /// be considered as True in a boolean expression context.
        /// </summary>
        internal const double SimilarityThreshold = 0.6;

        static ConcurrentSetDictionary cachedAdjectiveData = CreateConcurrentSetDictionary();

        static ConcurrentSetDictionary cachedAdverbData = CreateConcurrentSetDictionary();

        // Synonym LexicalLookup Caches
        static ConcurrentSetDictionary cachedNounData = CreateConcurrentSetDictionary();

        static ConcurrentSetDictionary cachedVerbData = CreateConcurrentSetDictionary();

        static Lazy<NameProvider> nameData = new Lazy<NameProvider>(() =>
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
        static Lazy<IEnumerable<string>> scrabbleDictionary = new Lazy<IEnumerable<string>>(() =>
        {
            var resourceName = "Scrabble Dictionary";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));

            Func<IImmutableSet<string>> loadWords = () => File.ReadAllText(Paths.ScrabbleDict)
                .SplitRemoveEmpty('\r', '\n')
                .Select(s => s.ToLower())
                .Except(NameData.AllNames, OrdinalIgnoreCase)
                .ToImmutableHashSet(OrdinalIgnoreCase);
            var (timed, timer) = loadWords.WithTimer();
            var words = timed();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedMiliseconds = timer.ElapsedMilliseconds });
            return words;
        }, isThreadSafe: true);

        static Lazy<WordNetLookup<Noun>> lazyNounLookup = CreateLazyWordNetLookup(() => new NounLookup(Paths.WordNet.Noun));

        static Lazy<WordNetLookup<Verb>> lazyVerbLookup = CreateLazyWordNetLookup(() => new VerbLookup(Paths.WordNet.Verb));

        static Lazy<WordNetLookup<Adjective>> lazyAdjectiveLookup = CreateLazyWordNetLookup(() => new AdjectiveLookup(Paths.WordNet.Adjective));

        static Lazy<WordNetLookup<Adverb>> lazyAdverbLookup = CreateLazyWordNetLookup(() => new AdverbLookup(Paths.WordNet.Adverb, AdjectiveLookup));

        #endregion

        static ConcurrentSetDictionary CreateConcurrentSetDictionary() => new ConcurrentDictionary<string, IImmutableSet<string>>(
            concurrencyLevel: Concurrency.Max,
            capacity: 40960
        );

        static Lazy<WordNetLookup<TWord>> CreateLazyWordNetLookup<TWord>(Func<WordNetLookup<TWord>> factory) where TWord : Word => new Lazy<WordNetLookup<TWord>>(
            valueFactory: () => LazyLoad(factory()),
            isThreadSafe: true
        );
    }
}
