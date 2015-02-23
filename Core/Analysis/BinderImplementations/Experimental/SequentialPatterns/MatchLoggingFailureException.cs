using System;
using System.Runtime.Serialization;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    /// <summary>
    /// The exception which is thrown when an error occurs in the Sequential Matching logging process.
    /// </summary>
    /// <seealso cref="SequenceMatch"/>
    /// <seealso cref="SequentialPatterns"/>
    [Serializable]
    public class MatchLoggingFailureException : Exception
    {
        /// <summary>
        /// Intializes a new instance of the MatchLoggingFailureException class.
        /// </summary>
        public MatchLoggingFailureException() { }
        /// <summary>
        /// Initializes a new instance of the MatchLoggingFailureException class with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MatchLoggingFailureException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the MatchLoggingFailureException class with the specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MatchLoggingFailureException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        ///  Initializes a new instance of the System.Exception class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/>  that contains contextual information
        /// about the source or destination.
        /// </param>
        protected MatchLoggingFailureException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}