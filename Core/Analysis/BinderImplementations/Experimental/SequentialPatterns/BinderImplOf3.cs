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
    using AE = Action<E>;
    using AV = Action<V>;
    using AC = Action<C>;
    using AD = Action<D>;
    using AR = Action<R>;
    using AA = Action<A>;
    using AP = Action<P>;
    using AS = Action<S>;
    public partial class BinderComponent : IBinderComponent<E, V, C, D, R, A, P>, IBinderComponent<P, E, V, C, D, R, A>, IBinderComponent<A, P, E, V, C, D, R>, IBinderComponent<R, A, P, E, V, C, D>, IBinderComponent<D, R, A, P, E, V, C>, IBinderComponent<R, D, C, A, P, E, V>, IBinderComponent<V, C, D, R, A, P, E>, IBinderComponent<E, R, D, R, A, P, E>, IBinderComponent<E, C, R, E, A, V, E>, IBinderComponent<E, C, E, V, C, V, A>, IBinderComponent<E, E, C, E, C, E, V>, IBinderComponent<S, V, C, D, R, A, P>, IBinderComponent<P, S, V, C, D, R, A>, IBinderComponent<A, P, S, V, C, D, R>, IBinderComponent<R, A, P, S, V, C, D>, IBinderComponent<D, R, A, P, S, V, C>, IBinderComponent<R, D, C, A, S, E, V>, IBinderComponent<V, C, D, R, A, S, E>, IBinderComponent<E, R, D, R, A, P, S>, IBinderComponent<S, C, R, E, A, V, E>, IBinderComponent<E, S, E, V, C, V, A>, IBinderComponent<E, S, C, E, C, E, V>, System.Collections.IEnumerable
    {
        public BinderComponent Match3(Func<A, Func<C, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<D, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<D, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<P, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<P, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<P, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<R, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<R, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<C, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<D, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<D, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<E, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<E, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<P, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<P, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<P, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<R, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<R, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<V, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<V, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<C, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<C, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<D, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<E, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<E, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<E, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<V, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<V, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<A, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<A, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<E, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<P, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<P, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<R, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<R, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<R, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<V, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<V, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<V, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<A, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<A, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<D, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<D, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<D, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<E, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<E, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<E, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<P, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<R, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<R, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<A, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<A, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<A, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<C, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<C, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<R, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<V, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<V, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<V, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<A, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<A, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<A, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<C, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<C, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<C, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<E, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<E, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<P, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<P, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<V, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<R, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<E, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<E, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<R, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<C, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<C, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<C, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<E, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<E, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<E, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<C, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<C, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<E, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<V, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<V, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<V, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<V, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<S, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<S, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<S, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<S, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<S, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<S, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<S, AP>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<S, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<S, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<P, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<V, Func<S, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<S, AV>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<P, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<P, Func<A, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<P, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<A, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<A, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<P, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<D, Func<R, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<R, AD>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<R, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<R, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<D, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<D, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<D, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<D, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<C, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<C, AA>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<A, Func<C, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<C, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<S, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<S, AS>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<R, Func<S, AR>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<S, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<E, Func<S, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<C, Func<S, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<E, AE>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
        public BinderComponent Match3(Func<S, Func<E, AC>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); }
    }
}

#pragma warning restore 1591