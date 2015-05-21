using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LASI.Content;
using LASI.Core;
using LASI.Utilities;
using LASI.Utilities.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp.Old
{
    /// <summary>
    /// Provides static and extension methods for serializing lexical elements and their
    /// relationships into JSON strings.
    /// </summary>
    public static class ContextMenuBuilder
    {
        public static int GetSerializationId(this ILexical element)
        {
            return idCache.GetOrAdd(element, System.Threading.Interlocked.Increment(ref idGenerator));
        }

        public static string GetJsonMenuData(this ILexical lexical) => lexical.Match()
                .Case((IReferencer r) => r.GetJsonMenuData())
                .Case((IEntity r) => r.GetJsonMenuData())
                .Case((IVerbal v) => v.GetJsonMenuData())
                .Result();

        public static string GetJsonMenuData(this IEntity entity)
        {
            Validate.NotNull(entity, nameof(entity));
            return JsonConvert.SerializeObject(
                new
                {
                    entity.SubjectOf,
                    entity.DirectObjectOf,
                    entity.IndirectObjectOf,
                    Referencers = entity.Referencers.Any() ? entity.Referencers.Select(GetSerializationId) : null,
                }
            );
        }

        public static string GetJsonMenuData(this IVerbal verbal)
        {
            Validate.NotNull(verbal, nameof(verbal));
            var data = new
            {
                Verbal = verbal.GetSerializationId(),
                Subjects = verbal.HasSubject() ? verbal.Subjects.Select(e => e.GetSerializationId()) : null,
                DirectObjects = verbal.HasDirectObject() ? verbal.DirectObjects.Select(e => e.GetSerializationId()) : null,
                IndirectObjects = verbal.HasIndirectObject() ? verbal.IndirectObjects.Select(e => e.GetSerializationId()) : null,
            };
            return JsonConvert.SerializeObject(data, SerializerSettings);
        }

        public static string GetJsonMenuData(this IReferencer referencer)
        {
            Validate.NotNull(referencer, nameof(referencer));
            var data = new
            {
                Referencer = referencer.GetSerializationId(),

                RefererredTo = referencer.RefersTo.Any() ? referencer.RefersTo.OfType<Phrase>().Select(e => e.GetSerializationId()).ToArray() : null
            };
            return JsonConvert.SerializeObject(data, SerializerSettings);
        }

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Reuse,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private static readonly ConcurrentDictionary<ILexical, int> idCache = new ConcurrentDictionary<ILexical, int>();
        private static int idGenerator = 0;
    }
}