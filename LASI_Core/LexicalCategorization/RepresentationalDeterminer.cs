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
            var r0 = from o in source.OfEntity()
                     let simGroups =
                     from i in source.OfEntity()
                     group o by o.IsSimilarTo(i) into g
                     where g.Key
                     orderby g.Key.ActualRatio
                     select g.MaxBy(v => v.Document.GetActions().Count(a => a.Text.EqualsIgnoreCase(v.Text)))
                     select o;
            var r1 = from o in source.OfType<IVerbal>()
                     let simGroups =
                     from i in source.OfType<IVerbal>()
                     group o by o.IsSimilarTo(i) into g
                     where g.Key
                     orderby g.Key.ActualRatio
                     select g.MaxBy(v => v.Document.GetActions().Count(a => a.Text.EqualsIgnoreCase(v.Text)))
                     select o;
            var r2 = from o in source.OfType<IDescriptor>()
                     let simGroups =
                     from i in source.OfType<IDescriptor>()
                     group o by o.IsSimilarTo(i) into g
                     where g.Key
                     orderby g.Key.ActualRatio
                     select g.MaxBy(v => v.Document.GetActions().Count(a => a.Text.EqualsIgnoreCase(v.Text)))
                     select o;
            var r3 = from o in source.OfType<IAdverbial>()
                     let simGroups =
                     from i in source.OfType<IAdverbial>()
                     group o by o.IsSimilarTo(i) into g
                     where g.Key
                     orderby g.Key.ActualRatio
                     select g.MaxBy(v => v.Document.GetActions().Count(a => a.Text.EqualsIgnoreCase(v.Text)))
                     select o;
            return new KeyedByTypeCollection<IEnumerable<ILexical>> { r0, r1, r2, r3 };
            //<IEnumerable<ILexical>>(new ILexical[] { r0, r1, r2, r3 });
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

