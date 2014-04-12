#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    // Alias types to shorten name and thus file size.
    using Ae = Action<E>;
    using Av = Action<V>;
    using Ac = Action<C>;
    using Ad = Action<D>;
    using Ar = Action<R>;
    using Aa = Action<A>;
    using Ap = Action<P>;
    using As = Action<S>;
    public partial class BinderComponent : IBinderComponent<E, V, C, D, R, A, P>, IBinderComponent<P, E, V, C, D, R, A>, IBinderComponent<A, P, E, V, C, D, R>, IBinderComponent<R, A, P, E, V, C, D>, IBinderComponent<D, R, A, P, E, V, C>, IBinderComponent<R, D, C, A, P, E, V>, IBinderComponent<V, C, D, R, A, P, E>, IBinderComponent<E, R, D, R, A, P, E>, IBinderComponent<E, C, R, E, A, V, E>, IBinderComponent<E, C, E, V, C, V, A>, IBinderComponent<E, E, C, E, C, E, V>, IBinderComponent<S, V, C, D, R, A, P>, IBinderComponent<P, S, V, C, D, R, A>, IBinderComponent<A, P, S, V, C, D, R>, IBinderComponent<R, A, P, S, V, C, D>, IBinderComponent<D, R, A, P, S, V, C>, IBinderComponent<R, D, C, A, S, E, V>, IBinderComponent<V, C, D, R, A, S, E>, IBinderComponent<E, R, D, R, A, P, S>, IBinderComponent<S, C, R, E, A, V, E>, IBinderComponent<E, S, E, V, C, V, A>, IBinderComponent<E, S, C, E, C, E, V>
    {
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<C, Func<A, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<C, Func<V, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<E, Func<A, Func<P, Func<A, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<E, Func<A, Func<P, Func<C, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<P, Func<E, Func<V, Func<C, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<D, Func<R, Func<A, Func<P, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<P, Func<A, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<P, Func<C, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<R, Func<C, Func<D, Func<C, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<R, Func<C, Func<D, Func<P, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<D, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<E, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<R, Func<A, Func<P, Func<E, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<E, Func<D, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<E, Func<P, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<A, Func<E, Func<R, Func<D, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<A, Func<E, Func<R, Func<E, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<E, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<V, Func<C, Func<D, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<C, Func<P, Func<D, Func<P, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<C, Func<P, Func<D, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<E, Func<V, Func<C, Func<D, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<R, Func<P, Func<D, Func<C, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<R, Func<P, Func<D, Func<P, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<E, Func<D, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<E, Func<P, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<A, Func<P, Func<E, Func<V, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<P, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<E, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<R, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<D, Func<C, Func<A, Func<P, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<R, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<V, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<R, As>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<C, Func<D, Func<R, Func<A, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<A, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<V, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<A, As>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<R, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<V, Func<V, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<C, Func<D, Func<R, Func<A, As>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<R, As>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<V, As>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<V, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<R, Func<D, Func<R, Func<A, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<D, Func<E, Func<R, Func<A, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<D, Func<E, Func<R, Func<E, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<R, Func<A, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<D, Func<A, Func<R, Func<E, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<R, Func<E, Func<A, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<R, Func<E, Func<C, Func<A, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<R, Func<E, Func<C, Func<E, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<A, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<E, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<V, Func<C, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<C, Func<C, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<C, Func<E, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<C, Func<C, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<C, Func<E, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<C, Func<E, Func<C, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<E, Func<C, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<E, Func<E, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<E, Func<C, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<E, Func<E, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<V, Func<C, Func<D, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<V, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<V, Func<S, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<V, Func<S, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<S, Func<V, Func<C, Func<D, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<S, Func<D, Ar>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<P, Func<V, Func<P, Func<S, Func<P, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<S, Func<D, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<V, Func<D, Func<S, Func<P, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<P, Func<S, Func<V, Func<C, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<S, Func<A, Func<P, Func<C, Ad>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<S, Func<A, Func<P, Func<A, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<S, Func<C, Func<P, Func<C, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<S, Func<C, Func<P, Func<A, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<A, Func<P, Func<S, Func<V, Ac>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<P, Func<R, Func<A, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<V, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<P, Func<V, Func<A, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<R, Func<A, Func<P, Func<S, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<D, Func<A, Func<D, Func<R, Func<S, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<A, Func<S, Func<R, Func<S, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<A, Func<S, Func<R, Func<D, Ap>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<D, Func<C, Func<A, Func<S, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<R, Func<C, Func<R, Func<D, Func<S, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<D, Func<S, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<S, Func<D, Func<R, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<C, Func<D, Func<R, Func<A, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<V, Func<D, Func<V, Func<C, Func<A, Aa>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<C, Func<R, Func<E, Func<A, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<R, Func<S, Func<C, Func<A, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<S, Func<R, Func<S, Func<C, Func<S, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<A, Func<R, Func<A, Func<C, Func<S, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<S, Func<E, Func<V, Func<C, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<S, Func<C, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<E, Func<E, Func<S, Func<E, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<S, Func<C, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<E, Func<C, Func<S, Func<E, Av>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<S, Func<C, Func<E, Func<C, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<S, Func<C, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<E, Func<C, Func<E, Func<S, Func<E, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<S, Func<C, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
        public BinderComponent Match6(Func<C, Func<C, Func<C, Func<S, Func<E, Ae>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(6)), 6); }
    }
}
#pragma warning restore 1591