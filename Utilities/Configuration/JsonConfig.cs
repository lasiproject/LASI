using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities.Configuration
{
    public class JsonConfig : ConfigBase
    {
        public JsonConfig(string filePath) : base(filePath)
        {
            jObject = ParseAndValidateJson(RawConfigData);
        }

        public JsonConfig(Uri uri) : base(uri)
        {
            this.jObject = ParseAndValidateJson(RawConfigData);
        }

        public JsonConfig(JObject jObject) : base(jObject)
        {
            this.jObject = jObject;
        }

        private static JObject ParseAndValidateJson(string jsonText)
        {
            object data;
            try
            {
                data = JsonConvert.DeserializeObject(jsonText);
            }
            catch (JsonReaderException e)
            {
                throw new InvalidOperationException("Unable to parse data, ensure the file contains a valid JSON structure.", e);
            }
            ValidateJsonStructure(data);
            return data as JObject;
        }

        private static void ValidateJsonStructure(object configSource)
        {
            if (!(configSource is JObject))
            {
                throw new InvalidOperationException("The config source must be a JSON document with a single top level object.");
            }
        }

        public override string this[string name] => this[name, StringComparison.CurrentCulture];

        public override string this[string name, StringComparison stringComparison] => (string)jObject.GetValue(name, stringComparison);

 
        private readonly JObject jObject;
    }
}