using LASI.Core.Patternization;
using LASI.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics
{

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
            return entity.Match().Yield<Gender>().
                    With<IGendered>(p => p.Gender).
                    With<IReferencer>(p => GetGender(p)).
                    With<NounPhrase>(n => GetNounPhraseGender(n)).
                    With<CommonNoun>(n => Gender.Neutral).
                    When(e => e.Referees.Any()).
                    Then<IEntity>(e => (from pro in e.Referees
                                        let gen = pro.Match().Yield<Gender>().
                                            With<IGendered>(p => p.Gender)
                                        .Result()
                                        group gen by gen into byGen
                                        orderby byGen.Count() descending
                                        select byGen.Key).FirstOrDefault()).Result();
        }
        /// <summary>
        /// Returns a NameGender value indiciating the likely gender of the Pronoun based on its referrent if known, or else its PronounKind.
        /// </summary>
        /// <param name="referee">The Pronoun whose gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely gender of the Pronoun.</returns>
        private static Gender GetGender(IReferencer referee) {
            return referee.Match().Yield<Gender>()
                    .With<PronounPhrase>(p => GetPronounPhraseGender(p))
                    .When(referee.Referent != null)
                    .Then((from referent in referee.Referent
                           let gen =
                           referent.Match().Yield<Gender>().
                                  With<NounPhrase>(n => GetNounPhraseGender(n)).
                                  With<Pronoun>(r => r.Gender).
                                  With<ProperSingularNoun>(r => r.Gender).
                                  With<CommonNoun>(n => Gender.Neutral).
                           Result()
                           group gen by gen into byGen
                           where byGen.Count() == referee.Referent.Count()
                           select byGen.Key).FirstOrDefault()).
                    With<IGendered>(p => p.Gender)
                .Result();
        }




        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Female Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Female Name, false otherwise.</returns>
        public static bool IsFemaleFull(this NounPhrase name) {
            return GetNounPhraseGender(name).IsFemale();
        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Male Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Male Name, false otherwise.</returns>
        public static bool IsMaleFull(this NounPhrase name) {
            return GetNounPhraseGender(name).IsMale();
        }

        // TODO: refactor these two methods. their interaction is very opaque and error prone. Although they are private, they make maintaining related algorithms difficult.
        #region
        private static Gender GetNounPhraseGender(NounPhrase name) {
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
                Gender.Unknown;
        }
        private static Gender GetPronounPhraseGender(PronounPhrase name) {
            if (name.Words.All(w => w is Determiner))
                return Gender.Unknown;
            var genders = name.Words.OfType<IGendered>().Select(w => w.Gender);
            return name.Words.OfProperNoun().Any(n => !(n is IGendered)) ?
                GetNounPhraseGender(name) :
                genders.Any() && genders.All(g => g.IsFemale()) ? Gender.Female :
                genders.Any() && genders.All(g => g.IsMale()) ? Gender.Male :
                genders.Any() && genders.All(g => g.IsNeutral()) ? Gender.Neutral :
                Gender.Unknown;
        }
        #endregion

        #endregion

        #region First Name Lookup Methods
        /// <summary>
        /// Determines wether the provided ProperNoun is a FirstName.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the provided ProperNoun is a FirstName, false otherwise.</returns>
        public static bool IsFirstName(this ProperNoun proper) {
            return Names.IsFirstName(proper.Text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a last name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the ProperNoun's text corresponds to a last name in the english language, false otherwise.</returns>
        public static bool IsLastName(this ProperNoun proper) {
            return Names.IsLastName(proper.Text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a female first name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a female first name in the english language, false otherwise.</returns>
        public static bool IsFemaleFirst(this ProperNoun proper) {
            return Names.IsFemaleFirst(proper.Text);
        }
        /// <summary>
        /// Returns a value indicating wether the ProperNoun's text corresponds to a male first name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a male first name in the english language, false otherwise.</returns>
        public static bool IsMaleFirst(this ProperNoun proper) {
            return Names.IsMaleFirst(proper.Text);
        }




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
        /// <returns>True if the Noun instances are synonymous, false otherwise.</returns>
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
        /// <returns>True if the Verb instances are synonymous, false otherwise.</returns>
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
        /// <param name="word">The first Adjective.</param>
        /// <param name="other">The second Adjective</param>
        /// <returns>True if the Adjective instances are synonymous, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( Lookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Adjective word, Adjective other) {
            return FindSynonyms(word).Contains(other.Text);
        }
        /// <summary>
        /// Determines if two Adverb instances are synonymous.
        /// </summary>
        /// <param name="word">The first Adverb.</param>
        /// <param name="other">The second Adverb</param>
        /// <returns>True if the Adverb instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Adverb word, Adverb other) {
            return FindSynonyms(word).Contains(other.Text);
        }



        #endregion


        #endregion


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
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 1 / 5f) { ElapsedTime = timer.ElapsedMilliseconds });

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
            get { return Lookup.nounLookup.Value; }
        }

        private static WordNetLookup<Verb> VerbLookup {
            get { return Lookup.verbLookup.Value; }
        }

        private static WordNetLookup<Adjective> AdjectiveLookup {
            get { return Lookup.adjectiveLookup.Value; }
        }

        private static WordNetLookup<Adverb> AdverbLookup {
            get { return Lookup.adverbLookup.Value; }
        }
        #region Private Fields
        // WordNet Data File Paths
        static readonly string nounWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        static readonly string verbWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        static readonly string adverbWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adv";
        static readonly string adjectiveWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adj";
        static readonly string scrabbleDictsFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "dictionary.txt"; //scrabble dictionary
        // Internal Lookups
        static Lazy<WordNetLookup<Noun>> nounLookup = new Lazy<WordNetLookup<Noun>>(() => LazyLoad(new NounLookup(nounWNFilePath)), true);
        static Lazy<WordNetLookup<Verb>> verbLookup = new Lazy<WordNetLookup<Verb>>(() => LazyLoad(new VerbLookup(verbWNFilePath)), true);
        static Lazy<WordNetLookup<Adjective>> adjectiveLookup = new Lazy<WordNetLookup<Adjective>>(() => LazyLoad(new AdjectiveLookup(adjectiveWNFilePath)), true);
        static Lazy<WordNetLookup<Adverb>> adverbLookup = new Lazy<WordNetLookup<Adverb>>(() => LazyLoad(new AdverbLookup(adverbWNFilePath)), true);


        // Synonym LexicalLookup Caches
        static ConcurrentDictionary<string, ISet<string>> cachedNounData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedVerbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedAdjectiveData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedAdverbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);


        static Lazy<NameProvider> names = new Lazy<NameProvider>(() => {
            var resourceName = "Name Data";
            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var val = new NameProvider();
            val.Load();
            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedTime = timer.ElapsedMilliseconds });
            return val;
        }, true);

        private static NameProvider Names {
            get { return Lookup.names.Value; }
        }

        private static Lazy<ISet<string>> scrabbleDictionary = new Lazy<ISet<string>>(() => {
            var resourceName = "Scrabble Dictionary";

            ResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0));
            var timer = System.Diagnostics.Stopwatch.StartNew();
            ISet<string> dict;
            using (var reader = new StreamReader(scrabbleDictsFilePath)) {
                dict = reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                      .Select(s => s.ToLower())
                      .Except(Names.AllNames, StringComparer.OrdinalIgnoreCase)
                      .ToHashSet(StringComparer.OrdinalIgnoreCase);
            }


            ResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0) { ElapsedTime = timer.ElapsedMilliseconds });

            timer.Stop();
            return dict;
        }, true);


        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ration of a similarity comparison is >= the threshold, 
        /// then the similarity comparison will return true.
        /// </summary>
        public const double SIMILARITY_THRESHOLD = 0.6;

        #endregion


        private sealed class NameProvider
        {
            public void Load() {
                Task.Factory.ContinueWhenAll(
                  new[] {  
                    Task.Run(async () => last = await GetLinesAsync(lastFilePath)),
                    Task.Run(async () => female = await GetLinesAsync(femaleFilePath)),
                    Task.Run(async () => male = await GetLinesAsync(maleFilePath)) 
                },
                  results => {
                      genderAmbiguous =
                          new HashSet<string>(male.Intersect(female, comparer).Union(female.Intersect(male, comparer)), comparer);

                      var stratified =
                          from m in male.Select((s, i) => new { Rank = (double)i / male.Count, Name = s })
                          join f in female.Select((s, i) => new { Rank = (double)i / female.Count, Name = s })
                          on m.Name equals f.Name
                          group f.Name by f.Rank / m.Rank > 1 ? 'M' : m.Rank / f.Rank > 1 ? 'F' : 'U';

                      male.ExceptWith(from s in stratified where s.Key == 'F' from n in s select n);
                      female.ExceptWith(from s in stratified where s.Key == 'M' from n in s select n);
                  }
              ).Wait();
            }

            /// <summary>
            /// Determines if provided text is in the set of Female or Male first names.
            /// </summary>
            /// <param name="text">The text to check.</param>
            /// <returns>True if the provided text is in the set of Female or Male first names, false otherwise.</returns>
            public bool IsFirstName(string text) {
                return female.Count > male.Count ?
                    male.Contains(text) || female.Contains(text) :
                    female.Contains(text) || male.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating wether the provided string corresponds to a common lastname in the english language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>True if the provided string corresponds to a common lastname in the english language, false otherwise.</returns>
            public bool IsLastName(string text) {
                return last.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating wether the provided string corresponds to a common female name in the english language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>True if the provided string corresponds to a common female name in the english language, false otherwise.</returns>
            public bool IsFemaleFirst(string text) {
                return female.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating wether the provided string corresponds to a common male name in the english language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>True if the provided string corresponds to a common male name in the english language, false otherwise.</returns>
            public bool IsMaleFirst(string text) {
                return male.Contains(text);
            }


            private static async Task<ISet<string>> GetLinesAsync(string fileName) {
                using (var reader = new StreamReader(fileName)) {
                    string data = await reader.ReadToEndAsync();
                    return data.SplitRemoveEmpty('\r', '\n').Select(s => s.Trim()).ToHashSet(comparer);
                }
            }



            /// <summary>
            /// Gets a sequence of all known Last Names.
            /// </summary>
            public IReadOnlyCollection<string> LastNames {
                get {
                    return last.ToList().AsReadOnly();
                }
            }
            /// <summary>
            /// Gets a sequence of all known Female Names.
            /// </summary>
            public IReadOnlyCollection<string> FemaleNames {
                get {
                    return female.ToList().AsReadOnly();
                }
            }
            /// <summary>
            /// Gets a sequence of all known Male Names.
            /// </summary>
            public IReadOnlyCollection<string> MaleNames {
                get {
                    return male.ToList().AsReadOnly();
                }
            }
            /// <summary>
            /// Gets a sequence of all known Names which are just as likely to be Female or Male.
            /// </summary>
            public IReadOnlyCollection<string> GenderAmbiguousNames {
                get {
                    return genderAmbiguous.ToList().AsReadOnly();
                }
            }
            public IReadOnlyCollection<string> AllNames {
                get { return last.Union(male, comparer).Union(female, comparer).Union(genderAmbiguous, comparer).ToList().AsReadOnly(); }
            }

            #region Fields

            // Name Data File Paths
            private static readonly string lastFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "last.txt";
            private static readonly string femaleFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "femalefirst.txt";
            private static readonly string maleFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "malefirst.txt";
            // Name Data Sets
            private static ISet<string> last;
            private static ISet<string> male;
            private static ISet<string> female;
            private static ISet<string> genderAmbiguous;

            private static StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            #endregion

        }
        #endregion
    }






}