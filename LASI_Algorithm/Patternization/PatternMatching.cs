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
        public static MatchCase<T, R> MatchTo<T, R>(T value) where T : class, ILexical { return new MatchCase<T, R>(value); }
        /// <summary>
        /// Constructs the intermediate "On" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a On...To expression.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="value">The value to match with.</param>
        /// <returns>The intermediate "On" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a On...To expression.
        /// </returns>
        public static OnTo<T> On<T>(T value) where T : class, ILexical { return new OnTo<T>(value); }


        /// <summary>
        /// Represents the intermediate "From" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a From...To expression.
        /// </summary>
        /// <typeparam name="T">The Type of the value that will be matched with.</typeparam>
        public class OnTo<T> where T : class, ILexical
        {
            internal OnTo(T value) {
                _value = value;
            }
            /// <summary>
            /// Completes the From...To expression by specifying the type of the Result and constructing and returning the head of a result-yielding, Type based Pattern Matching expression.
            /// </summary>
            /// <typeparam name="R">The Type of the Results which may be returned by the expressions appended to the newly created expression.</typeparam>
            /// <returns>
            ///  The head of a result-yielding, Type based Pattern Matching expression.
            ///  </returns>  
            public MatchCase<T, R> To<R>() {
                return new MatchCase<T, R>(_value);
            }
            private T _value;
        }

    }





}