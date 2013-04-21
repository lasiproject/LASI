using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LASI.FileSystem.Serialization.XML
{
    public class SimpleLexicalSerializer : ILexicalWriter<IEnumerable<ILexical>, ILexical, XmlWriter>
    {
        public SimpleLexicalSerializer()
            : this(Console.Out) {
        }
        public SimpleLexicalSerializer(XmlWriter target) {
            Target = target;
        }
        public SimpleLexicalSerializer(string uri) {
            Target = XmlWriter.Create(uri);
        }
        public SimpleLexicalSerializer(System.IO.TextWriter textWriter) {
            Target = XmlWriter.Create(textWriter);
        }
        public void Write(IEnumerable<ILexical> resultSet, string resultSetTitle, DegreeOfOutput degreeOfOutput) {

            var serializedResults =
                SerializeDoc(resultSet, resultSetTitle, degreeOfOutput);
            serializedResults.Save(Target);

        }

        public XElement Serialize(IEnumerable<ILexical> resultSet, string resultSetTitle, DegreeOfOutput degreeOfOutput) {

            return new XElement("Root",
                             new XElement("Results",
                                 new XAttribute("Title", resultSetTitle),
                                 new XAttribute("Range", degreeOfOutput),
                             from l in resultSet
                             select new XElement(l.Type.Name,
                                 new XAttribute("Text", l.Text),
                                 new XElement("Weights",
                                     new XElement("Weight",
                                         new XAttribute("Level", "Document"), decimal.Round(l.Weight, 2)),
                                 new XElement("Weight",
                                     new XAttribute("Level", "Crossed"), decimal.Round(l.MetaWeight, 2))
                                     )
                                 )
                             )

                         );
        }
        public XDocument SerializeDoc(IEnumerable<ILexical> resultSet, string resultSetTitle, DegreeOfOutput degreeOfOutput) {
            return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
            Serialize(resultSet, resultSetTitle, degreeOfOutput)
                        );
        }

        public XmlWriter Target {
            get;
            protected set;
        }


        public void Dispose() {
            Target.Dispose();
        }


        public async Task WriteAsync(IEnumerable<ILexical> resultSet, string resultSetTitle, DegreeOfOutput degreeOfOutput) {
            await Target.WriteStringAsync(Serialize(resultSet, resultSetTitle, degreeOfOutput).ToString(SaveOptions.None));
        }
    }
}
