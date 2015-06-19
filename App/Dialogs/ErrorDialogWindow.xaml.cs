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
        public ErrorDialogWindow(string message, ErrorKind kind = ErrorKind.NonFatal)
        {
            InitializeComponent();
            Kind = kind;
            Message = message;
            ProceedButton.Content = kind == ErrorKind.Fatal ? "Quit LASI" : "Continue";
        }
        public string Message { get; set; }
        public ErrorKind Kind { get; }

        private const string DefaultMessage = "An unspecified error occured.\nPress OK to continue";
    }
}
