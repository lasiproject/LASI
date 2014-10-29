using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Analysis.Melding;
using LASI.Core.Heuristics;
using LASI.Utilities;

namespace LASI.Core.Analysis
{
    public static class MeldingExtensions
    {
        public static IEnumerable<IEntity> Meld(this IEnumerable<IEntity> entities) {
            var entityComparer = CustomComparer.Create<IEntity>((x, y) => x.IsSimilarTo(y));
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
