using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    public class TaggedFile : InputFile
    {


        public TaggedFile(string filePath)
            : base(filePath) {
            if (Ext != ".tagged") {
                throw new IOException(String.Format("File extension \"{0}\" does not match InputFile type: {1}", Ext, GetType()));
            }

        }
    }
}
