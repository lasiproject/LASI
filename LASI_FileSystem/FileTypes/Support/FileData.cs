using System;
namespace LASI.FileSystem
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
        /// <param name="fileName">The name of a file, not including the file extension.</param>
        /// <param name="fileExt">The extension of a file.</param>
        public FileData(string directory, string fileName, string fileExt)
            : this() {
            Directory = directory;
            FileNameSansExt = fileName;
            FileExt = fileExt;
            FileNameWithExt = fileName + fileExt;
            FullPathAndExt = directory + fileName + fileExt;
            FullPathSansExt = directory + fileName;
        }
        /// <summary>
        /// Constructs a new instance from the given pararameters.
        /// </summary>
        /// <param name="directory">The full path to a file, not including the file name itself.</param>
        /// <param name="fileNameWithExt">The name of a file, including the file extension.</param>
        public FileData(string directory, string fileNameWithExt)
            : this() {
            Directory = directory;
            FileNameWithExt = fileNameWithExt;
            try {
                FileExt = FileNameWithExt.Substring(FileNameWithExt.LastIndexOf('.'));
                FileNameSansExt = FileNameWithExt.Substring(0, FileNameWithExt.LastIndexOf('.'));
                FullPathAndExt = directory + fileNameWithExt;
            } catch (ArgumentOutOfRangeException) {
                FileExt = "";
                FileNameSansExt = FileNameWithExt;

            }

            FullPathAndExt = Directory + FileNameSansExt + FileExt;
            FullPathSansExt = Directory + FileNameSansExt;
        }
        /// <summary>
        /// Constructs a new instance from the given pararameters.
        /// </summary>
        /// <param name="fileNameWithPathAndExt">The full path, filename, and file extension of a file as single, non escaped, string.</param>
        public FileData(string fileNameWithPathAndExt)
            : this() {
            Directory = fileNameWithPathAndExt.Substring(0, fileNameWithPathAndExt.LastIndexOf('\\') + 1);
            FileNameWithExt = fileNameWithPathAndExt.Substring(fileNameWithPathAndExt.LastIndexOf('\\') + 1);


            try {
                FileExt = FileNameWithExt.Substring(FileNameWithExt.LastIndexOf('.'));
                FileNameSansExt = FileNameWithExt.Substring(0, FileNameWithExt.LastIndexOf('.'));
                FullPathSansExt = Directory + FileNameSansExt;

            } catch (ArgumentOutOfRangeException) {
                FileExt = "";
                FileNameSansExt = FileNameWithExt;
                FullPathSansExt = Directory + FileNameSansExt;
            }
            FullPathAndExt = Directory + FileNameSansExt + FileExt;
        }

        #endregion

        #region Methods

        public override string ToString() {
            return base.ToString() + String.Format("  -  File:  {0}, Location:  {1}", FileNameWithExt, Directory);
        }
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
        #endregion

        #region Properties

        public string Directory {
            get;
            set;
        }
        public string FileNameSansExt {
            get;
            set;
        }
        public string FileExt {
            get;
            set;
        }
        public string FileNameWithExt {
            get;
            set;
        }
        public string FullPathAndExt {
            get;
            set;
        }
        public string FullPathSansExt {
            get;
            set;
        }

        #endregion

        #region Operators

        public static bool operator ==(FileData A, FileData B) {
            return A.FullPathAndExt == B.FullPathAndExt;

        }

        public static bool operator !=(FileData A, FileData B) {
            return !(A == B);
        }


        #endregion
    }
}
