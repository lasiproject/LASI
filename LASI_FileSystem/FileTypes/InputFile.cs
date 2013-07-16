using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    /// <summary>
    /// This class serves as a wrapper around a file path, providing for direct access to the indvidual components of the file path.
    /// </summary>
    public abstract class InputFile
    {
        /// <summary>
        /// Initializes a new instance of the InputFile class wrapping the provided filepath.
        /// </summary>
        /// <param name="path">The path to the file</param>
        public InputFile(string path) {
            if (!System.IO.File.Exists(path))
                throw new System.IO.FileNotFoundException("File Not Found.", path);
            fileData = new FileData(path);
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

        public override bool Equals(object obj) {
            return this == obj as InputFile;
        }

        public override int GetHashCode() {
            return fileData.GetHashCode();
        }
        /// <summary>
        /// Returns a string prepsentation of the InputFile, including its full path.
        /// </summary>
        /// <returns>A string prepsentation of the InputFile, including its full path.</returns>
        public override string ToString() {
            return this.GetType() + fileData.ToString();
        }
        public static bool operator ==(InputFile lhs, InputFile rhs) {
            if (lhs as object == null && rhs as object == null)
                return true;
            else if (rhs as object == null || lhs as object == null)
                return false;
            else
                return lhs.fileData == rhs.fileData;
            //return lhs.Directory == rhs.Directory && lhs.Ext == rhs.Ext && lhs.FullPath == rhs.FullPath && lhs.Name == rhs.Name && lhs.NameSansExt == rhs.NameSansExt && lhs.PathSansExt == rhs.PathSansExt;
        }
        public static bool operator !=(InputFile lhs, InputFile rhs) {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Provides encapsulated access to underlying path information.
        /// </summary>
        private FileData fileData;

    }
}
