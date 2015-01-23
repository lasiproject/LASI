using LASI.Core.PatternMatching;
using LASI.Utilities.SupportTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LASI.Core
{
    /// <summary>
    /// Provides for implicit conversions from <see cref="Match{T, TResult}"/> expressions to an explicit IEnumerable&lt;<typeparamref name="TResult"/>&gt;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class MatchToEnumerableProvider<T, TResult> : IEnumerable<TResult> where T : class, ILexical
    {
        private Match<T, TResult> pattern;

        private MatchToEnumerableProvider(Match<T, TResult> pattern) { this.pattern = pattern; }

        #region Explicit IEnumerable<TResult> and Explicit IEnumerable
        IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        /// <summary>
        /// Provides the Enumerator by delegating to an Option of the converted pattern's result.
        /// </summary>
        /// <returns>The IEnumerator&lt;<see cref="TResult"/>&gt;.</returns>
        private IEnumerator<TResult> GetEnumerator() => pattern.ResultOption().AsEnumerable().GetEnumerator();


        #endregion

        public static implicit operator MatchToEnumerableProvider<T, TResult>(Match<T, TResult> pattern) => new MatchToEnumerableProvider<T, TResult>(pattern);
    }
}
