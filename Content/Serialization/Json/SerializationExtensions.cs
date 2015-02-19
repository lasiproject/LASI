using LASI.Core;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using LASI.Utilities;
using Newtonsoft.Json;
using System;

namespace LASI.Content.Serialization.Json
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
        public static JArray ToJArray(this IEnumerable<ILexical> elements)
        {
            return new JArray(elements.Select(ToJObject));
        }
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the lexical.
        /// </summary>
        /// <param name="lexical">The source entity.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the lexical.</returns>
        public static JObject ToJObject(this ILexical lexical)
        {
            return lexical.Match()
                    .Case((IEntity e) => e.ToJObject())
                    .Case((IVerbal v) => v.ToJObject())
                    .Case((IAdverbial a) => a.ToJObject())
                .Result(new JObject(GetRoleIndependentProperties(lexical)));
        }
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the Entity.
        /// </summary>
        /// <param name="entity">The source Entity.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the Entity.</returns>
        public static JObject ToJObject(this IEntity entity)
        {
            return new JObject(GetRoleIndependentProperties(entity))
            {
                new JProperty("subjectOf", ElementNameMappingProvider [entity.SubjectOf]),
                new JProperty("directObjectOf", ElementNameMappingProvider [entity.DirectObjectOf]),
                new JProperty("referees", from referencedBy in entity.Referencers select ElementNameMappingProvider [referencedBy]),
                new JProperty("descriptors", from descriptor in entity.Descriptors select ElementNameMappingProvider [descriptor]),
                new JProperty("possessed", from possession in entity.Possessions select ElementNameMappingProvider [possession])
            };
        }

        private static IEnumerable<JProperty> GetRoleIndependentProperties(ILexical element)
        {
            return GetCommonProperties(element).Concat(GetStructuralProperties(element));
        }
        private static IEnumerable<JProperty> GetStructuralProperties(ILexical element)
        {
            return element.Match()
                 .Case((Phrase p) => new[] { new JProperty("words", p.Words.ToJArray()) })
                 .Case((Clause c) => new[] { new JProperty("phrases", c.Phrases.ToJArray()) })
                 .Result(new JProperty[0]);
        }

        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the Verbal.
        /// </summary>
        /// <param name="verbal">The source verbal.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the Verbal.</returns>
        public static JObject ToJObject(this IVerbal verbal)
        {
            return new JObject(GetRoleIndependentProperties(verbal))
            {
                new JProperty("subjects", verbal.Subjects),
                new JProperty("directObjects", verbal.DirectObjects),
                new JProperty("indirectObjects", verbal.IndirectObjects),
                new JProperty("modality", verbal.Modality),
                new JProperty("adverbialModifiers", verbal.AdverbialModifiers.Select(ToJObject))
            };
        }
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the Adverbial.
        /// </summary>
        /// <param name="adverbial">The source Adverbial.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the Adverbial.</returns>
        public static JObject ToJObject(this IAdverbial adverbial)
        {
            return new JObject(GetRoleIndependentProperties(adverbial))
            {
                new JProperty("modifies", adverbial.Modifies)
            };
        }
        /// <summary>
        /// Gets common properties serialized for all lexical types.
        /// </summary>
        /// <param name="element">The element whose properties are to be serialized.</param>
        /// <returns>The common properties serialized for all lexical types.</returns>
        private static IEnumerable<JProperty> GetCommonProperties(ILexical element)
        {
            return new[] {
                new JProperty("text", element.Text),
                new JProperty("name", ElementNameMappingProvider [element]),
                new JProperty("weight", element.Weight ),
                new JProperty("metaWeight", element.MetaWeight)
            };
        }
        private static readonly NodeNameMapper ElementNameMappingProvider = new NodeNameMapper();
        private class AdverbialConverter : JsonConverter
        {
            private static readonly Type TargetType = typeof(IAdverbial);
            public override bool CanConvert(Type objectType)
            {
                return objectType.GetInterfaces().Any(interfaceType => interfaceType == TargetType);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                IAdverbial target = value as IAdverbial;
                writer.WriteRaw(target.ToJObject().ToString());
            }
        }
    }
}
