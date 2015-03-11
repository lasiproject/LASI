using LASI.App.Properties;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LASI.App.Dialogs
{
    using System.Windows.Input;
    using LASI.Utilities;
    using UsageManager = LASI.Interop.ResourceManagement.ResourceUsageManager;
    using LASI.Interop.ResourceManagement;

    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        /// <summary>
        /// Intializes a new instance of the PreferencesWindow class.
        /// </summary>
        public PreferencesWindow()
        {
            InitializeComponent();
            LoadCurrentPreferences();
            //menu = new PreferencesMenu();
            //mainFrame.Content = menu;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            UsageManager.SetPerformanceLevel(PerformanceMode);
            this.DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        #region fields
        //private PreferencesMenu menu;
        #endregion

        private void LoadCurrentPreferences()
        {
            LoadGeneralPreferences();
            LoadOutputPreferences();
            LoadAdvancedPreferences();
        }
        private void LoadOutputPreferences()
        {
            outputJson.IsChecked = Settings.Default.OutputFormat == "XML";
            outputXml.IsChecked = Settings.Default.OutputFormat == "JSON";
        }

        private void LoadGeneralPreferences()
        {
            autoNameCheckBox.IsChecked = Settings.Default.AutoNameProjects;

            minimizeToTrayCheckBox.IsChecked = Settings.Default.TrayMinimize;

        }

        private void LoadAdvancedPreferences()
        {
            try
            {
                PerformanceMode = (PerformanceLevel)Enum.Parse(typeof(PerformanceLevel), Settings.Default.PerformanceLevel);
                switch (PerformanceMode)
                {
                    case PerformanceLevel.High:
                    High.IsChecked = true;
                    break;
                    case PerformanceLevel.Normal:
                    Normal.IsChecked = true;
                    break;
                    case PerformanceLevel.Low:
                    Low.IsChecked = true;
                    break;
                }
            }
            catch (ArgumentException e)
            {
                Logger.Log(e.Message);
                Logger.Log(e.StackTrace);
                Normal.IsChecked = true;
                PerformanceMode = PerformanceLevel.Normal;
            }
        }

        private void anyPerformanceMode_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as RadioButton;
            if (checkBox.IsChecked ?? false)
            {
                Settings.Default.PerformanceLevel = checkBox.Name;
                PerformanceMode = (PerformanceLevel)Enum.Parse(typeof(PerformanceLevel), checkBox.Name);
            }
        }

        private void outputFormat_Checked(object sender, RoutedEventArgs e)
        {
            var option = sender as RadioButton;
            if (option.IsChecked ?? false) { Settings.Default.OutputFormat = option.Name; }
        }

        private void autoNameCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.AutoNameProjects = autoNameCheckBox.IsChecked ?? false;
        }

        private void minimizeToTrayCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.TrayMinimize = minimizeToTrayCheckBox.IsChecked ?? false;
        }
        private void logMessagesToFileCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.LogProcessMessagesToFile = logMessagesToFileCheckBox.IsChecked ?? false;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.DialogResult = false;
                this.Close();
            }
        }
        /// <summary>
        /// Gets the PerformanceLevel corresponding to the selected user preference.
        /// </summary>
        public Interop.ResourceManagement.PerformanceLevel PerformanceMode { get; private set; }

        #region Fields

        #endregion

    }
}
