#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core;

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
        private bool accepted;
        //public Phrase Next { get; private set; }
        private IEnumerable<BindingRule> rules;
        public List<Phrase> Vals { get; private set; }
        private bool CheckRules(ILexical arg) {
            foreach (var rule in rules) {
                if (!rule.Test(arg)) { return false; }
            }
            return true;
        }
        BinderComponent Guarding(params BindingRule[] rules) {
            this.rules = rules; return this;
        }
        public BinderComponent Bind(IEnumerable<Phrase> stream) { Vals = stream.ToList(); return this; }



        private BinderComponent Update(bool ac, int skip) {
            Vals = ac ? Vals.Skip(skip) : Vals;
            accepted = ac; return this;
        }
        public System.Collections.IEnumerator GetEnumerator() { yield break; }

        public BinderComponent Add(Func<E, Func<C, Func<S, Action<E>>>> p) {
            throw new NotImplementedException();
        }

        public BinderComponent Add(Func<R, Func<C, Func<D, Action<E>>>> p) {
            throw new NotImplementedException();
        }

        public BinderComponent Add(Func<V, Func<P, Action<D>>> p) {
            throw new NotImplementedException();
        }

        public BinderComponent Add(Func<D, Func<V, Action<A>>> p) {
            throw new NotImplementedException();
        }

        public BinderComponent Add(Func<P, Func<R, Action<E>>> p) {
            throw new NotImplementedException();
        }
    }
}
#pragma warning restore 1591