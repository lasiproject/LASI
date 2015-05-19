using LASI.Core;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp.LexicalElementInfo
{
    using Validate = Utilities.Validation.Validate;
    /// <summary>
    /// Provides static and extension methods for serializing lexical elements and their relationships
    /// into JSON strings.
    /// </summary>
    public static class ContextmenuBuilder
    {
        public static int GetSerializationId(this ILexical element)
        {
            Validate.NotNull(element, nameof(element));
            return IdCache.GetOrAdd(element, System.Threading.Interlocked.Increment(ref idGenerator));
        }
        public static string GetJsonMenuData(this ILexical lexical)
        {
            Validate.NotNull(lexical, nameof(lexical));
            return lexical.Match()
                    .Case((IReferencer r) => r.GetJsonMenuData())
                    .Case((IVerbal v) => v.GetJsonMenuData())
                    .Result();
        }
        public static string GetJsonMenuData(this IVerbal verbal)
        {
            Validate.NotNull(verbal, nameof(verbal));
            var data = new
            {
                Verbal = verbal.GetSerializationId(),
                Subjects = verbal.HasSubject() ? verbal.Subjects.Select(e => e.GetSerializationId()).ToArray() : null,
                DirectObjects = verbal.HasDirectObject() ? verbal.DirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
                IndirectObjects = verbal.HasIndirectObject() ? verbal.IndirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
            };
            return JsonConvert.SerializeObject(data, SerializerSettings);
        }
        public static string GetJsonMenuData(this IReferencer referencer)
        {
            Validate.NotNull(referencer, nameof(referencer));
            var data = new
            {
                Referencer = referencer.GetSerializationId(),
                RefersTo = referencer.RefersTo.Any() ? referencer.RefersTo.OfPhrase().Select(e => e.GetSerializationId()).ToArray() : null
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
        private static int idGenerator = 0;
        private static readonly ConcurrentDictionary<ILexical, int> IdCache = new ConcurrentDictionary<ILexical, int>();
    }
}
