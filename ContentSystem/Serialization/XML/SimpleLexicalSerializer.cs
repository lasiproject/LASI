using LASI;
using LASI.Utilities;
using LASI.Core;
using LASI.Core.PatternMatching;
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
    public static class SimpleLexicalSerializer
    {



        /// <summary>
        /// Serializes the provided sequence of ILexical instances into xml elements and returns a single XElement containing them.
        /// </summary>
        /// <param name="source">The sequence of ILexical instances to serialize into a single XElement .</param>
        /// <param name="parentElementTitle">The desired name for the resulting XElement .</param>
        /// <param name="degreeOfOutput">The DegreeOfOutput value specifying the per element amount of detail the serilization will retain.</param>
        /// <returns>A single XElement  containing the serialized representation of the given sequence of elements.</returns>
        public static XElement Serialize(IEnumerable<ILexical> source, string parentElementTitle, DegreeOfOutput degreeOfOutput) {

            return new XElement("Root",
                             new XElement("Results",
                                 new XAttribute("Title", parentElementTitle),
                                 new XAttribute("Range", degreeOfOutput),
                             from e in source.Zip(Enumerable.Range(0, Int32.MaxValue), (x, i) => new { Element = x, Id = i })
                             let l = e.Element
                             select new XElement(l.Type.Name,
                                 //new XAttribute("Id", e.Id),
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


    }
}
