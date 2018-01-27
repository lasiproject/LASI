using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities
{
    public static class NewtonsoftJsonExtensions
    {
        public static void Deconstruct(this JProperty property, out string name, out JToken value, out Action remove) =>
            (name, value, remove) = (property.Name, property.Value, property.Remove);
    }
}
