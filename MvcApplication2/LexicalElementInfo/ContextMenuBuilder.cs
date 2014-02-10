using LASI.Core;
using LASI.Core.Patternization;
using LASI.Utilities;
using LASI.ContentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace MvcApplication2
{
    /// <summary>
    /// Provides static and extension methods for serializing lexical elements and their relationships
    /// into JSON strings.
    /// </summary>
    public static class DataSerializationProvider
    {
        private static int idGenerator = 0;
        private static readonly ConcurrentDictionary<ILexical, int> idCache = new ConcurrentDictionary<ILexical, int>();
        public static int GetSerializationId(this ILexical element) {
            return idCache.GetOrAdd(element, System.Threading.Interlocked.Increment(ref idGenerator));
        }
        public static dynamic GetJsonMenuData(this IVerbal verbal) {
            if (verbal == null)
                throw new ArgumentNullException("verbal");
            var serializerSettings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Reuse,
                NullValueHandling = NullValueHandling.Ignore,
            };
            var data = new
            {
                Verbal = verbal.GetSerializationId(),
                Subjects = verbal.HasSubject() ? verbal.Subjects.Select(e => e.GetSerializationId()).ToArray() : null,
                DirectObjects = verbal.HasDirectObject() ? verbal.DirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
                IndirectObjects = verbal.HasIndirectObject() ? verbal.IndirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
            };
            return JsonConvert.SerializeObject(data, serializerSettings);
        }
    }
}
