using LASI;
using LASI.Core;
using LASI.Core.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace LASI.Content.Serialization.Xml
{
    public static class SerializationExtensions
    {
        public static XElement ToXElement(this IEntity entity) {

            return new XElement(ElementNames[entity],
                    new XAttribute("Weight",
                        entity.Weight),
                    new XAttribute("MetaWeight",
                        entity.MetaWeight),
                    new XElement("SubjectOf",
                        ElementNames[entity.SubjectOf]),
                    new XElement("DirectObjectOf",
                        ElementNames[entity.DirectObjectOf]),
                    new XElement("IndirectObjectOf",
                        ElementNames[entity.IndirectObjectOf]),
                    new XElement("BoundPronouns",
                        from r in entity.Referencers
                        select new XElement("Referees", ElementNames[r])),
                    new XElement("Descriptors",
                        from d in entity.Descriptors
                        select new XElement("DescribedBy", ElementNames[d])),
                    new XElement("Possessions",
                        from p in entity.Possessions
                        select new XElement("Possesses", ElementNames[p])));
        }

        private static readonly NodeNameMapper ElementNames = new NodeNameMapper();


    }
}
