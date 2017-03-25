using System;
using System.Linq;
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
    /// <summary>
    /// Provides clients with the ability to configure LASI analytical components.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Configures how components are initialized by specifying the locations of required
        /// resource files.
        /// </summary>
        /// <param name="configUrl">
        /// The location of an XML or JSON document containing configuration information.
        /// </param>
        /// <param name="format">
        /// Specifies the format of the document containing the configuration information.
        /// </param>
        public static void Initialize(string configUrl, ConfigFormat format) => Initialize(configUrl, format, null);

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required
        /// resource files.
        /// </summary>
        /// <param name="configUrl">
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
        public static void Initialize(string configUrl, ConfigFormat format, string subkey)
        {
            var isUrl = Uri.IsWellFormedUriString(configUrl, UriKind.Absolute);
            var isFsPath = File.Exists(Path.GetFullPath(configUrl));
            Validate.Either(isUrl, isFsPath, $"The specified url is neither an accessible file system location nor a valid Uri");
            Initialize(
                stream: isUrl ? System.Net.WebRequest.CreateHttp(configUrl).GetRequestStream() :
                        isFsPath ? new System.IO.FileStream(Path.GetFullPath(configUrl), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read) : null,
                format: format,
                subkey: subkey
            );
        }

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required
        /// resources files.
        /// </summary>
        /// <param name="stream">
        /// A stream containing a JSON or XML document holding the desired configuration information.
        /// </param>
        /// <param name="format">
        /// Specifies the format of the document containing the configuration information.
        /// </param>
        public static void Initialize(System.IO.Stream stream, ConfigFormat format) => Initialize(stream, format, null);

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required
        /// resources files.
        /// </summary>
        /// <param name="stream">
        /// A stream containing a JSON or XML document holding the desired configuration information.
        /// </param>
        /// <param name="format">
        /// Specifies the format of the document containing the configuration information.
        /// </param>
        /// <param name="subkey">
        /// Specifies the format of the stream containing the configuration information. Specifies
        /// the name of the child object or nested element containing the resource configuration
        /// information. For example, if the configuration source is a "classical" App.config file, such
        /// as that used by the LASI.App WPF application, the value of this argument would be "AppSettings".
        /// </param>
        public static void Initialize(System.IO.Stream stream, ConfigFormat format, string subkey)
        {
            using (var reader = new System.IO.StreamReader(stream))
            {
                InitializeImplementation(reader.ReadToEnd(), format, subkey);
            }
        }
        public static void Initialize(IConfig config) => Initialize(() => config);
        public static void Initialize(Func<IConfig> configFactory)
        {
            lock (InitializationLock)
            {
                Validate.False(alreadyConfigured, () => new AlreadyConfiguredException());
                var config = configFactory();
                InitializeComponents(config);
                alreadyConfigured = true;
            }
        }

        private static void InitializeImplementation(string raw, ConfigFormat format, string subkey)
        {
            Validate.ExistsIn(from ConfigFormat cf in Enum.GetValues(typeof(ConfigFormat)) select cf, format, nameof(format), $"Invalid config format, specify {typeof(ConfigFormat).Name}{nameof(ConfigFormat.Json)} or {nameof(ConfigFormat.Xml)}");

            lock (InitializationLock)
            {
                Validate.False(alreadyConfigured, () => new AlreadyConfiguredException());

                IConfig LoadXmlConfig() => new XmlConfig(subkey.IsNullOrWhiteSpace() ? XElement.Parse(raw) : XElement.Parse(raw).Element(subkey));

                IConfig LoadJsonConfig() => new JsonConfig((JObject) (subkey.IsNullOrWhiteSpace() ? JToken.Parse(raw) : JToken.Parse(raw).SelectToken(subkey)));


                var config = format == ConfigFormat.Json ? LoadJsonConfig() : LoadXmlConfig();
                InitializeComponents(config);
                alreadyConfigured = true;
            }
        }

        private static void InitializeComponents(IConfig settings)
        {
            Core.Configuration.Configuration.Initialize(settings);
            Content.InteropBindings.Configuration.Initialize(settings);
        }

        private static bool alreadyConfigured;
        private static readonly object InitializationLock = new object();
    }
    /// <summary>
    /// Defines the valid formats for configuration sources.
    /// </summary>
    public enum ConfigFormat
    {
        /// <summary>
        /// JSON
        /// </summary>
        Json,
        /// <summary>
        /// XML
        /// </summary>
        Xml
    }
}