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
    public partial class PreferencesMenu : PageFunction<String>
    {
        public PreferencesMenu(IDictionary<string, object> previousPreferences)
        {
            InitializeComponent();
            userPreferences = previousPreferences ?? new Dictionary<string, object>();
            LoadPresentConfiguration();
        }

        private void LoadPresentConfiguration()
        {
            LoadPerformanceSettings();
        }

        private void LoadPerformanceSettings()
        {
            LASI.InteropLayer.PerforamanceLevel performanceMode = ( LASI.InteropLayer.PerforamanceLevel )userPreferences["PerformanceMode"];
            if (performanceMode == InteropLayer.PerforamanceLevel.High)
                highPerformance.IsChecked = true;
            else if (performanceMode == InteropLayer.PerforamanceLevel.Normal)
                normalPerformance.IsChecked = true;
            else if (performanceMode == InteropLayer.PerforamanceLevel.Low)
                lowPerformance.IsChecked = true;
        }



        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            userPreferences["PerformanceMode"] =
                highPerformance.IsChecked ?? false ?
                LASI.InteropLayer.PerforamanceLevel.High :
                normalPerformance.IsChecked ?? false ?
                LASI.InteropLayer.PerforamanceLevel.Normal :
                lowPerformance.IsChecked ?? false ?
                LASI.InteropLayer.PerforamanceLevel.Low :
                LASI.InteropLayer.PerforamanceLevel.Normal;
        }


        private IDictionary<string, object> userPreferences;

        private void PageFunction_Return(object sender, ReturnEventArgs<string> e)
        {

        }
    }
}
