using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class IEnumerableOfIEntityExtensions
    {
        public static IEnumerable<IEntity> AsSubject(this IEnumerable<IEntity> entities) {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }
        public static IEnumerable<IEntity> AsSubject(this IEnumerable<IEntity> entities, Func<ITransitiveVerbial, bool> condition) {
            return from e in entities.AsSubject()
                   where condition(e.SubjectOf)
                   select e;
        }
        public static IEnumerable<IEntity> AsDirectObject(this IEnumerable<IEntity> entities) {
            return from e in entities
                   where e.DirectObjectOf != null
                   select e;
        }
        public static IEnumerable<IEntity> AsDirectObject(this IEnumerable<IEntity> entities, Func<ITransitiveVerbial, bool> condition) {
            return from e in entities.AsDirectObject()
                   where condition(e.DirectObjectOf)
                   select e;
        }
        public static IEnumerable<IEntity> AsIndirectObject(this IEnumerable<IEntity> entities) {
            return from e in entities
                   where e.IndirectObjectOf != null
                   select e;
        }
        public static IEnumerable<IEntity> AsIndirectObject(this IEnumerable<IEntity> entities, Func<ITransitiveVerbial, bool> condition) {
            return from e in entities.AsIndirectObject()
                   where condition(e.IndirectObjectOf)
                   select e;
        }
    }
}
