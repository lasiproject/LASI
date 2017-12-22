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
            : base($"Mismatch between\nWrapper Type: {typeof(TWrapper)} and File Extension: {mistmatchedExtension}")
        {
            MistmatchedExtension = mistmatchedExtension;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeWrapperMismatchException{TWrapper}"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="
        /// SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information
        /// about the source or destination.
        /// </param>
        public FileTypeWrapperMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            MistmatchedExtension = info.GetString(nameof(MistmatchedExtension));
        }

        public FileTypeWrapperMismatchException(string message, Exception inner) : base(message, inner)
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

        public sealed override string ToString() => base.ToString();

        public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(MistmatchedExtension), MistmatchedExtension, typeof(string));
        }

        public FileTypeWrapperMismatchException()
        {
        }

        public string MistmatchedExtension { get; }
    }
}
