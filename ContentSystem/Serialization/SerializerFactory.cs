using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core;

namespace LASI.ContentSystem.Serialization
{
    public static class SerializerFactory
    {
        public static ILexicalSerializer<ILexical, object> Create(string targetFormat) {
            Format format;
            Enum.TryParse(targetFormat, out format);
            switch (format) {
                case Format.XML: return new SimpleXmlSerializer();
                case Format.JSON: return new SimpleJsonSerializer();
                default: return new SimpleJsonSerializer();
            }
        }

        enum Format
        {
            JSON,
            XML
        }
    }
}
