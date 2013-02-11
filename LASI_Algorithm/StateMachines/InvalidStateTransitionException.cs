using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.StateMachines
{
    class InvalidStateTransitionException : StateMachineException
    {
        public InvalidStateTransitionException(int stateNumber, Phrase pharse)
            : base(String.Format("No transition defined from state \"{0}\" for phrase type {1}", pharse.GetType().Name)) {
        }
        public InvalidStateTransitionException(string stateName, Phrase pharse)
            : base(String.Format("No transition defined from state \"{0}\" for phrase type {1}", pharse.GetType().Name)) {
        }
    }
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
