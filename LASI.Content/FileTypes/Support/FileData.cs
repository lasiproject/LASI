using System;
using LASI.Utilities;

namespace LASI.Content
{
    /// <summary>
    /// Stores and differentiates distinct, as well as overlapping, aspects of a file path.
    /// </summary>
    internal struct FileData : IEquatable<FileData>
    {
        #region Constructors

       
        /// <summary>
        /// Constructs a new instance from the given parameters.
        /// </summary>
        /// <param name="fileNameWithPathAndExt">The complete path, filename, and file extension of the file as single, non escaped, string.</param>
        public FileData(string fileNameWithPathAndExt)
            : this() {
            Directory = fileNameWithPathAndExt.Substring(0, fileNameWithPathAndExt.LastIndexOf('\\') + 1);
            FileName = fileNameWithPathAndExt.Substring(fileNameWithPathAndExt.LastIndexOf('\\') + 1);


            try {
                Extension = FileName.Substring(FileName.LastIndexOf('.'));
                FileNameSansExt = FileName.Substring(0, FileName.LastIndexOf('.'));
                FullPathSansExt = Directory + FileNameSansExt;

            } catch (ArgumentOutOfRangeException) {
                Extension = string.Empty;
                FileNameSansExt = FileName;
                FullPathSansExt = Directory + FileNameSansExt;
            }
            FullPathAndExt = Directory + FileNameSansExt + Extension;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the wrapped path exists.
        /// </summary>
        /// <returns> <c>true</c> if the wrapped path exists; otherwise, <c>false</c>.</returns> 
        public bool Exists() => System.IO.File.Exists(FullPathAndExt);
        
        /// <summary>
        /// Returns a string representation of the FileData, containing its directory path and full name.
        /// </summary>
        /// <returns>A string representation of the FileData, containing its directory path and full name.</returns>
        public override string ToString() => $"  -  File:  {FileName}, Location:  {Directory}";

        /// <summary>
        /// Determines if the current instance is equal to the given FileData.
        /// </summary> 
        /// <param name="other">The FileData to equate to the current instance.</param>
        /// <returns> <c>true</c> if the two instances should be considered equal; otherwise, <c>false</c>.</returns>
        public bool Equals(FileData other) => this == other;

        /// <summary>
        /// Determines if the current instance is equal to the given object.
        /// </summary> 
        /// <param name="obj">The object to equate to the current instance.</param>
        /// <returns> <c>true</c> if the two instances should be considered equal; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj) => obj is FileData && this.Equals((FileData)obj);
        /// <summary>
        /// Gets a hash code for the FileData instance.
        /// </summary>
        /// <returns>A hash code of the current FileData instance.</returns>
        public override int GetHashCode() => FullPathAndExt.GetHashCode();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the full path of the directory in which the file resides.
        /// </summary>
        public string Directory { get; }
        /// <summary>
        /// Gets the extension of the file.
        /// </summary>
        public string Extension { get; }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// Gets the name of the file, not including its extension.
        /// </summary>
        public string FileNameSansExt { get; }
        /// <summary>
        /// Gets the full path of the file.
        /// </summary>
        public string FullPathAndExt { get; }
        /// <summary>
        /// Gets the full path of the file, not including its extension..
        /// </summary>
        public string FullPathSansExt { get; }

        #endregion

        #region Operators

        /// <summary>
        /// Determines if two instances of the FileData structure are equal.
        /// </summary>
        /// <param name="first">The first FileData</param>
        /// <param name="second">The second FileData</param>
        /// <returns> <c>true</c> if two instances of the FileData structure should be considered equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(FileData first, FileData second) => first.FullPathAndExt.EqualsIgnoreCase(second.FullPathAndExt);

        /// <summary>
        /// Determines if two instances of the FileData structure are unequal.
        /// </summary>
        /// <param name="A">The first FileData</param>
        /// <param name="B">The second FileData</param>
        /// <returns> <c>true</c> if two instances of the FileData structure should be considered unequal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(FileData A, FileData B) => !(A == B);

        #endregion
    }
}
