using System;
using System.IO;
using System.Reflection;
using LASI.Utilities;

namespace LASI.WebApp.Tests.TestAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PreconfigureLASIAttribute : Xunit.Sdk.BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
            ConfigureLASIComponents(Path.Combine(Directory.GetCurrentDirectory(), "config.json"), "Data");
            base.Before(methodUnderTest);
        }
        private static void ConfigureLASIComponents(string fileName, string subkey)
        {
            Interop.ResourceManagement.ResourceUsageManager.SetPerformanceLevel(Interop.ResourceManagement.PerformanceProfile.High);
            try { Interop.Configuration.Initialize(fileName, Interop.ConfigFormat.Json, subkey); }
            catch (Interop.SystemAlreadyConfiguredException e)
            {
                e.Log();
            }
        }
    }
}
