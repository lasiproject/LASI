using System.Windows;
using System.Windows.Input;

namespace LASI.App.Dialogs
{
    /// <summary>
    /// Interaction logic for ExportResultsDialog.xaml
    /// </summary>
    public partial class ExportResultsDialog : Window
    {
        /// <summary>
        /// Initializes a new instance of the ExportResultsDialog class.
        /// </summary>
        public ExportResultsDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) => Close();

        private void ExportNotificationDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}
