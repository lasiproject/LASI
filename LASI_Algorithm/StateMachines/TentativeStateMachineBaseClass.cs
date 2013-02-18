using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm.StateMachines
{
    using StateBase = PossiblePhraseStateBaseClass;
    
    internal abstract class PossiblePhraseStateBaseClass
    {

        #region Transitions

        public abstract PossiblePhraseStateBaseClass TransitionWith(NounPhrase n);
        public abstract PossiblePhraseStateBaseClass TransitionWith(VerbPhrase v);
        public abstract PossiblePhraseStateBaseClass TransitionWith(TransitiveVerbPhrase v);
        public abstract PossiblePhraseStateBaseClass TransitionWith(AdverbPhrase a);
        public abstract PossiblePhraseStateBaseClass TransitionWith(AdjectivePhrase a);
        public abstract PossiblePhraseStateBaseClass TransitionWith(PrepositionalPhrase a);
        public abstract PossiblePhraseStateBaseClass TransitionWith(PronounPhrase p);
        public abstract PossiblePhraseStateBaseClass TransitionWith(ParticlePhrase p);
        public abstract PossiblePhraseStateBaseClass TransitionWith(ConjunctionPhrase a);
        public abstract PossiblePhraseStateBaseClass TransitionWith(RoughListPhrase a);

        #endregion

        public abstract StateKind StateKind {
            get;
            protected set;
        }

    }

    internal class State0 : StateBase
    {

        public State0(StateKind stateKind) {
            StateKind = stateKind;
        }

        public override StateBase TransitionWith(NounPhrase n) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(VerbPhrase v) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(TransitiveVerbPhrase v) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(AdverbPhrase a) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(AdjectivePhrase a) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(PrepositionalPhrase a) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(PronounPhrase p) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(ParticlePhrase p) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(ConjunctionPhrase a) {
            throw new NotImplementedException();
        }

        public override StateBase TransitionWith(RoughListPhrase a) {
            throw new NotImplementedException();
        }

        public override StateKind StateKind {
            get;
            protected set;
        }
    }

}
