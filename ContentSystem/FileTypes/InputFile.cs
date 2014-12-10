using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Content
{
    /// <summary>
    /// This class serves as a wrapper around a file path, providing for direct access to the indvidual components of the file path.
    /// </summary>
    public abstract class InputFile : IRawTextSource
    {
        /// <summary>
        /// Initializes a new instance of the InputFile class wrapping the provided filepath.
        /// </summary>
        /// <param name="path">The absolute or relative path of the file</param>
        protected InputFile(string path) {
            var infile = new System.IO.FileInfo(path);
            if (!infile.Exists)
                throw new System.IO.FileNotFoundException("File Not Found.", infile.FullName);
            fileData = new FileData(infile.FullName);
        }
        /// <summary>
        /// Gets the full file path, including the file name and extension of the file.
        /// </summary>
        public string FullPath {
            get {
                return fileData.FullPathAndExt;
            }
        }
        /// <summary>
        /// Gets the file path, including the file name, but not the extension, of the file.
        /// </summary>
        public string PathSansExt {
            get {
                return fileData.FullPathSansExt;
            }
        }
        /// <summary>
        /// Gets the filename, including its extension.
        /// </summary>
        public string FileName {
            get {
                return fileData.FileName;
            }
        }
        /// <summary>
        /// Gets the filename, not including its extension.
        /// </summary>
        public string NameSansExt {
            get {
                return fileData.FileNameSansExt;
            }
        }
        /// <summary>
        /// Gets the extension of the file.
        /// </summary>
        public string Ext {
            get {
                return fileData.Ext;
            }
        }
        /// <summary>
        /// Gets the full path of the directory in which the file resides.
        /// </summary>
        public string Directory {
            get {
                return fileData.Directory;
            }
        }
        /// <summary>
        /// Returns a value that indicates whether the specified object is equal to the current InputFile.
        /// </summary>
        /// <param name="obj">The object to compare with.</param> 
        /// <returns>True if the specified object is equal to the current InputFile; otherwise, false.</returns> 
        public override bool Equals(object obj) {
            return this == obj as InputFile;
        }
        /// <summary>
        /// Gets the hash code of the InputFile.
        /// </summary>
        /// <returns>The hash code of the InputFile.</returns>
        public override int GetHashCode() {
            return fileData.GetHashCode();
        }
        /// <summary>
        /// Returns a string prepsentation of the InputFile, including its full path.
        /// </summary>
        /// <returns>A string prepsentation of the InputFile, including its full path.</returns>
        public override string ToString() {
            return string.Format("{0}: {1} in: {2}", this.GetType(), FileName, Directory);
        }
        /// <summary>
        /// Returns a value that indicates whether the InputFile on the left is equal to the InputFile on the right.
        /// </summary>
        /// <param name="left">The InputFile on the left.</param>
        /// <param name="right">The InputFile on the right.</param>
        /// <returns>True if the InputFile on the left is equal to the InputFile on the right.</returns>
        public static bool operator ==(InputFile left, InputFile right) {
            if (left as object == null && right as object == null) {
                return true;
            } else if (right as object == null || left as object == null) {
                return false;
            } else {
                return left.fileData == right.fileData;
            }
        }
        /// <summary>
        /// Returns a value that indicates whether the InputFile on the left is not equal to the InputFile on the right.
        /// </summary>
        /// <param name="left">The InputFile on the left.</param>
        /// <param name="right">The InputFile on the right.</param>
        /// <returns>True if the InputFile on the left is not equal to the InputFile on the right.</returns>
        public static bool operator !=(InputFile left, InputFile right) {
            return !(left == right);
        }

        /// <summary>
        /// Provides encapsulated access to underlying path information.
        /// </summary>
        private FileData fileData;

        /// <summary>
        /// Returns a single string containing all of the text in the InputFile.
        /// </summary>
        /// <returns>A string containing all of the text in the InputFile.</returns>
        public abstract string GetText();
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the InputFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the InputFile.</returns>
        public abstract Task<string> GetTextAsync();
        /// <summary>
        /// Gets the simple file name of the InputFile. This does not include its extension.
        /// </summary>
        public string SourceName {
            get { return NameSansExt; }
        }
    }
}
