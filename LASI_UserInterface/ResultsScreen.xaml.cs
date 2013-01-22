using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace LASI_UserInterface
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsScreen : Window
    {
        public ResultsScreen() {

            InitializeComponent();
            this.Closed += (s, e) => Application.Current.Shutdown();
            BuildAssociatedView();
        }
        private static string documentContent = @"
  The typically small numbers of COIs identified by stakeholders with specific problems should each be accompanied by a similarly small number ` effectiveness measures (Sproles, 2002) that stakeholders can use to make binary, 'yes' or 'no,' determinations as to whether or not COI and the problems they characterize have been resolved. Recalling their definition as 'emergent properties that induce rank orderings,' MOEs are truly the 'standards' of the same definition as well as, perhaps most plainly, the variables of 'goodness' described by Dockery (1986, p. 172). Their complete independence from solutions proposed to dispel the problems from which they fundamentally derive represents another of MOEs' most significant features, one reinforced with the Figure 3 that builds on Figure 2 by displaying only those entities - the problem and the COIs characterizing it - to which MOEs may be properly linked.
";
        private void BuildAssociatedView() {
            var brushes = new[] { Brushes.Orange, Brushes.Teal, Brushes.IndianRed };//Some colors

            var tokens = documentContent.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//Get whitespace delimited

            foreach (string T in tokens) {
                Label wordLabel = new Label {
                    Content = T,
                    Foreground = Brushes.Black,
                    Padding = new Thickness(1, 1, 1, 1),
                    ContextMenu = new ContextMenu()
                };
                var menuItem = new MenuItem {
                    Header = "Change Color",
                };
                menuItem.Click += (sender, e) => wordLabel.Foreground = brushes[new Random().Next(0, brushes.Length)];// change text to random color from
                wordLabel.ContextMenu.Items.Add(menuItem);
                wordLabel.MouseDoubleClick += (sender, e) => {
                    MessageBox.Show(wordLabel.Content.ToString());
                };
                wordsWrapPanel.Children.Add(wordLabel);
            }




        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {

            this.SwapWith(WindowManager.CreateProjectScreen);
        }

        private void AlphaSortButton_Click_1(object sender, RoutedEventArgs e) {
            var a = from abc in Frequency.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    orderby abc[0]
                    select abc;

            Frequency = new TextBlock();
            Frequency.Text = (from abc in a
                              select a).SelectMany((s) => s + "\n").ToString();
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();

        }

        private void SearchButton_Click_1(object sender, RoutedEventArgs e) {
            var searchText = SearchTextBox.Text;
            foreach (Label label in wordsWrapPanel.Children) {
                if (label.Content.ToString() == searchText)
                    label.Foreground = Brushes.WhiteSmoke;
                else
                    label.Foreground = Brushes.Black;
            }
        }




    }
}