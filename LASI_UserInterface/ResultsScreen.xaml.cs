using LASI.Algorithm;
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
using LASI.Algorithm.Binding;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm.Analysis;
using LASI.Utilities;

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

        //WORD RELATIONSHIPS TAB//

        public void BuildAssociationTextView() {  // This is for the word relationships tab
            foreach (var doc in documents) {
                var panel = new WrapPanel();
                var tab = new TabItem {
                    Header = doc.FileName,
                    Content = panel

                };
                var phraseLabels = new List<Label>();
                foreach (var phrase in doc.Phrases) {
                    var phraseLabel = new Label {
                        Content = phrase.Text,
                        Tag = phrase,
                        Foreground = Brushes.Black,
                        Padding = new Thickness(1, 1, 1, 1),
                        ContextMenu = new ContextMenu(),
                        ToolTip = phrase.Type.Name,
                    };
                    var vP = phrase as VerbPhrase;
                    if (vP != null && vP.BoundSubjects.Count() > 0) {
                        var visitSubjectMI = new MenuItem {
                            Header = "view subjects"
                        };
                        visitSubjectMI.Click += (sender, e) => {
                            var objlabels = from r in vP.BoundSubjects
                                            join l in phraseLabels on r equals l.Tag
                                            select l;
                            foreach (var l in objlabels) {
                                l.Foreground = Brushes.Black;
                                l.Background = Brushes.Red;
                            }
                        };
                        phraseLabel.ContextMenu.Items.Add(visitSubjectMI);
                    }
                    if (vP != null && vP.DirectObjects.Count() > 0) {
                        var visitSubjectMI = new MenuItem {
                            Header = "view direct objects"
                        };
                        visitSubjectMI.Click += (sender, e) => {
                            var objlabels = from r in vP.DirectObjects
                                            join l in phraseLabels on r equals l.Tag
                                            select l;
                            foreach (var l in objlabels) {
                                l.Foreground = Brushes.Black;
                                l.Background = Brushes.Red;
                            }
                        };
                        phraseLabel.ContextMenu.Items.Add(visitSubjectMI);
                    }
                    if (vP != null && vP.IndirectObjects.Count() > 0) {
                        var visitSubjectMI = new MenuItem {
                            Header = "view indirect objects"
                        };
                        visitSubjectMI.Click += (sender, e) => {
                            var objlabels = from r in
                                                vP.IndirectObjects.Concat<ILexical>(from f in new object[] { }
                                                                                    where vP.ObjectViaPreposition != null
                                                                                    select vP.ObjectViaPreposition)
                                            join l in phraseLabels on r equals l.Tag
                                            select l;
                            foreach (var l in objlabels) {
                                l.Foreground = Brushes.Black;
                                l.Background = Brushes.Red;
                            }
                        };
                        phraseLabel.ContextMenu.Items.Add(visitSubjectMI);
                    }
                    phraseLabels.Add(phraseLabel);
                }
                foreach (var l in phraseLabels) {
                    panel.Children.Add(l);
                }
                ResultsTabControl.Items.Add(tab);
            }
        } // end word relationships




        public void BuildFullSortedView() { //Build 
            foreach (var doc in documents) {
                var wordLabels = new List<Label>();
                var words = doc.Phrases.GetNounPhrases().Concat<Phrase>(doc.Phrases.GetAdverbPhrases()).
                   Concat<Phrase>(doc.Phrases.GetAdjectivePhrases()).Concat<Phrase>(doc.Phrases.GetVerbPhrases()).
                   GroupBy(w => new {
                       w.Text,
                       w.Type
                   }).Select(g => g.First());

                foreach (var word in words) {
                    var wordLabel = MakeWordLabel(word);
                    var menuItem1 = new MenuItem {
                        Header = "view definition",
                    };
                    menuItem1.Click += (sender, e) => {
                        Process.Start(String.Format("http://www.dictionary.reference.com/browse/{0}?s=t", word.Text));
                    };
                    var menuItem2 = new MenuItem {
                        Header = "Copy Text"
                    };
                    menuItem2.Click += (se, ee) => Clipboard.SetText((wordLabel.Tag as ILexical).Text);
                    wordLabel.ContextMenu.Items.Add(menuItem1);
                    wordLabel.ContextMenu.Items.Add(menuItem2);

                    wordLabels.Add(wordLabel);


                }


                MakeFrequencyChart(doc);



                var s = new ScrollViewer();
                var stackPanel = new StackPanel();
                s.Content = stackPanel;
                var grid = new Grid();
                grid.Children.Add(s);
                var tabItem = new TabItem {
                    Header = doc.FileName,
                    Content = grid
                };

                foreach (var l in from w in wordLabels
                                  orderby (w.Tag as ILexical).Weight descending
                                  select w) {
                    stackPanel.Children.Add(l);
                }
                WordCountLists.Items.Add(tabItem);


            }

        } //end build assorted view




        public void MakeFrequencyChart(Document docu) {

            var words = docu.Phrases.GetNounPhrases().Concat<Phrase>(docu.Phrases.GetAdverbPhrases()).
                   Concat<Phrase>(docu.Phrases.GetAdjectivePhrases()).Concat<Phrase>(docu.Phrases.GetVerbPhrases()).
                   GroupBy(w => new {
                       w.Text,
                       w.Type
                   }).Select(g => g.First());

            var valueList = new List<KeyValuePair<string, int>>();
            valueList.AddRange(
                (from w in words
                 orderby w.Weight descending
                 select new KeyValuePair<string, int>(w.Text, (int)w.Weight))
                .Take(15)
                .ToList());


            valueList.Reverse();

            var chart = new Chart {
                Name = docu.FileName,
                Title = docu.FileName + " Top Words",


            };
            Series series = new BarSeries {

                DependentValuePath = "Value",
                IndependentValuePath = "Key",
                ItemsSource = valueList,
                IsSelectionEnabled = true,

            };
            chart.Series.Add(series);

            var tabItem = new TabItem {
                Header = docu.FileName,
                Content = chart
            };
            FrequencyCharts.Items.Add(tabItem);
        }


        /// <summary>
        /// Creates a label UI element for a word
        /// </summary>
        /// <param name="word"></param>
        /// <returns>
        /// New label element whose tag properties is assigned to the given word
        /// </returns>


        private static Label MakeWordLabel(ILexical word) {
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




        private List<Document> documents = new List<Document>();

        public List<Document> Documents {
            get {
                return documents;
            }
            set {
                documents = value;
            }
        }

        private List<Series> DocumentDatapointRanges = new List<Series>();









        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {

            this.SwapWith(WindowManager.CreateProjectScreen);
        }


        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();

        }


        private void printButton_Click_1(object sender, RoutedEventArgs e) {
            var printDialog = new PrintDialog();
            printDialog.ShowDialog();
            // printDialog.PrintVisual(resultsGrid, "Current View");
           
           
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

        }

        public bool AutoExport {
            get;
            set;
        }

        private void changeChartType_Column_Click(object sender, RoutedEventArgs e) {

        }
    }
}
