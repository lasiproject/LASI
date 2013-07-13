using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Resolvers;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

namespace LASI.FileSystem
{
    public sealed class GenericXMLFile : InputFile
    {

        public GenericXMLFile(string filePath)
            : base(filePath) {
            if (!this.Ext.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                throw new FileTypeWrapperMismatchException(GetType().ToString(), Ext);

        }
        public IEnumerable<DataTable> Tables {
            get;
            private set;
        }
    }
}
