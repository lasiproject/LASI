using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm.Binding
{
    /// <summary>
    /// An Exception which is to be thrown when attempting to transition from a state on a lexical NounPointerSymbol for which no transition has been defined.
    /// </summary>
    [Serializable]
    public class InvalidStateTransitionException : Exception
    {
        public InvalidStateTransitionException(int stateNumber, ILexical errorOn)
            : base(String.Format("Invalid Transition\nAt State {0}\nOn {1}", stateNumber, errorOn)) {
        }
        public InvalidStateTransitionException(string stateName, ILexical errorOn)
            : base(String.Format("Invalid Transition\nAt State {0}\nOn {1}", stateName, errorOn)) {
        }
        public InvalidStateTransitionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    [Serializable]
    public class VerblessPhrasalSequenceException : Exception
    {
        public VerblessPhrasalSequenceException()
            : base("No verb phrases in sequence") {
        }
        public VerblessPhrasalSequenceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
