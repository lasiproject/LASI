using System;
using Microsoft.AspNet.Builder;

namespace AspSixApp.BuilderExtensions
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Bootstrap LASI by loading the necessary configuration information from the specified file.
        /// </summary>
        /// <param name="configFile">The location of a JSON file containing the desired settings.</param>
        /// <remarks>
        /// In the desktop application and previous versions of the web application, the
        /// configuration settings were stored in App.config and Web.config respectively, and were
        /// thus automatically loaded into the System.ConfigurationManager.AppSettings property.
        /// This is not possible with the current build of AspNet 5, so this is implemented to fill
        /// the gap. A better solution, one which abstracts the configuration from the assemblies
        /// entirely should be designed and implemented.
        /// </remarks>
        public static void UseLASIComponents(this IApplicationBuilder app, string configFile)
        {
            var lasiConfig = new LASI.Utilities.JsonConfig(configFile);
            TaggerInterop.SharpNLPTagger.InjectedConfiguration = lasiConfig;
            LASI.Core.Heuristics.Lexicon.InjectedConfiguration = lasiConfig;
        }
    }
}