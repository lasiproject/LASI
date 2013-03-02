using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem.FileTypes
{
    public class TaggedFile : InputFile
    {
        public TaggedFile(string filePath)
            : base(filePath) {
            if (this.Ext != ".tagged" && this.Ext != ".TAGGED") {
                throw new LASI.FileSystem.FileTypes.FileTypeWrapperMismatchException(GetType().ToString(), Ext);
            }

        }
    }
}
