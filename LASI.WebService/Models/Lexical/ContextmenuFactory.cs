using LASI.Core;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using Newtonsoft.Json.Serialization;

namespace LASI.WebService.Models.Lexical
{
    using System.Collections.Generic;
    using Validate = Utilities.Validation.Validate;
    /// <summary>
    /// Provides static and extension methods for serializing lexical elements and their relationships
    /// into JSON strings.
    /// </summary>
    public static class ContextmenuFactory
    {
        public static int GetSerializationId(this ILexical element)
        {
            Validate.NotNull(element, nameof(element));
            return IdCache.GetOrAdd(element, System.Threading.Interlocked.Increment(ref idGenerator));
        }
        public static ILexicalContextmenu Create(this ILexical lexical)
        {
            Validate.NotNull(lexical, nameof(lexical));
            return lexical.Match()
                    .Case((IReferencer r) => CreateForReferencer(r))
                    .Case((IVerbal v) => CreateForVerbal(v))
                    .Result();
        }
        private static ILexicalContextmenu CreateForVerbal(IVerbal verbal)
        {
            Validate.NotNull(verbal, nameof(verbal));
            return new LexicalContextmenu
            {
                LexicalId = verbal.GetSerializationId(),
                SubjectIds = verbal.HasSubject() ? verbal.Subjects.Select(e => e.GetSerializationId()).ToArray() : null,
                DirectObjectIds = verbal.HasDirectObject() ? verbal.DirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
                IndirectObjectIds = verbal.HasIndirectObject() ? verbal.IndirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
            };
        }
        private static ILexicalContextmenu CreateForReferencer(IReferencer referencer)
        {
            Validate.NotNull(referencer, nameof(referencer));
            return new LexicalContextmenu
            {
                LexicalId = referencer.GetSerializationId(),
                RefersToIds = referencer.RefersTo.Any() ? referencer.RefersTo.OfPhrase().Select(e => e.GetSerializationId()).ToArray() : null
            };
        }

        private static int idGenerator = 0;
        private static readonly ConcurrentDictionary<ILexical, int> IdCache = new ConcurrentDictionary<ILexical, int>();

        private class LexicalContextmenu : ILexicalContextmenu
        {
            public int LexicalId { get; set; }
            public IEnumerable<int> DirectObjectIds { get; set; }
            public IEnumerable<int> IndirectObjectIds { get; set; }
            public IEnumerable<int> SubjectIds { get; set; }
            public IEnumerable<int> RefersToIds { get; set; }
            public override int GetHashCode() => LexicalId;
            public override string ToString() => JsonConvert.SerializeObject(this);

            private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Reuse,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
