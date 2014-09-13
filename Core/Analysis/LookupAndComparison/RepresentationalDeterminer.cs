using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
using LASI.Utilities;
using LASI.Core.PatternMatching;

namespace LASI.Core.Heuristics
{
    static class CommonLexicalRepresentationChooser
    {
        public static KeyedByTypeCollection<IEnumerable<ILexical>> ToCommonRepresentation(this IEnumerable<ILexical> source) {

            var result = new KeyedByTypeCollection<IEnumerable<ILexical>> {
                source.AggregatedOnType<IEntity>((x, y) => x.IsSimilarTo(y)),
                source.AggregatedOnType<IVerbal>((x, y) => x.IsSimilarTo(y)),
                source.AggregatedOnType<IDescriptor>((x, y) => x.IsSimilarTo(y)),
                source.AggregatedOnType<IAdverbial>((x, y) => x.IsSimilarTo(y))
            };
            return result;
        }

        private static IEnumerable<TResult> AggregatedOnType<TResult>(this IEnumerable<ILexical> source, Func<TResult, TResult, SimilarityResult> correlator)
            where TResult : class, ILexical {
            var result = from outer in source.OfType<TResult>()
                         let compareToOuter = correlator.Apply(outer)
                         from inner in source.OfType<TResult>()
                         let simlarityRatio = compareToOuter(inner)
                         group inner by new
                         {
                             outer,
                             simlarityRatio
                         } into g
                         let count = g.Count()
                         orderby count, g.Key.simlarityRatio descending
                         select g.Key.outer;
            return result;
        }
    }
}