using LASI.App.Properties;
using LASI.Content;
using System;
using System.Windows;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using LASI.Utilities.Configuration;

namespace LASI.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// A global structure containing dynamic application configuration.
        /// </summary>
        public static IConfig Config { get; private set; }

        /// <summary>
        /// Initializes a new instances of the <see cref="App"/> class
        /// </summary>
        public App()
        {
            LoadPerformancePreference();
            Interop.Configuration.Initialize(new XmlConfig(new XElement(
                    name: "configuration",
                    content: from element in XElement.Load(@"..\..\App.config").Element("appSettings").Elements()
                             let name = element.Attribute("key")
                             let content = element.Attribute("value")
                             where name != null && content != null
                             select new XElement(name: name.Value, content: content.Value))));
        }

        private static IConfig ParseConfig(string configPath) => new XmlConfig(new XElement(
                    name: "configuration",
                    content: from element in XElement.Load(configPath).Element("appSettings").Elements()
                             let name = element.Attribute("key")
                             let content = element.Attribute("value")
                             where name != null && content != null
                             select new XElement(
                                 name: name.Value,
                                 content: content.Value
                             )));

        private static void LoadPerformancePreference()
        {
            if (Enum.TryParse(Settings.Default.PerformanceLevel, out Interop.PerformanceProfile performanceProfile))
            {
                Interop.ResourceUsageManager.SetPerformanceLevel(performanceProfile);
            }
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Settings.Default.AutoCleanProjectFiles && FileManager.Initialized)
            {
                FileManager.DecimateProject();
            }
        }

    }
}
