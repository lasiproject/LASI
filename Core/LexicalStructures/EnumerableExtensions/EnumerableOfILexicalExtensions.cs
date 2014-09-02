using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the ILexical interface.
    /// </summary>
    /// <see cref="ILexical"/>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}"/>
    /// <seealso cref="System.Linq.Enumerable"/>
    public static partial class LexicalEnumerable
    {
        #region Sequential Implementations
        /// <summary>
        /// Determines whether a sequence of ILexical constructs contains a specified element by using a specified equality comparison function.
        /// </summary>
        /// <param name="elements">The sequence of ILexical constructs in which to search for the given element.</param>
        /// <param name="element">The element to search for.</param>
        /// <param name="comparison">A function to compare two ILexicals for equality.</param>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <returns>True if the sequence contains the given element; otherwise, false.</returns> 
        /// <example>
        /// <code>
        /// var myNPs = myDoc.Phrases.GetNounPhrases();
        /// if (myNPs.Contains(NpToFind, (np1, np2) => np1.Text == np2.Text || np1.IsAliasFor(np2)))
        /// {
        ///     Console.WriteLine("Found!");
        /// }
        /// </code>
        /// </example>
        public static bool Contains<TLexical>(this IEnumerable<TLexical> elements, TLexical element, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return elements.Contains(element, LexicalComparers.Create(comparison));

        }
        /// <summary>
        /// Produces the set difference of two sequences by using the specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">The type of the elements of the input sequences. Any Type which implements the ILexical interface is applicable.</typeparam>
        /// <param name="first">A sequence whose elements that are not also in second will be returned.</param>
        /// <param name="second">A sequence whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <param name="comparison">A function to compare two instances of type TLexical and return true or false.</param>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        public static IEnumerable<TLexical> Except<TLexical>(this IEnumerable<TLexical> first, IEnumerable<TLexical> second, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return first.Except(second, LexicalComparers.Create(comparison));

        }
        /// <summary>
        ///  Returns distinct elements from a sequence of TLexical constructs by using a specified specified equality comparison function.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="elements">The sequence of TLexical constructs in which to eliminate duplicates.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns> A new sequence of ILexical constructs containing only the distinct elements of the source sequence as determined by the provided comparison function.
        /// </returns>
        /// <example>
        /// <code>
        /// var myNPs = myDoc.Phrases.GetNounPhrases();
        /// if (myNPs.Contains(NpToFind, (np1, np2) => np1.Text == np2.Text || np1.IsAliasFor(np2)))
        /// {
        ///     Console.WriteLine("Found!");
        /// }
        /// </code>
        /// </example>
        public static IEnumerable<TLexical> Distinct<TLexical>(this IEnumerable<TLexical> elements, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return elements.Distinct(LexicalComparers.Create(comparison));
        }

        /// <summary>
        ///  Produces the set intersection of two sequences of TLexical constructs by using the specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">A sequence of TLexical whose distinct elements that also appear in the second sequence of TLexical will be returned.</param>
        /// <param name="second">A sequence of TLexical whose distinct elements that also appear in the first sequence of TLexical will be returned.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A sequence that contains the elements that form the set intersection of the two sequences.
        /// </returns>
        /// <example>
        /// <code>
        /// var wordsInCommon = doc1.words.Intersect(doc2.words, (w1, w2) => w1.IsSynonymFor(w2));
        /// </code>
        /// </example>
        public static IEnumerable<TLexical> Intersect<TLexical>(this IEnumerable<TLexical> first, IEnumerable<TLexical> second, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return first.Intersect(second, LexicalComparers.Create(comparison));
        }
        /// <summary>
        /// Determines whether two sequences of TLexical are equal by comparing their elements by the specified comparison function.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">The left hand sequence of TLexicals to compare.</param>
        /// <param name="second">The right hand sequence of TLexicals to compare.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality. This will be applied a single time to each pair of inputs.</param>
        /// <returns>True if the two source sequences are of equal length and their corresponding elements compare equal according to provided Lexical comparison function; otherwise, false.
        /// </returns>
        public static bool SequenceEqual<TLexical>(this IEnumerable<TLexical> first, IEnumerable<TLexical> second, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return first.SequenceEqual(second, LexicalComparers.Create(comparison));
        }
        /// <summary>
        ///  Produces the set union of two sequences of ILexical constructs by using the specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">A sequence of TLexical whose distinct elements form the first set of the union.</param>
        /// <param name="second">A sequence of TLexical whose distinct elements form the second set of the union.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A sequence that contains the elements from both input sequences, excluding duplicates.
        /// </returns>
        /// <example>
        /// <code>
        /// var distinctActionsAcross = doc1.GetActions().Union(doc2.GetActions(), (a1, a2) => a1.IsSimilarTo(A2));
        /// </code>
        /// </example>
        public static IEnumerable<TLexical> Union<TLexical>(this IEnumerable<TLexical> first, IEnumerable<TLexical> second, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return first.Union(second, LexicalComparers.Create(comparison));
        }
        /// <summary>
        /// Gets all of the word instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>all of the word instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Word> OfWord(this IEnumerable<ILexical> elements) {
            return elements.SelectMany(e => {
                var p = e as Phrase;
                if (p != null) {
                    return p.Words;
                } else {
                    var c = e as Clause; if (c != null) {
                        return c.Words;
                    }
                    var w = e as Word;
                    return Enumerable.Repeat(w, w != null ? 1 : 0);

                }
            });
        }
        /// <summary>
        /// Gets all of the Phrase instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the Phrase instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Phrase> OfPhrase(this IEnumerable<ILexical> elements) {
            return elements.SelectMany(e => {
                var c = e as Clause;
                if (c != null) {
                    return c.Phrases;
                } else {
                    var p = e as Phrase;
                    return Enumerable.Repeat(p, p != null ? 1 : 0);
                }
            });
        }
        /// <summary>
        /// Gets all of the Clause instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the Clause instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Clause> OfClause(this IEnumerable<ILexical> elements) {
            return elements.OfType<Clause>();
        }
        /// <summary>
        /// Returns all Entities in the sequence.
        /// </summary>
        /// <param name="elements">The sequence of Lexicalsto filter.</param>
        /// <returns>All Entities in the sequence.</returns>
        public static IEnumerable<IEntity> OfEntity(this IEnumerable<ILexical> elements) {
            return elements.OfType<IEntity>();
        }
        /// <summary>
        /// Returns all Verbals in the sequence.
        /// </summary>
        /// <param name="elements">The sequence of Lexicals to filter</param>
        /// <returns>All Verbals in the sequence.</returns>
        public static IEnumerable<IVerbal> OfVerbal(this IEnumerable<ILexical> elements) {
            return elements.AsRecursiveEnumerable().OfType<IVerbal>();
        }

        #endregion

        #region Parallel Implementations
        /// <summary>
        /// Determines whether a sequence of ILexical constructs contains a specified element by using a specified equality comparison function.
        /// </summary>
        /// <param name="elements">The sequence of ILexical constructs in which to search for the given element.</param>
        /// <param name="element">The element to search for.</param>
        /// <param name="comparison">A function to compare two ILexicals for equality.</param>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <returns>True if the sequence contains the given element; otherwise, false.</returns> 
        /// <example>
        /// <code>
        /// var myNPs = myDoc.Phrases.GetNounPhrases();
        /// if (myNPs.Contains(NpToFind, (np1, np2) => np1.Text == np2.Text || np1.IsAliasFor(np2)))
        /// {
        ///     Console.WriteLine("Found!");
        /// }
        /// </code>
        /// </example>
        public static bool Contains<TLexical>(this ParallelQuery<TLexical> elements, TLexical element, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return elements.Contains(element, LexicalComparers.Create(comparison));

        }
        /// <summary>
        /// Produces the set difference of two sequences by using the specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">The type of the elements of the input sequences. Any Type which implements the ILexical interface is applicable.</typeparam>
        /// <param name="first">A sequence whose elements that are not also in second will be returned.</param>
        /// <param name="second">A sequence whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <param name="comparison">A function to compare two instances of type TLexical and return true or false.</param>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        public static ParallelQuery<TLexical> Except<TLexical>(this ParallelQuery<TLexical> first, ParallelQuery<TLexical> second, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return first.Except(second, LexicalComparers.Create(comparison));

        }
        /// <summary>
        ///  Returns distinct elements from a sequence of TLexical constructs by using a specified specified equality comparison function.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="elements">The sequence of TLexical constructs in which to eliminate duplicates.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns> A new sequence of ILexical constructs containing only the distinct elements of the source sequence as determined by the provided comparison function.
        /// </returns>
        /// <example>
        /// <code>
        /// var myNPs = myDoc.Phrases.GetNounPhrases();
        /// if (myNPs.Contains(NpToFind, (np1, np2) => np1.Text == np2.Text || np1.IsAliasFor(np2)))
        /// {
        ///     Console.WriteLine("Found!");
        /// }
        /// </code>
        /// </example>
        public static ParallelQuery<TLexical> Distinct<TLexical>(this ParallelQuery<TLexical> elements, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return elements.Distinct(LexicalComparers.Create(comparison));
        }
        /// <summary>
        ///  Produces the set intersection of two sequences of TLexical constructs by using the specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">A sequence of TLexical whose distinct elements that also appear in the second sequence of TLexical will be returned.</param>
        /// <param name="second">A sequence of TLexical whose distinct elements that also appear in the first sequence of TLexical will be returned.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A sequence that contains the elements that form the set intersection of the two sequences.
        /// </returns>
        /// <example>
        /// <code>
        /// var wordsInCommon = doc1.words.Intersect(doc2.words, (w1, w2) => w1.IsSynonymFor(w2));
        /// </code>
        /// </example>
        public static ParallelQuery<TLexical> Intersect<TLexical>(this ParallelQuery<TLexical> first, ParallelQuery<TLexical> second, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return first.Intersect(second, LexicalComparers.Create(comparison));
        }
        /// <summary>
        /// Determines whether two sequences of TLexical are equal by comparing their elements by the specified comparison function.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">The left hand sequence of TLexicals to compare.</param>
        /// <param name="second">The right hand sequence of TLexicals to compare.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality. This will be applied a single time to each pair of inputs.</param>
        /// <returns>True if the two source sequences are of equal length and their corresponding elements compare equal according to provided Lexical comparison function; otherwise, false.
        /// </returns>
        public static bool SequenceEqual<TLexical>(this ParallelQuery<TLexical> first, ParallelQuery<TLexical> second, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return first.SequenceEqual(second, LexicalComparers.Create(comparison));
        }
        /// <summary>
        ///  Produces the set union of two sequences of ILexical constructs by using the specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="TLexical">Any type which implements the ILexical interface.</typeparam>
        /// <param name="first">A sequence of TLexical whose distinct elements form the first set of the union.</param>
        /// <param name="second">A sequence of TLexical whose distinct elements form the second set of the union.</param>
        /// <param name="comparison">A function to compare two TLexicals for equality.</param>
        /// <returns>
        /// A sequence that contains the elements from both input sequences, excluding duplicates.
        /// </returns>
        /// <example>
        /// <code>
        /// var distinctActionsAcross = doc1.GetActions().Union(doc2.GetActions(), (a1, a2) => a1.IsSimilarTo(A2));
        /// </code>
        /// </example>
        public static ParallelQuery<TLexical> Union<TLexical>(this ParallelQuery<TLexical> first, ParallelQuery<TLexical> second, Func<TLexical, TLexical, bool> comparison) where TLexical : ILexical {
            return first.Union(second, LexicalComparers.Create(comparison));
        }
        /// <summary>
        /// Gets all of the word instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the word instances in the sequence of ILexicals.</returns>
        public static ParallelQuery<Word> OfWord(this ParallelQuery<ILexical> elements) {
            return elements.SelectMany(e => {
                var p = e as Phrase;
                if (p != null) {
                    return p.Words;
                } else {
                    var c = e as Clause; if (c != null) {
                        return c.Words;
                    }
                    var w = e as Word;
                    return Enumerable.Repeat(w, w != null ? 1 : 0);

                }
            });
        }
        /// <summary>
        /// Gets all of the Phrase instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the Phrase instances in the sequence of ILexicals.</returns>
        static ParallelQuery<Phrase> OfPhrase(this ParallelQuery<ILexical> elements) {
            return elements.SelectMany(e => {
                var c = e as Clause;
                if (c != null) {
                    return c.Phrases;
                } else {
                    var p = e as Phrase;
                    return Enumerable.Repeat(p, p != null ? 1 : 0);
                }
            });
        }
        /// <summary>
        /// Gets all of the Clause instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="elements">The source sequence of ILexical instances.</param>
        /// <returns>All of the Clause instances in the sequence of ILexicals.</returns>
        public static ParallelQuery<Clause> OClause(this ParallelQuery<ILexical> elements) {
            return elements.OfType<Clause>();
        }

        #endregion

    }
}
