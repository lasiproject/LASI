using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.IEnumerableExtensions
{
    static class IEnumerableOfParagraphExtensions
    {
        public static IEnumerable<Phrase> GetPhrases(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   from r in s.Phrases
                   select r;
        }
    }
    static class IEnumerableOfParagraphExtensions
    {
        public static IEnumerable<Sentence> GetSentences(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   select s;
        }
    }
    static class IEnumerableOfParagraphExtensions
    {
        public static IEnumerable<Word> GetWords(this IEnumerable<Paragraph> paragraphs) {
            return from p in paragraphs
                   from s in p.Sentences
                   from w in s.Words
                   select w;
        }
    }
}
