using LASI.Core.DocumentStructures;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LASI.App.Dialogs
{
    /// <summary>
    /// Interaction logic for CrossJoinSelectDialog.xaml
    /// </summary>
    public partial class CrossJoinSelectDialog : Window
    {
        /// <summary>
        /// Initializes a new instance of the CrossJoinSelectDialog in the context of the given results screen.
        /// </summary>
        /// <param name="owner">The results screen referencing the Documents to Display and owning the new dialog window.</param>
        public CrossJoinSelectDialog(ResultsWindow owner) {
            InitializeComponent();
            SelectedDocuments = new List<Document>();
            foreach (var doc in owner.Documents) {
                var docCheckBox = new CheckBox {
                    Content = doc.Name,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                docCheckBox.Checked += (sender, e) => {
                    SelectedDocuments.Add(doc);
                    okButton.IsEnabled = SelectedDocuments.Count > 1 ? true : false;
                };
                docCheckBox.Unchecked += (sender, e) => {
                    SelectedDocuments.Remove(doc);
                    okButton.IsEnabled = SelectedDocuments.Count > 1 ? true : false;
                };

                documentsPanel.Children.Add(docCheckBox);
            }
        }


        private void okButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                DialogResult = false;
                Close();
            }
        }


        /// <summary>
        /// Gets the documents selected by the user.
        /// </summary>
        public List<Document> SelectedDocuments { get; private set; }


    }
}
