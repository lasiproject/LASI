using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis.Binders.Experimental
{
    interface INestableLexical<out TLexical> : ILexical where TLexical : ILexical
    {
        TLexical Parent { get; }
        TLexical Self { get; }
        IEnumerable<INestableLexical<TLexical>> Children { get; }
    }
}
