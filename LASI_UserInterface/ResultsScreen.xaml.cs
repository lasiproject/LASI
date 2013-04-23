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
using System.Windows.Controls.DataVisualization;
using System.Collections.ObjectModel;
using LASI.Algorithm.Binding;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm.Analysis;
using LASI.Utilities;
using LASI.UserInterface.Dialogs;

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
        public async void CreateInteractiveViews() {
            //await Task.WhenAll((from doc in documents
            //                    select Task.Factory.StartNew(() => {
            foreach (var doc in documents) {
                var documentElements = doc.Phrases.GetNounPhrases().Concat<Phrase>(doc.Phrases.GetAdverbPhrases()).
                   Concat<Phrase>(doc.Phrases.GetAdjectivePhrases()).Concat<Phrase>(doc.Phrases.GetVerbPhrases()).
                   GroupBy(w => new {
                       w.Text,
                       w.Type
                   }).Select(g => g.First());
                var elementLabels = new List<Label>();
                foreach (var e in documentElements) {
                    var wordLabel = new Label {
                        Tag = e,
                        Content = String.Format("Weight : {0}  \"{1}\"", e.Weight, e.Text),
                        Foreground = Brushes.Black,
                        Padding = new Thickness(1, 1, 1, 1),
                        ContextMenu = new ContextMenu(),
                        ToolTip = e.Type.Name,
                    };
                    var menuItem1 = new MenuItem {
                        Header = "view definition",
                    };
                    menuItem1.Click += (sender, ee) => {
                        Process.Start(String.Format("http://www.dictionary.reference.com/browse/{0}?s=t", e.Text));
                    };
                    var menuItem2 = new MenuItem {
                        Header = "Copy Text"
                    };
                    menuItem2.Click += (se, ee) => Clipboard.SetText((wordLabel.Tag as ILexical).Text);
                    wordLabel.ContextMenu.Items.Add(menuItem1);
                    wordLabel.ContextMenu.Items.Add(menuItem2);
                    elementLabels.Add(wordLabel);
                }

                var scrollViewer = new ScrollViewer();
                var stackPanel = new StackPanel();
                scrollViewer.Content = stackPanel;
                var grid = new Grid();
                grid.Children.Add(scrollViewer);
                var tabItem = new TabItem {
                    Header = doc.FileName,
                    Content = grid
                };

                foreach (var l in from w in elementLabels
                                  orderby (w.Tag as ILexical).Weight descending
                                  select w) {
                    stackPanel.Children.Add(l);
                }
                WordCountLists.Items.Add(tabItem);
                BuildDefaultBarChartDisplay(doc);

            }

            BindChartViewControls();
        }

        //private Label SetupElementFunctionality(ILexical word) {
        //    var wordLabel = MakeLexicalLabel(word);
        //    var menuItem1 = new MenuItem {
        //        Header = "view definition",
        //    };
        //    menuItem1.Click += (sender, e) => {
        //        Process.Start(String.Format("http://www.dictionary.reference.com/browse/{0}?s=t", word.Text));
        //    };
        //    var menuItem2 = new MenuItem {
        //        Header = "Copy Text"
        //    };
        //    menuItem2.Click += (se, ee) => Clipboard.SetText((wordLabel.Tag as ILexical).Text);
        //    wordLabel.ContextMenu.Items.Add(menuItem1);
        //    wordLabel.ContextMenu.Items.Add(menuItem2);
        //    return wordLabel;
        //}



        public void BuildAssociationTextView() {  // This is for the lexial relationships tab
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
                wordRelationshipsTab.Items.Add(tab);
            }
        }
        Dictionary<Chart, Document> documetnsByChart = new Dictionary<Chart, Document>();
        public void BuildDefaultBarChartDisplay(Document document) {

            var valueList = ProjectToChartItemSource(document);
            Series series = new BarSeries {
                DependentValuePath = "Value",
                IndependentValuePath = "Key",
                ItemsSource = valueList,
                IsSelectionEnabled = true,
                Tag = document


            };


            var chart = new Chart {
                //Name = "ChartForDoc" + document.FileName,
                Title = string.Format("Key Relationships in {0}", document.FileName),
                Tag = valueList.ToArray()

            };
            documetnsByChart.Add(chart, document);
            chart.Series.Add(series);

            var tabItem = new TabItem {
                Header = document.FileName,
                Content = chart,
                Tag = chart


            };
            FrequencyCharts.Items.Add(tabItem);
        }
        #region Chart Transposing Methods


        /// <summary>
        /// Reconfigures all charts to a Column perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        async Task ToColumnCharts() {
            foreach (var chart in FrequencyCharts.Items) {
                var items = GetItemSourceFor(chart);
                var series = new ColumnSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true


                };
                ResetChartContent(chart, series);
            }
            await Task.Delay(1);
        }



        /// <summary>
        /// Reconfigures all charts to a Pie perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        async Task ToPieCharts() {
            foreach (var chart in FrequencyCharts.Items) {
                var items = GetItemSourceFor(chart);
                var series = new PieSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true,
                };
                series.IsMouseCaptureWithinChanged += (sender, e) => {
                    series.ToolTip = (series.SelectedItem as DataPoint).DependentValue;
                };

                ResetChartContent(chart, series);
            }
            await Task.Delay(1);
        }

        /// <summary>
        /// Reconfigures all charts to a Bar perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        async Task ToBarCharts() {
            foreach (var chart in FrequencyCharts.Items) {
                var items = GetItemSourceFor(chart);
                var series = new BarSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true,
                };
                ResetChartContent(chart, series);
            }
            await Task.Delay(1);
        }
        /// <summary>
        /// Reconfigures all charts to an Area perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        async Task ToAreaCharts() {
            foreach (var chart in FrequencyCharts.Items) {
                var items = GetItemSourceFor(chart);
                var series = new AreaSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true,
                };
                ResetChartContent(chart, series);
            }
            await Task.Delay(1);
        }


        async Task ToLineCharts() {
            foreach (var chart in FrequencyCharts.Items) {
                var items = GetItemSourceFor(chart);
                var series = new LineSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = items,
                    IsSelectionEnabled = true,
                };
                ResetChartContent(chart, series);
            }
            await Task.Delay(1);
        }

        #endregion

        #region General Chart Rebuilding Methods

        private static void ResetChartContent(object c, DataPointSeries series) {
            ((c as TabItem).Content as Chart).Series.Clear();
            ((c as TabItem).Content as Chart).Series.Add(series);
        }

        private List<KeyValuePair<string, int>> GetItemSourceFor(object chart) {
            var chartSource = ((chart as TabItem).Content as Chart).Tag as IEnumerable<KeyValuePair<string, int>>;
            var items = (from i in chartSource.ToArray()
                         select new KeyValuePair<string, int>(i.Key.ToString(), (int) i.Value)).ToList();
            return items;
        }

        #endregion





        private List<KeyValuePair<string, int>> ProjectToChartItemSource(Document document) {


            return GetSVData(document).Take(15).ToList();
        }




        private void ChangeChartKind(ChartKind chartKind) {
            foreach (var pair in documetnsByChart) {

                switch (chartKind) {
                    case ChartKind.SubjectVerb:
                        Document doc = pair.Value;
                        Chart chart = pair.Key;

                        IEnumerable<KeyValuePair<string, int>> data = GetSVData(doc);
                        data = data.Take(15);
                        pair.Key.Series.Clear();
                        pair.Key.Series.Add(new BarSeries {
                            DependentValuePath = "Value",
                            IndependentValuePath = "Key",
                            ItemsSource = data,
                            IsSelectionEnabled = true
                        });
                        chart.Title = string.Format("Key Relationships in {0}", pair.Value.FileName);
                        break;
                }
            }
        }

        private static IEnumerable<KeyValuePair<string, int>> GetSVData(Document doc) {
            IEnumerable<KeyValuePair<string, int>> data =
                from v in doc.Phrases.GetVerbPhrases().WithSubject()
                from s in v.BoundSubjects
                let relationWeight = s.Weight + v.Weight
                orderby relationWeight
                let SV = new KeyValuePair<string, int>(string.Format("{0} -> {1}", s.Text, v.Text), (int) relationWeight)
                group SV by SV into svg
                orderby svg.Sum(s => s.Value)
                select svg.Key;
            return data;
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
            var focusedChart = (FrequencyCharts.SelectedItem as TabItem).Content as Visual;
            try {
                printDialog.PrintVisual(focusedChart, "Current View");
            } catch (NullReferenceException) {
            }

        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

        }

        private async void ChangeToBarChartButton_Click(object sender, RoutedEventArgs e) {
            await ToBarCharts();
        }

        private async void ChangeToColumnChartButton_Click(object sender, RoutedEventArgs e) {
            await ToColumnCharts();
        }

        private async void ChangeToPieChartButton_Click(object sender, RoutedEventArgs e) {
            await ToPieCharts();
        }
        private async void ChangeToAreaChartButton_Click(object sender, RoutedEventArgs e) {
            await ToAreaCharts();
        }
        private async void ChangeToLineChartButton_Click(object sender, RoutedEventArgs e) {
            await ToLineCharts();
        }

        private void BindChartViewControls() {
            changeToBarChartButton.Click += ChangeToBarChartButton_Click;
            changeToColumnChartButton.Click += ChangeToColumnChartButton_Click;
            changeToPieChartButton.Click += ChangeToPieChartButton_Click;
            changeToAreaChartButton.Click += ChangeToAreaChartButton_Click;
            changeToLineChartButton.Click += ChangeToLineChartButton_Click;
        }



        private async void exportButton_Click(object sender, RoutedEventArgs e) {
            foreach (var doc in documents) {
                using (
                    var docWriter = new LASI.FileSystem.Serialization.XML.SimpleLexicalSerializer(
                    FileManager.ResultsDir + System.IO.Path.DirectorySeparatorChar + new string(
                    doc.FileName.TakeWhile(c => c != '.').ToArray()) + ".xml")) {
                    docWriter.Write(from S in doc.Sentences
                                    from R in S.Phrases
                                    select R, doc.FileName, FileSystem.Serialization.XML.DegreeOfOutput.Comprehensive);
                }
            }
            var exportDialog = new DialogToProcedeToResults();
            exportDialog.ShowDialog();
        }



        public bool AutoExport {
            get;
            set;
        }

        private void SetChartSourceButton_Click(object sender, RoutedEventArgs e) {
            ChangeChartKind(ChartKind.SubjectVerb);
        }
    }
}
//var topResultsForChart = document.Phrases.GetNounPhrases().Concat<Phrase>(document.Phrases.GetAdverbPhrases()).
//       Concat<Phrase>(document.Phrases.GetAdjectivePhrases()).Concat<Phrase>(document.Phrases.GetVerbPhrases()).
//       GroupBy(w => new {
//           w.Text,
//           w.Type
//       }).Select(g => g.First());
//var valueList = new List<KeyValuePair<string, int>>();
//valueList.AddRange(
//    (from w in topResultsForChart
//     orderby w.Weight descending
//     select new KeyValuePair<string, int>(w.Text, (int) w.Weight))