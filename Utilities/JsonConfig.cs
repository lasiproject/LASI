using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities
{
    public class JsonConfig : IConfig
    {
        public JsonConfig(string filePath) {
            ValidateFileExistence(filePath);
            using (var reader = System.IO.File.OpenText(filePath)) {
                var jsonText = reader.ReadToEnd();
                var jObject = ParseAndValidateJson(jsonText);
                configSource = JObjectToStringDictionary(jObject);
            }
        }

        public JsonConfig(Uri uri) {
            var request = System.Net.WebRequest.CreateHttp(uri);
            var response = request.GetResponse() as System.Net.HttpWebResponse;
            if (response?.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ArgumentException("Unable to retrieve the remote JSON object.");
            }
            Func<int> readByte = response.GetResponseStream().ReadByte;
            var chars = new System.Collections.Generic.List<int>();
            for (var b = readByte(); b != -1; b = readByte()) {
                chars.Add(b);
            }
            var str = new string(chars.Select(i => (char)i).ToArray());
            var jObject = ParseAndValidateJson(str);
            configSource = JObjectToStringDictionary(jObject);
        }
        private static System.Collections.Generic.IDictionary<string, string> JObjectToStringDictionary(JObject source) =>
            source.Properties().ToDictionary(
                property => property.Name,
                property => property.Value.ToString()
            );

        private static JObject ParseAndValidateJson(string jsonText) {
            object data;
            try {
                data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonText);
            } catch (Newtonsoft.Json.JsonReaderException e) {
                throw new InvalidOperationException("Unable to parse data, ensure the file contains a valid JSON structure.", e);
            }
            ValidateJsonStructure(data);
            return data as JObject;
        }

        private static void ValidateFileExistence(string filePath) {
            if (System.IO.File.Exists(filePath)) {
                throw new System.IO.FileNotFoundException("Unable to locate the specified file.", filePath);
            }
        }

        private static void ValidateJsonStructure(object configSource) {
            if (!(configSource is JObject)) {
                throw new InvalidOperationException("The config source must be a JSON document with a single top level object.");
            }
        }

        public string this[string key] => configSource[key];

        public string this[string key, StringComparison stringComparison, string defaultValue] =>
            configSource.FirstOrDefault(pair => pair.Key.Equals(key, stringComparison)).Value ?? defaultValue;

        public string this[string key, StringComparison stringComparison] => configSource.First(pair => pair.Key.Equals(key, stringComparison)).Value;
        public string this[string key, string defaultValue] => configSource.GetValueOrDefault(key, defaultValue);

        private System.Collections.Generic.IDictionary<string, string> configSource;
    }
}