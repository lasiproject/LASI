using LASI.Core.Interop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LASI.Core.Heuristics.WordNet;

namespace LASI.Core.Heuristics
{
    using System.Collections.Immutable;
    using Analysis.PatternMatching.LexicalSpecific.Experimental;
    using LASI.Utilities;


    /// <summary>
    /// Provides Comprehensive static facilities for Synonym Identification, Word and Phrase Comparison, Gender Stratification, and Named Entity Recognition.
    /// </summary>
    public static partial class Lookup
    {
        #region Public Methods

        #region Name Gender Lookup Methods

        /// <summary>
        /// Returns a NameGender value indicating the likely gender of the entity.
        /// </summary>
        /// <param name="entity">The entity whose gender to lookup.</param>
        /// <returns>A NameGender value indicating the likely gender of the entity.</returns>
        public static Gender GetGender(this IEntity entity) {
            return entity | new Match<Gender>
            {
                SimpleGendered = e => e.Gender,
                Referencer = r => GetGender(r),
                NounPhrase = n => DetermineNounPhraseGender(n),
                CommonNoun = n => Gender.Neutral,
                Entity = e => (from referener in e.Referencers
                               let gender = ((referener as ISimpleGendered)?.Gender).GetValueOrDefault()
                               group gender by gender).MaxBy(g => g.Count()).Key
            };
        }
        /// <summary>
        /// Returns a NameGender value indicating the likely gender of the Pronoun based on its referent if known, or else its PronounKind.
        /// </summary>
        /// <param name="referencer">The Pronoun whose gender to lookup.</param>
        /// <returns>A NameGender value indicating the likely gender of the Pronoun.</returns>
        private static Gender GetGender(IReferencer referencer) {
            return referencer.Match().Yield<Gender>()
                    .With((PronounPhrase p) => DeterminePronounPhraseGender(p))
                    .When(referencer.RefersTo != null)
                    .Then((from referent in referencer.RefersTo
                           let gender =
                           referent.Match().Yield<Gender>()
                              .With((NounPhrase n) => DetermineNounPhraseGender(n))
                              .With((Pronoun r) => r.Gender)
                              .With((ProperSingularNoun r) => r.Gender)
                              .With((CommonNoun n) => Gender.Neutral)
                              .Result()
                           group gender by gender into byGender
                           where byGender.Count() == referencer.RefersTo.Count()
                           select byGender.Key).FirstOrDefault())
                    .With((ISimpleGendered p) => p.Gender)
                    .Result();
        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Female Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Female Name; otherwise, false.</returns>
        public static bool IsFemaleFull(this NounPhrase name) {
            return DetermineNounPhraseGender(name).IsFemale();
        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Male Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Male Name; otherwise, false.</returns>
        public static bool IsMaleFull(this NounPhrase name) {
            return DetermineNounPhraseGender(name).IsMale();
        }

        // TODO: refactor these two methods. their interaction is very opaque and error prone. Although they are private, they make maintaining related algorithms difficult.
        #region
        private static Gender DetermineNounPhraseGender(NounPhrase name) {
            var properNouns = name.Words.OfProperNoun();
            var first = properNouns.OfSingular()
                .FirstOrDefault(n => n.Gender.IsMaleOrFemale());
            var last = properNouns.LastOrDefault(n => n != first && n.IsLastName());
            return first != null && (last != null || properNouns.All(n => n.GetGender() == first.Gender)) ?
                first.Gender :
                name.Words.OfNoun().All(n => n.GetGender().IsNeutral()) ?
                Gender.Neutral :
                Gender.Undetermined;
        }
        private static Gender DeterminePronounPhraseGender(PronounPhrase pronounPhrase) {
            if (pronounPhrase.Words.All(w => w is Determiner)) { return Gender.Undetermined; }
            var genders = pronounPhrase.Words.OfType<ISimpleGendered>().Select(w => w.Gender);
            bool any = genders.Any();
            return pronounPhrase.Words.OfProperNoun().Any(n => !(n is ISimpleGendered)) ?
                DetermineNounPhraseGender(pronounPhrase) :
                any && genders.All(g => g.IsFemale()) ? Gender.Female :
                any && genders.All(g => g.IsMale()) ? Gender.Male :
                any && genders.All(g => g.IsNeutral()) ? Gender.Neutral :
                Gender.Undetermined;
        }
        #endregion

        #endregion

        #region Synonym Lookup Methods

        /// <summary>
        /// Returns the synonyms for the provided Noun.
        /// </summary>
        /// <param name="noun">The Noun to lookup.</param>
        /// <returns>The synonyms for the provided Noun.</returns>
        public static IImmutableSet<string> GetSynonyms(this Noun noun) {
            return cachedNounData.GetOrAdd(noun.Text, key => NounLookup[key]);
        }
        /// <summary>
        /// Returns the synonyms for the provided Verb.
        /// </summary>
        /// <param name="verb">The Verb to lookup.</param>
        /// <returns>The synonyms for the provided Verb.</returns>
        public static IImmutableSet<string> GetSynonyms(this Verb verb) {
            return cachedVerbData.GetOrAdd(verb.Text, key => VerbLookup[key]);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adjective.
        /// </summary>
        /// <param name="adjective">The Adjective to lookup.</param>
        /// <returns>The synonyms for the provided Adjective.</returns>
        public static IImmutableSet<string> GetSynonyms(this Adjective adjective) {
            return cachedAdjectiveData.GetOrAdd(adjective.Text, key => AdjectiveLookup[key]);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adverb.
        /// </summary>
        /// <param name="adverb">The Adverb to lookup.</param>
        /// <returns>The synonyms for the provided Adverb.</returns>
        public static IImmutableSet<string> GetSynonyms(this Adverb adverb) {
            return cachedAdverbData.GetOrAdd(adverb.Text, key => AdverbLookup[key]);
        }
        /// <summary>
        /// Determines if two Noun instances are synonymous.
        /// </summary>
        /// <param name="first">The first Noun.</param>
        /// <param name="second">The second Noun</param>
        /// <returns>True if the Noun instances are synonymous; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if (Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if (vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Noun first, Noun second) => first?.GetSynonyms().Contains(second?.Text) ?? false;
        /// <summary>
        /// Determines if two Verb instances are synonymous.
        /// </summary>
        /// <param name="first">The first Verb.</param>
        /// <param name="second">The second Verb</param>
        /// <returns>True if the Verb instances are synonymous; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if (Lookup.IsSimilarTo(vp1, vp2)) { ... }</code>
        /// <code>if (vp1.IsSimilarTo(vp2)) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Verb first, Verb second) => first?.GetSynonyms().Contains(second?.Text) ?? false;

        /// <summary>
        /// Determines if two Adjective instances are synonymous.
        /// </summary>
        /// <param name="first">The first Adjective.</param>
        /// <param name="second">The second Adjective</param>
        /// <returns>True if the Adjective instances are synonymous; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if (Lookup.IsSimilarTo(vp1, vp2)) { ... }</code>
        /// <code>if (vp1.IsSimilarTo(vp2)) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Adjective first, Adjective second) => first?.GetSynonyms().Contains(second?.Text) ?? false;
        /// <summary>
        /// Determines if two Adverb instances are synonymous.
        /// </summary>
        /// <param name="first">The first Adverb.</param>
        /// <param name="second">The second Adverb</param>
        /// <returns>True if the Adverb instances are synonymous; otherwise, false.</returns>
        public static bool IsSynonymFor(this Adverb first, Adverb second) => first?.GetSynonyms().Contains(second?.Text) ?? false;


        #endregion

        #endregion

        /// <summary>
        /// Clears the cache of Noun synonym data.
        /// </summary>
        public static void ClearNounCache() => cachedNounData.Clear();
        /// <summary>
        /// Clears the cache of Verb synonym data.
        /// </summary>
        public static void ClearVerbCache() => cachedVerbData.Clear();
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
            ClearAdjectiveCache(); ClearVerbCache(); ClearAdverbCache(); ClearAdjectiveCache();
        }


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
        /// Gets the sequence of strings corresponding to all nouns in the Scrabble Dictionary data source.
        /// </summary>
        public static IEnumerable<string> ScrabbleDictionary => scrabbleDictionary.Value;

        #endregion

        #region Events
        /// <summary>
        /// Raised when a resource starts loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoading = delegate { };
        /// <summary>
        /// Raised when a data set resource finishes loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoaded = delegate { };

        #endregion

        private static WordNetLookup<Noun> NounLookup => nounLookup.Value;

        private static WordNetLookup<Verb> VerbLookup => verbLookup.Value;

        private static WordNetLookup<Adjective> AdjectiveLookup => adjectiveLookup.Value;

        private static WordNetLookup<Adverb> AdverbLookup => adverbLookup.Value;

        #region Private Fields
        // Resource Data File Paths
        private static readonly string resourcesDirectory = ConfigurationManager.AppSettings["ResourcesDirectory"];
        private static readonly string wordnetDataDirectory = resourcesDirectory + ConfigurationManager.AppSettings["WordnetFileDirectory"];
        private static readonly string nounWNFilePath = wordnetDataDirectory + "data.noun";
        private static readonly string verbWNFilePath = wordnetDataDirectory + "data.verb";
        private static readonly string adverbWNFilePath = wordnetDataDirectory + "data.adv";
        private static readonly string adjectiveWNFilePath = wordnetDataDirectory + "data.adj";
        private static readonly string scrabbleDictsFilePath = wordnetDataDirectory + "dictionary.txt";
        // scrabble dictionary
        // Internal Lookups
        private static Lazy<WordNetLookup<Noun>> nounLookup =
            new Lazy<WordNetLookup<Noun>>(() => LazyLoad(new NounLookup(nounWNFilePath)), true);
        private static Lazy<WordNetLookup<Verb>> verbLookup =
            new Lazy<WordNetLookup<Verb>>(() => LazyLoad(new VerbLookup(verbWNFilePath)), true);
        private static Lazy<WordNetLookup<Adverb>> adverbLookup =
            new Lazy<WordNetLookup<Adverb>>(() => LazyLoad(new AdverbLookup(adverbWNFilePath)), true);
        private static Lazy<WordNetLookup<Adjective>> adjectiveLookup =
            new Lazy<WordNetLookup<Adjective>>(() => LazyLoad(new AdjectiveLookup(adjectiveWNFilePath)), true);

        // Synonym LexicalLookup Caches
        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedNounData = new ConcurrentDictionary<string, IImmutableSet<string>>(
            concurrencyLevel: Concurrency.Max,
            capacity: 40960
        );
        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedVerbData = new ConcurrentDictionary<string, IImmutableSet<string>>(
            concurrencyLevel: Concurrency.Max,
            capacity: 40960
        );
        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedAdjectiveData = new ConcurrentDictionary<string, IImmutableSet<string>>(
            concurrencyLevel: Concurrency.Max,
            capacity: 40960
        );
        private static ConcurrentDictionary<string, IImmutableSet<string>> cachedAdverbData = new ConcurrentDictionary<string, IImmutableSet<string>>(
            concurrencyLevel: Concurrency.Max,
            capacity: 40960
        );

        private static Lazy<NameProvider> names = new Lazy<NameProvider>(() => {
            var resourceName = "Name Data";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var val = new NameProvider();
            val.Load();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedMiliseconds = timer.ElapsedMilliseconds });
            return val;
        }, true);

        private static NameProvider NameData { get { return names.Value; } }

        private static Lazy<ISet<string>> scrabbleDictionary = new Lazy<ISet<string>>(() => {
            var resourceName = "Scrabble Dictionary";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var stopwatch = FunctionExtensions.InvokeTimed(() => {
                using (var reader = new StreamReader(scrabbleDictsFilePath)) {
                    return reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                             .Select(s => s.ToLower())
                             .Except(NameData.AllNames, IgnoreCase)
                             .ToHashSet(IgnoreCase);
                }
            });
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedMiliseconds = stopwatch.Item1.ElapsedMilliseconds });
            return stopwatch.Item2;
        }, true);


        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ration of a similarity comparison is >= the threshold, 
        /// then the similarity comparison will return true.
        /// </summary>
        const double SIMILARITY_THRESHOLD = 0.6;

        #region Name Lookup Methods
        /// <summary>
        /// Determines whether the provided ProperNoun is a FirstName.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the provided ProperNoun is a FirstName; otherwise, false.</returns>
        public static bool IsFirstName(this ProperNoun proper) {
            return NameData.IsFirstName(proper.Text);
        }
        /// <summary>
        /// Determines whether the ProperNoun's text corresponds to a last name in the English language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the ProperNoun's text corresponds to a last name in the English language; otherwise, false.</returns>
        public static bool IsLastName(this ProperNoun proper) {
            return NameData.IsLastName(proper.Text);
        }
        /// <summary>
        /// Determines whether the ProperNoun's text corresponds to a female first name in the English language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a female first name in the English language; otherwise, false.</returns>
        public static bool IsFemaleFirst(this ProperNoun proper) {
            return NameData.IsFemaleFirst(proper.Text);
        }
        /// <summary>
        /// Returns a value indicating whether the ProperNoun's text corresponds to a male first name in the English language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a male first name in the English language; otherwise, false.</returns>
        public static bool IsMaleFirst(this ProperNoun proper) {
            return NameData.IsMaleFirst(proper.Text);
        }
        /// <summary>
        /// Determines if the text is equal to that of a known Common Noun.
        /// </summary>
        /// <param name="nounText">The text to test.</param>
        /// <returns>True if the text is equal to that of a known Common Noun; otherwise, false.</returns>
        public static bool IsCommon(string nounText) {
            return ScrabbleDictionary.Contains(nounText);
        }

        #endregion

        #endregion

        private sealed class NameProvider
        {
            public void Load() {
                Task.Factory.ContinueWhenAll(new[] {
                    Task.Run(async () => lastNames = await ReadLinesAsync(lastFilePath)),
                    Task.Run(async () => femaleNames = await ReadLinesAsync(femaleFilePath)),
                    Task.Run(async () => maleNames = await ReadLinesAsync(maleFilePath))
                }, results => {
                    genderAmbiguousNames = maleNames.Intersect(femaleNames);
                    var stratified =
                        from m in maleNames.Select((name, index) => new { Rank = (double)index / maleNames.Count, Name = name })
                        join f in femaleNames.Select((name, i) => new { Rank = (double)i / femaleNames.Count, Name = name })
                        on m.Name equals f.Name
                        group f.Name by f.Rank / m.Rank > 1 ? 'F' : m.Rank / f.Rank > 1 ? 'M' : 'U';
                    var byLikelyGender = stratified.ToDictionary(strata => strata.Key);
                    maleNames = maleNames.Except(byLikelyGender['F']);
                    femaleNames = femaleNames.Except(byLikelyGender['M']);
                }
              ).Wait();
            }

            /// <summary>
            /// Determines if provided text is in the set of Female or Male first names.
            /// </summary>
            /// <param name="text">The text to check.</param>
            /// <returns>True if the provided text is in the set of Female or Male first names; otherwise, false.</returns>
            public bool IsFirstName(string text) {
                return femaleNames.Count > maleNames.Count ?
                    maleNames.Contains(text) || femaleNames.Contains(text) :
                    femaleNames.Contains(text) || maleNames.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating whether the provided string corresponds to a common last name in the English language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>True if the provided string corresponds to a common last name in the English language; otherwise, false.</returns>
            public bool IsLastName(string text) {
                return lastNames.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating whether the provided string corresponds to a common female name in the English language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>
            /// True if the provided string corresponds to a common female name in the english language; otherwise, false.
            /// </returns>
            public bool IsFemaleFirst(string text) {
                return femaleNames.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating whether the provided string corresponds to a common male name in the english language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>
            /// True if the provided string corresponds to a common male name in the English language; otherwise, false.
            /// </returns>
            public bool IsMaleFirst(string text) {
                return maleNames.Contains(text);
            }

            private static async Task<ImmutableSortedSet<string>> ReadLinesAsync(string fileName) {
                using (var reader = new StreamReader(fileName)) {
                    string data = await reader.ReadToEndAsync();
                    return data.SplitRemoveEmpty('\r', '\n')
                        .Select(s => s.Trim())
                        .ToImmutableSortedSet(IgnoreCase);
                }
            }

            /// <summary>
            /// Gets a sequence of all known Last Names.
            /// </summary>
            public IReadOnlyCollection<string> LastNames {
                get { return lastNames.ToList().ToImmutableList(); }
            }
            /// <summary>
            /// Gets a sequence of all known Female Names.
            /// </summary>
            public IReadOnlyCollection<string> FemaleNames {
                get {
                    return femaleNames.ToList().AsReadOnly();
                }
            }
            /// <summary>
            /// Gets a sequence of all known Male Names.
            /// </summary>
            public IReadOnlyCollection<string> MaleNames {
                get {
                    return maleNames.ToList().AsReadOnly();
                }
            }
            /// <summary>
            /// Gets a sequence of all known Names which are just as likely to be Female or Male.
            /// </summary>
            public IReadOnlyCollection<string> GenderAmbiguousNames {
                get {
                    return genderAmbiguousNames.ToList().AsReadOnly();
                }
            }

            public IImmutableSet<string> AllNames => new[] { lastNames, maleNames, femaleNames, genderAmbiguousNames }
                .Aggregate((fold, current) => fold.Union(current))
                .WithComparer(IgnoreCase);

            #region Fields
            #region Fields

            // Name Data Sets
            private ImmutableSortedSet<string> lastNames;
            private ImmutableSortedSet<string> maleNames;
            private ImmutableSortedSet<string> femaleNames;
            private ImmutableSortedSet<string> genderAmbiguousNames;

            // Name Data File Paths
            private static readonly string lastFilePath = resourcesDirectory + ConfigurationManager.AppSettings["NameDataDirectory"] + "last.txt";
            private static readonly string femaleFilePath = resourcesDirectory + ConfigurationManager.AppSettings["NameDataDirectory"] + "femalefirst.txt";
            private static readonly string maleFilePath = resourcesDirectory + ConfigurationManager.AppSettings["NameDataDirectory"] + "malefirst.txt";


            #endregion
            #endregion

        }
        static StringComparer IgnoreCase => StringComparer.OrdinalIgnoreCase;
    }
}