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
    using LASI.Interop.ResourceManagement;

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
            UsageManager.SetPerformanceMode(PerformanceMode);
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
                PerformanceMode = (PerformanceMode)Enum.Parse(typeof(PerformanceMode), Settings.Default.PerformanceLevel);
                switch (PerformanceMode) {
                case PerformanceMode.High:
                    High.IsChecked = true;
                    break;
                case PerformanceMode.Normal:
                    Normal.IsChecked = true;
                    break;
                case PerformanceMode.Low:
                    Low.IsChecked = true;
                    break;
                }
            } catch (ArgumentException e) {
                Output.WriteLine(e.Message);
                Output.WriteLine(e.StackTrace);
                Normal.IsChecked = true;
                PerformanceMode = PerformanceMode.Normal;
            }
        }

        private void anyPerformanceMode_Checked(object sender, RoutedEventArgs e) {
            var checkBox = sender as RadioButton;
            if (checkBox.IsChecked ?? false) {
                Settings.Default.PerformanceLevel = checkBox.Name;
                PerformanceMode = (PerformanceMode)Enum.Parse(typeof(PerformanceMode), checkBox.Name);
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
        public Interop.ResourceManagement.PerformanceMode PerformanceMode { get; private set; }

        #region Fields

        #endregion

    }
}
