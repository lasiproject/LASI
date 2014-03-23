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
            Exit += (sender, e) => {
                if (Settings.Default.AutoCleanProjectFiles) {
                    try {
                        LASI.ContentSystem.FileManager.DecimateProject();
                    }
                    catch (ContentSystem.FileManagerNotInitializedException) {
                    }
                }
            };
        }

        private static void LoadPreferences() {
            Performance.Mode performanceLevel;
            if (Enum.TryParse<Performance.Mode>(Settings.Default.PerformanceLevel, out performanceLevel)) {
                Performance.SetPerformanceLevel(performanceLevel);
            }
        }
    }
}
