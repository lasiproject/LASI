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
        App()
        {
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

        private static void LoadPreferences()
        {
            PerforamanceLevel performanceLevel;
            if (Enum.TryParse<PerforamanceLevel>(Settings.Default.PerformanceLevel, out performanceLevel)) {
                PerformanceManager.SetPerformanceLevel(performanceLevel);
            }
        }
    }
}
