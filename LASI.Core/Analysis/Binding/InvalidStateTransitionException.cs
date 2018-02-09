using System;

namespace LASI.Core.Heuristics.Binding
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
            : base($"Invalid Transition\nAt State {stateName}\nOn {errorOn}")
        { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="InvalidStateTransitionException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information
        /// about the source or destination.
        /// </param>
        protected InvalidStateTransitionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public InvalidStateTransitionException() { }

        /// <summary>Initializes a new instance of the <see cref="Exception" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        public InvalidStateTransitionException(string message) : base(message) { }

        /// <summary>Initializes a new instance of the <see cref="Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified. </param>
        public InvalidStateTransitionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
