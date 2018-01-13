using LASI.Utilities;
using LASI.Utilities.Configuration;
using LASI.Utilities.Validation;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace LASI.Interop
{
    /// <summary>
    /// Provides clients with the ability to configure LASI analytical components.
    /// </summary>
    public static class Configuration
    {
        public static void Initialize(IConfig config) => Initialize(() => config);

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required resource files.
        /// </summary>
        /// <param name="configUrl">The location of an XML or JSON document containing configuration information.</param>
        /// <param name="format">Specifies the format of the document containing the configuration information.</param>
        public static void Initialize(string configUrl, ConfigurationFormat format) => Initialize(configUrl, format, null);

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required resource files.
        /// </summary>
        /// <param name="configUrl">The location of an XML or JSON document containing configuration information.</param>
        /// <param name="format">Specifies the format of the document containing the configuration information.</param>
        /// <param name="subkey">
        /// Specifies the name of the child object or nested element containing the resource configuration information. For example, if the configure source is a "classical" App.config file, such as
        /// that used by the LASI.App WPF application, the value of this argument would be "AppSettings".
        /// </param>
        public static void Initialize(string configUrl, ConfigurationFormat format, string subkey)
        {
            var isUrl = Uri.IsWellFormedUriString(configUrl, UriKind.Absolute);
            var isFsPath = File.Exists(Path.GetFullPath(configUrl));
            Validate.Either(isUrl, isFsPath,
                $"The specified url is neither an accessible file system location nor a valid Uri");
            Initialize(
                stream: isUrl ? WebRequest.CreateHttp(configUrl).GetRequestStream() :
                isFsPath ? new FileStream(
                    options: FileOptions.Asynchronous,
                    path: Path.GetFullPath(configUrl),
                    mode: FileMode.Open,
                    access: FileAccess.Read,
                    share: FileShare.Read,
                    bufferSize: 1024
                ) : null,
                format: format,
                subkey: subkey
            );
        }

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required resources files.
        /// </summary>
        /// <param name="stream">A stream containing a JSON or XML document holding the desired configuration information.</param>
        /// <param name="format">Specifies the format of the document containing the configuration information.</param>
        public static void Initialize(Stream stream, ConfigurationFormat format) => Initialize(stream, format, null);

        /// <summary>
        /// Configures how components are initialized by specifying the locations of required resources files.
        /// </summary>
        /// <param name="stream">A stream containing a JSON or XML document holding the desired configuration information.</param>
        /// <param name="format">Specifies the format of the document containing the configuration information.</param>
        /// <param name="subkey">
        /// Specifies the format of the stream containing the configuration information. Specifies the name of the child object or nested element containing the resource configuration information. For
        /// example, if the configuration source is a "classical" App.config file, such as that used by the LASI.App WPF application, the value of this argument would be "AppSettings".
        /// </param>
        public static void Initialize(Stream stream, ConfigurationFormat format, string subkey)
        {
            using (var reader = new StreamReader(stream))
            {
                InitializeImplementation(reader.ReadToEnd(), format, subkey);
            }
        }

        static void Initialize(Func<IConfig> configFactory)
        {
            lock (initializationLock)
            {
                Validate.False(alreadyConfigured, () => new AlreadyConfiguredException());
                var config = configFactory();
                InitializeComponents(config);
                alreadyConfigured = true;
            }
        }

        static void InitializeImplementation(string raw, ConfigurationFormat format, string subkey)
        {
            lock (initializationLock)
            {
                Validate.False<AlreadyConfiguredException>(alreadyConfigured);
                var config = format is ConfigurationFormat.Json is var j
                    ? loadJsonConfig()
                    : format is ConfigurationFormat.Xml is var x
                        ? loadXmlConfig()
                        : throw new ArgumentException(configFormatError);

                InitializeComponents(config);
                alreadyConfigured = true;
            }

            IConfig loadXmlConfig() =>
                    new XmlConfig(subkey.IsNullOrWhiteSpace()
                ? XElement.Parse(raw)
                : XElement.Parse(raw).Element(subkey));

            IConfig loadJsonConfig() =>
                    new JsonConfig((JObject)(subkey.IsNullOrWhiteSpace()
                ? JToken.Parse(raw)
                : JToken.Parse(raw).SelectToken(subkey)));
        }

        static void InitializeComponents(IConfig settings)
        {
            Core.Configuration.Configuration.Initialize(settings);
            LASI.Content.Configuration.Initialize(settings);
        }

        static bool alreadyConfigured;

        static readonly string configFormatError =
                $"Invalid config format, specify {nameof(ConfigurationFormat)}: {nameof(ConfigurationFormat.Json)} or {nameof(ConfigurationFormat)}{nameof(ConfigurationFormat.Xml)}";

        static readonly object initializationLock = new object();
    }
}
