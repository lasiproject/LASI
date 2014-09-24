using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.ContentSystem
{
    /// <summary>
    /// The Exception that is thrown when an attempt is made to construct a 
    /// strongly typed file Wrapper around a file with a different extension than the wrappers Type allows for.
    /// </summary>
    [Serializable]
    public class FileTypeWrapperMismatchException : FileSystemException
    {
        internal FileTypeWrapperMismatchException(string message)
            : base(message) {
        }
        internal FileTypeWrapperMismatchException(string wrapperName, string actualExtension)
            : base(string.Format("Mismatch between\nWrapper Type: {0} and File Extension{1}", wrapperName, actualExtension)) {
        }
        internal FileTypeWrapperMismatchException(string message, Exception inner)
            : base(message, inner) {
        }
        internal FileTypeWrapperMismatchException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
