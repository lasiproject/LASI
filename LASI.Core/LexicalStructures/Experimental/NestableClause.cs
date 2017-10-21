using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Binding.Experimental
{
    class NestableClause : Clause, INestableLexical<NestableClause>
    {
        public NestableClause(IEnumerable<INestableLexical<NestableClause>> constituentClauses, NestableClause parent = null) :
            base(constituentClauses.Select(clause => new
                {
                    clause,
                    phrases = (clause is Clause c && c.Phrases is var phrases ? phrases : Enumerable.Empty<Phrase>())
                })
                .Select(@t => phrase))
        {
            Parent = parent;
            Children = constituentClauses;
        }

        public IEnumerable<NestableClause> GetClausesSkippingSubordinates()
        {
            var temp = Children
                .TakeWhile(c => !(c is object o && o is SubordinateClause s))
                .ToList();
            var bookend = Children.Skip(temp.Count() + 1).Take(1);
            var result = temp.Take(temp.Count() - 1).Concat(new[] {new NestableClause(new[] {temp.Last(), bookend.FirstOrDefault()}), this});
            return result.Select(c => this);
        }

        #region Properties

        public NestableClause Parent { get; }

        public IEnumerable<INestableLexical<NestableClause>> Children { get; }

        #endregion Properties

        static IEnumerable<TResult> SelectOver<T, TResult>(IEnumerable<T> source, Func<T, TResult> f) => source.se
    }
}