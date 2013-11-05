using LASI.App.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LASI.Interop;

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
