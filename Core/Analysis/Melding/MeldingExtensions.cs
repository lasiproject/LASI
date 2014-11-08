using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Analysis.Melding;
using LASI.Core.Heuristics;
using LASI.Utilities;

namespace LASI.Core
{
    public static class MeldingExtensions
    {
        public static IEnumerable<IEntity> Meld<TEntity>(this IEnumerable<TEntity> entities) where TEntity : class, IEntity {
            return MeldImpl(entities, CustomComparer.Create((TEntity x, TEntity y) => x.IsSimilarTo(y)));
        }
        public static IEnumerable<IEntity> Meld<TEntity>(this IEnumerable<TEntity> entities, Func<TEntity, TEntity, bool> meldOn) where TEntity : class, IEntity {
            return MeldImpl(entities, CustomComparer.Create(meldOn));
        }
        private static IEnumerable<IEntity> MeldImpl<TEntity>(IEnumerable<TEntity> entities, IEqualityComparer<TEntity> entityComparer)
            where TEntity : class, IEntity {
            var groupsToMeld = entities.GroupJoin(
                            inner: entities,
                            outerKeySelector: outer => outer,
                            innerKeySelector: inner => inner,
                            comparer: entityComparer,
                            resultSelector: (outer, correlates) => correlates
                        );
            var result = from toMeld in groupsToMeld
                         let avatar = toMeld
                                .GroupBy(entity => entity.Text)
                                .MaxBy(g => g.Count())
                                .First()
                         select new LiftedEntity(representative: avatar, represented: toMeld);
            return result;
        }
    }
}
