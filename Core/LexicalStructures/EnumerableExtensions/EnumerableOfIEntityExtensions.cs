using LASI.Core.Interop;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the IEntity interface.
    /// </summary>
    /// <see cref="IEntity"/>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}"/>
    /// <seealso cref="System.Linq.Enumerable"/>
    public static partial class LexicalEnumerable
    {
        #region Sequential Implementations
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of Noun instances to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.</returns>
        public static IEnumerable<TEntity> InSubjectRole<TEntity>(this IEnumerable<TEntity> entities) where TEntity : IEntity {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all describables in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the SubjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<TEntity> InSubjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return from e in entities.InSubjectRole()
                   where condition(e.SubjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.</returns>
        public static IEnumerable<TEntity> InDirectObjectRole<TEntity>(this IEnumerable<TEntity> entities) where TEntity : IEntity {
            return from e in entities
                   where e.DirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the DirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<TEntity> InDirectObjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return from e in entities.InDirectObjectRole()
                   where condition(e.DirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.</returns>
        public static IEnumerable<TEntity> InIndirectObjectRole<TEntity>(this IEnumerable<TEntity> entities) where TEntity : IEntity {
            return from e in entities
                   where e.IndirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the IndirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<TEntity> InIndirectObjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return from e in entities.InIndirectObjectRole()
                   where condition(e.IndirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct OR Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct OR Indirect Object of an IVerbal construct.</returns>
        public static IEnumerable<TEntity> InObjectRole<TEntity>(this IEnumerable<TEntity> entities) where TEntity : IEntity {
            return entities.InDirectObjectRole().Union(entities.InIndirectObjectRole());
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct OR Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the IndirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct OR Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<TEntity> InObjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return entities.InDirectObjectRole(condition).Union(entities.InIndirectObjectRole(condition));
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of an IVerbal construct.</returns>
        public static IEnumerable<TEntity> InSubjectOrObjectRole<TEntity>(this IEnumerable<TEntity> entities) where TEntity : IEntity {
            return from e in entities
                   let verbal = e.SubjectOf ?? e.DirectObjectOf ?? e.IndirectObjectOf
                   where verbal != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the IVerbal bound to each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static IEnumerable<TEntity> InSubjectOrObjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return from e in entities
                   where e.SubjectOf != null && condition(e.SubjectOf)
                   || e.DirectObjectOf != null && condition(e.DirectObjectOf)
                   || e.IndirectObjectOf != null && condition(e.IndirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IDescribable Constructs in the given sequence which are bound to an IDescriptor that matches the given descriptorMatcher predicate function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IDescribable interface.</typeparam>
        /// <param name="entities">The sequence of IDescribables to filter.</param>
        /// <param name="condition">The function which examines the descriptors bound to each element in the sequence.</param>
        /// <returns>All IDescribable Constructs in the given sequence which are bound to an IDescriptor that matches the given descriptorMatcher predicate function.</returns>
        public static IEnumerable<TEntity> HavingDescriptor<TEntity>(this IEnumerable<TEntity> entities, Func<IDescriptor, bool> condition) where TEntity : IEntity {
            return entities.Where(d => d.Descriptors.Where(condition).Any());
        }
        /// <summary>
        /// Returns all the entities in the sequence such that, if they are referencers, their references will be returned in their place.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of entities to lift.</param>
        /// <returns>All entities in the sequence such that, if they are referencers, their references will be returned in their place.</returns>
        public static IEnumerable<IEntity> ResolveReferences<TEntity>(this IEnumerable<TEntity> entities) where TEntity : class, IEntity {
            return entities.SelectMany(e => e.Match().Yield<IEnumerable<IEntity>>()
            .With((IReferencer r) => r.RefersTo)
            .With((IEntity i) => new[] { i })
            .Result());
        }

        #endregion

        #region Parallel Implementations
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of Noun instances to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of an IVerbal construct.</returns>
        public static ParallelQuery<TEntity> InSubjectRole<TEntity>(this ParallelQuery<TEntity> entities) where TEntity : IEntity {
            return from e in entities
                   where e.SubjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all describables in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the SubjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static ParallelQuery<TEntity> InSubjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return from e in entities.InSubjectRole()
                   where condition(e.SubjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of an IVerbal construct.</returns>
        public static ParallelQuery<TEntity> InDirectObjectRole<TEntity>(this ParallelQuery<TEntity> entities) where TEntity : IEntity {
            return from e in entities
                   where e.DirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the DirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static ParallelQuery<TEntity> InDirectObjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return from e in entities.InDirectObjectRole()
                   where condition(e.DirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of an IVerbal construct.</returns>
        public static ParallelQuery<TEntity> InIndirectObjectRole<TEntity>(this ParallelQuery<TEntity> entities) where TEntity : IEntity {
            return from e in entities
                   where e.IndirectObjectOf != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the IndirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static ParallelQuery<TEntity> InIndirectObjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return from e in entities.InIndirectObjectRole()
                   where condition(e.IndirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct OR Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct OR Indirect Object of an IVerbal construct.</returns>
        public static ParallelQuery<TEntity> InObjectRole<TEntity>(this ParallelQuery<TEntity> entities) where TEntity : IEntity {
            return entities.InDirectObjectRole()
                .AsSequential()
                .Union(entities.InIndirectObjectRole()
                    .AsSequential()
                ).AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max);
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Direct OR Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the IndirectObjectOf property of each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Direct OR Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static ParallelQuery<TEntity> InObjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return entities.InDirectObjectRole(condition)
                .AsSequential()
                .Union(entities.InIndirectObjectRole(condition).AsSequential())
                .AsParallel()
                .WithDegreeOfParallelism(Concurrency.Max);
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of an IVerbal construct.</returns>
        public static ParallelQuery<TEntity> InSubjectOrObjectRole<TEntity>(this ParallelQuery<TEntity> entities) where TEntity : IEntity {
            return from e in entities
                   let verbal = e.SubjectOf ?? e.DirectObjectOf ?? e.IndirectObjectOf
                   where verbal != null
                   select e;
        }
        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="condition">The function which examines the IVerbal bound to each entity to determine if it should be included in the resulting sequence.</param>
        /// <returns>All IEntity constructs in the source sequence which have been bound as the Subject, Direct Object, or Indirect Object of any IVerbal construct which conforms the logic of the IVerbal selector function.</returns>
        public static ParallelQuery<TEntity> InSubjectOrObjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> condition) where TEntity : IEntity {
            return from e in entities
                   where e.SubjectOf != null && condition(e.SubjectOf)
                   || e.DirectObjectOf != null && condition(e.DirectObjectOf)
                   || e.IndirectObjectOf != null && condition(e.IndirectObjectOf)
                   select e;
        }
        /// <summary>
        /// Returns all IDescribable Constructs in the given sequence which are bound to an IDescriptor that matches the given descriptorMatcher predicate function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IDescribable interface.</typeparam>
        /// <param name="describables">The sequence of IDescribables to filter.</param>
        /// <param name="condition">The function which examines the descriptors bound to each element in the sequence.</param>
        /// <returns>All IDescribable Constructs in the given sequence which are bound to an IDescriptor that matches the given descriptorMatcher predicate function.</returns>
        public static ParallelQuery<TEntity> HavingDescriptor<TEntity>(this ParallelQuery<TEntity> describables, Func<IDescriptor, bool> condition) where TEntity : IEntity {
            return describables.Where(d => d.Descriptors.Where(condition).Any());
        }
        /// <summary>
        /// Returns all the entities in the sequence such that, if they are referencers, their references will be returned in their place.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of entities to lift.</param>
        /// <returns>All entities in the sequence such that, if they are referencers, their references will be returned in their place.</returns>
        public static ParallelQuery<IEntity> ResolveReferences<TEntity>(this ParallelQuery<TEntity> entities) where TEntity : class, IEntity {
            return entities.SelectMany(e => e.Match().Yield<IEnumerable<IEntity>>()
            .With((IReferencer r) => r.RefersTo)
            .With((IEntity i) => new[] { i })
            .Result());
        }
        #endregion

    }
}
