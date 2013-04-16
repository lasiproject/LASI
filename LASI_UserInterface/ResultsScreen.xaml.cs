using LASI.Algorithm;
using LASI.Algorithm.Thesauri;
using LASI.FileSystem;
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
using System.Windows.Controls.DataVisualization.Charting;
using System.Collections.ObjectModel;


namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsScreen : Window
    {
        public ResultsScreen() {

            InitializeComponent();
            this.Closed += (s, e) => Application.Current.Shutdown();

        }



        public void BuildAssociatedView() {

            var doc = new TaggedFileParser(FileManager.TaggedFiles.First()).LoadDocument();
            LASI.Algorithm.Analysis.Weighter.weight(doc);
            foreach (var word in doc.Words.GetNouns().Concat<Word>(doc.Words.GetAdverbs()).Concat<Word>(doc.Words.GetAdjectives()).Concat<Word>(doc.Words.GetVerbs()).GroupBy(w => new {
                w.Text,
                w.Type
            }).Select(g => g.First())) {
                var wordLabel = MakeWordLabel(word);
                var menuItem1 = new MenuItem {
                    Header = "view definition",
                };
                menuItem1.Click += (sender, e) => {
                    Process.Start(String.Format("http://www.dictionary.reference.com/browse/{0}?s=t", word.Text));
                };
                wordLabel.ContextMenu.Items.Add(menuItem1);
                //BuildUniqueClickAction(wordLabel);
                wordLabels.Add(wordLabel);


            }
            foreach (var l in from w in wordLabels
                              orderby (w.Tag as Word).Weight descending
                              select w) {
                TopWeightedView.Children.Add(l);
            }




        }

        private static Label MakeWordLabel(Word word) {
            var wordLabel = new Label {
                Tag = word,
                Content = String.Format("Weight : {0}  \"{1}\"", word.Weight, word.Text),
                Foreground = Brushes.Black,
                Padding = new Thickness(1, 1, 1, 1),
                ContextMenu = new ContextMenu(),
                ToolTip = word.Type.Name,
            };
            return wordLabel;
        }

        private void BuildUniqueClickAction(Label wordLabel) {
            wordLabel.MouseDown += (sender, e) => {
                var intraPhraseLabels = from w in (wordLabel.Tag as Word).ParentPhrase.Words
                                        join l in wordLabels on w.ID equals (l.Tag as Word).ID
                                        select l;
                foreach (var l in intraPhraseLabels) {
                    if (l.Background != Brushes.Green && wordLabel.Foreground != Brushes.Red) {
                        l.Foreground = Brushes.White;
                        l.Background = Brushes.Green;
                        wordLabel.Foreground = Brushes.Black;
                        wordLabel.Background = Brushes.Red;

                    }
                    else {
                        l.Background = Brushes.White;
                        l.Foreground = Brushes.Black;
                        wordLabel.Foreground = Brushes.Black;
                        wordLabel.Background = Brushes.White;
                    }
                }

            };
        }

        private List<Label> wordLabels = new List<Label>();

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {

            this.SwapWith(WindowManager.CreateProjectScreen);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();

        }






        private void SearchButton_Click_1(object sender, RoutedEventArgs e) {
            var searchText = SearchTextBox.Text;
            foreach (Label label in wordLabels) {
                if ((label.Tag as ILexical).Text.ToLower() == searchText.ToLower())
                    label.Foreground = Brushes.Red;
                else
                    label.Foreground = Brushes.Black;
            }
        }





        List<string> tokens = @"The typically small numbers of COIs identified by stakeholders with specific problems should each be accompanied by a similarly small number
` effectiveness measures (Sproles, 2002) that stakeholders can use to make binary, 'yes' or 'no,' determinations as to whether or not COI and 
the problems they characterize have been resolved. Recalling their definition as 'emergent properties that induce rank orderings,' 
MOEs are truly the 'standards' of the same definition as well as, perhaps most plainly, the variables of 'goodness' described by Dockery (1986, p. 172).
Their complete independence from solutions proposed to dispel the problems from which they fundamentally derive represents another of MOEs' 
most significant features, one reinforced with the Figure 3 that builds on Figure 2 by displaying only those entities - the problem and 
the COIs characterizing it - to which MOEs may be properly linked.".Split('\r', '\n', '\t', ' ').ToList();



        private void printButton_Click_1(object sender, RoutedEventArgs e) {
            var printDialog = new PrintDialog();
            printDialog.ShowDialog();
            printDialog.PrintVisual(resultsGrid, "Current View");
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

        }




        public bool AutoExport {
            get;
            set;
        }
    }
}
