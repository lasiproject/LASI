using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    public static class PhraseExtensions
    {
        public static bool IsCoClausalWith(this Phrase phrase, Phrase other) => phrase.Clause == other.Clause;

        public static bool IsCoClausalWith(this Phrase phrase, IEnumerable<Phrase> others) => others.All(phrase.IsCoClausalWith);

        public static bool IsCoClausalWith(this Phrase phrase, Phrase first, params Phrase[] rest) => phrase.IsCoClausalWith(rest.Prepend(first));
    }
}