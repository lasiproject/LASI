using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.PatternMatching;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    /// <summary>
    /// Provides extension methods for aquiring a <see cref="SequenceMatch"/> object to perform a operation.
    /// </summary>
    public static class MatchExtensions
    {
        /// <summary>
        /// Begins a sequential match operation of the given sentence.
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public static SequenceMatch Match(this Sentence sentence) {
            return new SequenceMatch(sentence);
        }

        public static SequenceMatch Match(this IEnumerable<ILexical> sequencialElements) {
            return new SequenceMatch(sequencialElements);
        }
    }
}