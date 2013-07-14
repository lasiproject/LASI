using LASI.Algorithm.DocumentConstructs;
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
    /// <see cref="DocumentConstructs.Document"/>
    /// <seealso cref="DocumentConstructs.Sentence"/>
    /// <seealso cref="DocumentConstructs.Paragraph"/>
    static class IEnumerableOfDocumentStructureExtensions
    {
        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Sentence> GetSentences(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   select s;
        }
        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Phrase> GetPhrases(this IEnumerable<Paragraph> paragraphs) {
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
        public static IEnumerable<Word> GetWords(this IEnumerable<Paragraph> paragraphs) {
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
        public static IEnumerable<Phrase> GetPhrases(this IEnumerable<Sentence> sentences) {
            return from s in sentences
                   from r in s.Phrases
                   select r;
        }
        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static IEnumerable<Word> GetWords(this IEnumerable<Sentence> sentences) {
            return from s in sentences
                   from w in s.Words
                   select w;
        }
    }
}
