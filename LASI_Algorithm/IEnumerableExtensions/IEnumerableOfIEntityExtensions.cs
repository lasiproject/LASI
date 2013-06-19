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



        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.
        /// </summary>
        /// <typeparam name="T">Any Noun which implemenets the IEntity interface.</typeparam>
        /// <param name="describables">The sequence of Noun instances to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.</returns>
        public static IEnumerable<T> InSubjectRole<T>(this IEnumerable<T> entities) where T : IEntity {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all describables in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="T">Any Noun which implemenets the IEntity interface.</typeparam>
        /// <param name="describables">The sequence of IEntity constructs to filter.</param>
        /// <param name="verbalSelector">The function which examines the SubjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.
        public static IEnumerable<T> InSubjectRole<T>(this IEnumerable<T> entities, Func<IVerbal, bool> condition) where T : IEntity {
            return from e in entities.InSubjectRole()
                   where condition(e.SubjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="T">Any Noun which implemenets the IEntity interface.</typeparam>
        /// <param name="describables">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.</returns>
        public static IEnumerable<T> InDirectObjectRole<T>(this IEnumerable<T> entities) where T : IEntity {
            return from e in entities
                   where e.DirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="T">Any Noun which implemenets the IEntity interface.</typeparam>
        /// <param name="describables">The sequence of IEntity constructs to filter.</param>
        /// <param name="verbalSelector">The function which examines the DirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        public static IEnumerable<T> InDirectObjectRole<T>(this IEnumerable<T> entities, Func<IVerbal, bool> condition) where T : IEntity {
            return from e in entities.InDirectObjectRole()
                   where condition(e.DirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="T">Any Noun which implemenets the IEntity interface.</typeparam>
        /// <param name="describables">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.</returns>
        public static IEnumerable<T> InIndirectObjectRole<T>(this IEnumerable<T> entities) where T : IEntity {
            return from e in entities
                   where e.IndirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="T">Any Noun which implemenets the IEntity interface.</typeparam>
        /// <param name="describables">The sequence of IEntity constructs to filter.</param>
        /// <param name="verbalSelector">The function which examines the IndirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        public static IEnumerable<T> InIndirectObjectRole<T>(this IEnumerable<T> entities, Func<IVerbal, bool> verbalSelector) where T : IEntity {
            return from e in entities.InIndirectObjectRole()
                   where verbalSelector(e.IndirectObjectOf)
                   select e;
        }

        public static IEnumerable<T> HavingDescriptor<T>(this IEnumerable<T> describables, Func<IDescriptor, bool> descriptorSelector) where T : IDescribable {
            return from describable in describables
                   where (from descriptor in describable.Descriptors
                          where descriptorSelector(descriptor)
                          select descriptor).Any()
                   select describable;
        }

    }
}
