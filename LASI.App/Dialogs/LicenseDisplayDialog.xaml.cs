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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LASI.App.Dialogs
{
    /// <summary>   
    /// Interaction logic for LicenseDisplayDialog.xaml
    /// </summary>
    public partial class LicenseDisplayDialog : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseDisplayDialog"/> class which will display the specified text.
        /// </summary>
        /// <param name="licenseText">The text of the license to which the window will display.</param>
        public LicenseDisplayDialog(string licenseText)
        {
            LicenseText = licenseText;

            InitializeComponent();
        }
        /// <summary>
        /// The text of the licence.
        /// </summary>
        public string LicenseText { get; }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

    }
}
