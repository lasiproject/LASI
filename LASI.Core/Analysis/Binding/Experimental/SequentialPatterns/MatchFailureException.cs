using System;
using System.Runtime.Serialization;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    /// <summary>
    /// An exception resulting from a pattern matching failure due to an API use error.
    /// </summary>
    [Serializable]
    public class MatchFailureException : Exception
    {
        public Delegate FailedCase { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchFailureException"/> class.
        /// </summary>
        public MatchFailureException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchFailureException"/> class.
        /// </summary>
        /// <param name="failedCase">The pattern which caused the error.</param>
        /// <param name="message">A message describing why or how the match failed.</param>
        public MatchFailureException(Delegate failedCase, string message) : base(message) { FailedCase = failedCase; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchFailureException"/> class with the given message.
        /// </summary>
        /// <param name="message">A message describing why or how the match failed.</param>
        public MatchFailureException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchFailureException"/> class with the given message and inner exception.
        /// </summary>
        /// <param name="message">A message describing why or how the match failed.</param>
        /// <param name="inner">The exception which propagated the failure.</param>
        public MatchFailureException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        ///  Initializes a new instance of the <see cref="MatchFailureException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/>  that contains contextual information
        /// about the source or destination.
        /// </param>
        protected MatchFailureException(
          SerializationInfo info,
          StreamingContext context) : base(info, context)
        { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FailedCase), FailedCase, typeof(Delegate));
        }
    }
}
