using System;
using System.Collections.Generic;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    internal struct ExceptionEntry
    {
        public ExceptionEntry(string key, List<string> value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; }
        public List<string> Value { get; }
    }
}