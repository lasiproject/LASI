using LASI.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcExperimentation.LexicalElementSerialization
{
    public static class DefaultLexicalElementSerializer
    {
        public static string ToJson<TLexical>(this TLexical toSerialize) where TLexical : ILexical {

            return JsonConvert.SerializeObject(toSerialize,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore,
                });
        }
    }
}