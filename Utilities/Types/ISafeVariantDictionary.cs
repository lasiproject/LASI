using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities.Types
{
    interface ISafeVariantDictionary<in TKey, out TValue>
    {
        TValue this[TKey value] { get; }
        int Count { get; }
    }
}
