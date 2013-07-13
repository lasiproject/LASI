using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    public sealed class TaggedFile : InputFile, Algorithm.ITaggedTextSource
    {
        public TaggedFile(string filePath)
            : base(filePath) {
            if (!this.Ext.Equals(".tagged", StringComparison.OrdinalIgnoreCase))
                throw new LASI.FileSystem.FileTypeWrapperMismatchException(GetType().ToString(), Ext);
        }

        public string GetText() {
            using (var reader = new System.IO.StreamReader(this.FullPath)) {
                return reader.ReadToEnd();
            }
        }

        public async Task<string> GetTextAsync() {
            using (var reader = new System.IO.StreamReader(new System.IO.FileStream(this.FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))) { return await reader.ReadToEndAsync(); }
        }





        public string DataName {
            get { return NameSansExt; }
        }
    }
}
