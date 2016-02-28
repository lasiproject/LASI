using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Configuration;
using LASI.Utilities.Specialized.Enhanced.Universal;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the IEntity interface.
    /// </summary>
    /// <seealso cref="IEntity"/>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}"/>
    /// <seealso cref="System.Linq.Enumerable"/>
    public static partial class LexicalEnumerable
    {
        /// <summary>
        /// Creates an <see cref="IAggregateEntity"/> from the collection of entities.
        /// </summary>
        /// <param name="entities">The collection of entities to aggregate.</param>
        /// <returns>An <see cref="IAggregateEntity"/> created from the collection of entities.</returns>
        public static IAggregateEntity ToAggregate(this IEnumerable<IEntity> entities) => new AggregateEntity(entities);

        #region Sequential Implementations

        /// <summary>
        /// Returns all Entities in the given sequence which are bound to an IDescriptor.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IDescribable interface.</typeparam>
        /// <param name="entities">The sequence of IDescribables to filter.</param>
        /// <returns>All Entities in the given sequence which are bound to an IDescriptor.</returns>
        public static IEnumerable<TEntity> HavingDescriptor<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : IEntity => from e in entities
                                       where e.Descriptors.Any()
                                       select e;

        /// <summary>
        /// Returns all IDescribable Constructs in the given sequence which are bound to an
        /// IDescriptor that matches the given descriptorMatcher predicate function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IDescribable interface.</typeparam>
        /// <param name="entities">The sequence of IDescribables to filter.</param>
        /// <param name="predicate">
        /// The function which examines the descriptors bound to each element in the sequence.
        /// </param>
        /// <returns>
        /// All IDescribable Constructs in the given sequence which are bound to an IDescriptor that
        /// matches the given descriptorMatcher predicate function.
        /// </returns>
        public static IEnumerable<TEntity> HavingDescriptor<TEntity>(this IEnumerable<TEntity> entities, Func<IDescriptor, bool> predicate)
            where TEntity : IEntity => from e in entities
                                       where e.Descriptors.Any(predicate)
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Direct Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Direct Object
        /// of an IVerbal construct.
        /// </returns>
        public static IEnumerable<TEntity> InDirectObjectRole<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : IEntity => from e in entities
                                       where e.DirectObjectOf == e.DirectObjectOf.Value
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the DirectObjectOf property of each entity to determine if
        /// it should be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Direct Object
        /// of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </returns>
        public static IEnumerable<TEntity> InDirectObjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity => from e in entities.InDirectObjectRole()
                                       where e.DirectObjectOf.Match(predicate)
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Indirect
        /// Object of an IVerbal construct.
        /// </returns>
        public static IEnumerable<TEntity> InIndirectObjectRole<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : IEntity => from e in entities
                                       where e.IndirectObjectOf != null
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Indirect Object of any IVerbal construct which conforms the logic of the IVerbal
        /// selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the IndirectObjectOf property of each entity to determine if
        /// it should be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Indirect
        /// Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </returns>
        public static IEnumerable<TEntity> InIndirectObjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity => from e in entities.InIndirectObjectRole()
                                       where predicate(e.IndirectObjectOf)
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Direct OR Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Direct OR
        /// Indirect Object of an IVerbal construct.
        /// </returns>
        public static IEnumerable<TEntity> InObjectRole<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : IEntity => entities.InDirectObjectRole().Union(entities.InIndirectObjectRole());

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Direct OR Indirect Object of any IVerbal construct which conforms the logic of the
        /// IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the IndirectObjectOf property of each entity to determine if
        /// it should be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Direct OR
        /// Indirect Object of any IVerbal construct which conforms the logic of the IVerbal
        /// selector function.
        /// </returns>
        public static IEnumerable<TEntity> InObjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity => entities.InDirectObjectRole(predicate).Union(entities.InIndirectObjectRole(predicate));

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Subject, Direct Object, or Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Subject,
        /// Direct Object, or Indirect Object of an IVerbal construct.
        /// </returns>
        public static IEnumerable<TEntity> InSubjectOrObjectRole<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : IEntity => entities.InSubjectRole().Union(entities.InObjectRole());

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Subject, Direct Object, or Indirect Object of any IVerbal construct which conforms the
        /// logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the IVerbal bound to each entity to determine if it should
        /// be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Subject,
        /// Direct Object, or Indirect Object of any IVerbal construct which conforms the logic of
        /// the IVerbal selector function.
        /// </returns>
        public static IEnumerable<TEntity> InSubjectOrObjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity => entities.InSubjectRole(predicate).Union(entities.InObjectRole(predicate));

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Subject of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of Noun instances to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Subject of an
        /// IVerbal construct.
        /// </returns>
        public static IEnumerable<TEntity> InSubjectRole<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : IEntity => from e in entities
                                       where e.SubjectOf != null
                                       select e;

        /// <summary>
        /// Returns all describables in the source sequence which have been bound as the Subject of
        /// any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the SubjectOf property of each entity to determine if it
        /// should be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Subject of
        /// any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </returns>
        public static IEnumerable<TEntity> InSubjectRole<TEntity>(this IEnumerable<TEntity> entities, Func<IVerbal, bool> predicate) where TEntity : IEntity =>
            entities.InSubjectRole().Where(entity => predicate(entity.SubjectOf));

        /// <summary>
        /// Returns all the entities in the sequence such that, if they are referencers, their
        /// references will be returned in their place.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of entities to lift.</param>
        /// <returns>
        /// All entities in the sequence such that, if they are referencers, their references will
        /// be returned in their place.
        /// </returns>
        public static IEnumerable<IEntity> ResolvingReferences<TEntity>(this IEnumerable<TEntity> entities) where TEntity : class, IEntity =>
            entities.Select(entity =>
               entity.Match()
                .When((IReferencer r) => r.RefersTo.Any())
                .Then((IReferencer r) => r.RefersTo)
                .Match((IEntity e) => e)
            .Result());

        #endregion Sequential Implementations

        #region Parallel Implementations

        /// <summary>
        /// Returns all Entities in the given sequence which are bound to an IDescriptor.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IDescribable interface.</typeparam>
        /// <param name="entities">The sequence of IDescribables to filter.</param>
        /// <returns>All Entities in the given sequence which are bound to an IDescriptor.</returns>
        public static ParallelQuery<TEntity> HavingDescriptor<TEntity>(this ParallelQuery<TEntity> entities)
            where TEntity : IEntity => from e in entities
                                       where e.Descriptors.Any()
                                       select e;

        /// <summary>
        /// Returns all IDescribable Constructs in the given sequence which are bound to an
        /// IDescriptor that matches the given descriptorMatcher predicate function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IDescribable interface.</typeparam>
        /// <param name="entities">The sequence of IDescribables to filter.</param>
        /// <param name="predicate">
        /// The function which examines the descriptors bound to each element in the sequence.
        /// </param>
        /// <returns>
        /// All IDescribable Constructs in the given sequence which are bound to an IDescriptor that
        /// matches the given descriptorMatcher predicate function.
        /// </returns>
        public static ParallelQuery<TEntity> HavingDescriptor<TEntity>(this ParallelQuery<TEntity> entities, Func<IDescriptor, bool> predicate)
            where TEntity : IEntity => from e in entities
                                       where e.Descriptors.Any(predicate)
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Direct Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Direct Object
        /// of an IVerbal construct.
        /// </returns>
        public static ParallelQuery<TEntity> InDirectObjectRole<TEntity>(this ParallelQuery<TEntity> entities)
            where TEntity : IEntity => from e in entities
                                       where e.DirectObjectOf.IsSome
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Direct Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the DirectObjectOf property of each entity to determine if
        /// it should be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Direct Object
        /// of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </returns>
        public static ParallelQuery<TEntity> InDirectObjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity => from e in entities.InDirectObjectRole()
                                       where e.DirectObjectOf.Match(predicate)
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Indirect
        /// Object of an IVerbal construct.
        /// </returns>
        public static ParallelQuery<TEntity> InIndirectObjectRole<TEntity>(this ParallelQuery<TEntity> entities)
            where TEntity : IEntity => from e in entities
                                       where e.IndirectObjectOf != null
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Indirect Object of any IVerbal construct which conforms the logic of the IVerbal
        /// selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the IndirectObjectOf property of each entity to determine if
        /// it should be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Indirect
        /// Object of any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </returns>
        public static ParallelQuery<TEntity> InIndirectObjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity => from e in entities.InIndirectObjectRole()
                                       where predicate(e.IndirectObjectOf)
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Direct OR Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Direct OR
        /// Indirect Object of an IVerbal construct.
        /// </returns>
        public static ParallelQuery<TEntity> InObjectRole<TEntity>(this ParallelQuery<TEntity> entities)
            where TEntity : IEntity
        {
            var direct = entities.InDirectObjectRole().AsSequential();
            var indirect = entities.InIndirectObjectRole().AsSequential();
            return direct.Union(indirect).AsParallel().WithDegreeOfParallelism(Concurrency.Max);
        }

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Direct OR Indirect Object of any IVerbal construct which conforms the logic of the
        /// IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the IndirectObjectOf property of each entity to determine if
        /// it should be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Direct OR
        /// Indirect Object of any IVerbal construct which conforms the logic of the IVerbal
        /// selector function.
        /// </returns>
        public static ParallelQuery<TEntity> InObjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity
        {
            var direct = entities.InDirectObjectRole(predicate).AsSequential();
            var indirect = entities.InIndirectObjectRole(predicate).AsSequential();
            return direct.Union(indirect).AsParallel().WithDegreeOfParallelism(Concurrency.Max);
        }

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Subject, Direct Object, or Indirect Object of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Subject,
        /// Direct Object, or Indirect Object of an IVerbal construct.
        /// </returns>
        public static ParallelQuery<TEntity> InSubjectOrObjectRole<TEntity>(this ParallelQuery<TEntity> entities)
            where TEntity : IEntity => from entity in entities
                                       let verbal = entity.SubjectOf ?? entity.DirectObjectOf.Match((IVerbal v) => v) ?? entity.IndirectObjectOf
                                       where verbal != null
                                       select entity;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Subject, Direct Object, or Indirect Object of any IVerbal construct which conforms the
        /// logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the IVerbal bound to each entity to determine if it should
        /// be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Subject,
        /// Direct Object, or Indirect Object of any IVerbal construct which conforms the logic of
        /// the IVerbal selector function.
        /// </returns>
        public static ParallelQuery<TEntity> InSubjectOrObjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity => from e in entities
                                       where e.SubjectOf != null && predicate(e.SubjectOf)
                                       || e.DirectObjectOf.Match(predicate)
                                       || e.IndirectObjectOf != null && predicate(e.IndirectObjectOf)
                                       select e;

        /// <summary>
        /// Returns all IEntity constructs in the source sequence which have been bound as the
        /// Subject of an IVerbal construct.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of Noun instances to filter.</param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Subject of an
        /// IVerbal construct.
        /// </returns>
        public static ParallelQuery<TEntity> InSubjectRole<TEntity>(this ParallelQuery<TEntity> entities)
            where TEntity : IEntity => from e in entities
                                       where e.SubjectOf != null
                                       select e;


        /// <summary>
        /// Returns all describables in the source sequence which have been bound as the Subject of
        /// any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of IEntity constructs to filter.</param>
        /// <param name="predicate">
        /// The function which examines the SubjectOf property of each entity to determine if it
        /// should be included in the resulting sequence.
        /// </param>
        /// <returns>
        /// All IEntity constructs in the source sequence which have been bound as the Subject of
        /// any IVerbal construct which conforms the logic of the IVerbal selector function.
        /// </returns>
        public static ParallelQuery<TEntity> InSubjectRole<TEntity>(this ParallelQuery<TEntity> entities, Func<IVerbal, bool> predicate)
            where TEntity : IEntity => entities.InSubjectRole().Where(entity => predicate(entity.SubjectOf));

        /// <summary>
        /// Returns all the entities in the sequence such that, if they are referencers, their
        /// references will be returned in their place.
        /// </summary>
        /// <typeparam name="TEntity">Any Type which implements the IEntity interface.</typeparam>
        /// <param name="entities">The sequence of entities to lift.</param>
        /// <returns>
        /// All entities in the sequence such that, if they are referencers, their references will
        /// be returned in their place.
        /// </returns>
        public static ParallelQuery<IEntity> ResolvingReferences<TEntity>(this ParallelQuery<TEntity> entities)
            where TEntity : class, IEntity => entities
                .Select(x => x.Match()
                    .When((IReferencer r) => r.RefersTo.Any())
                    .Then((IReferencer r) => r.RefersTo)
                    .Match((IEntity e) => e).Result()
                );

        #endregion Parallel Implementations
    }
}