using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App() {
            Exit += (sender, e) => {
                if (ConfigurationManager.AppSettings["AutoDebugCleanupOn"] == "true") {
                    try {
                        ContentSystem.FileManager.DecimateProject();
                    } catch (ContentSystem.FileManagerNotInitializedException) {
                    }
                }
            };
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsCollection = Configuration.AppSettings.Settings;
        }
        public KeyValueConfigurationCollection AppSettingsCollection { get; private set; }
        public Configuration Configuration { get; private set; }
    }
}
