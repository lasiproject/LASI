using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.ComparativeHeuristics
{
    public static class RepresentationalDeterminer
    {
        public static TLexical ToCommonRepresentation<TLexical>(this IEnumerable<TLexical> similarElements) where TLexical : ILexical {







            var byText =
                from element in similarElements
                group element by element.Text
                    into textGroup
                    orderby textGroup.Count() descending
                    select textGroup.Key;











            throw new NotImplementedException();
        }
    }
}
