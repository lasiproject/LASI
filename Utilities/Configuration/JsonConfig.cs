using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// A JSON Based configuration source.
    /// </summary>
    public class JsonConfig : ConfigBase
    {
        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="filePath">
        /// A file path specifying the location of the JSON document which will define the
        /// configuration object.
        /// </param>
        public JsonConfig(string filePath) : base(filePath)
        {
            jObject = ParseAndValidateJson(RawConfigData);
        }

        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="uri">
        /// A Uri specifying the location of the JSON document which will define the configuration object.
        /// </param>
        public JsonConfig(Uri uri) : base(uri)
        {
            this.jObject = ParseAndValidateJson(RawConfigData);
        }
        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="jObject">
        /// A <see cref="JObject"/> which will define the configuration object.
        /// </param>
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
                throw new ArgumentException(ErrorMessages.IllformedJsonDocument, e);
            }
            var result = data as JObject;
            if (result == null)
            {
                throw new ArgumentException(ErrorMessages.WrongTopLevelStructure);
            }
            return result;
        }
        /// <summary>Gets the value with the specified name.</summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <returns>The value with the specified name.</returns>
        public override string this[string name] => this[name, StringComparison.CurrentCulture];
        /// <summary>
        /// Gets the value with the specified name, matching based on the specified <see cref="System.StringComparison" />.
        /// </summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <param name="stringComparison">The string comparison to use for matching the name.</param>
        /// <returns>The value with the specified name, in the context of the <see cref="System.StringComparison" />.</returns>
        public override string this[string name, StringComparison stringComparison] => (string)jObject.GetValue(name, stringComparison);

        private readonly JObject jObject;

        private static class ErrorMessages
        {
            public const string IllformedJsonDocument = "Unable to parse the data, ensure the source is a well formed JSON document";
            public const string WrongTopLevelStructure = "The config source must be a JSON document with a single top level object.";
        }
    }
}