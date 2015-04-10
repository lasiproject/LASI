using LASI.App.Properties;
using LASI.Content;
using System;
using System.Windows;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;

namespace LASI.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instances of the <see cref="App"/> class
        /// </summary>
        public App()
        {
            LoadPerformancePreference();
            Interop.Configuration.Initialize(format: Interop.ConfigFormat.Xml,
                stream: new MemoryStream(
                    Encoding.Default.GetBytes(
                        new XElement(
                            name: "configuration",
                            content: from element in XElement.Load(@"..\..\App.config").Element("appSettings").Elements()
                                     let name = element.Attribute("key")
                                     let content = element.Attribute("value")
                                     where name != null && content != null
                                     select new XElement(name: name.Value, content: content.Value)
                        ).ToString()
                    )
                )
            );

            this.Exit += Application_Exit;
        }
        private static void LoadPerformancePreference()
        {
            Interop.ResourceManagement.PerformanceProfile performanceMode;
            if (Enum.TryParse(Settings.Default.PerformanceLevel, out performanceMode))
            {
                Interop.ResourceManagement.ResourceUsageManager.SetPerformanceLevel(performanceMode);
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
