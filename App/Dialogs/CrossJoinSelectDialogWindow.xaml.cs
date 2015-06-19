
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LASI.Core;

namespace LASI.App.Dialogs
{
    /// <summary>
    /// Interaction logic for CrossJoinSelectDialogWindow.xaml
    /// </summary>
    public partial class CrossJoinSelectDialogWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the CrossJoinSelectDialogWindow in the context of the given results screen.
        /// </summary>
        /// <param name="owner">The results screen referencing the Documents to Display and owning the new dialog window.</param>
        public CrossJoinSelectDialogWindow(ResultsWindow owner)
        {
            InitializeComponent();

            foreach (var doc in owner.Documents)
            {
                var docCheckBox = new CheckBox
                {
                    Content = doc.Name,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                docCheckBox.Checked += delegate { SelectedDocuments.Add(doc); };
                docCheckBox.Unchecked += delegate { SelectedDocuments.Remove(doc); };
                docCheckBox.Checked += delegate
                {
                    okButton.SetBinding(DependencyProperty.Register(nameof(ValidSelection), typeof(bool), typeof(CrossJoinSelectDialogWindow)), new System.Windows.Data.Binding());
                };
                docCheckBox.Unchecked += delegate { okButton.IsEnabled = SelectedDocuments.Count > 1; };
                documentsPanel.Children.Add(docCheckBox);
            }
        }

        private bool ValidSelection => okButton.IsEnabled = SelectedDocuments.Count > 1;
        private void okButton_Click(object sender, RoutedEventArgs e) => DialogResult = true;

        private void cancelButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.DialogResult = false;
                this.Close();
            }
        }


        /// <summary>
        /// Gets the documents selected by the user.
        /// </summary>
        public List<Document> SelectedDocuments { get; } = new List<Document>();


    }
}
