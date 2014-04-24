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

namespace LASI.ContentSystem.Serialization.XML.ILexicalExtensions
{
    static class SerializationExtensions
    {
        public static JObject ToJObject(this IEntity entity) {
            return new JObject{
                { "Name: ", ElementNames[entity] },
                { "Weight", entity.Weight }, 
                { "MetaWeight", entity.MetaWeight },
                { "SubjectOf", ElementNames[entity.SubjectOf] },
                { "DirectObjectOf", ElementNames[entity.DirectObjectOf] },
                { "Referees",
                    new JArray(
                    from r in entity.Referencers 
                    select ElementNames[r]) },
                { "Descriptors",
                    new JArray(
                    from d in entity.Descriptors 
                    select ElementNames[d]) },
                { "Possessed",
                    new JArray(
                    from p in entity.Possessed 
                    select ElementNames[p]) },
            };
        }
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
                        from p in entity.Possessed
                        select new XElement("Possesses", ElementNames[p])));
        }

        private static readonly NodeNameMapper ElementNames = new NodeNameMapper();

        private class NodeNameMapper
        {
            public string this[ILexical element] { get { return GetNodeName(element); } }
            private string GetNodeName(ILexical element) {
                return element != null ?
                    element.Type.Name + " " + elementIds.GetOrAdd(element, e => System.Threading.Interlocked.Increment(ref wordIdGenerator)) :
                    string.Empty;
            }
            private int wordIdGenerator;
            private System.Collections.Concurrent.ConcurrentDictionary<ILexical, int> elementIds = new System.Collections.Concurrent.ConcurrentDictionary<ILexical, int>();
        }
    }
}
