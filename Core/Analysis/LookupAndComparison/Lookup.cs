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


    /// <summary>
    /// Provides Comprehensive static facilities for Synoynm Identification, Word and Phrase Comparison, Gender Stratification, and Named Entity Recognition.
    /// </summary>
    public static partial class Lookup
    {
        #region Public Methods

        #region Name Gender Lookup Methods

        /// <summary>
        /// Returns a NameGender value indiciating the likely gender of the entity.
        /// </summary>
        /// <param name="entity">The entity whose gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely gender of the entity.</returns>
        public static Gender GetGender(this IEntity entity) {
            return entity.Match().Yield<Gender>()
                    .With<ISimpleGendered>(p => p.Gender)
                    .With<IReferencer>(p => GetGender(p))
                    .With<NounPhrase>(n => DetermineNounPhraseGender(n))
                    .With<CommonNoun>(n => Gender.Neutral)
                    .When(e => e.Referencers.Any())
                    .Then<IEntity>(e => (
                        from referener in e.Referencers
                        let gendered = referener as ISimpleGendered
                        let gender = gendered != null ? gendered.Gender : default(Gender)
                        group gender by gender into byGender
                        orderby byGender.Count() descending
                        select byGender.Key).FirstOrDefault())
                    .Result();
        }
        /// <summary>
        /// Returns a NameGender value indiciating the likely gender of the Pronoun based on its referrent if known, or else its PronounKind.
        /// </summary>
        /// <param name="referee">The Pronoun whose gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely gender of the Pronoun.</returns>
        private static Gender GetGender(IReferencer referee) {
            return referee.Match().Yield<Gender>()
                    .With<PronounPhrase>(p => DeterminePronounPhraseGender(p))
                    .When(referee.RefersTo != null)
                    .Then((from referent in referee.RefersTo
                           let gender =
                           referent.Match().Yield<Gender>()
                              .With((NounPhrase n) => DetermineNounPhraseGender(n))
                              .With((Pronoun r) => r.Gender)
                              .With((ProperSingularNoun r) => r.Gender)
                              .With((CommonNoun n) => Gender.Neutral)
                              .Result()
                           group gender by gender into byGender
                           where byGender.Count() == referee.RefersTo.Count()
                           select byGender.Key).FirstOrDefault()).
                    With<ISimpleGendered>(p => p.Gender)
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
            var propers = name.Words
                .OfProperNoun();
            var first = propers
                .Singulars()
                .FirstOrDefault(n => n.Gender.IsMaleOrFemale());
            var last = propers
                .LastOrDefault(n => n != first && n.IsLastName());
            return first != null && (last != null || propers.All(n => n.GetGender() == first.Gender)) ?
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
        public static IEnumerable<string> GetSynonyms(this Noun noun) {
            return FindSynonyms(noun);
        }
        /// <summary>
        /// Returns the synonyms for the provided Verb.
        /// </summary>
        /// <param name="verb">The Verb to lookup.</param>
        /// <returns>The synonyms for the provided Verb.</returns>
        public static IEnumerable<string> GetSynonyms(this Verb verb) {
            return FindSynonyms(verb);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adjective.
        /// </summary>
        /// <param name="adjective">The Adjective to lookup.</param>
        /// <returns>The synonyms for the provided Adjective.</returns>
        public static IEnumerable<string> GetSynonyms(this Adjective adjective) {
            return FindSynonyms(adjective);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adverb.
        /// </summary>
        /// <param name="adverb">The Adverb to lookup.</param>
        /// <returns>The synonyms for the provided Adverb.</returns>
        public static IEnumerable<string> GetSynonyms(this Adverb adverb) {
            return FindSynonyms(adverb);
        }


        /// <summary>
        /// Determines if two Noun instances are synonymous.
        /// </summary>
        /// <param name="first">The first Noun.</param>
        /// <param name="second">The second Noun</param>
        /// <returns>True if the Noun instances are synonymous; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Noun first, Noun second) {
            return FindSynonyms(first).Contains(second.Text);
        }
        /// <summary>
        /// Determines if two Verb instances are synonymous.
        /// </summary>
        /// <param name="first">The first Verb.</param>
        /// <param name="second">The second Verb</param>
        /// <returns>True if the Verb instances are synonymous; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Verb first, Verb second) {
            if (first == null || second == null)
                return false;
            return FindSynonyms(first).Contains(second.Text);
        }
        /// <summary>
        /// Determines if two Adjective instances are synonymous.
        /// </summary>
        /// <param name="first">The first Adjective.</param>
        /// <param name="second">The second Adjective</param>
        /// <returns>True if the Adjective instances are synonymous; otherwise, false.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Adjective first, Adjective second) {
            return FindSynonyms(first).Contains(second.Text);
        }
        /// <summary>
        /// Determines if two Adverb instances are synonymous.
        /// </summary>
        /// <param name="first">The first Adverb.</param>
        /// <param name="second">The second Adverb</param>
        /// <returns>True if the Adverb instances are synonymous; otherwise, false.</returns>
        public static bool IsSynonymFor(this Adverb first, Adverb second) {
            return FindSynonyms(first).Contains(second.Text);
        }



        #endregion


        #endregion

        /// <summary>
        /// Clears the cache of Noun synonym data.
        /// </summary>
        public static void ClearNounCache() {
            cachedNounData.Clear();
        }
        /// <summary>
        /// Clears the cache of Verb synonym data.
        /// </summary>
        public static void ClearVerbCache() {
            cachedVerbData.Clear();
        }
        /// <summary>
        /// Clears the cache of Adjective synonym data.
        /// </summary>
        public static void ClearAdjectiveCache() {
            cachedAdjectiveData.Clear();
        }
        /// <summary>
        /// Clears the cache of Adverb synonym data.
        /// </summary>
        public static void ClearAdverbCache() {
            cachedAdverbData.Clear();
        }


        #region Internal Syonym Lookup Methods

        private static ISet<string> FindSynonyms(Noun noun) {
            return cachedNounData.GetOrAdd(noun.Text, key => NounLookup[key]);
        }
        private static ISet<string> FindSynonyms(Verb verb) {
            return cachedVerbData.GetOrAdd(verb.Text, key => VerbLookup[key]);
        }
        private static ISet<string> FindSynonyms(Adverb adverb) {
            return cachedAdverbData.GetOrAdd(adverb.Text, key => AdverbLookup[key]);
        }
        private static ISet<string> FindSynonyms(Adjective adjective) {
            return cachedAdjectiveData.GetOrAdd(adjective.Text, key => AdjectiveLookup[key]);
        }

        #endregion

        private static WordNetLookup<TWord> LazyLoad<TWord>(WordNetLookup<TWord> lookup) where TWord : Word {
            var startedHandler = ResourceLoading;
            var resourceName = typeof(TWord).Name + " Thesaurus";

            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            lookup.Load();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 1 / 5f) { ElapsedMiliseconds = timer.ElapsedMilliseconds });

            return lookup;
        }

        #region Properties



        /// <summary>
        /// Gets the sequence of strings corresponding to all nouns in the Scrabble Dictionary data source.
        /// </summary>
        public static IEnumerable<string> ScrabbleDictionary { get { return scrabbleDictionary.Value; } }


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


        private static WordNetLookup<Noun> NounLookup {
            get { return nounLookup.Value; }
        }

        private static WordNetLookup<Verb> VerbLookup {
            get { return verbLookup.Value; }
        }

        private static WordNetLookup<Adjective> AdjectiveLookup {
            get { return adjectiveLookup.Value; }
        }

        private static WordNetLookup<Adverb> AdverbLookup {
            get { return adverbLookup.Value; }
        }
        #region Private Fields
        // Resource Data File Paths
        static readonly string resourcesDirectory = ConfigurationManager.AppSettings["ResourcesDirectory"];
        static readonly string wordnetDataDirectory = resourcesDirectory + ConfigurationManager.AppSettings["WordnetFileDirectory"];
        static readonly string nounWNFilePath = wordnetDataDirectory + "data.noun";
        static readonly string verbWNFilePath = wordnetDataDirectory + "data.verb";
        static readonly string adverbWNFilePath = wordnetDataDirectory + "data.adv";
        static readonly string adjectiveWNFilePath = wordnetDataDirectory + "data.adj";
        static readonly string scrabbleDictsFilePath = wordnetDataDirectory + "dictionary.txt";
        //scrabble dictionary
        // Internal Lookups
        static Lazy<WordNetLookup<Noun>> nounLookup = new Lazy<WordNetLookup<Noun>>(() => LazyLoad(new NounLookup(nounWNFilePath)), true);
        static Lazy<WordNetLookup<Verb>> verbLookup = new Lazy<WordNetLookup<Verb>>(() => LazyLoad(new VerbLookup(verbWNFilePath)), true);
        static Lazy<WordNetLookup<Adjective>> adjectiveLookup = new Lazy<WordNetLookup<Adjective>>(() => LazyLoad(new AdjectiveLookup(adjectiveWNFilePath)), true);
        static Lazy<WordNetLookup<Adverb>> adverbLookup = new Lazy<WordNetLookup<Adverb>>(() => LazyLoad(new AdverbLookup(adverbWNFilePath)), true);



        // Synonym LexicalLookup Caches
        static ConcurrentDictionary<string, ISet<string>> cachedNounData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max * 2, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedVerbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max * 2, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedAdjectiveData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max * 2, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedAdverbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max * 2, 40960);


        static Lazy<NameProvider> names = new Lazy<NameProvider>(() => {
            var resourceName = "Name Data";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var val = new NameProvider();
            val.Load();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedMiliseconds = timer.ElapsedMilliseconds });
            return val;
        }, true);

        private static NameProvider NameData {
            get { return names.Value; }
        }

        private static Lazy<ISet<string>> scrabbleDictionary = new Lazy<ISet<string>>(() => {
            var resourceName = "Scrabble Dictionary";

            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            ISet<string> dict;
            using (var reader = new StreamReader(scrabbleDictsFilePath)) {
                dict = reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                      .Select(s => s.ToLower())
                      .Except(NameData.AllNames, StringComparer.OrdinalIgnoreCase)
                      .ToHashSet(StringComparer.OrdinalIgnoreCase);
            }


            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedMiliseconds = timer.ElapsedMilliseconds });

            timer.Stop();
            return dict;
        }, true);


        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ration of a similarity comparison is >= the threshold, 
        /// then the similarity comparison will return true.
        /// </summary>
        const double SIMILARITY_THRESHOLD = 0.6;

        #region Name Lookup Methods
        /// <summary>
        /// Determines wether the provided ProperNoun is a FirstName.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the provided ProperNoun is a FirstName; otherwise, false.</returns>
        public static bool IsFirstName(this ProperNoun proper) {
            return NameData.IsFirstName(proper.Text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a last name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the ProperNoun's text corresponds to a last name in the english language; otherwise, false.</returns>
        public static bool IsLastName(this ProperNoun proper) {
            return NameData.IsLastName(proper.Text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a female first name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a female first name in the english language; otherwise, false.</returns>
        public static bool IsFemaleFirst(this ProperNoun proper) {
            return NameData.IsFemaleFirst(proper.Text);
        }
        /// <summary>
        /// Returns a value indicating wether the ProperNoun's text corresponds to a male first name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a male first name in the english language; otherwise, false.</returns>
        public static bool IsMaleFirst(this ProperNoun proper) {
            return NameData.IsMaleFirst(proper.Text);
        }

        #endregion
        #endregion
        private sealed class NameProvider
        {

            public void Load() {
                Task.Factory.ContinueWhenAll(
                  new[] {
                    Task.Run(async () => lastNames = await GetLinesAsync(lastFilePath)),
                    Task.Run(async () => femaleNames = await GetLinesAsync(femaleFilePath)),
                    Task.Run(async () => maleNames = await GetLinesAsync(maleFilePath))
                },
                  results => {
                      genderAmbiguousNames =
                          new HashSet<string>(maleNames.Intersect(femaleNames, caseless).Union(femaleNames.Intersect(maleNames, caseless)), caseless);

                      var stratified =
                          from m in maleNames.Select((s, i) => new { Rank = (double)i / maleNames.Count, Name = s })
                          join f in femaleNames.Select((s, i) => new { Rank = (double)i / femaleNames.Count, Name = s })
                          on m.Name equals f.Name
                          group f.Name by f.Rank / m.Rank > 1 ? 'M' : m.Rank / f.Rank > 1 ? 'F' : 'U';

                      maleNames.ExceptWith(from s in stratified where s.Key == 'F' from n in s select n);
                      femaleNames.ExceptWith(from s in stratified where s.Key == 'M' from n in s select n);
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
            /// Returns a value indicating wether the provided string corresponds to a common lastname in the english language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>True if the provided string corresponds to a common lastname in the english language; otherwise, false.</returns>
            public bool IsLastName(string text) {
                return lastNames.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating wether the provided string corresponds to a common female name in the english language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>True if the provided string corresponds to a common female name in the english language; otherwise, false.</returns>
            public bool IsFemaleFirst(string text) {
                return femaleNames.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating wether the provided string corresponds to a common male name in the english language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>True if the provided string corresponds to a common male name in the english language; otherwise, false.</returns>
            public bool IsMaleFirst(string text) {
                return maleNames.Contains(text);
            }


            private static async Task<ISet<string>> GetLinesAsync(string fileName) {
                using (var reader = new StreamReader(fileName)) {
                    string data = await reader.ReadToEndAsync();
                    return data.SplitRemoveEmpty('\r', '\n').Select(s => s.Trim()).ToHashSet(caseless);
                }
            }



            /// <summary>
            /// Gets a sequence of all known Last Names.
            /// </summary>
            public IReadOnlyCollection<string> LastNames {
                get {
                    return lastNames.ToList().AsReadOnly();
                }
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
            public ISet<string> AllNames {
                get { return lastNames.Concat(maleNames).Concat(femaleNames).Concat(genderAmbiguousNames).ToImmutableHashSet(caseless); }
            }

            #region Fields

            // Name Data File Paths
            private static readonly string lastFilePath = resourcesDirectory + ConfigurationManager.AppSettings["NameDataDirectory"] + "last.txt";
            private static readonly string femaleFilePath = resourcesDirectory + ConfigurationManager.AppSettings["NameDataDirectory"] + "femalefirst.txt";
            private static readonly string maleFilePath = resourcesDirectory + ConfigurationManager.AppSettings["NameDataDirectory"] + "malefirst.txt";
            // Name Data Sets
            private static ISet<string> lastNames;
            private static ISet<string> maleNames;
            private static ISet<string> femaleNames;
            private static ISet<string> genderAmbiguousNames;

            private static StringComparer caseless = StringComparer.OrdinalIgnoreCase;
            #endregion


        }
    }
}