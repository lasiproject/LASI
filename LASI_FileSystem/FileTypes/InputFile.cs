using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    /// <summary>
    /// This class serves as a wrapper around a file path, providing for direct access to the indvidual components of the path.
    /// </summary>
    public abstract class InputFile
    {
        /// <summary>
        /// Initializes a new instance of the InputFile class.
        /// </summary>
        /// <param name="path"></param>
        public InputFile(string path) {
            if (!System.IO.File.Exists(path))
                throw new System.IO.FileNotFoundException();
            FInfo = new FileData(path);
        }
        private FileData FInfo {
            get;
            set;
        }
        /// <summary>
        /// Gets the full path, including the file name and extension of the file.
        /// </summary>
        public string FullPath {
            get {
                return FInfo.FullPathAndExt;
            }
        }
        /// <summary>
        /// Gets the path, including the file name, but not the extension, of the file.
        /// </summary>
        public string PathSansExt {
            get {
                return FInfo.FullPathSansExt;
            }
        }
        /// <summary>
        /// Gets the filename, including its extension.
        /// </summary>
        public string Name {
            get {
                return FInfo.FileNameWithExt;
            }
        }
        /// <summary>
        /// Gets the filename, not including its extension.
        /// </summary>
        public string NameSansExt {
            get {
                return FInfo.FileNameSansExt;
            }
        }
        /// <summary>
        /// Gets the extension of the file.
        /// </summary>
        public string Ext {
            get {
                return FInfo.FileExt;
            }
        }
        /// <summary>
        /// Gets the path of the full path of the directory in which the file resides.
        /// </summary>
        public string Directory {
            get {
                return FInfo.Directory;
            }
        }
        
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
        
        public override string ToString() {
            return FullPath;
        }
        public static bool operator ==(InputFile lhs, InputFile rhs) {
            return lhs.Directory == rhs.Directory && lhs.Ext == rhs.Ext && lhs.FullPath == rhs.FullPath && lhs.Name == rhs.Name && lhs.NameSansExt == rhs.NameSansExt && lhs.PathSansExt == rhs.PathSansExt;
        }
        public static bool operator !=(InputFile lhs, InputFile rhs) {
            return lhs != rhs;
        }

    }
}
