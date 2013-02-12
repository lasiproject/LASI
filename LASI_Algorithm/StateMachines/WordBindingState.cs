using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
namespace LASI.Algorithm.StateMachines
{
    abstract class BindingState
    {
        protected BindingState(StateKind stateKind, string stateName, KeyedByTypeCollection<Delegate> transitionProvided) {
            this.stateKind = stateKind;
            this.stateName = stateName;
        }
        protected readonly string stateName;
        protected readonly StateKind stateKind = StateKind.Initial;
    }

    class WordBindingCollection : BindingState
    {
        internal WordBindingCollection(StateKind stateKind, string stateName, KeyedByTypeCollection<Delegate> transitionsProvided)
            : base(stateKind, stateName, transitionsProvided) {
        }
    }
}
