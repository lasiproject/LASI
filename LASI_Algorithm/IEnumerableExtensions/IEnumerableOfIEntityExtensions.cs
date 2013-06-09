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
        /// Returns all NounPhrase instances in the source sequence which have been bound as the Subject of an IVerbal construct.
        /// </summary>
        /// <param name="entities">The sequence of NounPhrases instances to filter.</param>
        /// <returns>All NounPhrase instances in the source sequence which have been bound as the Subject of an IVerbal construct.</returns>
        public static IEnumerable<NounPhrase> InSubjectRole(this IEnumerable<NounPhrase> entities)
        {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all Noun instances in the source sequence which have been bound as the Subject of an IVerbal construct.
        /// </summary>
        /// <param name="entities">The sequence of Noun instances to filter.</param>
        /// <returns>All Noun instances in the source sequence which have been bound as the Subject of an IVerbal construct.</returns>
        public static IEnumerable<Noun> InSubjectRole(this IEnumerable<Noun> entities)
        {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.
        /// </summary>
        /// <param name="entities">The sequence of Noun instances to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.</returns>
        public static IEnumerable<IEntity> InSubjectRole(this IEnumerable<IEntity> entities)
        {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all entities in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="verbalSelector">The function which examines the SubjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.
        public static IEnumerable<IEntity> InSubjectRole(
            this IEnumerable<IEntity> entities,
            Func<IVerbal, bool> condition
            )
        {
            return from e in entities.InSubjectRole()
                   where condition(e.SubjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.
        /// </summary>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.</returns>
        public static IEnumerable<IEntity> InDirectObjectRole(this IEnumerable<IEntity> entities)
        {
            return from e in entities
                   where e.DirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="verbalSelector">The function which examines the DirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        public static IEnumerable<IEntity> InDirectObjectRole(
            this IEnumerable<IEntity> entities,
            Func<IVerbal, bool> condition
            )
        {
            return from e in entities.InDirectObjectRole()
                   where condition(e.DirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.
        /// </summary>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.</returns>
        public static IEnumerable<IEntity> InIndirectObjectRole(this IEnumerable<IEntity> entities)
        {
            return from e in entities
                   where e.IndirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="verbalSelector">The function which examines the IndirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        public static IEnumerable<IEntity> InIndirectObjectRole(
            this IEnumerable<IEntity> entities,
            Func<IVerbal, bool> verbalSelector
            )
        {
            return from e in entities.InIndirectObjectRole()
                   where verbalSelector(e.IndirectObjectOf)
                   select e;
        }
    }
}
