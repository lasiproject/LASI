using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem.FileTypes
{

    public class DocXFile : InputFile
    {
        public DocXFile(string absolutePath)
            : base(absolutePath) {
            if (this.Ext != ".docx" && this.Ext != ".DOCX") {
                throw new LASI.FileSystem.FileTypes.FileTypeWrapperMismatchException(GetType().ToString(), Ext);
            }
        }


    }
}
