using LASI.App.Properties;
using LASI.Content;
using System;
using System.Windows;

namespace LASI.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Intializes a new instances of the <see cref="App"/> class
        /// </summary>
        public App()
        {
            LoadPerformancePreference();
            BindEventHandlers();
        }
        private static void LoadPerformancePreference()
        {
            Interop.ResourceManagement.PerformanceLevel performanceMode;
            if (Enum.TryParse(Settings.Default.PerformanceLevel, out performanceMode))
            {
                Interop.ResourceManagement.ResourceUsageManager.SetPerformanceLevel(performanceMode);
            }
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Settings.Default.AutoCleanProjectFiles && FileManager.Initialized) FileManager.DecimateProject();
        }
        private void BindEventHandlers()
        {
            Exit += Application_Exit;
        }


    }
}
