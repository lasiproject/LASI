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

namespace LASI.App.Dialogs
{
    /// <summary>
    /// Interaction logic for ErrorDialogWindow.xaml
    /// </summary>
    public partial class ErrorDialogWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDialogWindow"/> class.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        /// <param name="kind">The <see cref="ErrorKind"/> of the error.</param>
        public ErrorDialogWindow(string message, ErrorKind kind = ErrorKind.NonFatal)
        {
            InitializeComponent();
            Kind = kind;
            Message = message;
            ProceedButton.Content = kind == ErrorKind.Fatal ? "Quit LASI" : "Continue";
        }
        /// <summary>
        /// The error message displayed by the dialog.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The ErrorKind displayed by the dialog.
        /// </summary>
        public ErrorKind Kind { get; }

        private const string DefaultMessage = "An unspecified error occurred.\nPress OK to continue";
    }
}
