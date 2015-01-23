using System;
using System.Collections.Generic;

namespace LASI.Core.PatternMatching
{
    public interface IMatchQueryExpressionEnabled<TResult>
    {
        IMatchQueryExpressionEnabled<TResult> Where(object dummy);
        IEnumerable<TResult> Select<TX>(Func<TResult, TX> f);

        IEnumerable<TX> SelectMany<TX>(Func<IMatchQueryExpressionEnabled<TResult>, IEnumerable<TX>> f);
        IEnumerable<TX> SelectMany<TX>(Func<Match<ILexical, TResult>, TX> f);

    }
}