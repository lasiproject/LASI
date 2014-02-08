using LASI.Core;
using LASI.Core.Patternization;
using LASI.Utilities;
using LASI.ContentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace MvcApplication2.LexicalElementInfo
{
    public class RelationshipMenuEntry
    {
        internal RelationshipMenuEntry(int elementId, string entryText, IEnumerable<int> relatedElementIds) {
            ElementId = elementId;
            EntryText = entryText;
            RelatedElementIds = relatedElementIds;
        }
        public int ElementId { get; private set; }
        public string EntryText { get; private set; }

        public IEnumerable<int> RelatedElementIds { get; private set; }
    }




    public static class SerializationDataProvider
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

                Subjects = verbal.HasSubject() ? verbal.Subjects.Select(e => e.GetSerializationId()).ToArray() : null,
                DirectObjects = verbal.HasDirectObject() ? verbal.DirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,
                IndrectObjects = verbal.HasIndirectObject() ? verbal.IndirectObjects.Select(e => e.GetSerializationId()).ToArray() : null,

            };

            return JsonConvert.SerializeObject(data, serializerSettings);


        }
    }
}
