using System;
using System.IO;
using System.Linq;

namespace LASI.Utilities
{
    public abstract class ConfigBase : IConfig
    {
        private readonly string rawConfigData;

        protected ConfigBase(string filePath) {
            ValidateFileExistence(filePath);
            var xmlText = File.ReadAllText(filePath);
        }

        protected ConfigBase(Uri uri) {
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
            rawConfigData = new string(chars.Select(i => (char)i).ToArray());
        }

        private static void ValidateFileExistence(string filePath) {
            if (System.IO.File.Exists(filePath)) {
                throw new FileNotFoundException("Unable to locate the specified file.", filePath);
            }
        }
        public string RawConfigData => rawConfigData;

        public abstract string this[string name, StringComparison stringComparison] { get; }
        public abstract string this[string name] { get; }
    }
}