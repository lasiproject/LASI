using System.Collections.Generic;

namespace LASI.Core.Binding.Experimental
{
    interface INestableLexical<out TLexical> : ILexical where TLexical : ILexical
    {
        TLexical Parent { get; }

        IEnumerable<INestableLexical<TLexical>> Children { get; }
    }
}