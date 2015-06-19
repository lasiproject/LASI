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
    public partial class LicenseDisplayDialogWindow : Window
    {
        public LicenseDisplayDialogWindow(string licenseText)
        {
            InitializeComponent();

            LicenseText = licenseText; // .SetBinding(LicenseDocument.

            LicenseDocument.Blocks.Add(new Paragraph(new Run(LicenseText)));
        }

        public string LicenseText { get; }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

    }
}
