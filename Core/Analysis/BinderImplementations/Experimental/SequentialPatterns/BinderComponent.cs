#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
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

    public partial class BinderComponent : IBinderComponent<E, V, C, D, R, A, P>, IBinderComponent<P, E, V, C, D, R, A>, IBinderComponent<A, P, E, V, C, D, R>, IBinderComponent<R, A, P, E, V, C, D>, IBinderComponent<D, R, A, P, E, V, C>, IBinderComponent<R, D, C, A, P, E, V>, IBinderComponent<V, C, D, R, A, P, E>, IBinderComponent<E, R, D, R, A, P, E>, IBinderComponent<E, C, R, E, A, V, E>, IBinderComponent<E, C, E, V, C, V, A>, IBinderComponent<E, E, C, E, C, E, V>, IBinderComponent<S, V, C, D, R, A, P>, IBinderComponent<P, S, V, C, D, R, A>, IBinderComponent<A, P, S, V, C, D, R>, IBinderComponent<R, A, P, S, V, C, D>, IBinderComponent<D, R, A, P, S, V, C>, IBinderComponent<R, D, C, A, S, E, V>, IBinderComponent<V, C, D, R, A, S, E>, IBinderComponent<E, R, D, R, A, P, S>, IBinderComponent<S, C, R, E, A, V, E>, IBinderComponent<E, S, E, V, C, V, A>, IBinderComponent<E, S, C, E, C, E, V>,
        System.Collections.IEnumerable
    {
        private bool accepted;
        //public Phrase Next { get; private set; }
        private IEnumerable<BindingRule> rules;
        public List<Phrase> Vals {
            get; private set;
        }
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

        public BinderComponent Match3(Func<A, Func<A, Action<A>>> pattern) { Update(pattern.TryApply(Vals.FindAll(CheckRules).Take(3)), 3); return this; }

        private BinderComponent Update(bool ac, int skip) {
            Vals = ac ? Vals.Skip(skip) : Vals;
            accepted = ac; return this;
        }

        public System.Collections.IEnumerator GetEnumerator() { yield break; }

    }
}
#pragma warning restore 1591
