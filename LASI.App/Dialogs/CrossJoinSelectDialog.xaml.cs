﻿
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LASI.Core;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

namespace LASI.App.Dialogs
{
    /// <summary>
    /// Interaction logic for CrossJoinSelectDialogWindow.xaml
    /// </summary>
    public partial class CrossJoinSelectDialog : Window
    {
        /// <summary>
        /// Initializes a new instance of the CrossJoinSelectDialogWindow in the context of the given results screen.
        /// </summary>
        /// <param name="owner">The results screen referencing the Documents to Display and owning the new dialog window.</param>
        public CrossJoinSelectDialog(ResultsWindow owner)
        {
            InitializeComponent();

            Top = owner.Top;
            Left = owner.Left;
            owner.Documents
                .Select(CreateCheckBoxForDocument)
                .ToList()
                .ForEach(checkBox => documentsPanel.Children.Add(checkBox));
        }

        private CheckBox CreateCheckBoxForDocument(Document document)
        {
            var docCheckBox = new CheckBox
            {
                Content = document.Name,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            docCheckBox.Checked += delegate { SelectedDocuments.Add(document); };
            docCheckBox.Unchecked += delegate { SelectedDocuments.Remove(document); };
            docCheckBox.Checked += delegate { okButton.SetBinding(DependencyProperty.Register(nameof(ValidSelection), typeof(bool), typeof(CrossJoinSelectDialog)), new System.Windows.Data.Binding()); };
            docCheckBox.Unchecked += delegate { okButton.IsEnabled = SelectedDocuments.Count > 1; };
            return docCheckBox;
        }

        private bool ValidSelection => okButton.IsEnabled = SelectedDocuments.Count > 1;

        private void okButton_Click(object sender, RoutedEventArgs e) => DialogResult = true;

        private void cancelButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
            }
        }


        /// <summary>
        /// The documents selected by the user.
        /// </summary>
        public List<Document> SelectedDocuments { get; } = new List<Document>();

    }
}
