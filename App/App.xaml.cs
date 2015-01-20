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
        App() {
            LoadPerformancePreference();
            BindEventHandlers();
        }
        private static void LoadPerformancePreference() {
            Interop.ResourceManagement.UsageManager.Mode performanceLevel;
            if (Enum.TryParse(Settings.Default.PerformanceLevel, out performanceLevel)) {
                Interop.ResourceManagement.UsageManager.SetPerformanceLevel(performanceLevel);
            }
        }
        private void Application_Exit(object sender, ExitEventArgs e) {
            if (Settings.Default.AutoCleanProjectFiles && FileManager.Initialized) FileManager.DecimateProject();
        }
        private void BindEventHandlers() {
            Exit += Application_Exit;
        }


    }
}
