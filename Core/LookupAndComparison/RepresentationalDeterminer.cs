using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
using LASI.Utilities;
using LASI.Core.Patternization;

namespace LASI.Core.Heuristics
{
    public static class CommonLexicalRepresentationChooser
    {
        public static KeyedByTypeCollection<IEnumerable<ILexical>> ToCommonRepresentation(this IEnumerable<ILexical> source) {

            return new KeyedByTypeCollection<IEnumerable<ILexical>> { source.AggregatedOnType<IEntity>((x, y) => x.IsSimilarTo(y)),
                source.AggregatedOnType<IVerbal>((x, y) => x.IsSimilarTo(y)),
                source.AggregatedOnType<IDescriptor>((x, y) => x.IsSimilarTo(y)),
                source.AggregatedOnType<IAdverbial>((x, y) => x.IsSimilarTo(y))
            };
        }

        //private static DocumentStructures.Document findSourceDocument(ILexical arg) {
        //    return 
        //}
        private static IEnumerable<TResult> AggregatedOnType<TResult>(this IEnumerable<ILexical> source, Func<TResult, TResult, SimilarityResult> aggregator)
            where TResult : class, ILexical {
            return from o in source.OfType<TResult>()
                   let simGroups =
                   from i in source.OfType<TResult>()
                   group o by aggregator(o, i) into g
                   where g.Key
                   orderby g.Count(), g.Key.ActualRatio descending
                   select g
                   select o;
        }

        private static SimilarityResult SimV<T>(T o, T i) where T : IVerbal {
            return o.IsSimilarTo(i);
        }
        private static SimilarityResult SimE<T>(T o, T i) where T : IEntity {
            return o.IsSimilarTo(i);
        }
        private static SimilarityResult SimA<T>(T o, T i) where T : IAdverbial {
            return o.IsSimilarTo(i);
        }
        private static SimilarityResult SimD<T>(T o, T i) where T : IDescriptor {
            return o.IsSimilarTo(i);
        }
    }
}

//var byText =
//    from element in similarElements
//    group element by element.Text
//        into textGroup
//        orderby textGroup.Count() descending
//        select textGroup.Key;

