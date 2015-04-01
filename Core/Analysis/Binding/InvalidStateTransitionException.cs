using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities;

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
        /// <param name="stateName">The number representing the State where the error occurred.</param>
        /// <param name="errorOn">The ILeixcal instance which caused the error.</param>
        public InvalidStateTransitionException(string stateName, ILexical errorOn)
            : base(string.Format("Invalid Transition\nAt State {0}\nOn {1}", stateName, errorOn))
        {
        }
        protected InvalidStateTransitionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
  
}
