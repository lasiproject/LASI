using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Analysis.Melding;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Provides extension methods for Melding sequences of entities.
    /// </summary>
    public static class MeldingExtensions
    {
        public static IEnumerable<IEntity> Meld<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : class, IEntity => entities.Meld((e1, e2) => e1.IsSimilarTo(e2));

        public static IEnumerable<IEntity> Meld<TEntity>(this IEnumerable<TEntity> entities, Func<TEntity, TEntity, bool> meldWhen)
            where TEntity : class, IEntity => MeldImplementation(entities, ComparerFactory.Create(meldWhen));

        private static IEnumerable<IEntity> MeldImplementation<TEntity>(IEnumerable<TEntity> entities, IEqualityComparer<TEntity> comparer)
            where TEntity : class, IEntity
        {
            var groupsToMeld = entities.GroupJoin(
                inner: entities, // Group join the set entities to itself using the given comparer.
                comparer: comparer,
                outerKeySelector: e => e,
                innerKeySelector: e => e,
                resultSelector: (entity, alikeEntities) => alikeEntities
            );
            var result = from toMeld in groupsToMeld
                         let avatar = toMeld
                                .GroupBy(entity => entity.Text)
                                .MaxBy(g => g.Count())
                                .First()
                         select new LiftedEntity(avatar: avatar, represented: toMeld);
            return result;
        }
    }
}