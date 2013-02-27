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
            this IEnumerable<ITransitiveAction> transitiveVerbPhrases,
            Func<NounPhrase, bool> matchCondition
            ) {
            return from TVP in transitiveVerbPhrases
                   let vobject = TVP.DirectObject as NounPhrase
                   where vobject != null && matchCondition(vobject)
                   select TVP;
        }
        /// <summary>
        /// Filters a collection of Transitive Actions, returning those whose direct objects match the provided object testing function
        /// </summary>
        /// <param name="transitiveVerbPhrases">A collection of elements which implements the ITRansitiveAction interface.</param>
        /// <param name="matchCondition"></param>
        /// <returns></returns>
        public static IEnumerable<ITransitiveAction> WithDirectObject(
           this IEnumerable<ITransitiveAction> transitiveVerbPhrases,
           Func<Noun, bool> matchCondition
            ) {
            return from TVP in transitiveVerbPhrases
                   let vobject = TVP.DirectObject as Noun
                   where vobject != null && matchCondition(vobject)
                   select TVP;
        }
        public static IEnumerable<ITransitiveAction> WithIndirectDirectObject(
            this IEnumerable<ITransitiveAction> transitiveVerbPhrases,
            Func<NounPhrase, bool> matchCondition
            ) {
            return from TVP in transitiveVerbPhrases
                   let vobject = TVP.IndirectObject as NounPhrase
                   where vobject != null && matchCondition(vobject)
                   select TVP;
        }
        public static IEnumerable<ITransitiveAction> WithIndirectDirectObject(
           this IEnumerable<ITransitiveAction> transitiveVerbPhrases,
           Func<Noun, bool> matchCondition
            ) {
            return from TVP in transitiveVerbPhrases
                   let vobject = TVP.IndirectObject as Noun
                   where vobject != null && matchCondition(vobject)
                   select TVP;
        }
    }
}
