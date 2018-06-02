using System;
using LASI.Content.Exceptions;
using LASI.Utilities;

namespace LASI.Content.FileTypes
{
    public abstract class InputFile<T> : InputFile, IEquatable<InputFile<T>> where T : InputFile
    {
        #region Constructors

        protected InputFile(string path) : base(path)
        {
            if (!Extension.EqualsIgnoreCase(CanonicalExtension))
            {
                throw FileTypeWrapperMismatch();
            }
        }

        #endregion Constructors

        #region Methods

        public sealed override bool Equals(InputFile other) => base.Equals(other);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true"/> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false"/>.</returns>
        public bool Equals(InputFile<T> other) => other is InputFile<T> it && base.Equals(it);

        private protected FileTypeWrapperMismatchException FileTypeWrapperMismatch() => new FileTypeWrapperMismatchException(this);

        #endregion Methods

        #region Classes

        [Serializable]
        private protected sealed class FileTypeWrapperMismatchException : FileTypeWrapperMismatchException<T>
        {
            public FileTypeWrapperMismatchException(InputFile<T> inputFile) : base(inputFile.Extension)
            {
            }

            public FileTypeWrapperMismatchException(string message) : base(message)
            {
            }

            public FileTypeWrapperMismatchException(string message, FileTypeWrapperMismatchException inner) : base(message, inner)
            {
            }

        }

        #endregion Classes
    }
}