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

namespace LASI.ContentSystem.Serialization.Json
{
    static class SerializationExtensions
    {
        public static JArray ToJArray(this IEnumerable<ILexical> elements) {
            return new JArray(elements.Select(element => element.ToJObject()));
        }
        public static JObject ToJObject(this ILexical element) {
            return element.Match().Yield<JObject>()
                .Case((IEntity e) => e.ToJObject())
                .Case((IVerbal v) => v.ToJObject())
                .Result(new JObject(GetCommonProperties(element)));
        }

        public static JObject ToJObject(this IEntity entity) {
            return new JObject(GetCommonProperties(entity)) {
                new JProperty("subjectOf", elementNames[entity.SubjectOf]),
                new JProperty("directObjectOf", elementNames[entity.DirectObjectOf]),
                new JProperty("referees", from referencedBy in entity.Referencers select elementNames[referencedBy]),
                new JProperty("descriptors", from descriptor in entity.Descriptors select elementNames[descriptor]),
                new JProperty("possessed", from possession in entity.Possessions select elementNames[possession])
            };
        }

        public static JObject ToJObject(this IVerbal verbal) {
            return new JObject(GetCommonProperties(verbal)) {
                new JProperty("subjects", verbal.Subjects),
                new JProperty("directObjects", verbal.DirectObjects),
                new JProperty("indirectObjects", verbal.IndirectObjects),
                new JProperty("modality", verbal.AdverbialModifiers),
                new JProperty("adverbialModifiers", verbal.AdverbialModifiers)
            };
        }
        /// <summary>
        /// Gets common properties serialized for all lexical types.
        /// </summary>
        /// <param name="element">The element whose properties are to be serialized.</param>
        /// <returns>The common properties serialized for all lexical types.</returns>
        private static IEnumerable<JProperty> GetCommonProperties(ILexical element) {
            return new[] {
                new JProperty( "name: ", elementNames[element]),
                new JProperty("weight", element.Weight ),
                new JProperty( "metaWeight", element.MetaWeight )
            };
        }
        private static readonly NodeNameMapper elementNames = new NodeNameMapper();
    }
}
