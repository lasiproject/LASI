using LASI.Core.DocumentStructures;
using System;
using LASI;
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
    public static partial class LexicalEnumerable
    {
        static bool Match<T1, TResult>(this Sentence s,
                  Func<T1, Func<TResult>> logic)
                  where T1 : class, ILexical {
            return s.Phrases.FirstOrDefault().Match().Yield<bool>().With<T1>(l => { logic(l); return true; }).Result();
        }
        static bool Match<T1, T2, TResult>(this Sentence s,
                     Func<T1, Func<T2, Func<Func<TResult>>>> logic)
                     where T1 : class, ILexical
                     where T2 : class, ILexical { return false; }


        static bool Match<T1, T2, T3, TResult>(this Sentence s,
                              Func<T1, Func<T2, Func<T3, Func<Func<TResult>>>>> logic)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical { return false; }
        static bool Match<T1, T2, T3, T4, TResult>(this Sentence s,
                     Func<T1, Func<T2, Func<T3, Func<T4, Func<Func<TResult>>>>>> logic)
                     where T1 : class, ILexical
                     where T2 : class, ILexical
                     where T3 : class, ILexical
                     where T4 : class, ILexical { return false; }
        static bool Match<T1, T2, T3, T4, T5, TResult>(this Sentence s,
                  Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<Func<TResult>>>>>>> logic)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical { return false; }
        static bool Match<T1, T2, T3, T4, T5, T6, TResult>(this Sentence s,
               Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<TResult>>>>>>> logic)
               where T1 : class, ILexical
               where T2 : class, ILexical
               where T3 : class, ILexical
               where T4 : class, ILexical
               where T5 : class, ILexical
               where T6 : class, ILexical { return false; }
        static bool Match<T1, T2, T3, T4, T5, T6, T7, TResult>(this Sentence s,
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<TResult>>>>>>>> logic)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical { return false; }
        static bool Match<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Sentence s,
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TResult>>>>>>>> logic)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical { return false; }
        static TResult RecursiveMatch<TSource, TResult>(this IEnumerable<TSource> list, Func<TSource, TResult> processHead, Func<TResult, TResult, TResult> accumulator) {
            return list.Any() ? accumulator(processHead(list.First()), list.Skip(1).RecursiveMatch(processHead, accumulator)) : default(TResult);
        }

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
