using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace LASI.Utilities
{
    public abstract class ConfigBase : IConfig
    {
        private readonly string rawConfigData;
        protected ConfigBase(object documentObject)
        {
            rawConfigData = documentObject.ToString();
        }
        protected ConfigBase(string filePath)
        {
            ValidateFileExistence(filePath);
            rawConfigData = File.ReadAllText(filePath);
        }

        protected ConfigBase(Uri uri)
        {
            var request = System.Net.WebRequest.CreateHttp(uri);
            var response = request.GetResponse() as System.Net.HttpWebResponse;
            if (response?.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException("Unable to retrieve the remote document.");
            }
            Func<int> readByte = response.GetResponseStream().ReadByte;
            var chars = new List<char>();
            for (var b = readByte(); b != -1; b = readByte())
            {
                chars.Add((char)b);
            }
            rawConfigData = new string(chars.ToArray());
        }

        private static void ValidateFileExistence(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Unable to locate the specified file.", filePath);
            }
        }
        public string RawConfigData => rawConfigData;

        public abstract string this[string name, StringComparison stringComparison] { get; }
        public abstract string this[string name] { get; }
    }
}