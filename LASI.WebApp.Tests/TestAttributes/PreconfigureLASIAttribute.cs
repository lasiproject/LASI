using System;
using System.Reflection;
//using System.IO;
//using System.Reflection;
using LASI.Utilities;

namespace LASI.WebApp.Tests.TestAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class PreconfigureLASIAttribute : Xunit.Sdk.BeforeAfterTestAttribute
    {
        private const string ConfigFileName = "config.json";
        private const string LASIComponentConfigSubkey = "Resources";

        public override void Before(MethodInfo methodUnderTest)
        {
            base.Before(methodUnderTest);
            ConfigureLASIComponents(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), ConfigFileName), LASIComponentConfigSubkey);
        }
        private static void ConfigureLASIComponents(string fileName, string subkey)
        {
            Interop.ResourceUsageManager.SetPerformanceLevel(Interop.PerformanceProfile.High);
            try
            {
                Interop.Configuration.Initialize(fileName, Interop.ConfigFormat.Json, subkey);
            }
            catch (Interop.AlreadyConfiguredException)
            {
                //Console.WriteLine("LASI was already setup by a previous test; continuing");
            }
            Logger.SetToSilent();
        }
    }
}
