using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.StateMachines
{
    abstract class WordBindingState
    {
        public abstract List<WordBindingState> Transitions {
            get;
            protected set;
        }
        public abstract WordBindingState PreviousState {
            get;
            protected set;
        }
        protected readonly StateKind stateKind;
        public abstract WordBindingState Transition(Verb verb);
        public abstract WordBindingState Transition(Noun noun);

    }
}
