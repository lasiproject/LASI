using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    public static class ClauseExtensions
    {
        public static bool Includes(this Clause clause, Phrase phrase) => clause == phrase.Clause;

        public static bool Includes(this Clause clause, IEnumerable<Phrase> phrases) => phrases.All(clause.Includes);

        public static bool Includes(this Clause clause, Phrase phrase, params Phrase[] phrases) => clause.Includes(phrases.Prepend(phrase));
    }
}