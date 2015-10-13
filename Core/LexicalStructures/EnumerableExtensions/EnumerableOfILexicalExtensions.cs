using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized;
using LASI.Utilities.Specialized.Enhanced.Universal;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the ILexical interface.
    /// </summary>
    /// <seealso cref="ILexical" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    /// <seealso cref="System.Linq.Enumerable" />
    public static partial class LexicalEnumerable
    {
        #region Sequential Implementations

        /// <summary>
        /// Determines whether a sequence of ILexical constructs contains a specified element by
        /// using a specified equality comparison function.
        /// </summary>
        /// <param name="elements">
        /// The sequence of ILexical constructs in which to search for the given element.
        /// </param>
        /// <param name="element">The element to search for.</param>
        /// <param name="comparison">A function to compare two ILexicals for equality.</param>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <returns><c>true</c> if the sequence contains the given element; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// var myNPs = myDoc.Phrases.OfNounPhrase();
        /// if (myNPs.Contains(NpToFind, (np1, np2) =&gt; np1.Text == np2.Text || np1.IsAliasFor(np2)))
        /// {
        ///     Console.WriteLine("Found!");
        /// }
        /// </code>
        /// </example>
        public static bool Contains<TLexical>(this IEnumerable<TLexical> elements, TLexical element, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => elements.Contains(element, Comparer.Create(comparison));

        /// <summary>
        /// Produces the set difference of two sequences by using the specified comparison function
        /// to compare values.
        /// </summary>
        /// <typeparam name="TLexical">
        /// The type of the elements of the input sequences. Any Type which implements the ILexical
        /// interface is applicable.
        /// </typeparam>
        /// <param name="first">
        /// A sequence whose elements that are not also in second will be returned.
        /// </param>
        /// <param name="second">
        /// A sequence whose elements that also occur in the first sequence will cause those
        /// elements to be removed from the returned sequence.
        /// </param>
        /// <param name="comparison">
        /// A function to compare two instances of type TLexical and return true or false.
        /// </param>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        public static IEnumerable<TLexical> Except<TLexical>(this IEnumerable<TLexical> first, IEnumerable<TLexical> second, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => first.Except(second, Comparer.Create(comparison));

        /// <summary>
        /// Returns distinct elements from a sequence of TLexical constructs by using a specified
        /// equality comparison function.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="elements">The sequence of TLexical constructs in which to eliminate duplicates.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A new sequence of ILexical constructs containing only the distinct elements of the
        /// source sequence as determined by the provided comparison function.
        /// </returns>
        /// <example>
        /// <code>
        /// var distinctNounPhrases = myDoc.Phrases
        ///     .OfNounPhrase()
        ///     .Distinct((np1, np2) =&gt; np1.Text == np2.Text || np1.IsAliasFor(np2));
        /// </code>
        /// </example>
        public static IEnumerable<TLexical> Distinct<TLexical>(this IEnumerable<TLexical> elements, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => elements.Distinct(Comparer.Create(comparison));

        /// <summary>
        /// Produces the set intersection of two sequences of TLexical constructs by using the
        /// specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">
        /// A sequence of TLexical whose distinct elements that also appear in the second sequence
        /// of TLexical will be returned.
        /// </param>
        /// <param name="second">
        /// A sequence of TLexical whose distinct elements that also appear in the first sequence of
        /// TLexical will be returned.
        /// </param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A sequence that contains the elements that form the set intersection of the two sequences.
        /// </returns>
        /// <example>
        /// <code>
        /// var wordsInCommon = doc1.words.Intersect(doc2.words, (w1, w2) =&gt; w1.IsSynonymFor(w2));
        /// </code>
        /// </example>
        public static IEnumerable<TLexical> Intersect<TLexical>(this IEnumerable<TLexical> first, IEnumerable<TLexical> second, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => first.Intersect(second, Comparer.Create(comparison));

        /// <summary>
        /// Determines whether two sequences of TLexical are equal by comparing their elements by
        /// the specified comparison function.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">The left hand sequence of TLexicals to compare.</param>
        /// <param name="second">The right hand sequence of TLexicals to compare.</param>
        /// <param name="comparison">
        /// A function to compare two TLexicals for equality. This will be applied a single time to
        /// each pair of inputs.
        /// </param>
        /// <returns>
        /// <c>true</c> if the two source sequences are of equal length and their corresponding
        /// elements compare equal according to provided Lexical comparison function; otherwise, <c>false</c>.
        /// </returns>
        public static bool SequenceEqual<TLexical>(this IEnumerable<TLexical> first, IEnumerable<TLexical> second, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => first.SequenceEqual(second, Comparer.Create(comparison));

        /// <summary>
        /// Produces the set union of two sequences of ILexical constructs by using the specified
        /// comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">
        /// A sequence of TLexical whose distinct elements form the first set of the union.
        /// </param>
        /// <param name="second">
        /// A sequence of TLexical whose distinct elements form the second set of the union.
        /// </param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A sequence that contains the elements from both input sequences, excluding duplicates.
        /// </returns>
        /// <example>
        /// <code>
        /// var distinctActionsAcross = doc1.GetActions().Union(doc2.GetActions(), (a1, a2) =&gt; a1.IsSimilarTo(A2));
        /// </code>
        /// </example>
        public static IEnumerable<TLexical> Union<TLexical>(this IEnumerable<TLexical> first, IEnumerable<TLexical> second, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => first.Union(second, Comparer.Create(comparison));

        /// <summary>Gets all of the word instances in the sequence of ILexicals.</summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>all of the word instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Word> OfWord(this IEnumerable<ILexical> elements) =>
            elements.SelectMany(e =>
                e.Match()
                    .Case((Clause c) => c.Words)
                    .Case((Phrase p) => p.Words)
                    .Case((Word w) => w.Lift())
                .Result().EmptyIfNull());

        /// <summary>Gets all of the Phrase instances in the sequence of ILexicals.</summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the Phrase instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Phrase> OfPhrase(this IEnumerable<ILexical> elements) =>
            elements.SelectMany(e =>
                e.Match()
                    .Case((Clause c) => c.Phrases)
                    .Case((Phrase p) => p.Lift())
                .Result().EmptyIfNull());

        /// <summary>Gets all of the Clause instances in the sequence of ILexicals.</summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the Clause instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Clause> OfClause(this IEnumerable<ILexical> elements) => elements.OfType<Clause>();

        /// <summary>Returns all Entities in the sequence.</summary>
        /// <param name="elements">The sequence of Lexicals to filter.</param>
        /// <returns>All Entities in the sequence.</returns>
        public static IEnumerable<IEntity> OfEntity(this IEnumerable<ILexical> elements) => elements.OfType<IEntity>();

        /// <summary>Returns all Verbals in the sequence.</summary>
        /// <param name="elements">The sequence of Lexicals to filter</param>
        /// <returns>All Verbals in the sequence.</returns>
        public static IEnumerable<IVerbal> OfVerbal(this IEnumerable<ILexical> elements) => elements.AsRecursivelyEnumerable().OfType<IVerbal>();

        /// <summary>Returns all Descriptors in the sequence.</summary>
        /// <param name="elements">The sequence of Lexicals to filter</param>
        /// <returns>All Descriptors in the sequence.</returns>
        public static IEnumerable<IDescriptor> OfDescriptor(this IEnumerable<ILexical> elements) => elements.AsRecursivelyEnumerable().OfType<IDescriptor>();

        #endregion Sequential Implementations

        #region Parallel Implementations

        /// <summary>
        /// Determines whether a sequence of ILexical constructs contains a specified element by
        /// using a specified equality comparison function.
        /// </summary>
        /// <param name="elements">
        /// The sequence of ILexical constructs in which to search for the given element.
        /// </param>
        /// <param name="element">The element to search for.</param>
        /// <param name="comparison">A function to compare two ILexicals for equality.</param>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <returns><c>true</c> if the sequence contains the given element; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// var myNPs = myDoc.Phrases.OfNounPhrase();
        /// if (myNPs.Contains(NpToFind, (np1, np2) =&gt; np1.Text == np2.Text || np1.IsAliasFor(np2)))
        /// {
        ///     Console.WriteLine("Found!");
        /// }
        /// </code>
        /// </example>
        public static bool Contains<TLexical>(this ParallelQuery<TLexical> elements, TLexical element, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => elements.Contains(element, Comparer.Create(comparison));

        /// <summary>
        /// Produces the set difference of two sequences by using the specified comparison function
        /// to compare values.
        /// </summary>
        /// <typeparam name="TLexical">
        /// The type of the elements of the input sequences. Any Type which implements the ILexical
        /// interface is applicable.
        /// </typeparam>
        /// <param name="first">
        /// A sequence whose elements that are not also in second will be returned.
        /// </param>
        /// <param name="second">
        /// A sequence whose elements that also occur in the first sequence will cause those
        /// elements to be removed from the returned sequence.
        /// </param>
        /// <param name="comparison">
        /// A function to compare two instances of type TLexical and return true or false.
        /// </param>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        public static ParallelQuery<TLexical> Except<TLexical>(this ParallelQuery<TLexical> first, ParallelQuery<TLexical> second, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => first.Except(second, Comparer.Create(comparison));

        /// <summary>
        /// Returns distinct elements from a sequence of TLexical constructs by using a specified
        /// equality comparison function.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="elements">The sequence of TLexical constructs in which to eliminate duplicates.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A new sequence of ILexical constructs containing only the distinct elements of the
        /// source sequence as determined by the provided comparison function.
        /// </returns>
        /// <example>
        /// <code>
        /// var distinctNounPhrases = myDoc.Phrases
        ///     .AsParallel()
        ///     .OfNounPhrase()
        ///     .Distinct((np1, np2) =&gt; np1.Text == np2.Text || np1.IsAliasFor(np2));
        /// </code>
        /// </example>
        public static ParallelQuery<TLexical> Distinct<TLexical>(this ParallelQuery<TLexical> elements, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => elements.Distinct(Comparer.Create(comparison));

        /// <summary>
        /// Produces the set intersection of two sequences of TLexical constructs by using the
        /// specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">
        /// A sequence of TLexical whose distinct elements that also appear in the second sequence
        /// of TLexical will be returned.
        /// </param>
        /// <param name="second">
        /// A sequence of TLexical whose distinct elements that also appear in the first sequence of
        /// TLexical will be returned.
        /// </param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A sequence that contains the elements that form the set intersection of the two sequences.
        /// </returns>
        /// <example>
        /// <code>
        /// var wordsInCommon = doc1.words.Intersect(doc2.words, (w1, w2) =&gt; w1.IsSynonymFor(w2));
        /// </code>
        /// </example>
        public static ParallelQuery<TLexical> Intersect<TLexical>(this ParallelQuery<TLexical> first, ParallelQuery<TLexical> second, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => first.Intersect(second, Comparer.Create(comparison));

        /// <summary>
        /// Determines whether two sequences of TLexical are equal by comparing their elements by
        /// the specified comparison function.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">The left hand sequence of TLexicals to compare.</param>
        /// <param name="second">The right hand sequence of TLexicals to compare.</param>
        /// <param name="comparison">
        /// A function to compare two TLexicals for equality. This will be applied a single time to
        /// each pair of inputs.
        /// </param>
        /// <returns>
        /// <c>true</c> if the two source sequences are of equal length and their corresponding
        /// elements compare equal according to provided Lexical comparison function; otherwise, <c>false</c>.
        /// </returns>
        public static bool SequenceEqual<TLexical>(this ParallelQuery<TLexical> first, ParallelQuery<TLexical> second, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => first.SequenceEqual(second, Comparer.Create(comparison));

        /// <summary>
        /// Produces the set union of two sequences of ILexical constructs by using the specified
        /// comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">
        /// A sequence of TLexical whose distinct elements form the first set of the union.
        /// </param>
        /// <param name="second">
        /// A sequence of TLexical whose distinct elements form the second set of the union.
        /// </param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A sequence that contains the elements from both input sequences, excluding duplicates.
        /// </returns>
        /// <example>
        /// <code>
        /// var distinctActionsAcross = doc1.Verbals
        ///     .Union(doc2.Verbals, (a1, a2) =&gt; a1.IsSimilarTo(A2));
        /// </code>
        /// </example>
        public static ParallelQuery<TLexical> Union<TLexical>(this ParallelQuery<TLexical> first, ParallelQuery<TLexical> second, Func<TLexical, TLexical, bool> comparison)
            where TLexical : ILexical => first.Union(second, Comparer.Create(comparison));

        /// <summary>Gets all of the word instances in the sequence of ILexicals.</summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the word instances in the sequence of ILexicals.</returns>
        public static ParallelQuery<Word> OfWord(this ParallelQuery<ILexical> elements) =>
            elements.SelectMany(e =>
                e.Match()
                    .Case((Clause c) => c.Words)
                    .Case((Phrase p) => p.Words)
                    .Case((Word w) => new[] { w })
                .Result(new Word[0]));

        /// <summary>Gets all of the Clause instances in the sequence of ILexicals.</summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the Clause instances in the sequence of ILexicals.</returns>
        public static ParallelQuery<Clause> OfClause(this ParallelQuery<ILexical> elements) => elements.OfType<Clause>();

        /// <summary>Returns all Entities in the sequence.</summary>
        /// <param name="elements">The sequence of Lexicals to filter.</param>
        /// <returns>All Entities in the sequence.</returns>
        public static ParallelQuery<IEntity> OfEntity(this ParallelQuery<ILexical> elements) => elements.OfType<IEntity>();

        /// <summary>Returns all Verbals in the sequence.</summary>
        /// <param name="elements">The sequence of Lexicals to filter</param>
        /// <returns>All Verbals in the sequence.</returns>
        public static ParallelQuery<IVerbal> OfVerbal(this ParallelQuery<ILexical> elements) => elements.AsRecursivelyEnumerable().OfType<IVerbal>().AsParallel();

        /// <summary>Returns all Descriptors in the sequence.</summary>
        /// <param name="elements">The sequence of Lexicals to filter</param>
        /// <returns>All Descriptors in the sequence.</returns>
        public static ParallelQuery<IDescriptor> OfDescriptor(this ParallelQuery<ILexical> elements) => elements.AsRecursivelyEnumerable().OfType<IDescriptor>().AsParallel();

        /// <summary>Gets all of the Phrase instances in the sequence of ILexicals.</summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the Phrase instances in the sequence of ILexicals.</returns>
        private static ParallelQuery<Phrase> OfPhrase(this ParallelQuery<ILexical> elements) =>
            elements.SelectMany(e =>
                e.Match()
                    .Case((Clause c) => c.Phrases)
                    .Case((Phrase p) => new[] { p })
                .Result(new Phrase[0]));

        #endregion Parallel Implementations
    }
}