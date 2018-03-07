using System;
using System.Collections;
using System.Runtime.Serialization;
using LASI.Content.FileTypes;

namespace LASI.Content.Exceptions
{
    /// <summary>
    /// The Exception that is thrown when an attempt is made to construct a
    /// strongly typed file Wrapper around a file with a different extension than the wrappers Type allows for.
    /// </summary>
    [Serializable]
    public class FileTypeWrapperMismatchException<TWrapper> : ContentFileException, ISerializable where TWrapper : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException{TWrapper}"/> class specifying the name of the wrapper and the actual extension of the file.
        /// </summary>
        /// <param name="mistmatchedExtension">The actual extension of the file the wrapper was instantiated around.</param>
        public FileTypeWrapperMismatchException(string mistmatchedExtension)
            : base(CreateMessage(mistmatchedExtension)) => this.mistmatchedExtension = mistmatchedExtension;

        private static string CreateMessage(string mistmatchedExtension) =>
            $"Mismatch between\nWrapper Type: {typeof(TWrapper)} and File Extension: {mistmatchedExtension}";

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException{TWrapper}"/> class with serialized data.
        /// </summary>
        /// <param name="info">   The object that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The object that holds the serialized object data about the exception being thrown.</param>
        public FileTypeWrapperMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context) =>
            mistmatchedExtension = info.GetString(nameof(mistmatchedExtension));

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:LASI.Content.Exceptions.FileTypeWrapperMismatchException`1" /> class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException parameter is not null, the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        protected FileTypeWrapperMismatchException(string message, Exception inner) : base(message, inner) { }

        /// <inheritdoc />
        public FileTypeWrapperMismatchException() { }

        /// <inheritdoc />
        public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(mistmatchedExtension), mistmatchedExtension, typeof(string));
        }

        private readonly string mistmatchedExtension;
    }
}