using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    static class IEnumerableOfParagraphExtensions
    {
        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">a sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Sentence> GetSentences(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   select s;
        }
        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">a sequence of Paragraph instances.</param>
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
        /// <param name="paragraphs">a sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Word> GetWords(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   from w in s.Words
                   select w;
        }
    }
}
