
namespace LASI.Content.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using ILexical = Core.ILexical;
    using static System.Linq.Enumerable;
    using LASI.Utilities;

    /// <summary>
    /// Provides basic XML serialization of for various configuration of ILexical elements.
    /// </summary>
    public class SimpleXmlSerializer : ILexicalSerializer<ILexical, XElement>
    {
        /// <summary>
        /// Serializes the provided sequence of ILexical instances into XML elements and returns a single XElement containing them.
        /// </summary>
        /// <param name="source">The sequence of ILexical instances to serialize into a single XElement .</param>
        /// <param name="parentElementTitle">The desired name for the resulting XElement .</param>
        /// <param name="degreeOfOutput">The DegreeOfOutput value specifying the per element amount of detail the serialization will retain.</param>
        /// <returns>A single XElement  containing the serialized representation of the given sequence of elements.</returns>
        public XElement Serialize(IEnumerable<ILexical> source, string parentElementTitle) =>
            new XElement("Root",
                new XElement("Results",
                    new XAttribute("Title", parentElementTitle),
                    from e in source.WithIndex()
                    select new XElement(e.Element.GetType().Name,
                        new XAttribute("Id", e.Index),
                        new XAttribute("Text", e.Element.Text),
                        new XElement("Weights",
                            new XElement("Weight", new XAttribute("Level", "Document"), Math.Round(e.Element.Weight, 2)),
                        new XElement("Weight", new XAttribute("Level", "Crossed"), Math.Round(e.Element.MetaWeight, 2)))
                        ))
                );
    }
}
