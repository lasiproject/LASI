using LASI.App.Properties;
using LASI.Interop;
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
            LoadPreferences();
            BindEventHandlers();
        }
        private static void LoadPreferences() {
            Interop.ResourceMonitoring.UsageManager.Mode performanceLevel;
            if (Enum.TryParse(Settings.Default.PerformanceLevel, out performanceLevel)) {
                Interop.ResourceMonitoring.UsageManager.SetPerformanceLevel(performanceLevel);
            }
        }
        private void Application_Exit(object sender, ExitEventArgs e) {
            if (Settings.Default.AutoCleanProjectFiles) {
                try {
                    ContentSystem.FileManager.DecimateProject();
                }
                catch (ContentSystem.FileManagerNotInitializedException) {
                }
            }
        }
        private void BindEventHandlers() {
            Exit += Application_Exit;
        }


    }
}
