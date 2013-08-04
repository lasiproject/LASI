using LASI.InteropLayer;
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
            menu = new PreferencesMenu();

            mainFrame.Content = menu;
        }
        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);

        }
        private void saveButton_Click(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.Save();
            PerformanceManager.SetPerformanceLevel(menu.ChosenPerformanceLevel);
            this.DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }


        #region fields
        private PreferencesMenu menu;
        #endregion
    }
}
