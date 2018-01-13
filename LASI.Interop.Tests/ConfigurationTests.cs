using System;
using System.Text;
using System.IO;
using Xunit;
using NFluent;

namespace LASI.Interop.Tests
{
    public class ConfigurationTests
    {
        [Fact]
        public void InitializeTwiceThrowsSystemAlreadyConfiguredException()
        {
            Configures[new Random().Next(0, 4)]();

            Check.ThatCode(Configures[new Random().Next(0, 4)]).Throws<AlreadyConfiguredException>();
        }

        static readonly Action[] Configures =
        {
            InitializeWithJSONSubkeyConfig,
            InitializeWithSimpleJSONConfig,
            InitializeWithSimpleJSONConfigFromStream,
            InitializeWithSimpleJSONSubkeyConfigFromStream
        };

        /// <summary>
        /// A test for <see cref="Configuration.Initialize(string, ConfigurationFormat)"/>
        /// </summary>
        private static void InitializeWithSimpleJSONConfig()
        {
            var path = @"..\..\configAtTopLevel.json";
            var format = ConfigurationFormat.Json;
            Configuration.Initialize(path, format);
        }


        /// <summary>
        /// A test for <see cref="Configuration.Initialize(string, ConfigurationFormat, string)"/>
        /// </summary>
        static void InitializeWithJSONSubkeyConfig()
        {
            var path = @"..\..\configWithDataSubkey.json";
            var format = ConfigurationFormat.Json;
            var subkey = "Data";
            Configuration.Initialize(path, format, subkey);
        }

        /// <summary>
        /// A test for <see cref="Configuration.Initialize(Stream, ConfigurationFormat)"/>
        /// </summary>
        static void InitializeWithSimpleJSONConfigFromStream()
        {
            var path = @"..\..\configAtTopLevel.json";
            var format = ConfigurationFormat.Json;
            Configuration.Initialize(new MemoryStream(Encoding.Default.GetBytes(File.ReadAllText(path))), format);
        }

        /// <summary>
        /// A test for <see cref="Configuration.Initialize(Stream, ConfigurationFormat, string)"/>
        /// </summary>
        static void InitializeWithSimpleJSONSubkeyConfigFromStream()
        {
            var path = @"..\..\configAtTopLevel.json";
            var format = ConfigurationFormat.Json;
            var subkey = "Data";
            Configuration.Initialize(new MemoryStream(Encoding.Default.GetBytes(File.ReadAllText(path))), format,
                subkey);
        }
    }
}