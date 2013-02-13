using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.IEnumerableExtensions
{
    public static class IEnumerableOfTransitiveVerbPhraseExtensions
    {
        public static IEnumerable<TransitiveVerbPhrase> WithDirectObject(
            this IEnumerable<TransitiveVerbPhrase> transitiveVerbPhrases, 
            Func<NounPhrase, bool> matchCondition) {
            return from TVP in transitiveVerbPhrases
                   let vobject = TVP.DirectObject as NounPhrase
                   where vobject != null && matchCondition(vobject)
                   select TVP;
        }
        public static IEnumerable<TransitiveVerbPhrase> WithDirectObject(
           this IEnumerable<TransitiveVerbPhrase> transitiveVerbPhrases,
           Func<Noun, bool> matchCondition) {
            return from TVP in transitiveVerbPhrases
                   let vobject = TVP.DirectObject as Noun
                   where vobject != null && matchCondition(vobject)
                   select TVP;
        }
        public static IEnumerable<TransitiveVerbPhrase> WithIndirectDirectObject(
            this IEnumerable<TransitiveVerbPhrase> transitiveVerbPhrases,
            Func<NounPhrase, bool> matchCondition) {
            return from TVP in transitiveVerbPhrases
                   let vobject = TVP.IndirectObject as NounPhrase
                   where vobject != null && matchCondition(vobject)
                   select TVP;
        }
        public static IEnumerable<TransitiveVerbPhrase> WithIndirectDirectObject(
           this IEnumerable<TransitiveVerbPhrase> transitiveVerbPhrases,
           Func<Noun, bool> matchCondition) {
            return from TVP in transitiveVerbPhrases
                   let vobject = TVP.IndirectObject as Noun
                   where vobject != null && matchCondition(vobject)
                   select TVP;
        }
    }
}
