using System;
using System.IO;
using System.Linq;
using LASI.Utilities.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// A JSON Based configuration source.
    /// </summary>
    public sealed class JsonConfig : IConfig
    {

        /// <summary>
        /// Initializes a new instance of the JsonConfig class.
        /// </summary>
        /// <param name="filePath">A file path specifying the location of the JSON document which will define the configuration object.</param>
        public JsonConfig(string filePath) => MakeDictionary(ref data, ParseAndValidateJson(File.ReadAllText(filePath)));

        /// <summary>
        /// Initializes a new instance of the JsonConfig class.
        /// </summary>
        /// <param name="jObject">A <see cref="JObject"/> which will define the configuration object.</param>
        public JsonConfig(JObject jObject) => MakeDictionary(ref data, jObject);


        /// <summary>
        /// Parses a JSON text containing configuration and returns the result as a <see cref="JObject"/>.
        /// </summary>
        /// <param name="json">The serialized configuration.</param>
        /// <returns>A <see cref="JObject"/> containing the deserialized result.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="json"/> does not represent a valid JSON object.</exception>
        static JObject ParseAndValidateJson(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<JObject>(json) ?? throw new InvalidOperationException("The config source is invalid. Valid config sources must be represented as a single top level JSON object");
            }
            catch (JsonReaderException e)
            {
                throw new ArgumentException("Unable to parse the data, ensure the source is a well formed JSON document", paramName: nameof(json), e);
            }
        }

        void MakeDictionary(ref ILookup<string, string> dict, JObject configSource)
        {
            dict = configSource.Properties().ToLookup(p => p.Name, p => p.Value?.ToString());
        }

        /// <summary>
        /// Gets the value with the specified name.
        /// </summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <returns>The value with the specified name.</returns>
        public string this[string name] => data[name].FirstOrDefault();
        readonly ILookup<string, string> data;
    }

    static class JTokenExtensions
    {
        public static void Deconstruct(this JProperty jProperty, out string name, out JToken value) => (name, value) = (jProperty.Name, jProperty.Value);
    }
}