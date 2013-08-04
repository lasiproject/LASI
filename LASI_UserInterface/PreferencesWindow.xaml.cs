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

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        public PreferencesWindow() {
            InitializeComponent();
            var appSettings = (Application.Current as App).Configuration.AppSettings.Settings;
            menu = new PreferencesMenu(
                (from Key in appSettings.AllKeys
                 select new { Key, appSettings[Key].Value })
                .ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value,
                    StringComparer.OrdinalIgnoreCase));
            mainFrame.Content = menu;
        }
        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);

        }
        private void saveButton_Click(object sender, RoutedEventArgs e) {
            var oldSettingsSource = (Application.Current as App).Configuration;
            oldSettingsSource.AppSettings.Settings.Clear();
            foreach (var item in menu.UserPreferences) {
                oldSettingsSource.AppSettings.Settings.Add(item.Key, item.Value);
            }
            oldSettingsSource.Save(ConfigurationSaveMode.Modified);
            this.DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e) { this.DialogResult = false; }


        #region fields
        private PreferencesMenu menu;
        #endregion
    }
}
