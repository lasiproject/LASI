using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LASI.Algorithm.Patternization
{
    /// <summary>
    /// Provides for the construction of flexible Pattern Matching expressions.
    /// </summary>
    public static class PatternMatching
    {
        /// <summary>
        /// Constructs the head of a non result returning Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="value">The value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression with the specified value.</returns>
        public static MatchCase<T> Match<T>(this T value) where T : class, ILexical { return new MatchCase<T>(value); }
        /// <summary>
        /// Constructs the head of a result-yielding, Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <typeparam name="R">The Type of the result to be yielded.</typeparam> 
        /// <param name="value">The value to match with.</param>     
        /// <returns>The head of a result-yielding, Type based Pattern Matching expression with the specified value.</returns>
        public static MatchCase<T, R> MatchAndYield<T, R>(T value) where T : class, ILexical { return new MatchCase<T, R>(value); }

    }
}
