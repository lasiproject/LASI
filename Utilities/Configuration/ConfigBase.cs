using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// Provides a base class for implementations of the <see cref="IConfig"/> Interface.
    /// </summary>
    public abstract class ConfigBase : IConfig
    {
        private readonly string rawConfigData;
        /// <summary>
        /// Initializes a new instance of the ConfigBase class with the provided source.
        /// </summary>
        /// <param name="documentObject">The config source.</param>
        protected ConfigBase(object documentObject)
        {
            rawConfigData = documentObject.ToString();
        }
        /// <summary>
        /// Initializes a new instance of the ConfigBase class with the data from the specified file.
        /// </summary>
        /// <param name="filePath">The file to load configuration settings from.</param>
        protected ConfigBase(string filePath)
        {
            ValidateFileExistence(filePath);
            rawConfigData = File.ReadAllText(filePath);
        }
        /// <summary>
        /// Initializes a new instance of the ConfigBase class with the data located at the specified URI.
        /// </summary>
        /// <param name="uri">The URI locating the configuration settings.</param>
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
        /// <summary>
        /// Gets the raw textual representation of the configuration object.
        /// </summary>
        public string RawConfigData => rawConfigData;
        /// <summary>
        /// Gets the value with the specified key under the specified string comparison.
        /// </summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <param name="stringComparison">The string comparison to use when matching the name.</param>
        /// <returns>The value with the specified key under the specified string comparison.</returns>
        public abstract string this[string name, StringComparison stringComparison] { get; }
        /// <summary>
        /// Gets the value with the specified key.
        /// </summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <returns>The value with the specified key under the specified string comparison.</returns>
        public abstract string this[string name] { get; }
    }
}