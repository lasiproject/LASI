#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Alias types to shorten name and thus file size.
using E = LASI.Core.IEntity;
using V = LASI.Core.IVerbal;
using C = LASI.Core.IConjunctive;
using D = LASI.Core.IDescriptor;
using R = LASI.Core.IReferencer;
using A = LASI.Core.IAdverbial;
using P = LASI.Core.IPrepositional;
using S = LASI.Core.SymbolPhrase;
namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    using Ae = Action<E>;
    using Av = Action<V>;
    using Ac = Action<C>;
    using Ad = Action<D>;
    using Ar = Action<R>;
    using Aa = Action<A>;
    using Ap = Action<P>;
    using As = Action<S>;
    public partial class BinderComponent : IBinderComponent<E, V, C, D, R, A, P>, IBinderComponent<P, E, V, C, D, R, A>, IBinderComponent<A, P, E, V, C, D, R>, IBinderComponent<R, A, P, E, V, C, D>, IBinderComponent<D, R, A, P, E, V, C>, IBinderComponent<R, D, C, A, P, E, V>, IBinderComponent<V, C, D, R, A, P, E>, IBinderComponent<E, R, D, R, A, P, E>, IBinderComponent<E, C, R, E, A, V, E>, IBinderComponent<E, C, E, V, C, V, A>, IBinderComponent<E, E, C, E, C, E, V>, IBinderComponent<S, V, C, D, R, A, P>, IBinderComponent<P, S, V, C, D, R, A>, IBinderComponent<A, P, S, V, C, D, R>, IBinderComponent<R, A, P, S, V, C, D>, IBinderComponent<D, R, A, P, S, V, C>, IBinderComponent<R, D, C, A, S, E, V>, IBinderComponent<V, C, D, R, A, S, E>, IBinderComponent<E, R, D, R, A, P, S>, IBinderComponent<S, C, R, E, A, V, E>, IBinderComponent<E, S, E, V, C, V, A>, IBinderComponent<E, S, C, E, C, E, V>, System.Collections.IEnumerable
    {
        public BinderComponent Match3(Func<A, Func<C, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<D, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<D, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<P, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<P, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<P, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<R, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<R, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<C, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<D, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<D, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<E, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<E, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<P, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<P, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<P, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<R, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<R, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<V, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<V, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<C, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<C, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<D, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<E, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<E, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<E, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<V, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<V, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<A, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<A, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<E, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<P, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<P, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<R, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<R, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<R, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<V, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<V, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<V, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<A, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<A, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<D, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<D, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<D, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<E, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<E, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<E, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<P, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<R, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<R, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<A, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<A, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<A, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<C, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<C, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<R, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<V, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<V, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<V, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<A, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<A, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<A, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<C, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<C, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<C, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<E, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<E, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<P, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<P, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<V, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<R, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<E, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<E, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<R, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<C, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<C, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<C, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<E, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<E, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<E, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<C, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<C, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<E, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<V, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<V, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<V, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<V, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<S, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<S, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<S, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<S, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<S, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<S, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<S, Ap>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<S, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<S, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<P, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<S, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<S, Av>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<P, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<A, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<P, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<A, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<A, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<P, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<R, Ad>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<R, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<R, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<D, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<D, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<D, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<C, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<C, Aa>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<C, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<S, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<S, As>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<S, Ar>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<S, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<S, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<S, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<E, Ae>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<E, Ac>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
    }
}

#pragma warning restore 1591