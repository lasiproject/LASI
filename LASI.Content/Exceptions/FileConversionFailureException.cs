using System;
using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
using StreamingContext = System.Runtime.Serialization.StreamingContext;

namespace LASI.Content
{
    /// <summary>
    /// The exception thrown when a conversion between document file formats fails.
    /// </summary>
    [Serializable]
    public class FileConversionFailureException : FileManagerException
    {
        public FileConversionFailureException() : base("File conversion failed.") { }
        /// <summary>
        /// Initializes a new instance of the FileConversionFailureException with a message based on the supplied fileName, source type, and target type
        /// </summary>
        /// <param name="fileName">The name of the file for which conversion failed.</param>
        /// <param name="sourceType">The extension of the source file format.</param>
        /// <param name="targetType">The extension of the target file format</param>
        public FileConversionFailureException(string fileName, string sourceType, string targetType) : base($"File conversion failed\nCould not convert {fileName} from {sourceType} to {targetType}.") { }
        /// <summary>
        /// Initializes a new instance of the FileConversionFailureException with a message based on the supplied fileName, source type, and target type
        /// </summary>
        /// <param name="fileName">The name of the file for which conversion failed.</param>
        /// <param name="sourceType">The extension of the source file format.</param>
        /// <param name="targetType">The extension of the target file format</param>
        /// <param name="inner">The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.</param>
        public FileConversionFailureException(string fileName, string sourceType, string targetType, Exception inner) : base($"File conversion failed\nCould not convert {fileName} from {sourceType} to {targetType}.", inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileConversionFailureException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FileConversionFailureException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileConversionFailureException"/> class with the specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public FileConversionFailureException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileConversionFailureException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information
        /// about the source or destination.
        /// </param>
        protected FileConversionFailureException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}