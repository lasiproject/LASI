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


        private void DrawBasicDataVisualizations(IEnumerable<ILexical> topResults) {




            var dataPoints = from r in topResults
                             group r by new {
                                 Text = r.Text,
                                 Type = r.GetType()
                             } into g
                             orderby g.Count()
                             select new BarDataPoint {
                                 ActualIndependentValue = g.Key.Text,
                                 ActualDependentValue = g.Count()
                             };


            tornadoChartData = dataPoints;
            //  TornadoChartTopPhrases.ItemsSource = tornadoChartData;
        }



        public void BuildAssociatedView() {



            FocusedDocument = new TaggedFileParser(FileManager.TaggedFiles.First()).LoadDocument();




            //  foreach (var word in doc.Words) //{
            //    var wordLabel = new Label {
            //        Tag = word,
            //        Content = word.Text,
            //        Foreground = Brushes.Black,
            //        Padding = new Thickness(1, 1, 1, 1),
            //        ContextMenu = new ContextMenu()
            //    };
            //    var menuItem1 = new MenuItem {
            //        Header = "view definition",
            //    };

            //    menuItem1.Click += (sender, e) => {
            //        Process.Start(String.Format("http://www.dictionary.reference.com/browse/{0}?s=t", word.Text));
            //    };

            //    // change text to random color from the colors array
            //    wordLabel.ContextMenu.Items.Add(menuItem1);




            //    wordLabel.MouseDown += (sender, e) => {
            //        var intraPhraseLabels = from w in (wordLabel.Tag as Word).ParentPhrase.Words
            //                                join l in wordLabels on w.ID equals (l.Tag as Word).ID
            //                                select l;
            //        foreach (var l in intraPhraseLabels) {
            //            if (l.Background != Brushes.Green && wordLabel.Foreground != Brushes.Red) {
            //                l.Foreground = Brushes.White;
            //                l.Background = Brushes.Green;
            //                wordLabel.Foreground = Brushes.Black;
            //                wordLabel.Background = Brushes.Red;

            //            } else {
            //                l.Background = Brushes.White;
            //                l.Foreground = Brushes.Black;
            //                wordLabel.Foreground = Brushes.Black;
            //                wordLabel.Background = Brushes.White;
            //            }
            //        }

            //    };
            //    wordLabels.Add(wordLabel);
            //    testViewWrap.Children.Add(wordLabel);
            //}
            DrawBasicDataVisualizations(FocusedDocument.Phrases);




        }

        private List<Label> wordLabels = new List<Label>();

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {

            this.SwapWith(WindowManager.CreateProjectScreen);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();

        }






        //private void SearchButton_Click_1(object sender, RoutedEventArgs e) {
        //    var searchText = SearchTextBox.Text;
        //    foreach (Label label in testViewWrap.Children) {
        //        if (String.Compare(label.Content.ToString(),
        //            searchText,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //            label.Foreground = Brushes.Red;
        //        else
        //            label.Foreground = Brushes.Black;
        //    }
        //}



        Document focusedDocument;

        public Document FocusedDocument {
            get {
                return focusedDocument;
            }
            set {
                focusedDocument = value;
            }
        }
        IEnumerable<BarDataPoint> tornadoChartData = new List<BarDataPoint>();

        public IEnumerable<BarDataPoint> TornadoChartData {
            get {
                return tornadoChartData;
            }
            set {
                tornadoChartData = value;
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



    }
}


/*
  var menuItem2 = new MenuItem {
                    Header = "highligh synonyms"
                };
                menuItem2.Click += (sender, e) => {
                    try {
                        var synonyms =
                            ThesaurusManager.
                            VerbThesaurus[wordLabel.Content.ToString().ToLower()];
                        foreach (var syn in synonyms) {
                            var applicableLabels = from l in wordLabels
                                                   where synonyms.Contains(l.Content.ToString())
                                                   select new Label {
                                                       Content = l.Content,
                                                       ContextMenu = l.ContextMenu,
                                                       Background = Brushes.Azure
                                                   };

                        }
                    } catch (NullReferenceException) {
                        menuItem2.RaiseEvent(e);
                    }
                };
 */