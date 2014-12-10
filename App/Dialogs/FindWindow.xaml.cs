using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LASI.App.Dialogs
{
    /// <summary>
    /// Interaction logic for FindWindow.xaml
    /// </summary>
    public partial class FindWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the FindWindow class.
        /// </summary>
        public FindWindow() {
            InitializeComponent();
        }
        /// <summary>
        /// Executes a lexical search over applicable elements when invoked.
        /// </summary>
        /// <param name="sender">The source of the click event.</param>
        /// <param name="e">The arguments passed invocation.</param>
        private void findButton_Click(object sender, RoutedEventArgs e) {
            PerformFind(searchForTextBox.Text);
        }
        /// <summary>
        /// Executes a lexical search over an applicable set of elements in the current UI context using the provided text as the search seed.
        /// </summary>
        /// <param name="searchText">The text which seeds the search.</param>
        private void PerformFind(string searchText) {
            //var toHighLight = from ContentControl element in GetElementsToSearch()
            //                  where element.Content.ToString().IndexOf(searchText, StringComparer.OrdinalIgnoreCase) >= 0
            //                  select element;
        }

        /// <summary>
        /// Returns the collection of ContentControl UIElements over whose contents to search.
        /// </summary>
        /// <returns>The collection of ContentControl UIElements over whose contents to search.</returns>
        private IEnumerable<ContentControl> GetElementsToSearch() {
            throw new NotImplementedException();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                this.DialogResult = false;
                this.Close();
            }
        }

    }
}
