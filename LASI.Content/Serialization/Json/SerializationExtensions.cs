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
        /// <param name="elements">The sequence of lexicals from which to construct the Newtonsoft.Linq.JArray</param>
        /// <returns>A Newtonsoft.Linq.JArray from the sequence of lexicals.</returns>
        public static JArray ToJArray(this IEnumerable<ILexical> elements) => new JArray(elements.Select(ToJObject));
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the lexical.
        /// </summary>
        /// <param name="lexical">The source entity.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the lexical.</returns>
        public static JObject ToJObject(this ILexical lexical) => lexical.Match()
            .Case((IEntity e) => e.ToJObject())
            .Case((IVerbal v) => v.ToJObject())
            .Case((IAdverbial a) => a.ToJObject())
            .Result(new JObject(GetRoleIndependentProperties(lexical)));
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the Entity.
        /// </summary>
        /// <param name="entity">The source Entity.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the Entity.</returns>
        public static JObject ToJObject(this IEntity entity) => new JObject(GetRoleIndependentProperties(entity))
        {
            ["subjectOf"] = ElementNameMappingProvider[entity.SubjectOf],
            ["directObjectOf"] = ElementNameMappingProvider[entity.DirectObjectOf.Match((IVerbal v) => v)],
            ["referees"] = new JArray(from referencedBy in entity.Referencers select ElementNameMappingProvider[referencedBy]),
            ["descriptors"] = new JArray(from descriptor in entity.Descriptors select ElementNameMappingProvider[descriptor]),
            ["possessed"] = new JArray(from possession in entity.Possessions select ElementNameMappingProvider[possession])
        };

        private static IEnumerable<JProperty> GetRoleIndependentProperties(ILexical element)
        {
            foreach (var property in GetCommonProperties(element))
            {
                yield return property;
            }
            foreach (var property in GetStructuralProperties(element))
            {
                yield return property;
            }
        }
        private static IEnumerable<JProperty> GetStructuralProperties(ILexical element) =>
            from result in element.Match()
                .Case((Phrase p) => new JProperty("words", p.Words.ToJArray()))
                .Case((Clause c) => new JProperty("phrases", c.Phrases.ToJArray()))
            select result;


        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the Verbal.
        /// </summary>
        /// <param name="verbal">The source verbal.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the Verbal.</returns>
        public static JObject ToJObject(this IVerbal verbal) => new JObject(GetRoleIndependentProperties(verbal))
        {
            ["subjects"] = verbal.Subjects.ToJArray(),
            ["directObjects"] = verbal.DirectObjects.ToJArray(),
            ["indirectObjects"] = verbal.IndirectObjects.ToJArray(),
            ["modality"] = verbal.Modality?.ToJObject(),
            ["adverbialModifiers"] = verbal.AdverbialModifiers.EmptyIfNull().ToJArray()
        };
        /// <summary>
        /// Creates a Newtonsoft.Linq.JObject representation of the Adverbial.
        /// </summary>
        /// <param name="adverbial">The source Adverbial.</param>
        /// <returns>A Newtonsoft.Linq.JObject representation of the Adverbial.</returns>
        public static JObject ToJObject(this IAdverbial adverbial) => new JObject(GetRoleIndependentProperties(adverbial))
        {
            new JProperty("modifies", adverbial.Modifies)
        };
        /// <summary>
        /// Gets common properties serialized for all lexical types.
        /// </summary>
        /// <param name="element">The element whose properties are to be serialized.</param>
        /// <returns>The common properties serialized for all lexical types.</returns>
        private static IEnumerable<JProperty> GetCommonProperties(ILexical element)
        {
            yield return new JProperty("text", element.Text);
            yield return new JProperty("name", ElementNameMappingProvider[element]);
            yield return new JProperty("weight", element.Weight);
            yield return new JProperty("metaWeight", element.MetaWeight);
        }
        private static readonly NodeNameMapper ElementNameMappingProvider = new NodeNameMapper();
    }
}

