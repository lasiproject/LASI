using LASI.App.Properties;
using LASI.Interop;
using System;
using System.Windows;
using System.Windows.Controls;

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
            Performance.SetPerformanceLevel(PerformanceLevel);
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
                PerformanceLevel = (Performance.Mode)Enum.Parse(typeof(Performance.Mode), Settings.Default.PerformanceLevel);
                switch (PerformanceLevel) {
                    case Performance.Mode.High:
                        High.IsChecked = true;
                        break;
                    case Performance.Mode.Normal:
                        Normal.IsChecked = true;
                        break;
                    case Performance.Mode.Low:
                        Low.IsChecked = true;
                        break;
                }
            }
            catch (ArgumentException) {
                Normal.IsChecked = true;
                PerformanceLevel = Performance.Mode.Normal;
            }
        }



        private void anyPerformanceMode_Checked(object sender, RoutedEventArgs e) {
            var checkBox = sender as RadioButton;
            if (checkBox.IsChecked ?? false) {
                Settings.Default.PerformanceLevel = checkBox.Name;
                PerformanceLevel = (Performance.Mode)Enum.Parse(typeof(Performance.Mode), checkBox.Name);
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
        public Performance.Mode PerformanceLevel { get; private set; }


        #region Fields

        #endregion


    }
}
