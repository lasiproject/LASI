using LASI;
using LASI.Algorithm;
using LASI.Utilities.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace LASI.ContentSystem.Serialization.XML
{
    /// <summary>
    /// Provides basic Xml serialization of for various configuration of ILexical elements.
    /// </summary>
    public class SimpleLexicalSerializer : ILexicalWriter<IEnumerable<ILexical>, ILexical, XmlWriter>
    {
        /// <summary>
        /// Initializes a new instance of the SimpleLexicalSerializer class which will output to the given XmlWriter.
        /// </summary>
        /// <param name="target">The XmlWriter to which to output.</param>
        public SimpleLexicalSerializer(XmlWriter target) {
            Target = target;
        }
        /// <summary>
        /// Initializes a new instance of the SimpleLexicalSerializer class which will output to the Uniform Resource Identifier specified by the string.
        /// </summary>
        /// <param name="uri">The string specifying the Uniform Resource Identifier to which to output.</param>
        public SimpleLexicalSerializer(string uri) {
            Target = XmlWriter.Create(uri);
        }
        /// <summary>
        /// Initializes a new instance of the SimpleLexicalSerializer class which will output to the given System.IO.TextWriter.
        /// </summary>
        /// <param name="textWriter">The System.IO.TextWriter to which to output.</param>
        public SimpleLexicalSerializer(System.IO.TextWriter textWriter) {
            Target = XmlWriter.Create(textWriter);
        }
        /// <summary>
        /// Serializes the provided sequence of ILexical instances into xml elements and returns a single XElement containing them.
        /// </summary>
        /// <param name="source">The sequence of ILexical instances to serialize into a single XElement .</param>
        /// <param name="parentElementTitle">The desired name for the resulting XElement .</param>
        /// <param name="degreeOfOutput">The DegreeOfOutput value specifying the per element amount of detail the serilization will retain.</param>
        /// <returns>A single XElement  containing the serialized representation of the given sequence of elements.</returns>
        public XElement Serialize(IEnumerable<ILexical> source, string parentElementTitle, DegreeOfOutput degreeOfOutput) {

            return new XElement("Root",
                             new XElement("Results",
                                 new XAttribute("Title", parentElementTitle),
                                 new XAttribute("Range", degreeOfOutput),
                             from l in source
                             select new XElement(l.Type.Name,
                                 new XAttribute("Text", l.Text),
                                 new XElement("Weights",
                                     new XElement("Weight",
                                         new XAttribute("Level", "Document"), Math.Round(l.Weight, 2)),
                                 new XElement("Weight",
                                     new XAttribute("Level", "Crossed"), Math.Round(l.MetaWeight, 2))
                                     )
                                 )
                             )

                         );
        }
        /// <summary>
        /// Serializes the provided sequence of ILexical instances into xml elements and returns an XDocument containing them.
        /// </summary>
        /// <param name="source">The sequence of ILexical instances to serialize into an XDocument.</param>
        /// <param name="documentTitle">The desired name for the resulting XDocument.</param>
        /// <param name="degreeOfOutput">The DegreeOfOutput value specifying the per element amount of detail the serilization will retain.</param>
        /// <returns>An XDocument containing the serialized representation of the given sequence of elements.</returns>
        public XDocument SerializeSequence(IEnumerable<ILexical> source, string documentTitle, DegreeOfOutput degreeOfOutput) {
            return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
            Serialize(source, documentTitle, degreeOfOutput)
                        );
        }
        /// <summary>
        /// Gets the XmlWriter object targeted by all Write operations of the SimpleLexicalSerializer.
        /// </summary>
        public XmlWriter Target {
            get;
            protected set;
        }
        /// <summary>
        /// Frees any unmanaged resources associated with the SimpleLexicalSerializer.
        /// </summary>
        public void Dispose() {
            Target.Dispose();
        }
        /// <summary>
        /// Serializes the provided sequence of ILexical instances into xml elements, writing them to the XmlWriter associated with this instance.
        /// </summary>
        /// <param name="source">The sequence of ILexical instances to serialize.</param>
        /// <param name="title">The desired name for the parent xml element which will be created as the root element of the source sequence.</param>
        /// <param name="degreeOfOutput">The DegreeOfOutput value specifying the per element amount of detail the serilization will retain.</param>
        public void Write(IEnumerable<ILexical> source, string title, DegreeOfOutput degreeOfOutput) {

            var serializedResults =
                SerializeSequence(source, title, degreeOfOutput);
            serializedResults.Save(Target);

        }
        /// <summary>
        /// Asynchronously serializes the provided sequence of ILexical instances into xml elements, writing them to the XmlWriter associated with this instance.
        /// </summary>
        /// <param name="source">The sequence of ILexical instances to serialize.</param>
        /// <param name="title">The desired name for the parent xml element which will be created as the root element of the source sequence.</param>
        /// <param name="degreeOfOutput">The DegreeOfOutput value specifying the per element amount of detail the serilization will retain.</param>
        /// <returns>A System.Threading.Tasks.Task representing the ongoing asynchronous operation.</returns>
        public async Task WriteAsync(IEnumerable<ILexical> source, string title, DegreeOfOutput degreeOfOutput) {
            await Target.WriteStringAsync(Serialize(source, title, degreeOfOutput).ToString(SaveOptions.None));
        }
    }
}
