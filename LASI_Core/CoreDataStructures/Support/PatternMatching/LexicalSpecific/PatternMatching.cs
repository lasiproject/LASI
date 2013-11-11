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
    public static class PatternMatching
    {

        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified ILexical value.
        /// </summary> 
        /// <param name="value">The ILexical value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified ILexical value.</returns>
        public static IMatchCase<ILexical> Match(this ILexical value) { return new MatchCase<ILexical>(value); }
        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified IEntity value.
        /// </summary> 
        /// <param name="value">The IEntity value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified IEntity value.</returns>
        public static IMatchCase<IEntity> Match(this IEntity value) { return new MatchCase<IEntity>(value); }
        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified IVerbal value.
        /// </summary> 
        /// <param name="value">The IVerbal value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified IVerbal value.</returns>
        public static IMatchCase<IVerbal> Match(this IVerbal value) { return new MatchCase<IVerbal>(value); }
        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified IDescriptor value.
        /// </summary> 
        /// <param name="value">The IDescriptor value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified IDescriptor value.</returns>
        public static IMatchCase<IDescriptor> Match(this IDescriptor value) { return new MatchCase<IDescriptor>(value); }
        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified IAdverbial value.
        /// </summary> 
        /// <param name="value">The IAdverbial value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified IAdverbial value.</returns>
        public static IMatchCase<IAdverbial> Match(this IAdverbial value) { return new MatchCase<IAdverbial>(value); }
        /// <summary>
        /// Begins a non result returning Type based Pattern Matching expression over the specified IPrepositional value.
        /// </summary> 
        /// <param name="value">The IPrepositional value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression over the specified IPrepositionale value.</returns>
        public static IMatchCase<IPrepositional> Match(this IPrepositional value) { return new MatchCase<IPrepositional>(value); }

    }
}
