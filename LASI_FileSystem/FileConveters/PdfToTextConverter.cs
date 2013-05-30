using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem.FileConveters
{
    class PdfToTextConverter : FileConverter
    {
        public PdfToTextConverter(FileTypes.PdfFile infile)
            : base(infile) {
        }

        public override InputFile ConvertFile() {
            throw new NotImplementedException();
        }

        public override async Task<InputFile> ConvertFileAsync() {
            return await Task.Run(() => ConvertFile());
        }

        public override InputFile Converted {
            get;
            protected set;
        }
    }
}
