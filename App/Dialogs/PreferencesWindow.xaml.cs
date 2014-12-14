using LASI.App.Properties;
using LASI.Interop;
using System;
using System.Windows;
using System.Windows.Controls;
using LASI.App;

namespace LASI.App.Dialogs
{
    using System.Windows.Input;
    using LASI.Utilities;
    using UsageManager = LASI.Interop.ResourceManagement.UsageManager;
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
            UsageManager.SetPerformanceLevel(PerformanceLevel);
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
            LoadOutputPreferences();
            LoadAdvancedPreferences();
        }
        private void LoadOutputPreferences() {
            outputJson.IsChecked = Settings.Default.OutputFormat == "XML";
            outputXml.IsChecked = Settings.Default.OutputFormat == "JSON";
        }

        private void LoadGeneralPreferences() {
            autoNameCheckBox.IsChecked = Settings.Default.AutoNameProjects;

            minimizeToTrayCheckBox.IsChecked = Settings.Default.TrayMinimize;

        }

        private void LoadAdvancedPreferences() {
            try {
                PerformanceLevel = (UsageManager.Mode)Enum.Parse(typeof(UsageManager.Mode), Settings.Default.PerformanceLevel);
                switch (PerformanceLevel) {
                case UsageManager.Mode.High:
                    High.IsChecked = true;
                    break;
                case UsageManager.Mode.Normal:
                    Normal.IsChecked = true;
                    break;
                case UsageManager.Mode.Low:
                    Low.IsChecked = true;
                    break;
                }
            } catch (ArgumentException e) {
                Output.WriteLine(e.Message);
                Output.WriteLine(e.StackTrace);
                Normal.IsChecked = true;
                PerformanceLevel = UsageManager.Mode.Normal;
            }
        }

        private void anyPerformanceMode_Checked(object sender, RoutedEventArgs e) {
            var checkBox = sender as RadioButton;
            if (checkBox.IsChecked ?? false) {
                Settings.Default.PerformanceLevel = checkBox.Name;
                PerformanceLevel = (UsageManager.Mode)Enum.Parse(typeof(UsageManager.Mode), checkBox.Name);
            }
        }

        private void outputFormat_Checked(object sender, RoutedEventArgs e) {
            var option = sender as RadioButton;
            if (option.IsChecked ?? false) { Settings.Default.OutputFormat = option.Name; }
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
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                this.DialogResult = false;
                this.Close();
            }
        }
        /// <summary>
        /// Gets the PerformanceLevel corresponding to the selected user preference.
        /// </summary>
        public Interop.ResourceManagement.UsageManager.Mode PerformanceLevel { get; private set; }

        #region Fields

        #endregion

    }
}
