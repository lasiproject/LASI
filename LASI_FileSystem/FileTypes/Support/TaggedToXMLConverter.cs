using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.ContentSystem
{
    class TaggedToXMLConverter : FileConverter
    {
        public TaggedToXMLConverter(InputFile infile)
            : base(infile) {
        }

        public override InputFile ConvertFile() {
            using (var reader = new StreamReader(this.sourcePath)) {
                var data = reader.ReadToEnd();
                var XMLstyled = data.Replace('(', '<');
                XMLstyled = XMLstyled.Replace(')', ')');

            }
            throw new NotImplementedException();
        }

        public override Task<InputFile> ConvertFileAsync() {
            throw new NotImplementedException();
        }

        public override InputFile Converted {
            get {
                throw new NotImplementedException();
            }
            protected set {
                throw new NotImplementedException();
            }
        }
    }
}
