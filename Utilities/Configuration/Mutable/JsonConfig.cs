using System;
using System.Linq;
using System.Text;

namespace LASI.Utilities.Configuration.Mutable
{
    using System.Collections.Generic;
    using JObject = Newtonsoft.Json.Linq.JObject;
    public class JsonConfig : MutableConfigBase, IMutableConfig
    {
        private readonly  IDictionary<string, string> data;

        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="filePath">
        /// A file path specifying the location of the JSON document which will define the
        /// configuration object.
        /// </param>
        public JsonConfig(string filePath)
        {
            data = MakeDictionary(ParseAndValidateJson(ReadConfigDataFromFile(filePath)));
        }

        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="uri">
        /// A Uri specifying the location of the JSON document which will define the configuration object.
        /// </param>
        public JsonConfig(Uri uri)
        {
            data = MakeDictionary(ParseAndValidateJson(DownloadRemoteConfigData(uri)));
        }

        /// <summary>Initializes a new instance of the JsonConfig class.</summary>
        /// <param name="jObject">
        /// A <see cref="JObject"/> which will define the configuration object.
        /// </param>
        public JsonConfig(JObject jObject)
        {
            data = MakeDictionary(jObject);
        }

        private IDictionary<string, string> MakeDictionary(JObject configSource) => configSource.Properties().ToDictionary(e => e.Name, e => (string)e.Value);

        public override void Persist()
        {
            throw new NotImplementedException();
        }

        public override string this[string name]
        {
            get { return data[name]; }
            set { data[name] = value; }
        }
    }
}
