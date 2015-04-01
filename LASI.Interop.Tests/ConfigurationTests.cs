using LASI.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Test.Helpers;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Interop.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        [ExpectedException(typeof(SystemAlreadyConfiguredException))]
        public void Initialize_Twice_Throws_SystemAlreadyConfiguredException()
        {
            var configures = new Action[] {
                Initialize_With_JSON_Subkey_Config_Test,
                Initialize_With_Simple_JSON_Config_Test,
                Initialize_With_Simple_JSON_Subkey_Config_From_Stream_Test,
                Initialize_With_Simple_JSON_Config_From_Stream_Test
            };
            var index = new Random().Next(0, 4);
            var configure = configures[index];
            configure();
            //($"Called: {configure.Method.Name} with arguments {string.Join(", ", configure.Method.GetParameters().Select(p => $"{p.ParameterType}: {p.Name}"))}");


            var secondIndex = new Random().Next(0, 4);
            configures[secondIndex]();

        }

        /// <summary>
        /// A test for <see cref="Configuration.Initialize(string, ConfigFormat)"/>
        /// </summary>
        public void Initialize_With_Simple_JSON_Config_Test()
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
        public void Initialize_With_JSON_Subkey_Config_Test()
        {
            var path = @"..\..\configWithDataSubkey.json";
            var format = ConfigFormat.Json;
            var subkey = "Data";
            Configuration.Initialize(path, format, subkey);
        }
        /// <summary>
        /// A test for <see cref="Configuration.Initialize(System.IO.Stream, ConfigFormat)"/>
        /// </summary>
        public void Initialize_With_Simple_JSON_Config_From_Stream_Test()
        {
            var path = @"..\..\configAtTopLevel.json";
            var format = ConfigFormat.Json;
            Configuration.Initialize(new MemoryStream(Encoding.Default.GetBytes(File.ReadAllText(path))), format);
        }
        /// <summary>
        /// A test for <see cref="Configuration.Initialize(System.IO.Stream, ConfigFormat, string)"/>
        /// </summary>
        public void Initialize_With_Simple_JSON_Subkey_Config_From_Stream_Test()
        {
            var path = @"..\..\configAtTopLevel.json";
            var format = ConfigFormat.Json;
            var subkey = "Data";
            Configuration.Initialize(new MemoryStream(Encoding.Default.GetBytes(File.ReadAllText(path))), format, subkey);
        }
    }
}