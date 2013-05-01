using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.FileSystem;
using LASI.UserInterface.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;

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
            chartKind = ChartKind.NounPhrasesOnly;
        }
        public async Task CreateInteractiveViews() {

            foreach (var doc in documents) {
                var documentElements = (from de in doc.Paragraphs.Except(doc.EnumContainingParagraphs)
                                        select de.Phrases into phraseElements
                                        from phrase in phraseElements
                                        select phrase)
                                       .GetNounPhrases().Concat<Phrase>(doc.Phrases.GetAdverbPhrases()).
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
                await BuildSVOIGridViewAsync(doc);

            }

            BindChartViewControls();
        }





        public void BuildAssociationTextView() {  // This is for the lexial relationships tab
            foreach (var doc in documents) {
                var panel = new WrapPanel();
                var tab = new TabItem {
                    Header = doc.FileName,
                    Content = new ScrollViewer {
                        Content = panel
                    }

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
                                                vP.IndirectObjects
                                            join l in phraseLabels on r equals l.Tag
                                            select l;
                            foreach (var l in objlabels) {
                                l.Foreground = Brushes.Black;
                                l.Background = Brushes.Red;
                            }
                        };
                        phraseLabel.ContextMenu.Items.Add(visitSubjectMI);
                    }
                    if (vP != null && vP.ObjectViaPreposition != null) {
                        var visitSubjectMI = new MenuItem {
                            Header = "view prepositional object"
                        };
                        visitSubjectMI.Click += (sender, e) => {
                            var objlabels = from l in phraseLabels
                                            where l.Tag.Equals(vP.ObjectViaPreposition)
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
                recomposedDocumentsTabControl.Items.Add(tab);
            }
        }
        Dictionary<Chart, Document> documentsByChart = new Dictionary<Chart, Document>();



        public async void BuildDefaultBarChartDisplay(Document document) {

            var valueList = GetAppropriateDataSet(document);
            Series series = new BarSeries {
                DependentValuePath = "Value",
                IndependentValuePath = "Key",
                ItemsSource = valueList,
                IsSelectionEnabled = true,
                Tag = document,

            };

            var chart = new Chart {
                Title = string.Format("Key Subjects in {0}", document.FileName),
                Tag = valueList.ToArray()
            };

            series.MouseMove += (sender, e) => {
                series.ToolTip = (e.Source as DataPoint).IndependentValue;
            };
            documentsByChart.Add(chart, document);
            chart.Series.Add(series);

            var tabItem = new TabItem {
                Header = document.FileName,
                Content = chart,
                Tag = chart


            };
            FrequencyCharts.Items.Add(tabItem);
            await ToBarCharts();
        }

        private IEnumerable<KeyValuePair<string, float>> GetAppropriateDataSet(Document document) {
            var valueList = chartKind == ChartKind.NounPhrasesOnly ? GetNounPhraseData(document) : chartKind == ChartKind.SubjectVerbObject ? GetSVOIData(document) : GetSVOIData(document);
            return valueList;
        }
        #region Chart Transposing Methods


        /// <summary>
        /// Reconfigures all charts to Subjects Column perspective
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
        /// Reconfigures all charts to Subjects Pie perspective
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
        /// Reconfigures all charts to Subjects Bar perspective
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
                var items = GetAppropriateData(chart);
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

        private IEnumerable<KeyValuePair<string, float>> GetAppropriateData(object chart) {
            var items = GetAppropriateDataSet(documentsByChart[((chart as TabItem).Content as Chart)]);
            return items;
        }


        async Task ToLineCharts() {
            foreach (var chart in FrequencyCharts.Items) {
                var items = GetAppropriateData(chart);
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

        private List<KeyValuePair<string, float>> GetItemSourceFor(object chart) {
            var chartSource = ((chart as TabItem).Content as Chart).Tag as IEnumerable<KeyValuePair<string, float>>;
            var items = (from i in chartSource.ToArray()

                         orderby i.Value descending
                         select new KeyValuePair<string, float>(i.Key.ToString(), i.Value)).Take(10).ToList();
            return items;
        }

        #endregion






        private void ChangeChartKind(ChartKind chartKind) {
            foreach (var pair in documentsByChart) {

                Document doc = pair.Value;
                Chart chart = pair.Key;

                IEnumerable<KeyValuePair<string, float>> data = null;

                switch (chartKind) {

                    case ChartKind.SubjectVerbObject:
                        data = GetSVOIData(doc);
                        break;
                    case ChartKind.NounPhrasesOnly:
                        data = GetNounPhraseData(doc);
                        break;
                }
                data = data.Take(ChartItemLimit);
                chart.Series.Clear();
                chart.Series.Add(new BarSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = data,
                    IsSelectionEnabled = true,

                });
                chart.Title = string.Format("Key Relationships in {0}", doc.FileName);
                break;
            }
        }


        private static IEnumerable<KeyValuePair<string, float>> GetSVOIData(Document doc) {
            var data = GetVerbWiseAssociationData(doc);
            return from svs in data

                   let SV = new KeyValuePair<string, float>(string.Format("{0} -> {1}\n", svs.Subject.Text, svs.Verbial.Text) + (svs.Direct != null ? " -> " + svs.Direct.Text : "") + (svs.Indirect != null ? " -> " + svs.Indirect.Text : ""),
                       (float) Math.Round(svs.SumWeight, 2))
                   group SV by SV into svg
                   select svg.Key;

        }

        private static IEnumerable<NpVpNpNpQuatruple> GetVerbWiseAssociationData(Document doc) {
            var data =
                 from svPair in
                     (from v in doc.Phrases.GetVerbPhrases().WithSubject()
                      from s in v.BoundSubjects
                      from dobj in v.DirectObjects
                      from iobj in v.IndirectObjects
                      let relationshipWeight = s.Weight + v.Weight + dobj.Weight + iobj.Weight

                      select new NpVpNpNpQuatruple {
                          Subject = s as NounPhrase ?? null,
                          Verbial = v as VerbPhrase ?? null,
                          Direct = dobj as NounPhrase ?? null,
                          Indirect = iobj as NounPhrase ?? null,
                          SumWeight = relationshipWeight
                      }).Distinct(new SVComparer())
                 select svPair into svps

                 orderby svps.SumWeight

                 select svps;
            return data.ToArray();
        }

        private static IEnumerable<KeyValuePair<string, float>> GetNounPhraseData(Document doc) {
            return from NP in doc.Phrases.GetNounPhrases().Distinct() //.Except(doc.Phrases.GetPronounPhrases()) 

                   group NP by new {
                       NP.Text,
                       NP.Weight
                   } into NP
                   select NP.Key into master
                   orderby master.Weight descending
                   select new KeyValuePair<string, float>(master.Text, (float) Math.Round(master.Weight, 2));
        }

        #region Result Selector Helper Structs
        /// <summary>
        /// A little data type which defines Subjects predicate function which is passed to the "Distinct()" method call above
        /// It is defined because distinct does not support lambda(read function) arguments like my query operatorrs do.
        /// Pay this type little heed
        /// </summary>
        private struct SVComparer : IEqualityComparer<NpVpNpNpQuatruple>
        {
            public bool Equals(NpVpNpNpQuatruple x, NpVpNpNpQuatruple y) {
                return x.Subject.Text == y.Subject.Text || x.Subject.IsSimilarTo(y.Subject) &&
                    x.Verbial.Text == y.Verbial.Text || x.Verbial.IsSimilarTo(y.Verbial);
            }

            public int GetHashCode(NpVpNpNpQuatruple obj) {
                return obj.GetHashCode();
            }
        }
        /// <summary>
        /// Sometimes an anonymous type simple will not do. So this little struct is defined to 
        /// store temporary query data from transposed tables. god it is late. I can't document properly.
        /// </summary>
        private struct NpVpNpNpQuatruple
        {
            public NounPhrase Subject {
                get;
                set;
            }
            public VerbPhrase Verbial {
                get;
                set;
            }
            public NounPhrase Direct {
                get;
                set;
            }
            public NounPhrase Indirect {
                get;
                set;
            }
            public decimal SumWeight {
                get;
                set;
            }

        }

        #endregion


        private List<Document> documents = new List<Document>();
        private const int ChartItemLimit = 14;
        public List<Document> Documents {
            get {
                return documents;
            }
            set {
                documents = value;
            }
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




        private void ToggleEntitiesAndRelationshipsButton_Click(object sender, RoutedEventArgs e) {
            ChangeChartKind(ChartKind.SubjectVerbObject);
            PreserveStyle();
            toggleChartSourceButton.Click -= ToggleEntitiesAndRelationshipsButton_Click;
            RoutedEventHandler togglefunction = null;
            togglefunction = (sen, evt) => {
                ChangeChartKind(ChartKind.NounPhrasesOnly);
                PreserveStyle();
                toggleChartSourceButton.Click -= togglefunction;
                toggleChartSourceButton.Click += ToggleEntitiesAndRelationshipsButton_Click;

            };
            toggleChartSourceButton.Click += togglefunction;
        }

        private async void PreserveStyle() {
            switch (ChartStyle) {
                case ChartStyle.Bar:
                    await ToBarCharts();
                    break;
                case ChartStyle.Col:
                    await ToColumnCharts();
                    break;
                case ChartStyle.Pie:
                    await ToPieCharts();
                    break;
            }
        }

        private static IEnumerable<string> topIndirectObjects(Document doc) {
            return from io in doc.Phrases.GetNounPhrases().InIndirectObjectRole()
                   orderby io.Weight descending
                   select string.Format("{0}  [{1}]", io.Text, Math.Round(io.Weight, 2));
        }

        private static IEnumerable<string> topDirectObjects(Document doc) {
            return from dO in doc.Phrases.GetNounPhrases().InDirectObjectRole()
                   orderby dO.Weight descending
                   select string.Format("{0}  [{1}]", dO.Text, Math.Round(dO.Weight, 2));
            ;
        }

        private static IEnumerable<string> topVerbs(Document doc) {
            return from v in doc.Phrases.GetVerbPhrases().WithSubject()
                   select string.Format("{0}  [{1}]", v.Text, Math.Round(v.Weight, 2));
        }

        private static IEnumerable<string> topSubjects(Document doc) {
            return from s in doc.Phrases.GetNounPhrases().InSubjectRole()
                   orderby s.Weight descending
                   select string.Format("{0}  [{1}]", s.Text, Math.Round(s.Weight, 2));
        }



        private static IEnumerable<object> CreateStringListsForData(IEnumerable<NpVpNpNpQuatruple> elementsToSerialize) {
            var dataRows = from result in elementsToSerialize
                           orderby result.SumWeight
                           select new {
                               Subject = result.Subject != null ? result.Subject.Text : string.Empty,
                               Verbial = result.Verbial != null ? result.Verbial.Text : string.Empty,
                               Direct = result.Direct != null ? result.Direct.Text : string.Empty,
                               Indirect = result.Indirect != null ? result.Indirect.Text : string.Empty,

                           };
            return dataRows;
        }
        private async Task BuildSVOIGridViewAsync(Document doc) {

            var transformedData = await Task.Factory.StartNew(() => {
                return CreateStringListsForData(GetVerbWiseAssociationData(doc));
            });
            var wpfToolKitDataGrid = new Microsoft.Windows.Controls.DataGrid {
                ItemsSource = transformedData,
            };
            var tab = new TabItem {
                Header = doc.FileName,
                Content = wpfToolKitDataGrid
            };
            SVODResultsTabControl.Items.Add(tab);

        }
        private ChartStyle ChartStyle = ChartStyle.Bar;
        private ChartKind chartKind;


    }
    enum ChartStyle
    {
        Bar,
        Col,
        Pie
    }
}
