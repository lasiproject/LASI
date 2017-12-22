using System;
using LASI.Content.Exceptions;
using LASI.Utilities;

namespace LASI.Content.FileTypes
{
    public abstract class InputFile<T> : InputFile where T : InputFile
    {

        protected InputFile(string path) : base(path)
        {
            if (!Extension.EqualsIgnoreCase(CanonicalExtension))
            {
                throw FileTypeWrapperMismatch();
            }
        }
        private protected FileTypeWrapperMismatchException FileTypeWrapperMismatch() => new FileTypeWrapperMismatchException(this);
        public sealed override bool Equals(InputFile other) => other is InputFile<T> it && base.Equals(it);

        [Serializable]
        private protected sealed class FileTypeWrapperMismatchException : FileTypeWrapperMismatchException<T>
        {
            public FileTypeWrapperMismatchException(InputFile<T> inputFile) : base(inputFile.Extension) { }

            public FileTypeWrapperMismatchException(string message) : base(message) { }
            public FileTypeWrapperMismatchException(string message, FileTypeWrapperMismatchException inner) : base(message, inner) { }
        }
    }
}
