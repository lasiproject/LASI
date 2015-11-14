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

            Check.ThatCode(Configures[new Random().Next(0, 4)]).Throws<SystemAlreadyConfiguredException>();

        }

        private static readonly Action[] Configures = {
            InitializeWithJSONSubkeyConfig,
            InitializeWithSimpleJSONConfig,
            InitializeWithSimpleJSONConfigFromStream,
            InitializeWithSimpleJSONSubkeyConfigFromStream
        };
        /// <summary>
        /// A test for <see cref="Configuration.Initialize(string, ConfigFormat)"/>
        /// </summary>
        public static void InitializeWithSimpleJSONConfig()
        {
            var path = @"..\..\configAtTopLevel.json";
            var format = ConfigFormat.Json;
            try
            {
                Configuration.Initialize(path, format);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// A test for <see cref="Configuration.Initialize(string, ConfigFormat, string)"/>
        /// </summary>
        public static void InitializeWithJSONSubkeyConfig()
        {
            var path = @"..\..\configWithDataSubkey.json";
            var format = ConfigFormat.Json;
            var subkey = "Data";
            Configuration.Initialize(path, format, subkey);
        }
        /// <summary>
        /// A test for <see cref="Configuration.Initialize(Stream, ConfigFormat)"/>
        /// </summary>
        public static void InitializeWithSimpleJSONConfigFromStream()
        {
            var path = @"..\..\configAtTopLevel.json";
            var format = ConfigFormat.Json;
            Configuration.Initialize(new MemoryStream(Encoding.Default.GetBytes(File.ReadAllText(path))), format);
        }
        /// <summary>
        /// A test for <see cref="Configuration.Initialize(Stream, ConfigFormat, string)"/>
        /// </summary>
        public static void InitializeWithSimpleJSONSubkeyConfigFromStream()
        {
            var path = @"..\..\configAtTopLevel.json";
            var format = ConfigFormat.Json;
            var subkey = "Data";
            Configuration.Initialize(new MemoryStream(Encoding.Default.GetBytes(File.ReadAllText(path))), format, subkey);
        }
    }
}