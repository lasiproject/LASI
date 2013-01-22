using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Resolvers;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Threading.Tasks;
using System.Data;

namespace LASI.FileSystem
{
    public class DcxToTxtXMLBasedConverter : DocxToTextConverter
    {
        public DcxToTxtXMLBasedConverter(InputFile infile)
            : base(infile) {
        }
        public override InputFile ConvertFile() {
            //var dataReaders = NewMethod();
            //var paragraphreader = dataReaders.Where(r => r.ToString() == "p").First();
            //var docx = new XmlDocument() {
            //};
            var zip = DocxToZip();
            var docxXML = GetRelevantXMLFile(zip);

            using (var writer = XmlWriter.Create(Original.PathSansExt +".txt")) {
                using (var reader = XmlReader.Create(docxXML.FullPath, new XmlReaderSettings {
                    IgnoreWhitespace = true
                })) {

                    while (reader.Read()) {
                        if (reader.LocalName == "w:p") {
                            if (!string.IsNullOrWhiteSpace(reader.Value)) {
                                writer.WriteElementString("PARAGRAPH", reader.Value);
                            }

                        }
                    }
                    return new TextFile(Original.PathSansExt + ".txt");
                }
            }
        }

        //private IEnumerable<IDataReader> NewMethod() {
        //    var zip = DocxToZip();
        //    var docxXML = GetRelevantXMLFile(zip);
        //    var dataSet = docxXML.Tables;
        //    return from d in dataSet
        //           select d.CreateDataReader();
        //}
    }
}
