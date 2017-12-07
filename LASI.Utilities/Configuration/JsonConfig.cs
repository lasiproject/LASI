using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using LASI.Utilities;
using System.Collections.Generic;

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
        public string this[string name] => data.ContainsKey(name) ? data[name] : null;

        void MakeDictionary(ref IVariantDictionary<string, string> dict, JObject configSource)
        {
            dict = configSource
                .Properties()
                .ToVariantDictionary(p => p.Name, p => p.Value?.ToString());
        }

        readonly IVariantDictionary<string, string> data;
    }

    public static class JTokenExtensions
    {
        public static void Deconstruct(this JProperty jToken, out string name, out JToken value) => (name, value) = (jToken.Name, jToken.Value);
    }
}