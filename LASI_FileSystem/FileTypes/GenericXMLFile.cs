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
    public class GenericXMLFile : InputFile
    {

        public GenericXMLFile(string filePath)
            : base(filePath) {
                //var XMLs = new DataSet {
                //    DataSetName = "MicrosoftDocxOpenXML"
                //};
                //XMLs.InferXmlSchema(XmlReader.Create(filePath),
                //    (new XmlSchema {
                //        SourceUri = filePath
                //    }.Elements.Names.Cast<string>().ToArray()));
                //var datareader = XMLs;
                //var x = datareader.Tables;
                //Tables = new DataTable[x.Count];
                //x.CopyTo(Tables.ToArray(), 0);
                ////foreach (var tr in x) {
                ////    Console.WriteLine(tr);
                ////}

        }
        public virtual IEnumerable<DataTable> Tables {
            get;
            protected set;
        }
    }
}
