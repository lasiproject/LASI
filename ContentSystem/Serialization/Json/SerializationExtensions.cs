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
        public static JObject ToJObject(this IEntity entity) {
            return NewMethod(entity);
        }

        private static JObject NewMethod(IEntity entity) {
            return new JObject(
                GetCommonProperties(entity),
                new JProperty("subjectOf", ElementNames[entity.SubjectOf]),
                new JProperty("directObjectOf", ElementNames[entity.DirectObjectOf]),
                new JProperty("referees", from r in entity.Referencers select ElementNames[r]),
                new JProperty("descriptors", from descriptor in entity.Descriptors select ElementNames[descriptor]),
                new JProperty("possessed", from possession in entity.Possessions select ElementNames[possession])
            );
        }
        /// <summary>
        /// Gets common properties serialized for all lexical types.
        /// </summary>
        /// <param name="element">The element whose properties are to be serialized.</param>
        /// <returns></returns>
        private static IEnumerable<JProperty> GetCommonProperties(ILexical element) {
            return new[] { new JProperty( "name: ", ElementNames[element]),
                new JProperty("weight", element.Weight ),
                new JProperty( "metaWeight", element.MetaWeight )};
        }
        public static JObject ToJObject(this IVerbal verbal) {
            return new JObject(
                GetCommonProperties(verbal),
                new JProperty("subjects", verbal.Subjects),
                new JProperty("directObjects", verbal.DirectObjects),
                new JProperty("indirectObjects", verbal.IndirectObjects),
                new JProperty("modality", verbal.AdverbialModifiers),
                new JProperty("adverbialModifiers", verbal.AdverbialModifiers)
            );
        }

        private static readonly NodeNameMapper ElementNames = new NodeNameMapper();
    }
}
