using LASI.InteropLayer;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for PageFunction1.xaml
    /// </summary>
    public partial class PreferencesMenu : PageFunction<string>
    {
        public PreferencesMenu(IDictionary<string, string> currentPreferences = null) {
            InitializeComponent();
            UserPreferences = currentPreferences ?? defaultPreferences.ToDictionary(entry => entry.Key, entry => entry.Value);
            LoadCurrentPreferences();
        }
        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
        }
        private void LoadCurrentPreferences() {
            LoadGeneralPreferences();
            LoadPerformancePreferences();
        }

        private void LoadGeneralPreferences() {
            try {
                autoNameCheckBox.IsChecked = bool.Parse(UserPreferences["AutoNameProjects"]);
            } catch (FormatException) {
                Output.WriteLine("Preferences: Could not load Auto Name setting.\nResetting to default value ({0})", defaultPreferences["AutoNameProjects"]);
                autoNameCheckBox.IsChecked = false;
            }
            try {
                minimizeToTrayCheckBox.IsChecked = bool.Parse(UserPreferences["TrayMinimize"]);
            } catch (FormatException) {
                Output.WriteLine("Preferences: Could not load Tray Minimization setting.\nResetting to default value ({0})", defaultPreferences["TrayMinimize"]);
                minimizeToTrayCheckBox.IsChecked = false;
            }
        }

        private void LoadPerformancePreferences() {
            PerforamanceLevel performanceLevel;
            try {
                performanceLevel = (PerforamanceLevel)Enum.Parse(typeof(PerforamanceLevel), UserPreferences["CurrentPerformanceMode"]);
                if (performanceLevel == PerforamanceLevel.High)
                    High.IsChecked = true;
                else if (performanceLevel == PerforamanceLevel.Normal)
                    Normal.IsChecked = true;
                else if (performanceLevel == PerforamanceLevel.Low)
                    Low.IsChecked = true;
            } catch (ArgumentException) {
                Normal.IsChecked = true;
            }
        }



        private void anyPerformanceMode_Checked(object sender, RoutedEventArgs e) {
            var checkBox = sender as RadioButton;
            if (checkBox.IsChecked ?? false) {
                UserPreferences["CurrentPerformanceMode"] = checkBox.Name;
                PerformanceManager.SetPerformanceLevel((PerforamanceLevel)Enum.Parse(typeof(PerforamanceLevel), checkBox.Name));
            }
        }




        private void autoNameCheckBox_Checked(object sender, RoutedEventArgs e) {
            UserPreferences["AutoNameProjects"] = autoNameCheckBox.IsChecked ?? false ? "true" : "false";
        }

        private void PageFunction_Return(object sender, ReturnEventArgs<string> e) { }

        #region Fields
        public IDictionary<string, string> UserPreferences;
        private readonly IReadOnlyDictionary<string, string> defaultPreferences = new Dictionary<string, string> { 
            { "AutoNameProjects", "false" },
            { "TrayMinimize", "false" },
            { "CurrentPerformanceMode", "Normal" },
        };
        #endregion


        private void minimizeToTrayCheckBox_Checked(object sender, RoutedEventArgs e) {

        }
    }
}
