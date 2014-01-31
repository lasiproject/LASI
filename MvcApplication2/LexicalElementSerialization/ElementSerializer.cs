using LASI.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcApplication2.LexicalElementSerialization
{
    public static class DefaultLexicalElementSerializer
    {

        public static string ToJson<TLexical>(this TLexical toSerialize) where TLexical : ILexical {

            return JsonConvert.SerializeObject(toSerialize,

                    new JsonSerializerSettings {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ObjectCreationHandling = ObjectCreationHandling.Reuse,
                        PreserveReferencesHandling = PreserveReferencesHandling.All,
                        ContractResolver = new LexicalElementSerialization.LexicalElementJsonContractResolver(),
                        MaxDepth = 1,
                    });
        }
    }
    public class LexicalElementJsonContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
            return base.CreateProperties(type, memberSerialization)
                .AsParallel()
                .Where(jp => jp.PropertyType != typeof(LASI.Core.DocumentStructures.Document) &&
                    jp.PropertyType.Assembly.GetName().Name != "LASI.Core.DocumentStructures.Document" &&
                    jp.PropertyType.GetInterface("System.Collections.IEnumerable") == null)
                .ToList();
        }
    }
}