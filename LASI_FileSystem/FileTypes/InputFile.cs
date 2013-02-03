using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    public abstract class InputFile
    {
        public InputFile(string path) {
            if (!System.IO.File.Exists(path))
                throw new System.IO.FileNotFoundException();
            FInfo = new FileData(path);
        }
        private FileData FInfo {
            get;
            set;
        }
        public string FullPath {
            get {
                return FInfo.FullPathAndExt;
            }
        }
        public string Name {
            get {
                return FInfo.FileNameWithExt;
            }
        }
        public string NameSansExt {
            get {
                return FInfo.FileNameSansExt;
            }
        }
        public string Directory {
            get {
                return FInfo.Directory;
            }
        }
        public string Ext {
            get {
                return FInfo.FileExt;
            }
        }
        public string PathSansExt {
            get {
                return FInfo.FullPathSansExt;
            }
        }
        public static bool operator ==(InputFile lhs, InputFile rhs) {
            return lhs.Directory == rhs.Directory && lhs.Ext == rhs.Ext && lhs.FullPath == rhs.FullPath && lhs.Name == rhs.Name && lhs.NameSansExt == rhs.NameSansExt && lhs.PathSansExt == rhs.PathSansExt;
        }
        public static bool operator !=(InputFile lhs, InputFile rhs) {
            return lhs != rhs;
        }

    }
}
