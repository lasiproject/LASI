using LASI.Interop;
using LASI.App.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace LASI.App
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        /// <summary>
        /// Intializes a new instance of the PreferencesWindow class.
        /// </summary>
        public PreferencesWindow() {
            InitializeComponent();
            LoadCurrentPreferences();
            //menu = new PreferencesMenu();
            //mainFrame.Content = menu;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.Save();
            PerformanceManager.SetPerformanceLevel(ChosenPerformanceLevel);
            this.DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }


        #region fields
        //private PreferencesMenu menu;
        #endregion

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

        private void minimizeToTrayCheckBox_Checked(object sender, RoutedEventArgs e) {
            Settings.Default.TrayMinimize = minimizeToTrayCheckBox.IsChecked ?? false;
        }
        private void logMessagesToFileCheckBox_Checked(object sender, RoutedEventArgs e) {
            Settings.Default.LogProcessMessagesToFile = logMessagesToFileCheckBox.IsChecked ?? false;
        }
        /// <summary>
        /// Gets the PerformanceLevel corresponding to the selected user preference.
        /// </summary>
        public PerforamanceLevel ChosenPerformanceLevel { get; private set; }


        #region Fields

        #endregion


    }
}
