using System;
using LASI.Utilities;

namespace LASI.Interop
{
    using File = System.IO.File;
    using IConfig = Utilities.Configuration.IConfig;
    using JObject = Newtonsoft.Json.Linq.JObject;
    using JsonConfig = Utilities.Configuration.JsonConfig;
    using JToken = Newtonsoft.Json.Linq.JToken;
    using Path = System.IO.Path;
    using Validate = Utilities.Validation.Validate;
    using XElement = System.Xml.Linq.XElement;
    using XmlConfig = Utilities.Configuration.XmlConfig;

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
        /// configuration information. For example, if the configure source is a "classical"
        /// App.config file, such as that used by the LASI.App WPF application, the value of this
        /// argument would be "AppSettings".
        /// </param>
        public static void Initialize(string resourceConfigSourceLocation, ConfigFormat format, string subkey)
        {
            var rawConfigData = File.ReadAllText(Path.GetFullPath(resourceConfigSourceLocation));
            InitializeImplementation(rawConfigData, format, subkey);
        }

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
        /// information. For example, if the configuration source is a "classical" App.config file, such
        /// as that used by the LASI.App WPF application, the value of this argument would be "AppSettings".
        /// </param>
        public static void Initialize(System.IO.Stream stream, ConfigFormat format, string subkey)
        {
            var rawConfigData = new System.IO.StreamReader(stream).ReadToEnd();
            InitializeImplementation(rawConfigData, format, subkey);
        }

        private static void InitializeImplementation(string raw, ConfigFormat format, string subkey)
        {
            Validate.ExistsIn(format, nameof(format), ConfigFormat.Json, ConfigFormat.Xml);

            lock (initializationLock)
            {
                if (configured)
                {
                    throw new InvalidOperationException(AlreadyConfiguredErrorMessage);
                };

                Func<IConfig> loadXmlConfig = () => new XmlConfig(subkey.IsNullOrWhiteSpace() ? XElement.Parse(raw) : XElement.Parse(raw).Element(subkey));

                Func<IConfig> loadJsonConfig = () => new JsonConfig((JObject)(subkey.IsNullOrWhiteSpace() ? JToken.Parse(raw) : JToken.Parse(raw).SelectToken(subkey)));

                Action<IConfig> initializeComponents = settings =>
                {
                    Core.Configuration.Configuration.Initialize(settings);
                    Content.InteropBindings.Configuration.Initialize(settings);
                };

                var config = format == ConfigFormat.Json ? loadJsonConfig() : loadXmlConfig();
                initializeComponents(config);
                configured = true;
            }
        }

        private static bool configured;
        private static readonly object initializationLock = new object();
        private const string AlreadyConfiguredErrorMessage = "Configuration has already been Initialized. Initialize may only be called once.";
    }

    public enum ConfigFormat
    {
        Json,
        Xml
    }
}