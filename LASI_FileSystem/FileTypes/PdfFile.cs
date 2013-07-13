using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    public sealed class PdfFile : InputFile
    {
        public PdfFile(string absolutePath)
            : base(absolutePath) {
            if (!this.Ext.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                throw new LASI.FileSystem.FileTypeWrapperMismatchException(GetType().ToString(), Ext);

        }
    }
}
