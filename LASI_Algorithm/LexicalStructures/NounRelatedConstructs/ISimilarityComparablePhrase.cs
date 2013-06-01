using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.LexicalStructures.NounRelatedConstructs
{
    public interface ISimilarityComparablePhrase<in T> where T : Phrase
    {
        bool IsSimilarTo(T first, T second);
    }
}
