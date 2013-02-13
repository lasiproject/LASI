using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.IEnumerableExtensions
{
    public static class IEnumerableOfVerbPhraseExtensions
    {
        public static IEnumerable<TransitiveVerbPhrase> GetTransitiveVerbPhrases(this IEnumerable<VerbPhrase> verbPhrases) {
            return verbPhrases.OfType<TransitiveVerbPhrase>();
        }
        public static IEnumerable<VerbPhrase> WithSubject(this IEnumerable<VerbPhrase> verbPhrases, Func<NounPhrase, bool> matchCondition) {
            return from VP in verbPhrases
                   let subject = VP.BoundSubject as NounPhrase
                   where subject != null && matchCondition(subject)
                   select VP;
        }
        public static IEnumerable<VerbPhrase> WithSubject(this IEnumerable<VerbPhrase> verbPhrases, Func<Noun, bool> matchCondition) {
            return from VP in verbPhrases
                   let subject = VP.BoundSubject as Noun
                   where subject != null && matchCondition(subject)
                   select VP;
        }
    }
}
