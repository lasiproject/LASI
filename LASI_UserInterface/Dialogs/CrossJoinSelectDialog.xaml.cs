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
using System.Windows.Shapes;
using LASI.Algorithm.DocumentConstructs;

namespace LASI.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for CrossJoinSelectDialog.xaml
    /// </summary>
    public partial class CrossJoinSelectDialog : Window
    {
        public CrossJoinSelectDialog(ResultsScreen owner) {
            InitializeComponent();

            foreach (var doc in owner.Documents) {
                var docCheckBox = new CheckBox {
                    Content = doc.FileName,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                docCheckBox.Checked += (sender, e) => {
                    selectDocuments.Add(doc);
                    okButton.IsEnabled = selectDocuments.Count > 1 ? true : false;
                };
                docCheckBox.Unchecked += (sender, e) => {
                    selectDocuments.Remove(doc);
                    okButton.IsEnabled = selectDocuments.Count > 1 ? true : false;
                };

                documentSelectStackPanel.Children.Add(docCheckBox);
            }
        }

        private List<Document> selectDocuments = new List<Document>();

        public List<Document> SelectDocuments {
            get {
                return selectDocuments;
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Escape:
                    DialogResult = false;
                    this.Close();
                    break;
            }
        }

    }
}
