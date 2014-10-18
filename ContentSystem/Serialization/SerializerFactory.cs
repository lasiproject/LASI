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
            SerializationFormat format;
            Enum.TryParse<SerializationFormat>(targetFormat, out format); switch (format) {
                case SerializationFormat.XML: return new SimpleXmlSerializer();
                case SerializationFormat.JSON: return new SimpleJsonSerializer();
                default: return new SimpleJsonSerializer();
            }
        }

        enum SerializationFormat
        {
            JSON,
            XML
        }
    }
}
