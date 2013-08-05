using LASI.InteropLayer;
using LASI.UserInterface.Properties;
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
        public PreferencesMenu() {

            InitializeComponent();
            LoadCurrentPreferences();
        }

        private void LoadCurrentPreferences() {
            LoadGeneralPreferences();
            LoadAdvancedPreferences();
        }

        private void LoadGeneralPreferences() {
            autoNameCheckBox.IsChecked = Settings.Default.AutoNameProjects;

            minimizeToTrayCheckBox.IsChecked = Settings.Default.TrayMinimize;

        }

        private void LoadAdvancedPreferences() {
            try {
                ChosenPerformanceLevel = (PerforamanceLevel)Enum.Parse(typeof(PerforamanceLevel), Settings.Default.PerformanceLevel);
                switch (ChosenPerformanceLevel) {
                    case PerforamanceLevel.High:
                        High.IsChecked = true;
                        break;
                    case PerforamanceLevel.Normal:
                        Normal.IsChecked = true;
                        break;
                    case PerforamanceLevel.Low:
                        Low.IsChecked = true;
                        break;
                }
            } catch (ArgumentException) {
                Normal.IsChecked = true;
                ChosenPerformanceLevel = PerforamanceLevel.Normal;
            }
        }



        private void anyPerformanceMode_Checked(object sender, RoutedEventArgs e) {
            var checkBox = sender as RadioButton;
            if (checkBox.IsChecked ?? false) {
                Settings.Default.PerformanceLevel = checkBox.Name;
                ChosenPerformanceLevel = (PerforamanceLevel)Enum.Parse(typeof(PerforamanceLevel), checkBox.Name);
            }
        }




        private void autoNameCheckBox_Checked(object sender, RoutedEventArgs e) {
            Settings.Default.AutoNameProjects = autoNameCheckBox.IsChecked ?? false;
        }

        private void PageFunction_Return(object sender, ReturnEventArgs<string> e) { }

        #region Fields

        #endregion


        private void minimizeToTrayCheckBox_Checked(object sender, RoutedEventArgs e) {
            Settings.Default.TrayMinimize = minimizeToTrayCheckBox.IsChecked ?? false;
        }

        public PerforamanceLevel ChosenPerformanceLevel { get; private set; }
    }
}
