using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Content
{
    /// <summary>
    /// The Exception that is thrown when an attempt is made to construct a 
    /// strongly typed file Wrapper around a file with a different extension than the wrappers Type allows for.
    /// </summary>
    [Serializable]
    public class FileTypeWrapperMismatchException : ContentFileException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FileTypeWrapperMismatchException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException"/> class specifying the name of the wrapper and the actual extension of the file.
        /// </summary>
        /// <param name="wrapperName">The name of the wrapper.</param>
        /// <param name="actualExtension">The actual extension of the file the wrapper was instantiated around.</param>
        public FileTypeWrapperMismatchException(string wrapperName, string actualExtension)
            : base($"Mismatch between\nWrapper Type: {wrapperName} and File Extension{actualExtension}")
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException"/> class with the specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public FileTypeWrapperMismatchException(string message, Exception inner)
            : base(message, inner)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information
        /// about the source or destination.
        /// </param>
        protected FileTypeWrapperMismatchException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
