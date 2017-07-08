using System;
using System.Collections.Generic;

namespace LASI.Utilities
{
    public interface IVariantKeyValuePair<in TKey, out TValue>
    {
        TKey Key { set; }
        TValue Value { get; }
    }
}