using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the IEntity interface.
    /// </summary>
    /// <see cref="IEntity"/>
    public static class IEnumerableOfIEntityExtensions
    {
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of Noun instances to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.</returns>
        public static IEnumerable<T> InSubjectRole<T>(this IEnumerable<T> entities) where T : IEntity {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all describables in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the SubjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<T> InSubjectRole<T>(this IEnumerable<T> entities, Func<IVerbal, bool> condition) where T : IEntity {
            return from e in entities.InSubjectRole()
                   where condition(e.SubjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.</returns>
        public static IEnumerable<T> InDirectObjectRole<T>(this IEnumerable<T> entities) where T : IEntity {
            return from e in entities
                   where e.DirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the DirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<T> InDirectObjectRole<T>(this IEnumerable<T> entities, Func<IVerbal, bool> condition) where T : IEntity {
            return from e in entities.InDirectObjectRole()
                   where condition(e.DirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.</returns>
        public static IEnumerable<T> InIndirectObjectRole<T>(this IEnumerable<T> entities) where T : IEntity {
            return from e in entities
                   where e.IndirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the IndirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<T> InIndirectObjectRole<T>(this IEnumerable<T> entities, Func<IVerbal, bool> condition) where T : IEntity {
            return from e in entities.InIndirectObjectRole()
                   where condition(e.IndirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of an IVerbal construct.</returns>
        public static IEnumerable<T> InSubjectOrObjectRole<T>(this IEnumerable<T> entities) where T : IEntity {
            return from e in entities
                   let verbal = e.SubjectOf ?? e.DirectObjectOf ?? e.IndirectObjectOf
                   where verbal != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the IVerbal bound to each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<T> InSubjectOrObjectRole<T>(this IEnumerable<T> entities, Func<IVerbal, bool> condition) where T : IEntity {
            return from e in entities
                   where e.SubjectOf != null && condition(e.SubjectOf)
                   || e.DirectObjectOf != null && condition(e.DirectObjectOf)
                   || e.IndirectObjectOf != null && condition(e.IndirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IDescribable Constructs in the given sequence which are bound to an IDescriptor that matches the given descriptorMatcher predicate function.
        /// </summary>
        /// <typeparam name="T">Any Type which implemenets the IDescribable interface.</typeparam>
        /// <param name="describables">The sequence of IDescribables to filter.</param>
        /// <param name="condition">The function which examines the descriptors bound to each element in the sequence.</param>
        /// <returns>All IDescribable Constructs in the given sequence which are bound to an IDescriptor that matches the given descriptorMatcher predicate function.</returns>
        public static IEnumerable<T> HavingDescriptor<T>(this IEnumerable<T> describables, Func<IDescriptor, bool> condition) where T : IDescribable {
            return from describable in describables
                   where (from descriptor in describable.Descriptors
                          where condition(descriptor)
                          select descriptor).Any()
                   select describable;
        }
    }
}
