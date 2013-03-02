using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem.FileTypes
{
    public class TextFile : InputFile
    {
        public TextFile(string absolutePath)
            : base(absolutePath) {
            if (this.Ext != ".txt" && this.Ext != ".TXT")
                throw new FileTypeWrapperMismatchException(GetType().ToString(), Ext);
        }

    }
}
