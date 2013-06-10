using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    public static class IEnumerableOfILexicalExtensions
    {
        /// <summary>
        ///  Returns distinct elements from a sequence of ILexical constructs by using a specified specified equality comparison function.
        /// </summary>
        /// <typeparam name="T">Any type which implements the ILexical interface.</typeparam>
        /// <param name="elements">The sequence of ILexical constructs in which to search for the given element.</param>
        /// <param name="comparison">A function to compare two ILexicals for equality.</param>
        /// <returns>A System.Collections.Generic.IEnumerable<T> that contains only the distinct elements of the source sequence as determined by the provided comparison function.
        /// </returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> elements, Func<T, T, bool> comparison) where T : ILexical
        {
            return elements.Distinct(Comparisons<T>.CreateCustom(comparison));

        }
        /// <summary>
        /// Determines whether a sequence of ILexical constructs contains a specified element by using a specified equality comparison function.
        /// </summary>
        /// <param name="elements">The sequence of ILexical constructs in which to search for the given element.</param>
        /// <param name="element">The element to search for.</param>
        /// <param name="comparison">A function to compare two ILexicals for equality.</param>
        /// <typeparam name="T">Any type which implements the ILexical interface.</typeparam>
        /// <returns>True if the sequence contains the given element, false otherwise.</returns>
        public static bool Contains<T>(this IEnumerable<T> elements, T element, Func<T, T, bool> comparison) where T : ILexical
        {
            return elements.Contains(element, Comparisons<T>.CreateCustom(comparison));

        }
        /// <summary>
        /// Gets all of the word instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="lexicals">The source sequence of ILexical instances.</param>
        /// <returns>all of the word instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Word> GetWords(this IEnumerable<ILexical> lexicals)
        {
            return lexicals.OfType<Word>();
        }
        /// <summary>
        /// Gets all of the Phrase instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="lexicals">The source sequence of ILexical instances.</param>
        /// <returns>all of the Phrase instances in the sequence of ILexicals.</returns>
        public static IEnumerable<Phrase> GetPhrases(this IEnumerable<ILexical> lexicals)
        {
            return lexicals.OfType<Phrase>();
        }
    }
}
