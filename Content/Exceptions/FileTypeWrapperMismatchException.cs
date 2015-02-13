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
        internal FileTypeWrapperMismatchException(string message)
            : base(message)
        {
        }
        internal FileTypeWrapperMismatchException(string wrapperName, string actualExtension)
            : base($"Mismatch between\nWrapper Type: {wrapperName} and File Extension{actualExtension}")
        {
        }
        internal FileTypeWrapperMismatchException(string message, Exception inner)
            : base(message, inner)
        {
        }
        internal FileTypeWrapperMismatchException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
