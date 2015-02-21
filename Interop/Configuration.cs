using System;
using LASI.Utilities;
using File = System.IO.File;
using JContainer = Newtonsoft.Json.Linq.JContainer;
using JObject = Newtonsoft.Json.Linq.JObject;
using JsonConfig = LASI.Utilities.Configuration.JsonConfig;
using JToken = Newtonsoft.Json.Linq.JToken;
using Path = System.IO.Path;
using Validate = LASI.Utilities.Validation.Validate;
using XElement = System.Xml.Linq.XElement;
using XmlConfig = LASI.Utilities.Configuration.XmlConfig;

namespace LASI.Interop
{
    public static class Configuration
    {
        /// <summary>
        /// Configures how components are initialized by specifying the locations of required
        /// resource files.
        /// </summary>
        /// <param name="resourceConfigSourceLocation">
        /// The location of an XML or JSON document containing configuration information.
        /// </param>
        /// <param name="format">
        /// Specifies the format of the document containing the configuration information.
        /// </param>
        public static void Initialize(string resourceConfigSourceLocation, ConfigFormat format) => Initialize(resourceConfigSourceLocation, format, null);
        /// <summary>
        /// Configures how components are initialized by specifying the locations of required
        /// resources files.
        /// </summary>
        /// <param name="stream">
        /// A stream containing a JSON or XML document holding the desired configuration information.
        /// </param>
        public static void Initialize(System.IO.Stream stream, ConfigFormat format) => Initialize(stream, format, null);

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required
        /// resources files.
        /// </summary>
        /// <param name="stream">
        /// A stream containing a JSON or XML document holding the desired configuration information.
        /// </param>
        /// <param name="subkey">
        /// Specifies the format of the stream containing the configuration information. Specifies
        /// the name of the child object or nested element containing the resource configuration
        /// inforamation. For example, if the config source is a "classical" App.config file, such
        /// as that used by the LASI.App WPF application, the value of this argument would be "AppSettings".
        /// </param>
        public static void Initialize(System.IO.Stream stream, ConfigFormat format, string subkey)
        {
            var rawConfigData = new System.IO.StreamReader(stream).ReadToEnd();
            PerformInitialization(
                () => LoadConfiguration(rawConfigData, format, null)
            );
        }

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required
        /// resource files.
        /// </summary>
        /// <param name="resourceConfigSourceLocation">
        /// The location of an XML or JSON document containing configuration information.
        /// </param>
        /// <param name="format">
        /// Specifies the format of the document containing the configuration information.
        /// </param>
        /// <param name="subkey">
        /// Specifies the name of the child object or nested element containing the resource
        /// configuration inforamation. For example, if the config source is a "classical"
        /// App.config file, such as that used by the LASI.App WPF application, the value of this
        /// argument would be "AppSettings".
        /// </param>
        public static void Initialize(string resourceConfigSourceLocation, ConfigFormat format, string subkey)
        {
            var path = Path.GetFullPath(resourceConfigSourceLocation);
            var rawConfigData = File.ReadAllText(path);
            PerformInitialization(
                () => LoadConfiguration(rawConfigData, format, subkey)
            );
        }

        private static void PerformInitialization(Action initialization)
        {
            lock (lockon)
            {
                if (configured)
                {
                    throw new System.InvalidOperationException(AlreadyConfiguredErrorMessage);
                }
                configured = true;
                initialization();
            }
        }

        private static void LoadConfiguration(string rawConfigData, ConfigFormat format, string subkey)
        {
            Validate.ExistsIn(format, nameof(format), ConfigFormat.Json, ConfigFormat.Xml);

            switch (format)
            {
                case ConfigFormat.Json:
                var jsonConfig = LoadJsonConfig(rawConfigData, subkey);
                InitializeCoreAndContentConfiguration(jsonConfig);
                break;

                case ConfigFormat.Xml:
                var xmlConfig = LoadXmlConfig(rawConfigData, subkey);
                InitializeCoreAndContentConfiguration(xmlConfig);
                break;
            }
        }

        private static void InitializeCoreAndContentConfiguration(Utilities.Configuration.IConfig config)
        {
            Core.InteropBindings.Configuation.Initialize(config);
            Content.InteropBindings.Configuration.Initialize(config);
        }

        private static JsonConfig LoadJsonConfig(string rawJson, string subkey)
        {
            var configObject = JToken.Parse(rawJson) as JContainer;
            return new JsonConfig((JObject)(subkey.IsNullOrWhiteSpace() ? configObject : configObject.SelectToken(subkey)));
        }

        private static XmlConfig LoadXmlConfig(string rawXml, string subkey)
        {
            return new XmlConfig(subkey.IsNullOrWhiteSpace() ? XElement.Parse(rawXml) : XElement.Parse(rawXml).Element(subkey));
        }

        private const string AlreadyConfiguredErrorMessage = "Configure has already been invoked. Configure may only be called once.";
        private static object lockon = new object();
        private static bool configured;
    }

    public enum ConfigFormat
    {
        Json,
        Xml
    }
}