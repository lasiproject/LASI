namespace LASI.Content.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using LASI.Utilities;

    /// <summary>
    /// Provides basic XML serialization of for various configuration of ILexical elements.
    /// </summary>
    public class SimpleXmlSerializer : ILexicalSerializer<Core.ILexical, XElement>
    {
        /// <summary>
        /// Serializes the provided sequence of ILexical instances into XML elements and returns a single XElement containing them.
        /// </summary>
        /// <param name="lexicals">The sequence of ILexical instances to serialize into a single XElement .</param>
        /// <param name="parentElementTitle">The desired name for the resulting XElement .</param>
        /// <returns>A single XElement  containing the serialized representation of the given sequence of elements.</returns>
        public XElement Serialize(IEnumerable<Core.ILexical> lexicals, string parentElementTitle) =>
            new XElement("Root",
                new XElement("Results",
                    new XAttribute("Name", parentElementTitle),
                    from e in lexicals.WithIndices()
                    let element = e.element
                    let index = e.index
                    select new XElement(element.GetType().Name,
                        new XAttribute("Id", index),
                        new XAttribute("Text", element.Text),
                        new XElement("Weights",
                            new XElement("Weight", new XAttribute("Level", "Document"), Math.Round(element.Weight, 2)),
                        new XElement("Weight", new XAttribute("Level", "Crossed"), Math.Round(element.MetaWeight, 2)))
                        )
                    )
                );
    }
}
