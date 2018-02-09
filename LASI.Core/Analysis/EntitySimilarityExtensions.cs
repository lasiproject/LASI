using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using LASI.Core.Configuration;
using LASI.Core.Heuristics;
using LASI.Utilities;
using static System.StringComparer;
using static LASI.Utilities.FunctionExtensions;

namespace LASI.Core.Heuristics
{
    public static partial class EntitySimilarityExtensions
    {
        /// <summary>Determines if two IEntity instances are similar.</summary>
        /// <param name="first">The first IEntity</param>
        /// <param name="second">The second IEntity</param>
        /// <returns><c>true</c> if the given IEntity instances are similar; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this IEntity first, IEntity second) =>
            first.Match()
                .When(Equals(first, second) || first.Text.EqualsIgnoreCase(second.Text))
                .Then(Similarity.Similar)
                .Case((IAggregateEntity ae1) => second.Match()
                        .Case((IAggregateEntity ae2) => ae1.IsSimilarTo(ae2))
                        .Case((IEntity e2) => Similarity.FromBoolean(ae1.Any(entity => entity.IsSimilarTo(e2))))
                    .Result())
                .Case((Noun n1) => second.Match()
                        .Case((Noun n2) => n1.IsSimilarTo(n2))
                        .Case((NounPhrase np2) => n1.IsSimilarTo(np2))
                        .Result())
                .Case((NounPhrase np1) => second.Match()
                        .Case((NounPhrase np2) => np1.IsSimilarTo(np2))
                        .Case((Noun n2) => np1.IsSimilarTo(n2))
                    .Result())
                .Result();

        /// <summary>Determines if two IAggregateEntity instances are similar.</summary>
        /// <param name="first">The first IAggregateEntity</param>
        /// <param name="second">The second IAggregateEntity</param>
        /// <returns>
        /// <c>true</c> if the given IAggregateEntity instances are similar; otherwise, <c>false</c>.
        /// </returns>
        private static Similarity IsSimilarTo(this IAggregateEntity first, IAggregateEntity second)
        {
            var simResults = from e1 in first
                             from e2 in second
                             select e1.IsSimilarTo(e2);
            return Similarity.FromRatio(simResults.Select(x => x.Boolean).PercentTrue() / 100);
        }

        /// <summary>Determines if the provided Noun is similar to the provided NounPhrase.</summary>
        /// <param name="first">The <see cref="Noun" />.</param>
        /// <param name="second">The <see cref="NounPhrase" />.</param>
        /// <returns>
        /// <c>true</c> if the provided Noun is similar to the provided NounPhrase; otherwise, <c>false</c>.
        /// </returns>
        private static Similarity IsSimilarTo(this Noun first, NounPhrase second)
        {
            var phraseNouns = second.Words.OfNoun().ToList();
            return Similarity.FromBoolean(phraseNouns.Count == 1 && phraseNouns.First().IsSimilarTo(first));
        }

        /// <summary>Determines if the provided NounPhrase is similar to the provided Noun.</summary>
        /// <param name="first">The NounPhrase.</param>
        /// <param name="second">The Noun.</param>
        /// <returns>
        /// <c>true</c> if the provided NounPhrase is similar to the provided Noun; otherwise, <c>false</c>.
        /// </returns>
        private static Similarity IsSimilarTo(this NounPhrase first, Noun second) => second.IsSimilarTo(first);

        /// <summary>Determines if two NounPhrases are similar.</summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns><c>true</c> if the given NounPhrases are similar; otherwise, <c>false</c>.</returns>
        public static Similarity IsSimilarTo(this NounPhrase first, NounPhrase second) => Similarity.FromRatio(GetSimilarityRatio(first, second));


        /// <summary>Determines if the two provided Noun instances are similar.</summary>
        /// <param name="first">The first Noun.</param>
        /// <param name="second">The second Noun.</param>
        /// <returns><c>true</c> if the first Noun is similar to the second; otherwise, <c>false</c>.</returns>
        private static Similarity IsSimilarTo(this Noun first, Noun second) =>
            Similarity.FromBoolean(Equals(first, second) || (first?.GetSynonyms().Contains(second?.Text) ?? false));

        /// <summary>Determines if the text is equal to that of a known Common Noun.</summary>
        /// <param name="nounText">The text to test.</param>
        /// <returns>
        /// <c>true</c> if the text is equal to that of a known Common Noun; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCommon(string nounText) => ScrabbleDictionary.Contains(nounText);

        /// <summary>
        /// Determines whether the ProperNoun's text corresponds to a female first name in the
        /// English language. Lookups are performed in a case insensitive manner and currently do
        /// not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>
        /// <c>true</c> if the ProperNoun's text corresponds to a female first name in the English
        /// language; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFemaleFirstName(this ProperNoun proper) => NameData.IsFemaleFirst(proper.Text);

        /// <summary>Determines if the provided NounPhrase is a known Full Female Name.</summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>
        /// <c>true</c> if the provided NounPhrase is a known Full Female Name; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFullFemaleName(this NounPhrase name) => DetermineNounPhraseGender(name).IsFemale();

        /// <summary>Determines whether the provided ProperNoun is a FirstName.</summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns><c>true</c> if the provided ProperNoun is a FirstName; otherwise, <c>false</c>.</returns>
        public static bool IsFirstName(this ProperNoun proper) => NameData.IsFirstName(proper.Text);

        /// <summary>
        /// Determines whether the ProperNoun's text corresponds to a last name in the English
        /// language. Lookups are performed in a case insensitive manner and currently do not
        /// respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>
        /// <c>true</c> if the ProperNoun's text corresponds to a last name in the English language;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLastName(this ProperNoun proper) => NameData.IsLastName(proper.Text);

        /// <summary>
        /// Returns a value indicating whether the ProperNoun's text corresponds to a male first
        /// name in the English language. Lookups are performed in a case insensitive manner and
        /// currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>
        /// <c>true</c> if the ProperNoun's text corresponds to a male first name in the English
        /// language; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMaleFirstName(this ProperNoun proper) => NameData.IsMaleFirst(proper.Text);

        /// <summary>Determines if the provided NounPhrase is a known Full Male Name.</summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>
        /// <c>true</c> if the provided NounPhrase is a known Full Male Name; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFullMaleName(this NounPhrase name) => DetermineNounPhraseGender(name).IsMale();

        /// <summary>
        /// Returns a NameGender value indicating the likely gender of the entity.
        /// </summary>
        /// <param name="entity">
        /// The entity whose gender to lookup.
        /// </param>
        /// <returns>
        /// A NameGender value indicating the likely gender of the entity.
        /// </returns>
        public static Gender GetGender(this IEntity entity) => entity.Match()
            .Case((ISimpleGendered p) => p.Gender)
            .Case((IReferencer p) => GetGender(p))
            .Case((NounPhrase n) => DetermineNounPhraseGender(n))
            .Case((CommonNoun n) => Gender.Neutral)
            .Case((IEntity e) => (from referener in e.Referencers
                                  let gendered = referener as ISimpleGendered
                                  let gender = gendered != null ? gendered.Gender : default
                                  group gender by gender into byGender
                                  orderby byGender.Count() descending
                                  select byGender.Key).DefaultIfEmpty().First(), when: e => e.Referencers.Any())
            .Result();

        /// <summary>
        /// Returns a NameGender value indicating the likely gender of the Pronoun based on its
        /// referent if known, or else its PronounKind.
        /// </summary>
        /// <param name="referencer">The Pronoun whose gender to lookup.</param>
        /// <returns>A NameGender value indicating the likely gender of the Pronoun.</returns>
        private static Gender GetGender(IReferencer referencer) => referencer.Match()
            .Case((PronounPhrase p) => DeterminePronounPhraseGender(p))
            .When(referencer.RefersTo != null)
            .Then((from referrenced in referencer.RefersTo
                   let gender = referrenced.Match()
                       .Case((NounPhrase n) => DetermineNounPhraseGender(n))
                       .Case((Pronoun r) => r.Gender)
                       .Case((ProperSingularNoun r) => r.Gender)
                       .Case((CommonNoun n) => Gender.Neutral)
                   .Result()
                   group gender by gender into byGender
                   where byGender.Count() == referencer.RefersTo.Count()
                   select byGender.Key).FirstOrDefault())
            .Case((ISimpleGendered p) => p.Gender)
        .Result();

        /// <summary>
        /// Returns a double value indicating the degree of similarity between two NounPhrases.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns>A double value indicating the degree of similarity between two NounPhrases.</returns>
        private static double GetSimilarityRatio(NounPhrase first, NounPhrase second)
        {
            var left = first.Words.OfNoun().ToList();
            if (left.Count == 0) { return 0; }
            var right = second.Words.OfNoun().ToList();
            if (right.Count == 0) { return 0; }
            var comparisonResults = from outer in (right.Count > left.Count ? left : right)
                                    from inner in (left.Count < right.Count ? right : left)
                                    select outer.IsSimilarTo(inner) ? 0.7 : 0;
            return comparisonResults.Average();
        }

        // TODO: refactor these two methods. their interaction is very opaque and error prone.
        //       Although they are private, they make maintaining related algorithms difficult.
        private static Gender DetermineNounPhraseGender(NounPhrase name)
        {
            var properNouns = name.Words.OfProperNoun();
            var first = properNouns.OfSingular()
                .FirstOrDefault(n => n.Gender.IsMaleOrFemale());
            var last = properNouns.LastOrDefault(n => n != first && n.IsLastName());
            return (first != null && (last != null || properNouns.All(n => n.GetGender() == first.Gender)))
                ? first.Gender
                : name.Words.OfNoun().All(n => n.GetGender().IsNeutral())
                    ? Gender.Neutral
                    : Gender.Undetermined;
        }

        private static Gender DeterminePronounPhraseGender(PronounPhrase pronounPhrase)
        {
            if (pronounPhrase.Words.All(w => w is Determiner)) { return Gender.Undetermined; }
            var genders = pronounPhrase.Words.OfType<ISimpleGendered>().Select(w => w.Gender);
            return pronounPhrase.Words.OfProperNoun().Any(n => !(n is ISimpleGendered))
                ? DetermineNounPhraseGender(pronounPhrase)
                : genders.Any()
                    ? genders.All(g => g.IsFemale()) ? Gender.Female
                    : genders.All(g => g.IsMale()) ? Gender.Male
                    : genders.All(g => g.IsNeutral()) ? Gender.Neutral : Gender.Undetermined
                    : Gender.Undetermined;
        }


        /// <summary>
        /// The sequence of strings corresponding to all nouns in the Scrabble Dictionary data source.
        /// </summary>
        public static IEnumerable<string> ScrabbleDictionary => scrabbleDictionary.Value;
        /// <summary>
        /// scrabble dictionary Internal Lookups
        /// </summary>
        static Lazy<IEnumerable<string>> scrabbleDictionary = new Lazy<IEnumerable<string>>(() =>
        {
            var resourceName = "Scrabble Dictionary";
            Lexicon.OnResourceLoading(null, new ResourceLoadEventArgs(resourceName, 0, 0));


            var (timed, timer) = FunctionExtensions.WithTimer(loadWords);
            var words = timed();
            Lexicon.OnResourceLoaded(null, new ResourceLoadEventArgs(resourceName, 0, timer.ElapsedMilliseconds));
            return words;

            IImmutableSet<string> loadWords() => File.ReadAllText(Paths.ScrabbleDict)
    .SplitRemoveEmpty('\r', '\n')
    .Select(s => s.ToLower())
    .Except(NameData.AllNames, OrdinalIgnoreCase)
    .ToImmutableHashSet(OrdinalIgnoreCase);
        }, isThreadSafe: true);


        public static event EventHandler<ResourceLoadEventArgs> ResourceLoaded
        {
            add => Lexicon.ResourceLoaded += value;
            remove => Lexicon.ResourceLoaded -= value;
        }

        /// <summary>
        /// Raised when a resource starts loading.
        /// </summary>
        public static event EventHandler<ResourceLoadEventArgs> ResourceLoading
        {
            add => Lexicon.ResourceLoading += value;
            remove => Lexicon.ResourceLoading -= value;
        }
        static NameProvider NameData => nameData.Value;

        static Lazy<NameProvider> nameData = new Lazy<NameProvider>(() =>
        {
            var resourceName = "Name Data";
            var nameProvider = new NameProvider();

            Lexicon.OnResourceLoading(nameProvider, new ResourceLoadEventArgs(resourceName, 0, 0));

            var (timed, timer) = Time(() => nameProvider
                  .InitializeAsync()
                  .GetAwaiter()
                  .GetResult());

            timed();

            Lexicon.OnResourceLoaded(nameProvider, new ResourceLoadEventArgs(resourceName, 0, timer.ElapsedMilliseconds));
            return nameProvider;
        }, isThreadSafe: true);

    }
}
