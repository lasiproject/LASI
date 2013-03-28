using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.FileSystem.FileTypes
{
    [Serializable]
    class FileTypeWrapperMismatchException : FileSystemException
    {
        public FileTypeWrapperMismatchException(string message)
            : base(message) {
        }
        public FileTypeWrapperMismatchException(string wrapperName, string actualExtension)
            : base(String.Format("Type mismatch between\nWrapper Type: {0} and File Extension{1}", wrapperName, actualExtension)) {
        }
        public FileTypeWrapperMismatchException(string message, Exception inner)
            : base(message, inner) {
        }
        public FileTypeWrapperMismatchException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
