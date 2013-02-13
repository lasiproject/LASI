using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfTransitiveVerbExtensions
    {
        public static IEnumerable<TransitiveVerb> WithDirectObject(this IEnumerable<TransitiveVerb> transitiveVerbs, Func<Noun, bool> matchCondition) {
            return from V in transitiveVerbs
                   let vobject = V.DirectObject as Noun
                   where vobject != null && matchCondition(vobject)
                   select V;
        }
        public static IEnumerable<TransitiveVerb> WithDirectObject(this IEnumerable<TransitiveVerb> transitiveVerbs, Func<NounPhrase, bool> matchCondition) {
            return from V in transitiveVerbs
                   let vobject = V.DirectObject as NounPhrase
                   where vobject != null && matchCondition(vobject)
                   select V;
        }
        public static IEnumerable<TransitiveVerb> WithIndirectDirectObject(this IEnumerable<TransitiveVerb> transitiveVerbs, Func<Noun, bool> matchCondition) {
            return from V in transitiveVerbs
                   let vobject = V.IndirectObject as Noun
                   where vobject != null && matchCondition(vobject)
                   select V;
        }
        public static IEnumerable<TransitiveVerb> WithIndirectDirectObject(this IEnumerable<TransitiveVerb> transitiveVerbs, Func<NounPhrase, bool> matchCondition) {
            return from V in transitiveVerbs
                   let vobject = V.IndirectObject as NounPhrase
                   where vobject != null && matchCondition(vobject)
                   select V;
        }
    }
}
