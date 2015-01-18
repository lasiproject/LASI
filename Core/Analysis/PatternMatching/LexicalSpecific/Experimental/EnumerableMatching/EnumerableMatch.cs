using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.EnumerableMatching
{
	using LASI.Utilities.Extra;
	using static Enumerable;
	using IL = ILexical;

	public class EnumerableMatch<T, TResult> : EnumerableMatchBase<T, TResult> where T : class, IL
	{
		public EnumerableMatch(IEnumerable<T> values) : base(values) { }

		public EnumerableMatch<T, TResult> Case<TCase>(Func<TCase, TResult> f) where TCase : class, IL => Apply(f);

		private EnumerableMatch<T, TResult> Apply<TCase>(Func<TCase, TResult> f) where TCase : class, IL {
			if (!Matched) {
				CaseResults = Values.OfType<TCase>().Select(x => {
					SetIsMatched();
					return f(x);
				});
			}
			return this;
		}
	}
}