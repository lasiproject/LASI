using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfTransitiveVerbExtensions
    {
        public  static IEnumerable<TransitiveVerb> WithObject(this IEnumerable<TransitiveVerb> transitiveVerbs, Func<IActionObject, bool> predicate) {
            return from V in transitiveVerbs
                                          where V.DirectObject != null && predicate(V.DirectObject)
                                          select V;
        }
    }
}
