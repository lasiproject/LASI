using LASI.Algorithm.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines extension methods for sequences of various document level constructs.
    /// </summary>
    /// <see cref="DocumentStructures.Document"/>
    /// <seealso cref="DocumentStructures.Sentence"/>
    /// <seealso cref="DocumentStructures.Paragraph"/>
    static class EnumerableOfDocumentStructureExtensions
    {
        #region Sequential Implementations
        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Sentence> OfSentence(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   select s;
        }
        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Phrase> OfPhrase(this IEnumerable<Paragraph> paragraphs) {
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
        public static IEnumerable<Word> OfWord(this IEnumerable<Paragraph> paragraphs) {
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
        public static IEnumerable<Phrase> OfPhrase(this IEnumerable<Sentence> sentences) {
            return from s in sentences
                   from r in s.Phrases
                   select r;
        }
        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static IEnumerable<Word> OfWord(this IEnumerable<Sentence> sentences) {
            return from s in sentences
                   from w in s.Words
                   select w;
        }
        #endregion

        #region Parallel Implementations
        /// <summary>
        /// Gets the parallel aggregation of all Sentence instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The parallel aggregation of all Sentence instances contained within the sequence of Paragraph instances.</returns>
        public static ParallelQuery<Sentence> OfSentence(this ParallelQuery<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   select s;
        }
        /// <summary>
        /// Gets the parallel aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The parallel aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static ParallelQuery<Phrase> OfPhrase(this ParallelQuery<Paragraph> paragraphs) {
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
        public static ParallelQuery<Word> OfWord(this ParallelQuery<Paragraph> paragraphs) {
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
        public static ParallelQuery<Phrase> OfPhrase(this ParallelQuery<Sentence> sentences) {
            return from s in sentences
                   from r in s.Phrases
                   select r;
        }
        /// <summary>
        /// Gets the parallel aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The parallel aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static ParallelQuery<Word> OfWord(this ParallelQuery<Sentence> sentences) {
            return from s in sentences
                   from w in s.Words
                   select w;
        }
        #endregion
    }
}
