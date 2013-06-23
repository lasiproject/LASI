using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.IEnumerableExtensions
{
    static class IEnumerableOfRecursiveIEnumerableILexicalsExtenstions
    {
        public static IEnumerable<T> GetRecursiveEnumerator<T>(this IEnumerable<T> source) where T : ILexical {
            foreach (var lexical in source) {
                yield return lexical;
                var innerEnumerable = lexical as IEnumerable<T>;
                if (innerEnumerable == null)
                    continue;
                foreach (var lex in GetRecursiveEnumerator(innerEnumerable))
                    yield return lex;
            }
        }
    }
}
