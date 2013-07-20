using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the ILexical interface.
    /// </summary>
    /// <see cref="ILexical"/>
    public static class IEnumerableOfILexicalExtensions
    {
        /// <summary>
        ///  Returns distinct elements from a sequence of ILexical constructs by using a specified specified equality comparison function.
        /// </summary>
        /// <typeparam name="T">Any type which implements the ILexical interface.</typeparam>
        /// <param name="elements">The sequence of ILexical constructs in which to NounText for the given element.</param>
        /// <param name="comparison">A function to compare two ILexicals for equality.</param>
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
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> elements, Func<T, T, bool> comparison) where T : ILexical {
            return elements.Distinct(LexicalComparers<T>.CreateCustom(comparison));

        }
        /// <summary>
        /// Determines whether a sequence of ILexical constructs contains a specified element by using a specified equality comparison function.
        /// </summary>
        /// <param name="elements">The sequence of ILexical constructs in which to NounText for the given element.</param>
        /// <param name="element">The element to search for.</param>
        /// <param name="comparison">A function to compare two ILexicals for equality.</param>
        /// <typeparam name="T">Any type which implements the ILexical interface.</typeparam>
        /// <returns>True if the sequence contains the given element, false otherwise.</returns> 
        /// <example>
        /// <code>
        /// var myNPs = myDoc.Phrases.GetNounPhrases();
        /// if (myNPs.Contains(NpToFind, (np1, np2) => np1.Text == np2.Text || np1.IsAliasFor(np2)))
        /// {
        ///     Console.WriteLine("Found!");
        /// }
        /// </code>
        /// </example>
        public static bool Contains<T>(this IEnumerable<T> elements, T element, Func<T, T, bool> comparison) where T : ILexical {
            return elements.Contains(element, LexicalComparers<T>.CreateCustom(comparison));

        }
        /// <summary>
        /// Produces the set difference of two sequences by using the specified comparison function to compare values.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequences. Any Type which implements the ILexical interface is applicable.</typeparam>
        /// <param name="first">A sequence whose elements that are not also in second will be returned.</param>
        /// <param name="second">A sequence whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <param name="comparison">A function to compare two instance of type T and return true or false.</param>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparison) where T : ILexical {
            return first.Except(second, LexicalComparers<T>.CreateCustom(comparison));

        }
        /// <summary>
        /// Returns a set representation of the given sequence of ILexical using the provided comparison function to determine element distinctness.
        /// </summary>
        /// <typeparam name="T">Any type which implements the ILexical interface.</typeparam>
        /// <param name="source">The sequence whose distinct elements will comprise the resulting set.</param>
        /// <param name="comparison">A function which compares two elements, returning false if they should be considered distinct and true otherwise.</param>
        /// <returns>A set representation of the given sequence of ILexical using the provided comparison function to determine element distinctness.</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> source, Func<T, T, bool> comparison) where T : ILexical {
            return new HashSet<T>(source, LexicalComparers<T>.CreateCustom(comparison));
        }
        /// <summary>
        /// Gets all of the word instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="lexicals">The source sequence of ILexical instances.</param>
        /// <returns>all of the word instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Word> GetWords(this IEnumerable<ILexical> lexicals) {
            return lexicals.OfType<Word>();
        }
        /// <summary>
        /// Gets all of the Phrase instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="lexicals">The source sequence of ILexical instances.</param>
        /// <returns>all of the Phrase instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Phrase> GetPhrases(this IEnumerable<ILexical> lexicals) {
            return lexicals.OfType<Phrase>();
        }
    }
}
