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
        public static IEnumerable<TResultantLexical> LiftReduce<TLexical, TResultantLexical>(this IEnumerable<TLexical> source)
            where TLexical : ILexical where TResultantLexical : Lifted<TLexical> {
            var lift = new Func<TLexical, TLexical, TResultantLexical>((x, y) => default(TResultantLexical));
            return source
                .PairWise()
                .Aggregate(
                    seed: Enumerable.Empty<TResultantLexical>(),
                    func: (x, y) => x.Append(lift(y.Item1, y.Item2))
                );
        }
        public static IEnumerable<IEntity> Meld(this IEnumerable<IEntity> entities) {
            var comparer = CustomComparer.Create<IEntity>((x, y) => x.IsSimilarTo(y));
            Func<IEnumerable<IEntity>, Func<IEntity, IVerbal>, IEnumerable<IVerbal>> verbalsSelector = (seq, sel) => seq.Select(sel).ToImmutableList();
            var entityGroups = entities.GroupJoin(inner: entities,
                            outerKeySelector: outer => outer,
                            innerKeySelector: inner => inner,
                            comparer: comparer,
                            resultSelector: (outer, correlates) => correlates
                        );
            var result = from entityGroup in entityGroups
                         from entity in entityGroup
                         group entity by entity.Text into byText
                         orderby byText.Count() descending
                         select new LiftedEntity(byText.First(), byText.ToImmutableList());
            return result;
        }
    }
}
