using System;
using LASI.Utilities;
using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
using StreamingContext = System.Runtime.Serialization.StreamingContext;

namespace LASI.Content.Exceptions
{

    /// <summary>
    /// The Exception thrown when an attempt is made to add a file of an unsupported type to a project.
    /// </summary>
    [Serializable]
    public class UnsupportedFileTypeException : FileManagerException
    {
        private static readonly string SupportedFormats = FileManager.WrapperMap.SupportedFormats.Format();
        private static string FormatMessage(string unsupportedFormat) => $"Files of type \"{unsupportedFormat}\" are not supported. Supported types are {SupportedFormats}";

        /// <summary>
        /// Initializes a new instance of the UnsupportedFileTypeException class with its message string set to message.
        /// </summary>
        /// <param name="unsupportedFormat">A description of the error. The content of message is intended to be understood</param>
        public UnsupportedFileTypeException(string unsupportedFormat)
            : base(FormatMessage(unsupportedFormat))
        {
        }
        /// <summary>
        /// Initializes a new instance of the UnsupportedFileTypeException class with its message string set to message.
        /// </summary>
        /// <param name="unsupportedFormat">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UnsupportedFileTypeException(string unsupportedFormat, Exception inner)
            : base(FormatMessage(unsupportedFormat), inner)
        {

        }
        /// <summary>
        ///Initializes a new instance of the UnsupportedFileTypeException class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected UnsupportedFileTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public UnsupportedFileTypeException() { }
    }
}
