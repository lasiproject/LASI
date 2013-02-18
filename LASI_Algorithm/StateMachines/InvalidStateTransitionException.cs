using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.StateMachines
{
    [Serializable]
    class InvalidStateTransitionException : StateMachineException
    {
        public InvalidStateTransitionException(int stateNumber, Phrase phrase)
            : base(String.Format("No transition defined from state \"{0}\" for phrase type {1}", stateNumber, phrase.GetType().Name)) {
        }
        public InvalidStateTransitionException(string stateName, Phrase phrase)
            : base(String.Format("No transition defined from state \"{0}\" for phrase type {1}", stateName, phrase.GetType().Name)) {
        }
    }
    [Serializable]
    public class StateMachineException : Exception
    {
        public StateMachineException(string message)
            : base(message) {
        }
        public StateMachineException(string message, Exception inner)
            : base(message, inner) {
        }
        public StateMachineException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
