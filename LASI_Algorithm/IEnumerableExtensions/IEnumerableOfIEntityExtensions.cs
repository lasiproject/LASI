using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides facilities to aid in the querying of IEnumerableCollections of IEntities.
    /// </summary>
    public static class IEnumerableOfIEntityExtensions
    {
        public static IEnumerable<IEntity> InSubjectRole(this IEnumerable<IEntity> entities) {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }
        public static IEnumerable<IEntity> InSubjectRole(
            this IEnumerable<IEntity> entities,
            Func<ITransitiveVerbial, bool> condition
            ) {
            return from e in entities.InSubjectRole()
                   where condition(e.SubjectOf)
                   select e;
        }
        public static IEnumerable<IEntity> InDirectObjectRole(this IEnumerable<IEntity> entities) {
            return from e in entities
                   where e.DirectObjectOf != null
                   select e;
        }
        public static IEnumerable<IEntity> InDirectObjectRole(
            this IEnumerable<IEntity> entities,
            Func<ITransitiveVerbial, bool> condition
            ) {
            return from e in entities.InDirectObjectRole()
                   where condition(e.DirectObjectOf)
                   select e;
        }
        public static IEnumerable<IEntity> InIndirectObjectRole(this IEnumerable<IEntity> entities) {
            return from e in entities
                   where e.IndirectObjectOf != null
                   select e;
        }
        public static IEnumerable<IEntity> InIndirectObjectRole(
            this IEnumerable<IEntity> entities,
            Func<ITransitiveVerbial, bool> condition
            ) {
            return from e in entities.InIndirectObjectRole()
                   where condition(e.IndirectObjectOf)
                   select e;
        }
    }
}
