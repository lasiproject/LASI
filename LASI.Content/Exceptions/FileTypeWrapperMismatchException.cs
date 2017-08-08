using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LASI.Content.Exceptions
{
    /// <summary>
    /// The Exception that is thrown when an attempt is made to construct a 
    /// strongly typed file Wrapper around a file with a different extension than the wrappers Type allows for.
    /// </summary>
    [Serializable]
    public class FileTypeWrapperMismatchException<TWrapper> : ContentFileException where TWrapper : InputFile
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
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information
        /// about the source or destination.
        /// </param>
        private FileTypeWrapperMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            MistmatchedExtension = info.GetString(nameof(MistmatchedExtension));
        }

        private FileTypeWrapperMismatchException(string message, Exception inner) : base(message, inner)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(MistmatchedExtension), MistmatchedExtension, typeof(string));
        }
        public string MistmatchedExtension { get; }
    }
}
