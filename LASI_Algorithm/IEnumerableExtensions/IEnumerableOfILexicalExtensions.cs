using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.SyntacticInterfaces;

namespace LASI.Algorithm
{
    public static class IEnumerableOfILexicalExtensions
    {
        /// <summary>
        /// Gets all of the Word instances in the sequence of ILexicals.
        /// </summary>
        /// <param name="lexicals">The source sequence of ILexical instances.</param>
        /// <returns>all of the Word instances in the sequence of ILexicals.</returns>
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
