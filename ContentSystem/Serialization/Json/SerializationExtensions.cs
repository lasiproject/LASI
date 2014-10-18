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
    /// <summary>
    /// Provides extension methods for converting Lexical constructs into Json structures.
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Creates a Newtonsoft.Linq.JArray from the sequence of lexicals.
        /// </summary>
        /// <param name="elements">The sequence of leixcals from which to construct the Newtonsoft.Linq.JArray</param>
        /// <returns>A Newtonsoft.Linq.JArray from the sequence of lexicals.</returns>
        public static JArray ToJArray(this IEnumerable<ILexical> elements) {
            return new JArray(elements.Select(ToJObject));
        }
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the lexical.
        /// </summary>
        /// <param name="lexical">The source entity.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the lexical.</returns>
        public static JObject ToJObject(this ILexical lexical) {
            return lexical.Match().Yield<JObject>()
                    .With((IEntity e) => e.ToJObject())
                    .With((IVerbal v) => v.ToJObject())
                .Result(new JObject(GetCommonProperties(lexical)));
        }
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the entity.
        /// </summary>
        /// <param name="entity">The source entity.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the entity.</returns>
        public static JObject ToJObject(this IEntity entity) {
            return new JObject(GetCommonProperties(entity)) {
                new JProperty("subjectOf", elementNames[entity.SubjectOf]),
                new JProperty("directObjectOf", elementNames[entity.DirectObjectOf]),
                new JProperty("referees", from referencedBy in entity.Referencers select elementNames[referencedBy]),
                new JProperty("descriptors", from descriptor in entity.Descriptors select elementNames[descriptor]),
                new JProperty("possessed", from possession in entity.Possessions select elementNames[possession])
            };
        }
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the verbal.
        /// </summary>
        /// <param name="verbal">The source verbal.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the verbal.</returns>
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
