using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{

    public sealed class DocXFile : InputFile
    {
        public DocXFile(string absolutePath)
            : base(absolutePath) {
            if (!this.Ext.Equals(".docx", StringComparison.OrdinalIgnoreCase)) {
                throw new LASI.FileSystem.FileTypeWrapperMismatchException(GetType().ToString(), Ext);
            }
        }


    }
}
