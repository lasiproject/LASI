using LASI.Algorithm.ComparativeHeuristics.Morphemization;
using LASI.Algorithm.Patternization;
using LASI.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.ComparativeHeuristics
{
    /// <summary>
    /// Provides Comprehensive static facilities for Synoynm Identification, Word and Phrase Comparison, Gender Stratification, and Named Entity Recognition.
    /// </summary>
    public static class Lookup
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
                    .Case<IGendered>(p => p.Gender)
                    .Case<IReferencer>(p => p.GetPronounGender())
                    .Case<NounPhrase>(n => n.GetGender())
                    .Case<CommonNoun>(n => Gender.Neutral)
                    .When(e => e.BoundPronouns.Any())
                    .Then<IEntity>(e => (from pro in e.BoundPronouns
                                         let gen = pro.Match().Yield<Gender>().Case<IGendered>(p => p.Gender).Result()
                                         group gen by gen into byGen
                                         orderby byGen.Count() descending
                                         select byGen.Key).FirstOrDefault())
                    .Result();
        }
        /// <summary>
        /// Returns a NameGender value indiciating the likely gender of the Pronoun based on its referrent if known, or else its PronounKind.
        /// </summary>
        /// <param name="pronoun">The Pronoun whose gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely gender of the Pronoun.</returns>
        private static Gender GetPronounGender(this IReferencer pronoun) {
            return pronoun.Match().Yield<Gender>()
                .When<IReferencer>(p => p.Referent != null)
                .Then<IReferencer>(p => {
                    return (from referent in p.Referent
                            let gen =
                               referent.Match().Yield<Gender>()
                                   .Case<NounPhrase>(r => r.GetGender())
                                   .Case<Pronoun>(r => r.GetPronounGender())
                                   .Case<ProperSingularNoun>(r => r.Gender)
                                   .Case<CommonNoun>(n => Gender.Neutral)
                               .Result()
                            group gen by gen into byGen
                            where byGen.Count() == pronoun.Referent.Count()
                            select byGen.Key).FirstOrDefault();
                })
                    .Case<IGendered>(p => p.Gender)
                    .Case<PronounPhrase>(p => GetPhraseGender(p))
                .Result();
        }

        /// <summary>
        /// Returns a NameGender value indiciating the likely prevailing gender within the NounPhrase.
        /// </summary>
        /// <param name="name">The NounPhrase whose prevailing gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely prevailing gender of the NounPhrase.</returns>
        static Gender GetGender(this NounPhrase name) {
            return GetNounPhraseGender(name);
        }

        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Name, false otherwise.</returns>
        public static bool IsFullName(this NounPhrase name) {
            return GetNounPhraseGender(name).IsMaleOrFemale() && name.Words.OfProperNoun().Any(n => n.IsLastName());

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


        private static Gender GetNounPhraseGender(NounPhrase name) {
            var propers = name.Words.OfProperNoun();
            var first = propers.Singulars().FirstOrDefault(n => n.Gender.IsMaleOrFemale());
            var last = propers.LastOrDefault(n => n != first && n.IsLastName());
            return first != null && (last != null || propers.All(n => n.GetGender() == first.Gender)) ?
                first.Gender : name.Words.OfNoun().All(n => n.GetGender().IsNeutral()) ? Gender.Neutral : Gender.Undetermined;
        }
        private static Gender GetPhraseGender(PronounPhrase name) {
            if (name.Words.All(w => w is Determiner))
                return Gender.Neutral;
            var genderedWords = name.Words.OfType<IGendered>().Select(w => w.Gender);
            return name.Words.OfProperNoun().Any(n => !(n is IGendered)) ?
                GetNounPhraseGender(name)
                :
                genderedWords.All(p => p.IsFemale()) ? Gender.Female :
                genderedWords.All(p => p.IsMale()) ? Gender.Male :
                genderedWords.All(p => p.IsNeutral()) ? Gender.Neutral :
                Gender.Undetermined;
        }

        #endregion

        #region First Name Lookup Methods
        /// <summary>
        /// Determines wether the provided ProperNoun is a FirstName.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the provided ProperNoun is a FirstName, false otherwise.</returns>
        public static bool IsFirstName(this ProperNoun proper) {
            return IsFirstName(proper.Text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a last name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the ProperNoun's text corresponds to a last name in the english language, false otherwise.</returns>
        public static bool IsLastName(this ProperNoun proper) {
            return IsLastName(proper.Text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a female first name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a female first name in the english language, false otherwise.</returns>
        public static bool IsFemaleFirst(this ProperNoun proper) {
            return IsFemaleFirst(proper.Text);
        }
        /// <summary>
        /// Returns a value indicating wether the ProperNoun's text corresponds to a male first name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a male first name in the english language, false otherwise.</returns>
        public static bool IsMaleFirst(this ProperNoun proper) {
            return IsMaleFirst(proper.Text);
        }
        /// <summary>
        /// Determines if provided text is in the set of Female or Male first names.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True if the provided text is in the set of Female or Male first names, false otherwise.</returns>
        private static bool IsFirstName(string text) {
            return femaleNames.Count > maleNames.Count ?
                maleNames.Contains(text) || femaleNames.Contains(text) :
                femaleNames.Contains(text) || maleNames.Contains(text);
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common lastname in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common lastname in the english language, false otherwise.</returns>
        private static bool IsLastName(string text) {
            return lastNames.Contains(text);
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common female name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common female name in the english language, false otherwise.</returns>
        private static bool IsFemaleFirst(string text) {
            return femaleNames.Contains(text);
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common male name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common male name in the english language, false otherwise.</returns>
        private static bool IsMaleFirst(string text) {
            return maleNames.Contains(text);
        }

        #endregion

        #region Lookup Loading Methods
        /// <summary>
        /// Returns a sequence of Tasks containing all of the yet unstarted Lookup loading operations.
        /// Await each Task to start its corresponding loading operation.
        /// </summary>
        /// <returns>a sequence of Tasks containing all of the yet unstarted Lookup loading operations.</returns>
        public static IEnumerable<Task<string>> GetLoadingTasks() {
            return new[] {
                NounThesaurusLoadTask,
                VerbThesaurusLoadTask, 
                AdjectiveThesaurusLoadTask, 
                AdverbThesaurusLoadTask, 
                NameDataLoadTask,
                ScrabbleDictionaryLoadTask
                }
            .Where(t => t != null);
        }
        /// <summary>
        /// Automatically loads all resources used by the Lookup class.
        /// </summary>
        public static void LoadAllData() {
            Task.WaitAll(GetLoadingTasks().ToArray());
        }
        /// <summary>
        /// Returns a single Task which, when awaited, will load all resources used by the Lookup class.
        /// </summary>
        /// <returns>A single Task which, when awaited, will load all resources used by the Lookup class.</returns>
        public static async Task LoadAllDataAsync() { await Task.WhenAll(GetLoadingTasks().ToArray()); }

        #endregion
        #region Synonym Lookup Methods

        /// <summary>
        /// Returns the synonyms for the provided Noun.
        /// </summary>
        /// <param name="noun">The Noun to lookup.</param>
        /// <returns>The synonyms for the provided Noun.</returns>
        public static IEnumerable<string> GetSynonyms(this Noun noun) {
            return InternalLookup(noun);
        }
        /// <summary>
        /// Returns the synonyms for the provided Verb.
        /// </summary>
        /// <param name="verb">The Verb to lookup.</param>
        /// <returns>The synonyms for the provided Verb.</returns>
        public static IEnumerable<string> GetSynonyms(this Verb verb) {
            return InternalLookup(verb);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adjective.
        /// </summary>
        /// <param name="adjective">The Adjective to lookup.</param>
        /// <returns>The synonyms for the provided Adjective.</returns>
        public static IEnumerable<string> GetSynonyms(this Adjective adjective) {
            return InternalLookup(adjective);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adverb.
        /// </summary>
        /// <param name="adverb">The Adverb to lookup.</param>
        /// <returns>The synonyms for the provided Adverb.</returns>
        public static IEnumerable<string> GetSynonyms(this Adverb adverb) {
            return InternalLookup(adverb);
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
            return InternalLookup(first).Contains(second.Text);
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
            return InternalLookup(first).Contains(second.Text);
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
            return InternalLookup(word).Contains(other.Text);
        }

        #region Adjective Specific Lookups
        static bool IsSynonymFor(this Adjective word, SuperlativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdjective word, Adjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdjective word, SuperlativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdjective word, ComparativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this Adjective word, ComparativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdjective word, Adjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdjective word, SuperlativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdjective word, ComparativeAdjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        #endregion

        /// <summary>
        /// Determines if two Adverb instances are synonymous.
        /// </summary>
        /// <param name="word">The first Adverb.</param>
        /// <param name="other">The second Adverb</param>
        /// <returns>True if the Adverb instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Adverb word, Adverb other) {
            return InternalLookup(word).Contains(other.Text);
        }


        #region Adverb Specific Lookups
        static bool IsSynonymFor(this Adverb word, SuperlativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdverb word, Adverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdverb word, SuperlativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdverb word, ComparativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this Adverb word, ComparativeAdverb other) {
            var otherRoot = AdverbMorpher.FindRoot(other);
            return InternalLookup(word).Contains(otherRoot);
        }
        static bool IsSynonymFor(this ComparativeAdverb word, Adverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this ComparativeAdverb word, SuperlativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        static bool IsSynonymFor(this SuperlativeAdverb word, ComparativeAdverb other) {
            return InternalLookup(word).Contains(other.Text);
        }
        #endregion


        #endregion


        #region Internal Syonym Lookup Methods

        private static ISet<string> InternalLookup(Noun noun) {
            return cachedNounData.GetOrAdd(noun.Text, key => nounLookup[key]);
        }
        private static ISet<string> InternalLookup(Verb verb) {
            return cachedVerbData.GetOrAdd(verb.Text, key => verbLookup[key]);
        }
        private static ISet<string> InternalLookup(Adverb adverb) {
            return cachedAdverbData.GetOrAdd(adverb.Text, key => adverbLookup[key]);
        }
        private static ISet<string> InternalLookup(Adjective adjective) {
            return cachedAdjectiveData.GetOrAdd(adjective.Text, key => adjectiveLookup[key]);
        }

        #endregion
        #endregion

        #region Private Methods



        private static async Task LoadNameDataAsync() {
            await Task.Factory.ContinueWhenAll(
                new[] {  
                    Task.Run(async () => lastNames = await GetLinesAsync(lastNamesFilePath)),
                    Task.Run(async () => femaleNames = await GetLinesAsync(femaleNamesFilePath)),
                    Task.Run(async () => maleNames = await GetLinesAsync(maleNamesFilePath)) 
                },
                results => {
                    genderAmbiguousNames =
                        new HashSet<string>(maleNames.Intersect(femaleNames).Concat(femaleNames.Intersect(maleNames)), StringComparer.OrdinalIgnoreCase);

                    var stratified =
                        from m in maleNames.Select((s, i) => new { Rank = (double)i / maleNames.Count, Name = s })
                        join f in femaleNames.Select((s, i) => new { Rank = (double)i / femaleNames.Count, Name = s })
                        on m.Name equals f.Name
                        group f.Name by f.Rank / m.Rank > 1 ? 'M' : m.Rank / f.Rank > 1 ? 'F' : 'U';

                    maleNames.ExceptWith(from s in stratified where s.Key == 'F' from n in s select n);
                    femaleNames.ExceptWith(from s in stratified where s.Key == 'M' from n in s select n);
                }
            );
        }

        private static async Task<ISet<string>> GetLinesAsync(string fileName) {
            using (var reader = new StreamReader(fileName)) {
                string data = await reader.ReadToEndAsync();
                return data.SplitRemoveEmpty('\r', '\n').Select(s => s.Trim()).ToSet(StringComparer.OrdinalIgnoreCase);
            }
        }

        #endregion

        #region Public Properties



        /// <summary>
        /// Gets the sequence of strings corresponding to all nouns in the Scrabble Dictionary data source.
        /// </summary>
        public static IEnumerable<string> ScrabbleDictionary { get { return scrabbleDictionary; } }

        #endregion

        #region Name Collection Accessors

        /// <summary>
        /// Gets a sequence of all known Last Names.
        /// </summary>
        public static IReadOnlyCollection<string> LastNames {
            get {
                return lastNames.ToList().AsReadOnly();
            }
        }
        /// <summary>
        /// Gets a sequence of all known Female Names.
        /// </summary>
        public static IReadOnlyCollection<string> FemaleNames {
            get {
                return femaleNames.ToList().AsReadOnly();
            }
        }
        /// <summary>
        /// Gets a sequence of all known Male Names.
        /// </summary>
        public static IReadOnlyCollection<string> MaleNames {
            get {
                return maleNames.ToList().AsReadOnly();
            }
        }
        /// <summary>
        /// Gets a sequence of all known Names which are just as likely to be Female or Male.
        /// </summary>
        public static IReadOnlyCollection<string> GenderAmbiguousNames {
            get {
                return genderAmbiguousNames.ToList().AsReadOnly();
            }
        }

        #endregion


        #region Private Properties

        /// <summary>
        /// Exposes properties which construct the Tasks which correspond to the various loading operations of the Lookup class.
        /// </summary>
        #region Loading Task Builders

        internal static Task<string> ScrabbleDictionaryLoadTask = Task.Run(() => {
            scrabbleDictionary = new List<string>();
            using (var reader = new StreamReader(scrabbleDictsFilePath)) {
                scrabbleDictionary = reader.ReadToEnd().SplitRemoveEmpty('\r', '\n').Select(s => s.ToLower()).ToList();
            }
            return "Finished Loading Scrabble Dictionary";
        });
        internal static Task<string> AdjectiveThesaurusLoadTask {
            get {
                var result = adjectiveLoadingState == LoadingState.NotStarted ?
                    Task.Run(async () => {
                        await adjectiveLookup.LoadAsync();
                        adjectiveLoadingState = LoadingState.Finished;
                        return "Adjective Thesaurus Loaded";
                    }) :
                    null;
                adjectiveLoadingState = LoadingState.InProgress;
                return result;
            }
        }
        internal static Task<string> AdverbThesaurusLoadTask {
            get {
                var result = adverbLoadingState == LoadingState.NotStarted ?
                    Task.Run(async () => {
                        await adverbLookup.LoadAsync();
                        adverbLoadingState = LoadingState.Finished;
                        return "Adverb Thesaurus Loaded";
                    }) : null;
                adverbLoadingState = LoadingState.InProgress;
                return result;
            }
        }
        internal static Task<string> VerbThesaurusLoadTask {
            get {
                var result = verbLoadingState == LoadingState.NotStarted ?
                    Task.Run(async () => {
                        await verbLookup.LoadAsync();
                        verbLoadingState = LoadingState.Finished;
                        return "Verb Thesaurus Loaded";
                    }) : null;
                verbLoadingState = LoadingState.InProgress;
                return result;
            }
        }
        internal static Task<string> NounThesaurusLoadTask {
            get {
                var result = nounLoadingState == LoadingState.NotStarted ?
                    Task.Run(async () => {
                        await nounLookup.LoadAsync();
                        nounLoadingState = LoadingState.Finished;
                        return "Noun Thesaurus Loaded";
                    }) : null;
                nounLoadingState = LoadingState.InProgress;
                return result;
            }
        }
        internal static Task<string> NameDataLoadTask {
            get {
                var result = nameDataLoadingState == LoadingState.NotStarted ?
                    Task.Run(async () => {
                        await LoadNameDataAsync();
                        nameDataLoadingState = LoadingState.Finished;
                        return "Loaded Name Data";
                    }) : null;
                nameDataLoadingState = LoadingState.InProgress;
                return result;
            }

        }
        #endregion


        #endregion

        #region Private Fields
        // WordNet Data File Paths
        static readonly string nounWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        static readonly string verbWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        static readonly string adverbWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adv";
        static readonly string adjectiveWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adj";
        static readonly string scrabbleDictsFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "dictionary.txt"; //scrabble dictionary
        // Internal Thesauri
        static NounLookup nounLookup = new NounLookup(nounWNFilePath);
        static VerbLookup verbLookup = new VerbLookup(verbWNFilePath);
        static AdjectiveLookup adjectiveLookup = new AdjectiveLookup(adjectiveWNFilePath);
        static AdverbLookup adverbLookup = new AdverbLookup(adverbWNFilePath);
        // Synonym LexicalLookup Caches
        static ConcurrentDictionary<string, ISet<string>> cachedNounData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedVerbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedAdjectiveData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        static ConcurrentDictionary<string, ISet<string>> cachedAdverbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.Max, 40960);
        // Name Data File Paths
        static readonly string lastNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "last.txt";
        static readonly string femaleNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "femalefirst.txt";
        static readonly string maleNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "malefirst.txt";
        // Name Data Sets
        static ISet<string> lastNames;
        static ISet<string> maleNames;
        static ISet<string> femaleNames;
        static ISet<string> genderAmbiguousNames;
        static List<string> scrabbleDictionary;
        //Loading states for specific data items
        static LoadingState nounLoadingState = LoadingState.NotStarted;
        static LoadingState verbLoadingState = LoadingState.NotStarted;
        static LoadingState adjectiveLoadingState = LoadingState.NotStarted;
        static LoadingState adverbLoadingState = LoadingState.NotStarted;
        static LoadingState nameDataLoadingState = LoadingState.NotStarted;

        /// <summary>
        /// Similarity threshold for lexical element comparisons. If the computed ration of a similarity comparison is >= the threshold, 
        /// then the similarity comparison will return true.
        /// </summary>
        public const double SIMILARITY_THRESHOLD = 0.6;

        #endregion

        #region Utility Types




        /// <summary>
        /// Represents the various states of a loading operation.
        /// </summary>
        private enum LoadingState
        {
            NotStarted,
            InProgress,
            Finished
        }
        #endregion
    }
}