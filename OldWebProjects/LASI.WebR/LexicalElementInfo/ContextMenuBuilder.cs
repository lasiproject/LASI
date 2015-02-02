using LASI.Core;
using LASI.Utilities;
using LASI.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using Newtonsoft.Json.Serialization;

namespace LASI.WebR
{
    using Validator = LASI.Utilities.Validation.Validator;
    /// <summary>
    /// Provides static and extension methods for serializing lexical elements and their relationships
    /// into JSON strings.
    /// </summary>
    public static class ContextMenuBuilder
    {
        public static int GetSerializationId(this ILexical element) {
            return idCache.GetOrAdd(element, System.Threading.Interlocked.Increment(ref idGenerator));
        }
        public static string GetJsonMenuData(this ILexical lexical) {
            return lexical.Match()
                .Case((IReferencer r) => r.GetJsonMenuData())
                .Case((IVerbal v) => v.GetJsonMenuData())
                .Result();
        }
        public static string GetJsonMenuData(this IVerbal verbal) {
            Validator.ThrowIfNull(verbal, nameof(verbal));
            var data = new
            {
                Verbal = verbal.GetSerializationId(),
                Subjects = verbal.HasSubject() ? verbal.Subjects.Select(e => e.GetSerializationId()).ToArray() : null,
                DirectObjects = verbal.HasDirectObject() ? verbal.DirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
                IndirectObjects = verbal.HasIndirectObject() ? verbal.IndirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
            };
            return JsonConvert.SerializeObject(data, SerializerSettings);
        }
        public static string GetJsonMenuData(this IReferencer referencer) {
            Validator.ThrowIfNull(referencer, nameof(referencer));
            var data = new
            {
                Referencer = referencer.GetSerializationId(),

                RefererredTo = referencer.RefersTo.Any() ? referencer.RefersTo.OfType<Phrase>().Select(e => e.GetSerializationId()).ToArray() : null
            };
            return JsonConvert.SerializeObject(data, SerializerSettings);
        }
        private static JsonSerializerSettings SerializerSettings {
            get {
                return new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ObjectCreationHandling = ObjectCreationHandling.Reuse,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            }
        }
        private static int idGenerator = 0;
        private static readonly ConcurrentDictionary<ILexical, int> idCache = new ConcurrentDictionary<ILexical, int>();

    }
}
