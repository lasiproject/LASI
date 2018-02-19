using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using LASI.Core.Configuration;
using LASI.Core.Heuristics.WordNet;
using LASI.Utilities;
using static LASI.Utilities.FunctionExtensions;
using ConcurrentSetDictionary = System.Collections.Concurrent.ConcurrentDictionary<string, System.Collections.Immutable.IImmutableSet<string>>;

namespace LASI.Core.Heuristics
{
    /// <summary>
    /// Provides Comprehensive static facilities for Synonym Identification, Word and Phrase
    /// Comparison, Gender Stratification, and Named Entity Recognition.
    /// </summary>
    public static class Lexicon
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
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0, 0));
            wordNetLookup.ProgressChanged += ResourceLoading;

            var (load, timer) = Time(wordNetLookup.Load);
            load();
            ResourceLoaded(wordNetLookup, new ResourceLoadEventArgs(resourceName, increment: 1 / 5f, elapsedMilliseconds: timer.ElapsedMilliseconds));
            return wordNetLookup;
        }

        #region Events
        internal static void OnResourceLoaded(object sender, ResourceLoadEventArgs e) => ResourceLoaded(sender, e);
        internal static void OnResourceLoading(object sender, ResourceLoadEventArgs e) => ResourceLoading(sender, e);

        /// <summary>
        /// Raised when a data set resource finishes loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoaded = (s, e) => { };

        /// <summary>
        /// Raised when a resource starts loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoading = (s, e) => { };
        #endregion

        #region Properties



        static WordNetLookup<Adjective> AdjectiveLookup => lazyAdjectiveLookup.Value;

        static WordNetLookup<Adverb> AdverbLookup => lazyAdverbLookup.Value;

        static WordNetLookup<Noun> NounLookup => lazyNounLookup.Value;

        static WordNetLookup<Verb> VerbLookup => lazyVerbLookup.Value;
        #endregion

        #region Private Fields

        // Resource Data File Paths

        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ratio of a
        /// similarity comparison is &gt;= the threshold, then the similarity comparison result will
        /// be considered as True in a boolean expression context.
        /// </summary>
        internal const double SimilarityThreshold = 0.6;

        static readonly ConcurrentSetDictionary cachedAdjectiveData = CreateConcurrentSetDictionary();

        static readonly ConcurrentSetDictionary cachedAdverbData = CreateConcurrentSetDictionary();

        // Synonym LexicalLookup Caches
        static readonly ConcurrentSetDictionary cachedNounData = CreateConcurrentSetDictionary();

        static readonly ConcurrentSetDictionary cachedVerbData = CreateConcurrentSetDictionary();


        static readonly Lazy<WordNetLookup<Noun>> lazyNounLookup = CreateLazyWordNetLookup(() => new NounLookup(Paths.WordNet.Noun));

        static readonly Lazy<WordNetLookup<Verb>> lazyVerbLookup = CreateLazyWordNetLookup(() => new VerbLookup(Paths.WordNet.Verb));

        static readonly Lazy<WordNetLookup<Adjective>> lazyAdjectiveLookup = CreateLazyWordNetLookup(() => new AdjectiveLookup(Paths.WordNet.Adjective));

        static readonly Lazy<WordNetLookup<Adverb>> lazyAdverbLookup = CreateLazyWordNetLookup(() => new AdverbLookup(Paths.WordNet.Adverb, AdjectiveLookup));

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
