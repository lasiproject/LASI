#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns.Test
{
    // Alias types to shorten name and thus file size.
    using E = IEntity;
    using V = IVerbal;
    using C = IConjunctive;
    using D = IDescriptor;
    using R = IReferencer;
    using A = IAdverbial;
    using P = IPrepositional;
    using S = SymbolPhrase;
    public partial class BinderComponent :
           IBinderComponent<E, V, C, D, R, A, P>,
           IBinderComponent<P, E, V, C, D, R, A>,
           IBinderComponent<A, P, E, V, C, D, R>,
           IBinderComponent<R, A, P, E, V, C, D>,
           IBinderComponent<D, R, A, P, E, V, C>,
           IBinderComponent<R, D, C, A, P, E, V>,
           IBinderComponent<V, C, D, R, A, P, E>,
           IBinderComponent<E, R, D, R, A, P, E>,
           IBinderComponent<E, C, R, E, A, V, E>,
           IBinderComponent<E, C, E, V, C, V, A>,
           IBinderComponent<E, E, C, E, C, E, V>,
           IBinderComponent<S, V, C, D, R, A, P>,
           IBinderComponent<P, S, V, C, D, R, A>,
           IBinderComponent<A, P, S, V, C, D, R>,
           IBinderComponent<R, A, P, S, V, C, D>,
           IBinderComponent<D, R, A, P, S, V, C>,
           IBinderComponent<R, D, C, A, S, E, V>,
           IBinderComponent<V, C, D, R, A, S, E>,
           IBinderComponent<E, R, D, R, A, P, S>,
           IBinderComponent<S, C, R, E, A, V, E>,
           IBinderComponent<E, S, E, V, C, V, A>,
           IBinderComponent<E, S, C, E, C, E, V>,
           System.Collections.IEnumerable
    {
        public BinderComponent Add(Func<A, Func<A, Func<A, Func<P, Func<E, Func<V, Func<C, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<E, Func<D, Func<A, Func<P, Func<A, Func<A, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<E, Func<D, Func<A, Func<P, Func<V, Func<C, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<E, Func<D, Func<E, Func<P, Func<A, Func<A, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<E, Func<D, Func<E, Func<P, Func<A, Func<V, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<R, Func<A, Func<C, Func<C, Func<P, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<R, Func<A, Func<C, Func<D, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<R, Func<A, Func<C, Func<D, Func<C, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<R, Func<A, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<C, Func<A, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<C, Func<A, Func<P, Func<D, Func<R, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<C, Func<A, Func<P, Func<P, Func<P, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<C, Func<A, Func<P, Func<P, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<C, Func<A, Func<R, Func<D, Func<P, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<R, Func<A, Func<C, Func<D, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<R, Func<A, Func<C, Func<D, Func<P, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<R, Func<A, Func<P, Func<D, Func<C, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<R, Func<A, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<R, Func<A, Func<P, Func<P, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<P, Func<R, Func<A, Func<P, Func<P, Func<P, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<P, Func<D, Func<P, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<P, Func<D, Func<R, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<P, Func<D, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<R, Func<R, Func<P, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<C, Func<C, Func<D, Func<R, Func<A, Func<P, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<C, Func<R, Func<E, Func<C, Func<D, Func<A, Func<P, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<C, Func<R, Func<E, Func<C, Func<D, Func<C, Func<C, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<C, Func<R, Func<E, Func<R, Func<D, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<C, Func<R, Func<E, Func<R, Func<D, Func<C, Func<C, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<D, Func<D, Func<D, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<D, Func<D, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<D, Func<E, Func<D, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<D, Func<E, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<P, Func<E, Func<D, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<P, Func<E, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<P, Func<E, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<D, Func<E, Func<D, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<D, Func<E, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<D, Func<E, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<P, Func<E, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<P, Func<E, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<P, Func<P, Func<D, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<D, Func<A, Func<V, Func<A, Func<R, Func<D, Func<D, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<D, Func<A, Func<V, Func<A, Func<R, Func<D, Func<P, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<D, Func<A, Func<V, Func<D, Func<R, Func<D, Func<D, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<D, Func<A, Func<V, Func<D, Func<R, Func<P, Func<E, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<D, Func<D, Func<R, Func<A, Func<P, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<E, Func<C, Func<D, Func<E, Func<E, Func<R, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<E, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<E, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<E, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<E, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<E, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<R, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<A, Func<C, Func<V, Func<E, Func<D, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<A, Func<C, Func<V, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<A, Func<E, Func<V, Func<D, Func<R, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<A, Func<E, Func<V, Func<E, Func<E, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<P, Func<E, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<P, Func<E, Func<R, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<P, Func<E, Func<R, Func<R, Func<V, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<P, Func<E, Func<V, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<P, Func<E, Func<V, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<P, Func<E, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<V, Func<P, Func<E, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<V, Func<P, Func<E, Func<R, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<V, Func<P, Func<E, Func<R, Func<A, Func<V, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<V, Func<P, Func<E, Func<V, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<V, Func<P, Func<E, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<V, Func<P, Func<E, Func<V, Func<V, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<V, Func<P, Func<E, Func<V, Func<V, Func<V, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<D, Func<D, Func<E, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<E, Func<R, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<E, Func<R, Func<D, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<E, Func<R, Func<E, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<E, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<E, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<E, Func<A, Func<P, Func<D, Func<R, Func<E, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<E, Func<A, Func<P, Func<E, Func<E, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<E, Func<A, Func<P, Func<E, Func<E, Func<E, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<E, Func<A, Func<P, Func<E, Func<R, Func<D, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<E, Func<A, Func<P, Func<E, Func<R, Func<E, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<P, Func<E, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<V, Func<R, Func<P, Func<E, Func<C, Func<D, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<V, Func<R, Func<P, Func<E, Func<P, Func<P, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<V, Func<R, Func<V, Func<E, Func<P, Func<C, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<V, Func<R, Func<V, Func<E, Func<P, Func<P, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<A, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<V, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<C, Func<V, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<V, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<V, Func<C, Func<V, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<V, Func<C, Func<V, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<C, Func<E, Func<C, Func<D, Func<R, Func<A, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<C, Func<E, Func<C, Func<D, Func<R, Func<R, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<C, Func<E, Func<R, Func<D, Func<A, Func<P, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<C, Func<E, Func<R, Func<D, Func<R, Func<R, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<P, Func<C, Func<P, Func<A, Func<R, Func<E, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<P, Func<C, Func<P, Func<A, Func<R, Func<R, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<P, Func<C, Func<R, Func<A, Func<E, Func<V, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<P, Func<C, Func<R, Func<A, Func<R, Func<R, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<R, Func<A, Func<P, Func<E, Func<V, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<R, Func<D, Func<C, Func<A, Func<P, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<V, Func<D, Func<R, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<V, Func<D, Func<R, Func<A, Func<C, Func<V, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<V, Func<D, Func<R, Func<A, Func<C, Func<V, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<V, Func<D, Func<R, Func<V, Func<C, Func<V, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<V, Func<D, Func<R, Func<V, Func<C, Func<V, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<V, Func<D, Func<R, Func<V, Func<V, Func<A, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<E, Func<V, Func<A, Func<A, Func<C, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<E, Func<V, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<E, Func<V, Func<A, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<E, Func<V, Func<C, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<E, Func<V, Func<C, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<E, Func<V, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<A, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<A, Func<P, Func<C, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<C, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<D, Func<P, Func<D, Func<C, Func<V, Func<R, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<D, Func<P, Func<D, Func<C, Func<V, Func<V, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<D, Func<P, Func<V, Func<C, Func<R, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<D, Func<P, Func<V, Func<C, Func<V, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<C, Func<D, Func<E, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<E, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<E, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<V, Func<C, Func<P, Func<R, Func<E, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<V, Func<C, Func<D, Func<R, Func<E, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<V, Func<C, Func<D, Func<R, Func<E, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<V, Func<C, Func<D, Func<R, Func<E, Func<D, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<A, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<D, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<D, Func<P, Func<C, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<P, Func<E, Func<R, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<P, Func<E, Func<V, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<P, Func<E, Func<V, Func<C, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<P, Func<E, Func<V, Func<C, Func<A, Func<V, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<A, Func<P, Func<D, Func<V, Func<R, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<A, Func<P, Func<E, Func<V, Func<R, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<A, Func<P, Func<E, Func<V, Func<R, Func<D, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<A, Func<P, Func<E, Func<V, Func<R, Func<E, Func<C, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<A, Func<R, Func<E, Func<D, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<A, Func<P, Func<E, Func<D, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<A, Func<P, Func<E, Func<D, Func<R, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<A, Func<P, Func<E, Func<D, Func<P, Func<V, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<V, Func<P, Func<C, Func<V, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<C, Func<V, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<C, Func<V, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<R, Func<D, Func<R, Func<A, Func<P, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<D, Func<P, Func<E, Func<R, Func<R, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<E, Func<R, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<D, Func<P, Func<D, Func<R, Func<E, Func<R, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<D, Func<P, Func<E, Func<R, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<D, Func<P, Func<D, Func<R, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<R, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<R, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<E, Func<R, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<A, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<E, Func<E, Func<A, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<E, Func<P, Func<R, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<V, Func<E, Func<C, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<E, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<V, Func<R, Func<C, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<V, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<E, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<E, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<A, Func<C, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<E, Func<E, Func<C, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<A, Func<A, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<A, Func<A, Func<A, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<E, Func<E, Func<E, Func<A, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<E, Func<A, Func<C, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<E, Func<C, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<R, Func<E, Func<A, Func<C, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<E, Func<E, Func<V, Func<C, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<C, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<E, Func<C, Func<V, Func<C, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<C, Func<C, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<E, Func<C, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<E, Func<C, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<E, Func<C, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<C, Func<C, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<E, Func<E, Func<C, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }

        public BinderComponent Add(Func<R, Func<P, Func<A, Action<C>>>> p) {
            throw new NotImplementedException();
        }

        public BinderComponent Add(Func<V, Func<E, Action<S>>> p) {
            throw new NotImplementedException();
        }

        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<C, Func<C, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<E, Func<C, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<C, Func<C, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<E, Func<V, Func<C, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<V, Func<C, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<V, Func<C, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<V, Func<C, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<C, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<C, Func<E, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<E, Func<E, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<C, Func<E, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<C, Func<C, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<C, Func<C, Func<C, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<E, Func<C, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<E, Func<E, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<E, Func<E, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<E, Func<C, Func<E, Func<E, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<S, Func<V, Func<C, Func<D, Func<R, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<C, Func<A, Func<S, Func<V, Func<D, Func<R, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<S, Func<C, Func<D, Func<S, Func<V, Func<S, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<C, Func<A, Func<C, Func<V, Func<S, Func<D, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<C, Func<A, Func<S, Func<V, Func<S, Func<S, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<C, Func<A, Func<C, Func<V, Func<S, Func<S, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<S, Func<C, Func<D, Func<R, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<S, Func<V, Func<R, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<R, Func<V, Func<S, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<S, Func<C, Func<D, Func<S, Func<V, Func<S, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<S, Func<V, Func<S, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<S, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<R, Func<R, Func<R, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<S, Func<C, Func<D, Func<S, Func<S, Func<R, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<S, Func<C, Func<D, Func<R, Func<V, Func<S, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<R, Func<C, Func<D, Func<S, Func<V, Func<S, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<S, Func<C, Func<D, Func<R, Func<V, Func<S, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<C, Func<D, Func<S, Func<A, Func<V, Func<S, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<S, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<C, Func<D, Func<R, Func<A, Func<V, Func<S, Func<P, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<P, Func<S, Func<V, Func<C, Func<D, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<V, Func<R, Func<P, Func<S, Func<C, Func<D, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<P, Func<S, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<V, Func<R, Func<V, Func<S, Func<P, Func<C, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<V, Func<R, Func<P, Func<S, Func<P, Func<P, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<P, Func<V, Func<R, Func<V, Func<S, Func<P, Func<P, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<D, Func<S, Func<D, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<D, Func<S, Func<D, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<P, Func<S, Func<D, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<D, Func<S, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<P, Func<S, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<P, Func<S, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<D, Func<S, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<D, Func<V, Func<C, Func<P, Func<S, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<P, Func<V, Func<C, Func<D, Func<S, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<V, Func<C, Func<P, Func<R, Func<S, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<V, Func<C, Func<D, Func<R, Func<S, Func<P, Func<A, Action<P>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<V, Func<C, Func<D, Func<R, Func<S, Func<P, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<C, Func<V, Func<C, Func<D, Func<R, Func<S, Func<D, Func<A, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<A, Func<P, Func<S, Func<V, Func<C, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<S, Func<D, Func<A, Func<P, Func<V, Func<C, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<S, Func<V, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<S, Func<D, Func<S, Func<P, Func<A, Func<V, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<S, Func<D, Func<A, Func<P, Func<A, Func<A, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<A, Func<S, Func<D, Func<S, Func<P, Func<A, Func<A, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<S, Func<V, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<S, Func<V, Func<C, Func<P, Func<C, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<S, Func<V, Func<A, Func<P, Func<C, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<S, Func<V, Func<C, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<S, Func<V, Func<A, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<S, Func<V, Func<A, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<S, Func<V, Func<C, Func<C, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<S, Func<V, Func<C, Func<C, Func<C, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<S, Func<V, Func<A, Func<A, Func<C, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<S, Func<V, Func<C, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<S, Func<V, Func<A, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<A, Func<S, Func<V, Func<C, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<S, Func<V, Func<A, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<S, Func<V, Func<C, Func<D, Func<P, Func<A, Func<R, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<S, Func<V, Func<C, Func<D, Func<P, Func<A, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<S, Func<V, Func<C, Func<D, Func<P, Func<C, Func<R, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<R, Func<A, Func<P, Func<S, Func<V, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<P, Func<C, Func<R, Func<A, Func<S, Func<V, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<R, Func<P, Func<S, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<P, Func<C, Func<P, Func<A, Func<R, Func<S, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<V, Func<P, Func<S, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<R, Func<P, Func<S, Func<V, Func<A, Func<V, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<V, Func<P, Func<S, Func<R, Func<A, Func<V, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<V, Func<P, Func<S, Func<V, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<R, Func<P, Func<S, Func<R, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<V, Func<P, Func<S, Func<R, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<V, Func<P, Func<S, Func<V, Func<V, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<V, Func<P, Func<S, Func<V, Func<V, Func<V, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<R, Func<P, Func<S, Func<R, Func<R, Func<V, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<R, Func<P, Func<S, Func<V, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<V, Func<P, Func<S, Func<R, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<R, Func<P, Func<S, Func<V, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<P, Func<S, Func<R, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<P, Func<S, Func<V, Func<C, Func<A, Func<R, Func<D, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<P, Func<S, Func<V, Func<C, Func<A, Func<R, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<P, Func<S, Func<V, Func<C, Func<A, Func<V, Func<D, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<D, Func<D, Func<R, Func<A, Func<P, Func<S, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<D, Func<D, Func<A, Func<V, Func<D, Func<R, Func<P, Func<S, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<S, Func<A, Func<P, Func<S, Func<R, Func<S, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<S, Func<R, Func<S, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<S, Func<A, Func<P, Func<D, Func<R, Func<S, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<S, Func<A, Func<P, Func<S, Func<R, Func<D, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<S, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<S, Func<A, Func<P, Func<S, Func<S, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<S, Func<A, Func<P, Func<S, Func<S, Func<S, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<D, Func<D, Func<S, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<S, Func<R, Func<D, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<S, Func<A, Func<P, Func<D, Func<R, Func<D, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<D, Func<A, Func<P, Func<S, Func<R, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<A, Func<P, Func<S, Func<V, Func<R, Func<D, Func<C, Action<D>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<A, Func<P, Func<S, Func<V, Func<R, Func<D, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<P, Func<A, Func<P, Func<S, Func<V, Func<R, Func<S, Func<C, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<R, Func<D, Func<C, Func<A, Func<S, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<R, Func<C, Func<E, Func<R, Func<D, Func<A, Func<S, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<S, Func<C, Func<A, Func<S, Func<D, Func<S, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<S, Func<D, Func<S, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<S, Func<C, Func<A, Func<R, Func<D, Func<S, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<S, Func<C, Func<A, Func<S, Func<D, Func<R, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<S, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<S, Func<C, Func<A, Func<S, Func<S, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<S, Func<C, Func<A, Func<S, Func<S, Func<S, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<R, Func<R, Func<S, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<S, Func<D, Func<R, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<S, Func<C, Func<A, Func<R, Func<D, Func<R, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<R, Func<C, Func<A, Func<S, Func<D, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<A, Func<S, Func<E, Func<D, Func<R, Func<V, Action<R>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<A, Func<S, Func<E, Func<D, Func<R, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<A, Func<C, Func<A, Func<S, Func<E, Func<D, Func<S, Func<V, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<V, Func<C, Func<D, Func<R, Func<A, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<D, Func<S, Func<V, Func<C, Func<R, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<D, Func<S, Func<D, Func<C, Func<V, Func<R, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<D, Func<S, Func<V, Func<C, Func<V, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<V, Func<D, Func<S, Func<D, Func<C, Func<V, Func<V, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<V, Func<S, Func<C, Func<V, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<S, Func<C, Func<V, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<S, Func<C, Func<V, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<S, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<R, Func<D, Func<R, Func<A, Func<P, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<D, Func<P, Func<E, Func<R, Func<R, Func<A, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<E, Func<R, Func<E, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<D, Func<P, Func<E, Func<R, Func<E, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<R, Func<A, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<A, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<A, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<R, Func<E, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<E, Func<R, Func<E, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<E, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<E, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<A, Func<A, Func<A, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<E, Func<E, Func<A, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<E, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<A, Func<D, Func<R, Func<E, Func<R, Func<E, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<E, Func<D, Func<R, Func<A, Func<R, Func<E, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<E, Func<P, Func<R, Func<E, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<E, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<E, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<R, Func<D, Func<R, Func<A, Func<P, Func<R, Func<A, Func<S, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<S, Func<C, Func<R, Func<E, Func<A, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<R, Func<V, Func<S, Func<C, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<S, Func<R, Func<E, Func<S, Func<C, Func<S, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<R, Func<V, Func<R, Func<C, Func<S, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<R, Func<V, Func<S, Func<C, Func<S, Func<S, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<S, Func<S, Func<R, Func<V, Func<R, Func<C, Func<S, Func<S, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<S, Func<R, Func<E, Func<A, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<S, Func<C, Func<A, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<A, Func<C, Func<S, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<S, Func<R, Func<E, Func<S, Func<C, Func<S, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<S, Func<C, Func<S, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<A, Func<A, Func<S, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<A, Func<A, Func<A, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<S, Func<R, Func<E, Func<S, Func<S, Func<A, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<S, Func<R, Func<E, Func<A, Func<C, Func<S, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<A, Func<R, Func<E, Func<S, Func<C, Func<S, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<S, Func<R, Func<E, Func<A, Func<C, Func<S, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<E, Func<S, Func<V, Func<C, Func<S, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<S, Func<E, Action<S>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<R, Func<E, Func<A, Func<V, Func<C, Func<S, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<S, Func<E, Func<V, Func<C, Func<V, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<E, Func<S, Func<V, Func<C, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<E, Action<A>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<S, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<C, Func<S, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<E, Func<S, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<C, Func<S, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<E, Func<S, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<E, Func<S, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<C, Func<S, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<C, Func<E, Func<V, Func<E, Func<S, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<E, Func<V, Func<C, Func<S, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<E, Func<V, Func<S, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<V, Func<S, Func<E, Func<A, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<V, Func<S, Func<E, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<V, Func<E, Func<V, Func<C, Func<V, Func<S, Func<C, Func<A, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<E, Func<S, Func<C, Func<E, Func<C, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<S, Func<E, Func<C, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<C, Func<S, Func<E, Func<E, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<S, Func<E, Func<E, Action<V>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<C, Func<S, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<C, Func<S, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<E, Func<S, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<C, Func<S, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<E, Func<S, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<C, Func<S, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<C, Func<E, Func<E, Func<S, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<E, Func<C, Func<E, Func<C, Func<S, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<E, Func<E, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<E, Func<C, Func<E, Func<S, Func<E, Func<V, Action<E>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<E, Func<C, Func<E, Func<S, Func<E, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
        public BinderComponent Add(Func<E, Func<C, Func<E, Func<C, Func<E, Func<S, Func<C, Func<V, Action<C>>>>>>>>> p) { return Update(p.TryApply(Vals.FindAll(CheckRules).Take(9)), 9); }
    }
}
#pragma warning restore 1591