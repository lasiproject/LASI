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

        private static string CreateMessage(string mistmatchedExtension) => $"Mismatch between\nWrapper Type: {typeof(TWrapper)} and File Extension: {mistmatchedExtension}";

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException{TWrapper}"/> class with serialized data.
        /// </summary>
        /// <param name="info">   The object that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The object that holds the serialized object data about the exception being thrown.</param>
        public FileTypeWrapperMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context) =>
                mistmatchedExtension = info.GetString(nameof(mistmatchedExtension));

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException{TWrapper}"/> class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException parameter is not null, the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        public FileTypeWrapperMismatchException(string message, Exception inner) : base(message, inner)
        {
        }

        public FileTypeWrapperMismatchException()
        {
        }

        public sealed override IDictionary Data => base.Data;

        public sealed override string Message => base.Message;

        public sealed override string Source { get => base.Source; set => base.Source = value; }

        public sealed override string HelpLink { get => base.HelpLink; set => base.HelpLink = value; }

        public sealed override string StackTrace => base.StackTrace;

        public sealed override bool Equals(object obj) => base.Equals(obj);

        public sealed override Exception GetBaseException() => base.GetBaseException();

        public sealed override int GetHashCode() => base.GetHashCode();

        public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(mistmatchedExtension), mistmatchedExtension, typeof(string));
        }

        [System.Security.SecuritySafeCritical]
        public override string ToString() => base.ToString();

        private readonly string mistmatchedExtension;
    }
}
