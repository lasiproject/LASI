using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace LASI.WebService
{
    public sealed class TupleJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType.GetInterfaces().Contains(typeof(ITuple));

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
