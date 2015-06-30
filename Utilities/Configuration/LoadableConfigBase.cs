using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using LASI.Utilities.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// Base class for configuration objects which can be loaded from a persistent backing store.
    /// </summary>
    public abstract class LoadableConfigBase
    {
        /// <summary>
        /// Reads the contents of the file indicated by the given path returning a single string containing the entire file's contents.
        /// </summary>
        /// <param name="filePath">The path of the file to read.</param>
        /// <returns></returns>
        protected string ReadConfigDataFromFile(string filePath)
        {
            ValidateFileExistence(filePath);
            return File.ReadAllText(filePath);
        }
        /// <summary>
        /// Downloads config data from the specified URI and returns it as a single raw string.
        /// </summary>
        /// <param name="uri">The URI identifying the config data source.</param>
        protected string DownloadRemoteConfigData(Uri uri)
        {
            var request = System.Net.WebRequest.CreateHttp(uri);
            var response = request.GetResponse() as System.Net.HttpWebResponse;
            if (response?.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException("Unable to retrieve the remote document.");
            }
            return Encoding.GetEncoding(response.ContentEncoding).GetString(EnumerateResponseStreamBytes(response).ToArray());
        }
        /// <summary>
        /// Serializes and uploads the config dictionary to the remove location.
        /// </summary>
        /// <param name="uri">The location to upload.</param>
        /// <param name="contentType">The content type of to upload the serialized data as.</param>
        /// <param name="content">The collection of key value pairs to upload</param>
        protected void UploadRemoteConfigData(Uri uri, System.Net.Mime.ContentType contentType, IDictionary<string, string> content)
        {
            var request = WebRequest.CreateHttp(uri);
            request.Method = "POST";
            request.ContentType = contentType.MediaType;
            byte[] data;
            if (contentType.MediaType.EqualsIgnoreCase("application/json"))
            {
                data = Encoding.Unicode.GetBytes(new JObject(from pair in content select new JProperty(pair.Key, pair.Value)).ToString());
            }
            else if (contentType.MediaType.EqualsIgnoreCase("application/xml"))
            {
                data = data = Encoding.Unicode.GetBytes(new XDocument(from pair in content select new XElement(pair.Key, pair.Value)).ToString());
            }
            else { throw new ArgumentException($"Invalid content type {contentType.MediaType}"); }
            request.GetRequestStream().Write(data, 0, data.Length);
            var response = request.GetResponse() as HttpWebResponse;
            if (!new[] { HttpStatusCode.OK, HttpStatusCode.Created, HttpStatusCode.NotModified, HttpStatusCode.NoContent, HttpStatusCode.Accepted }.Contains(response.StatusCode))
            {
                throw new InvalidOperationException("Unable to persist config changes to the remote source");
            }
        }


        private static IEnumerable<byte> EnumerateResponseStreamBytes(System.Net.HttpWebResponse response)
        {
            unchecked
            {
                using (var stream = response.GetResponseStream())
                {

                    for (var b = stream.ReadByte(); b != -1; b = stream.ReadByte())
                    {
                        yield return (byte)b;
                    }
                }
            }
        }
        protected static JObject ParseAndValidateJson(string jsonText)
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
            Validate.NotNull(data, nameof(data), ErrorMessages.NoRootObject);

            return result;
        }
        private static void ValidateFileExistence(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Unable to locate the specified file.", filePath);
            }
        }
        private static class ErrorMessages
        {
            public const string IllformedJsonDocument = "Unable to parse the data, ensure the source is a well formed JSON document";
            public const string NoRootObject = @"The config source is invalid. Valid config sources must be represented as a single top level JSON object";
        }

    }
}
