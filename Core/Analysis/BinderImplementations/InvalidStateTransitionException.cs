﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Core.Binding
{
    /// <summary>
    /// The Exception which is to be thrown when attempting to transition from a state on a lexical Type for which no transition has been defined.
    /// </summary>
    [Serializable]
    public class InvalidStateTransitionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidStateTransitionException with a message indicating the state where the error occurred and the ILexical instance which caused the error.
        /// </summary>
        /// <param name="stateNumber">The number representing the State where the error occurred.</param>
        /// <param name="errorOn">The ILeixcal instance which caused the error.</param>
        internal InvalidStateTransitionException(int stateNumber, ILexical errorOn)
            : base(string.Format("Invalid Transition\nAt State {0}\nOn {1}", stateNumber, errorOn)) {
        }
        /// <summary>
        /// Initializes a new instance of the InvalidStateTransitionException with a message indicating the state where the error occurred and the ILexical instance which caused the error.
        /// </summary>
        /// <param name="stateName">The number representing the State where the error occurred.</param>
        /// <param name="errorOn">The ILeixcal instance which caused the error.</param>
        internal InvalidStateTransitionException(string stateName, ILexical errorOn)
            : base(string.Format("Invalid Transition\nAt State {0}\nOn {1}", stateName, errorOn)) {
        }
        protected InvalidStateTransitionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
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
        /// Initializes a new instance of the VerblessPhrasalSequenceException with a default message indicating that the sequence contained no VerbPhrase instances.
        /// </summary>
        internal VerblessPhrasalSequenceException()
            : base("No verb phrases in sequence") {
        }
        protected VerblessPhrasalSequenceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
