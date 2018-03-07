using System;
using System.Threading.Tasks;
using LASI.Content.Exceptions;

namespace LASI.Content.FileTypes
{
    /// <summary>
    /// This class serves as a wrapper around a file path, providing for direct access to the individual components of the file path.
    /// </summary>
    public abstract class InputFile : IRawTextSource, IEquatable<InputFile>
    {
        /// <summary>
        /// Initializes a new instance of the InputFile class wrapping the provided file path.
        /// </summary>
        /// <param name="path">The absolute or relative path of the file</param>
        protected InputFile(string path)
        {
            var infile = new System.IO.FileInfo(path);
            if (!infile.Exists)
            {
                throw new System.IO.FileNotFoundException("File Not Found.", infile.FullName);
            }

            fileData = new FileData(infile.FullName);
        }
        /// <summary>
        /// The full file path, including the file name and extension of the file.
        /// </summary>
        public string FullPath => fileData.FullPathAndExt;

        /// <summary>
        /// The file path, including the file name, but not the extension, of the file.
        /// </summary>
        public string PathSansExt => fileData.FullPathSansExt;

        /// <summary>
        /// The filename, including its extension.
        /// </summary>
        public string FileName => fileData.FileName;

        /// <summary>
        /// The filename, not including its extension.
        /// </summary>
        public string NameSansExt => fileData.FileNameSansExt;

        /// <summary>
        /// The extension of the file.
        /// </summary>
        public string Extension => fileData.Extension;

        /// <summary>
        /// The full path of the directory in which the file resides.
        /// </summary>
        public string Directory => fileData.Directory;

        /// <summary>
        /// Determines if the wrapped file exists.
        /// </summary>
        /// <returns> <c>true</c> if the wrapped file exists; otherwise, <c>false</c>.</returns>
        public bool Exists() => fileData.Exists();

        /// <summary>
        /// Returns a value that indicates whether the specified InputFile is equal to the current InputFile.
        /// </summary>
        /// <param name="other">The InputFile to compare with.</param>
        /// <returns> <c>true</c> if the specified InputFile is equal to the current InputFile; otherwise, <c>false</c>.</returns>
        public virtual bool Equals(InputFile other) => fileData == other.fileData;
        /// <summary>
        /// Returns a value that indicates whether the specified object is equal to the current InputFile.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns> <c>true</c> if the specified object is equal to the current InputFile; otherwise, <c>false</c>.</returns>
        public sealed override bool Equals(object obj) => obj is InputFile f && Equals(f);
        /// <summary>
        /// Gets the hash code of the InputFile.
        /// </summary>
        /// <returns>The hash code of the InputFile.</returns>
        public override int GetHashCode() => fileData.GetHashCode();
        /// <summary>
        /// Returns a string representation of the InputFile, including its full path.
        /// </summary>
        /// <returns>A string representation of the InputFile, including its full path.</returns>
        public override string ToString() => $"{GetType()}: {FileName} in: {Directory}";
        /// <summary>
        /// Returns a single string containing all of the text in the InputFile.
        /// </summary>
        /// <returns>A string containing all of the text in the InputFile.</returns>
        public abstract string LoadText();

        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the InputFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the InputFile.</returns>
        public abstract Task<string> LoadTextAsync();
        /// <summary>
        /// The simple file name of the InputFile. This does not include its extension.
        /// </summary>
        public string Name => NameSansExt;

        /// <summary>
        /// The canonical file extension for the associated input file format.
        /// </summary>
        public abstract string CanonicalExtension { get; }

        /// <summary>
        /// Provides encapsulated access to underlying path information.
        /// </summary>
        private readonly FileData fileData;

        /// <summary>
        /// Returns a value that indicates whether the InputFile on the left is equal to the InputFile on the right.
        /// Equality is defined by the <see cref="Type"/> and full paths of the operands.
        /// </summary>
        /// <param name="left">The InputFile on the left.</param>
        /// <param name="right">The InputFile on the right.</param>
        /// <returns> <c>true</c> if the InputFile on the left is equal to the InputFile on the right.</returns>
        public static bool operator ==(InputFile left, InputFile right) => Equals(left, right);

        /// <summary>
        /// Returns a value that indicates whether the InputFile on the left is not equal to the InputFile on the right.
        /// Equality is defined by the full <see cref="Type"/>s and paths of the operands.
        /// </summary>
        /// <param name="left">The InputFile on the left.</param>
        /// <param name="right">The InputFile on the right.</param>
        /// <returns> <c>true</c> if the InputFile on the left is not equal to the InputFile on the right.</returns>
        public static bool operator !=(InputFile left, InputFile right) => !(left == right);

        protected FileConversionFailureException CreateFileConversionFailureException(string targetExtension, Exception e) =>
            new FileConversionFailureException(FullPath, CanonicalExtension, targetExtension, e);
    }
}
