using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm.Binding
{
    /// <summary>
    /// The Exception which is to be thrown when attempting to transition from a state on a lexical Type for which no transition has been defined.
    /// </summary>
    [Serializable]
    public class InvalidStateTransitionException : Exception
    {
        /// <summary>
        /// Initializes a new isntance of the InvalidStateTransitionException with a message indicating the state where the error occured and the ILexical instance whicg caused the error.
        /// </summary>
        /// <param name="stateNumber">The number representing the State where the error occured.</param>
        /// <param name="errorOn">The ILeixcal instance which caused the error.</param>
        public InvalidStateTransitionException(int stateNumber, ILexical errorOn)
            : base(string.Format("Invalid Transition\nAt State {0}\nOn {1}", stateNumber, errorOn)) {
        }
        /// <summary>
        /// Initializes a new isntance of the InvalidStateTransitionException with a message indicating the state where the error occured and the ILexical instance whicg caused the error.
        /// </summary>
        /// <param name="stateName">The number representing the State where the error occured.</param>
        /// <param name="errorOn">The ILeixcal instance which caused the error.</param>
        public InvalidStateTransitionException(string stateName, ILexical errorOn)
            : base(string.Format("Invalid Transition\nAt State {0}\nOn {1}", stateName, errorOn))
        {
        }
        private InvalidStateTransitionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception which is thrown when an attempt is made to perform VerbPhrase related binding over a sequence of Phrases which contain no VerbPhrases.
    /// </summary>
    [Serializable]
    public class VerblessPhrasalSequenceException : Exception
    {
        /// <summary>
        /// Initializes a new isntance of the VerblessPhrasalSequenceException with a defualt message indicating that the sequence contained no VerbPhrase instances.
        /// </summary>
        public VerblessPhrasalSequenceException()
            : base("No verb phrases in sequence") {
        }
        private VerblessPhrasalSequenceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
