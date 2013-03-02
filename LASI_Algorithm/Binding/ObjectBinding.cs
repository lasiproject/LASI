using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    class PhraseWiseObjectBinder
    {
        public PhraseWiseObjectBinder(VerbPhrase bindingTarget, IEnumerable<Phrase> remainingPhrases) {
            _bindingTarget = bindingTarget;

            inputstream = new Stack<Phrase>(remainingPhrases);

        }

        protected IEntity directObject;
        protected IEntity indirectObject;
        protected Stack<IEntity> entities;

        protected Stack<Phrase> inputstream;
        protected VerbPhrase _bindingTarget;
        internal abstract class State
        {
            protected State(PhraseWiseObjectBinder machine) {
                _machine = machine;
            }
            private string stateName;

            protected string StateName {
                get {
                    return stateName;
                }
                set {
                    stateName = value;
                }
            }
            private int stateNumber;
            private PhraseWiseObjectBinder _machine;

            public PhraseWiseObjectBinder Machine {
                get {
                    return _machine;
                }
                set {
                    _machine = value;
                }
            }

            protected int StateNumber {
                get {
                    return stateNumber;
                }
                set {
                    stateNumber = value;
                }
            }


            public virtual State ProcessNext(Phrase error) {
                throw new InvalidStateTransitionException(stateNumber, error);
            }
        }
        internal class State0 : State
        {
            protected State0(PhraseWiseObjectBinder machine)
                : base(machine) {
            }
            public virtual State ProcessNext(NounPhrase nounPhrase) {
                Machine.entities.Push(nounPhrase);
                return new State1(Machine);

            }

        }


        internal class State1 : State
        {


            public State1(PhraseWiseObjectBinder Machine)
                : base(Machine) {
            }
        }
    }


    static class DynamicStackExtensions
    {
        internal static dynamic PopDynamic(this Stack<Phrase> stack) {
            return stack.Pop();
        }
    }

}
