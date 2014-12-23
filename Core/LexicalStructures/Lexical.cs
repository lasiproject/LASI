using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.PatternMatching;

namespace LASI.Core
{
    using System.Linq.Expressions;
    using LASI.Utilities.FunctionExtensions;
    public static partial class Lexical
    {
        public static Match<T> Match<T>(T lexical) where T : class, ILexical {
            return MatchExtensions.Match(lexical);
        }
    }

}