using System;
namespace LASI.ContentSystem
{
    /// <summary>
    /// Stores and differentiates distinct, as well as overlapping, aspects of a file path.
    /// </summary>
    internal struct FileData
    {
        #region Constructors

        /// <summary>
        /// Constructs a new instance from the given pararameters.
        /// </summary>
        /// <param name="directory">The full path to a file, not including the file name itself.</param>
        /// <param name="fileName">The name of the file, not including the file extension.</param>
        /// <param name="fileExt">The extension of the file.</param>
        public FileData(string directory, string fileName, string fileExt)
            : this()
        {
            Directory = directory;
            FileNameSansExt = fileName;
            Ext = fileExt;
            FileName = fileName + fileExt;
            FullPathAndExt = directory + fileName + fileExt;
            FullPathSansExt = directory + fileName;
        }
        /// <summary>
        /// Constructs a new instance from the given pararameters.
        /// </summary>
        /// <param name="directory">The full newPath to the file, not including the file name itself.</param>
        /// <param name="fileNameWithExt">The name of the file, including the file extension.</param>
        public FileData(string directory, string fileNameWithExt)
            : this()
        {
            Directory = directory;
            FileName = fileNameWithExt;
            try {
                Ext = FileName.Substring(FileName.LastIndexOf('.'));
                FileNameSansExt = FileName.Substring(0, FileName.LastIndexOf('.'));
                FullPathAndExt = directory + fileNameWithExt;
            }
            catch (ArgumentOutOfRangeException) {
                Ext = "";
                FileNameSansExt = FileName;

            }

            FullPathAndExt = Directory + FileNameSansExt + Ext;
            FullPathSansExt = Directory + FileNameSansExt;
        }
        /// <summary>
        /// Constructs a new instance from the given pararameters.
        /// </summary>
        /// <param name="fileNameWithPathAndExt">The full newPath, filename, and file extension of the file as single, non escaped, string.</param>
        public FileData(string fileNameWithPathAndExt)
            : this()
        {
            Directory = fileNameWithPathAndExt.Substring(0, fileNameWithPathAndExt.LastIndexOf('\\') + 1);
            FileName = fileNameWithPathAndExt.Substring(fileNameWithPathAndExt.LastIndexOf('\\') + 1);


            try {
                Ext = FileName.Substring(FileName.LastIndexOf('.'));
                FileNameSansExt = FileName.Substring(0, FileName.LastIndexOf('.'));
                FullPathSansExt = Directory + FileNameSansExt;

            }
            catch (ArgumentOutOfRangeException) {
                Ext = "";
                FileNameSansExt = FileName;
                FullPathSansExt = Directory + FileNameSansExt;
            }
            FullPathAndExt = Directory + FileNameSansExt + Ext;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Returns a string prepsentation of the FileData, containing its directory path and full name.
        /// </summary>
        /// <returns>A string prepsentation of the FileData, containing its directory path and full name.</returns>
        public override string ToString()
        {
            return string.Format("  -  File:  {0}, Location:  {1}", FileName, Directory);
        }
        /// <summary>
        /// Determines if the current instance is equal to the given object.
        /// </summary> 
        /// <param name="obj">The object to equate to the current instance.</param>
        /// <returns>True if the two instances should be considered equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            try {
                return this == (FileData)obj;
            }
            catch (InvalidCastException) {
                return false;
            }
        }
        /// <summary>
        /// Gets the hash code of the FileData.
        /// </summary>
        /// <returns>The hash code of the FileData.</returns>
        public override int GetHashCode()
        {
            return FullPathAndExt.GetHashCode();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the full path of the directory in which the file resides.
        /// </summary>
        public string Directory
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the extension of the file.
        /// </summary>
        public string Ext
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string FileName
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the name of the file, not encluding its extension.
        /// </summary>
        public string FileNameSansExt
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the full path of the file.
        /// </summary>
        public string FullPathAndExt
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the full path of the file, not encluding its extension..
        /// </summary>
        public string FullPathSansExt
        {
            get;
            private set;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Determines if two instances of the FileData struct should be consisdered equal.
        /// </summary>
        /// <param name="A">The first FileData</param>
        /// <param name="B">The second FileData</param>
        /// <returns>True if the two instances should be considered equal, false otherwise.</returns>
        public static bool operator ==(FileData A, FileData B)
        {
            return string.Equals(A.FullPathAndExt, B.FullPathAndExt, StringComparison.OrdinalIgnoreCase);

        }
        /// <summary>
        /// Determines if two instances of the FileData struct should be consisdered unequal.
        /// </summary>
        /// <param name="A">The first FileData</param>
        /// <param name="B">The second FileData</param>
        /// <returns>True if the two instances should be considered unequal, false otherwise.</returns>
        public static bool operator !=(FileData A, FileData B)
        {
            return !(A == B);
        }

        #endregion
    }
}
