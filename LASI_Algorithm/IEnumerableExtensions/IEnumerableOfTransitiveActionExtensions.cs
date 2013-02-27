using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfTransitiveActionExtensions
    {
        public static IEnumerable<ITransitiveAction> WithDirectObject(
            this IEnumerable<ITransitiveAction> transitiveVerbPhrases) {
            return from TA in transitiveVerbPhrases
                   where TA.DirectObject != null
                   select TA;
        }
        /// <summary>
        /// Filters a collection of Transitive Actions, returning those whose direct objects match the provided object testing function
        /// </summary>
        /// <param name="transitives">A collection of elements which implements the ITRansitiveAction interface.</param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IEnumerable<ITransitiveAction> WithDirectObject(
           this IEnumerable<ITransitiveAction> transitives,
           Func<IEntity, bool> condition
            ) {
            return from TA in transitives.WithDirectObject()
                   where condition(TA.BoundSubject)
                   select TA;
        }
        public static IEnumerable<ITransitiveAction> WithIndirectObject(
           this IEnumerable<ITransitiveAction> transitives) {
            return from TA in transitives
                   where TA.IndirectObject != null
                   select TA;
        }
        public static IEnumerable<ITransitiveAction> WithIndirectObject(
            this IEnumerable<ITransitiveAction> transitives,
            Func<IEntity, bool> condition
            ) {
            return from TA in transitives.WithIndirectObject()
                   where condition(TA.IndirectObject)
                   select TA;
        }
    }
}
