using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// A JSON Based configuration source.
    /// </summary>
    public class JsonConfig : LoadableConfigBase, IConfig
    {
        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="filePath">
        /// A file path specifying the location of the JSON document which will define the
        /// configuration object.
        /// </param>
        public JsonConfig(string filePath)
        {
            MakeDictionary(ref data, ParseAndValidateJson(ReadConfigDataFromFile(filePath)));
        }

        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="uri">
        /// A Uri specifying the location of the JSON document which will define the configuration object.
        /// </param>
        public JsonConfig(Uri uri)
        {
            MakeDictionary(ref data, ParseAndValidateJson(DownloadRemoteConfigData(uri)));
        }

        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="jObject">
        /// A <see cref="JObject"/> which will define the configuration object.
        /// </param>
        public JsonConfig(JObject jObject)
        {
            MakeDictionary(ref data, jObject);
        }

        /// <summary>Gets the value with the specified name.</summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <returns>The value with the specified name.</returns>
        public string this[string name] => data.ContainsKey(name) ? data[name] : default;

        private void MakeDictionary(ref IVariantDictionary<string, string> dict, JObject configSource)
        {
            dict = (from property in configSource.Properties()
                    let isAggregate = property.Value is JContainer
                    select new
                    {
                        property.Name,
                        Value = property.Value.ToString()
                    })
                   .ToVariantDictionary(p => p.Name, p => p.Value);
        }

        private readonly IVariantDictionary<string, string> data;
    }
}
