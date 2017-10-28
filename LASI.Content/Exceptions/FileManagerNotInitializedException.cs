using System;
using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
using StreamingContext = System.Runtime.Serialization.StreamingContext;

namespace LASI.Content
{

    /// <summary>
    /// The exception thrown when methods are invoked or properties accessed on the FileManager before a call has been made to initialize it.
    /// </summary>
    [Serializable]
    public sealed class FileManagerNotInitializedException : FileManagerException
    {
        /// <summary>
        /// Initializes a new instance of the FileManagerNotInitializedException class with its message string set to message.
        /// </summary> 
        public FileManagerNotInitializedException()
            : this("File Manager has not been initialized. No directory context in which to operate.")
        {
        }

        private FileManagerNotInitializedException(string message) : base(message) { }

        public FileManagerNotInitializedException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileManagerNotInitializedException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information
        /// about the source or destination.
        /// </param>
        private FileManagerNotInitializedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}