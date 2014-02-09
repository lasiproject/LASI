using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LASI.Core.Patternization
{
    /// <summary>
    /// Provides for the construction of flexible Pattern Matching expressions.
    /// </summary>
    public static class Matcher
    {

        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified ILexical value.
        /// </summary> 
        /// <param name="value">The ILexical value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified ILexical value.</returns>
        public static Match<T> Match<T>(this T value) where T : class, ILexical
        {
            return new Match<T>(value);
        }

    }
}
