using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of various document level constructs.
    /// </summary>
    /// <see cref="DocumentStructures.Document"/>
    /// <seealso cref="DocumentStructures.Sentence"/>
    /// <seealso cref="DocumentStructures.Paragraph"/>
    public static partial class LASIEnumerable
    {
        #region Sequential Implementations

        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Phrase> AllPhrases(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   from r in s.Phrases
                   select r;
        }
        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Word> AllWords(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   from w in s.Words
                   select w;
        }
        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Sentence instances.</returns>
        public static IEnumerable<Phrase> AllPhrases(this IEnumerable<Sentence> sentences) {
            return from s in sentences
                   from r in s.Phrases
                   select r;
        }
        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static IEnumerable<Word> AllWords(this IEnumerable<Sentence> sentences) {
            return from s in sentences
                   from w in s.Words
                   select w;
        }
        #endregion

        #region Parallel Implementations

        /// <summary>
        /// Gets the parallel aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The parallel aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static ParallelQuery<Phrase> AllPhrases(this ParallelQuery<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   from r in s.Phrases
                   select r;
        }
        /// <summary>
        /// Gets the parallel aggregation of all Word instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The parallel aggregation of all Word instances contained within the sequence of Paragraph instances.</returns>
        public static ParallelQuery<Word> AllWords(this ParallelQuery<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   from w in s.Words
                   select w;
        }
        /// <summary>
        /// Gets the parallel aggregation of all Phrase instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The parallel aggregation of all Phrase instances contained within the sequence of Sentence instances.</returns>
        public static ParallelQuery<Phrase> AllPhrases(this ParallelQuery<Sentence> sentences) {
            return from s in sentences
                   from r in s.Phrases
                   select r;
        }
        /// <summary>
        /// Gets the parallel aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The parallel aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static ParallelQuery<Word> AllWords(this ParallelQuery<Sentence> sentences) {
            return from s in sentences
                   from w in s.Words
                   select w;
        }
        #endregion
    }
}
