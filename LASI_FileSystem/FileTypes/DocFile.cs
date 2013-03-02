using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem.FileTypes
{
    public class DocFile : InputFile
    {
        public DocFile(string absolutePath)
            : base(absolutePath) {
            if (this.Ext != ".doc" && this.Ext != ".DOC") {
                throw new LASI.FileSystem.FileTypes.FileTypeWrapperMismatchException(GetType().ToString(), Ext);
            }
        }

    }

}
