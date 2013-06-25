using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.InteropLayer;


namespace LASI.UserInterface.DataVisualzationProviders
{
    public static class ChartingManager
    {


        #region Chart Transposing Methods

        public static void ChangeChartKind(ChartKind chartKind) {
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


        /// <summary>
        /// Reconfigures all charts to Subjects Column perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static async Task ToColumnCharts() {
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items) {
                var items = GetItemSourceFor(chart);
                items.Reverse();
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
        public static async Task ToPieCharts() {
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items) {
                var items = GetItemSourceFor(chart);
                items.Reverse();
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
        public static async Task ToBarCharts() {
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items) {
                var items = GetItemSourceFor(chart);
                items.Reverse();
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
        public static IEnumerable<KeyValuePair<string, float>> GetAppropriateData(object chart) {
            var items = ChartingManager.GetAppropriateDataSet(documentsByChart[((chart as TabItem).Content as Chart)]);
            return items;
        }
        public static async Task BuildMainChartDisplay(Document document) {
            var chart = BuildBarChart(document);
            documentsByChart.Add(chart, document);
            var tabItem = new TabItem {
                Header = document.FileName,
                Content = chart,
                Tag = chart
            };
            WindowManager.ResultsScreen.FrequencyCharts.Items.Add(tabItem);
            await ChartingManager.ToBarCharts();
        }


        private static Chart BuildBarChart(Document document) {

            var valueList = ChartingManager.GetAppropriateDataSet(document);
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

            chart.Series.Add(series);
            return chart;
        }

        /// <summary>
        /// This needs to be modified
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Chart BuildEntityActionEntityChart(Document document) {
            var verticalTopEntites = from np in
                                         (from np in document.Phrases.GetNounPhrases().InSubjectRole()
                                          orderby np.Weight
                                          select np).Take(20)
                                     select new KeyValuePair<string, string>(np.Text, np.SubjectOf.Text);
            var horizontalTopEntites = from np in
                                           (from np in document.Phrases.GetNounPhrases().InDirectObjectRole()
                                            orderby np.Weight
                                            select np).Take(20)
                                       select new KeyValuePair<string, string>(np.Text, np.SubjectOf.Text);
            var horizontalEntitySeries = new LineSeries {
                IndependentValuePath = "Key",
                DependentValuePath = "Value",
                ItemsSource = horizontalTopEntites,
                IsEnabled = true,
                Tag = document
            };
            var verticalEntitySeries = new LineSeries {
                IndependentValuePath = "Key",
                DependentValuePath = "Value",
                ItemsSource = verticalTopEntites,
                IsEnabled = true,
                Tag = document
            };


            var chart = new Chart {
                Title = string.Format("E A E Relationships in{0}", document.FileName)
            };
            chart.Series.Add(horizontalEntitySeries);
            chart.Series.Add(verticalEntitySeries);
            return chart;
        }


        #endregion

        #region General Chart Building Methods

        public static void ResetChartContent(object c, DataPointSeries series) {
            ((c as TabItem).Content as Chart).Series.Clear();
            ((c as TabItem).Content as Chart).Series.Add(series);
        }

        public static List<KeyValuePair<string, float>> GetItemSourceFor(object chart) {
            var chartSource = ((chart as TabItem).Content as Chart).Tag as IEnumerable<KeyValuePair<string, float>>;
            var items = (from i in chartSource.ToArray()

                         orderby i.Value descending
                         select new KeyValuePair<string, float>(i.Key.ToString(), i.Value)).Take(10).ToList();
            return items;
        }

        #endregion
        private static IEnumerable<KeyValuePair<string, float>> GetSVOIData(Document doc) {
            var data = GetVerbWiseAssociationData(doc);
            return from svs in data

                   let SV = new KeyValuePair<string, float>(
                       string.Format("{0} -> {1}\n", svs.Subject.Text, svs.Verbal.Text) +
                       (svs.Direct != null ? " -> " + svs.Direct.Text : "") +
                       (svs.Indirect != null ? " -> " + svs.Indirect.Text : ""),
                       ( float )Math.Round(svs.RelationshipWeight, 2))
                   group SV by SV into svg
                   select svg.Key;

        }
        private static IEnumerable<RelationshipTuple> GetVerbWiseAssociationData(Document doc) {
            var data =
                 from svPair in
                     (from v in doc.Phrases.GetVerbPhrases()
                          .WithSubject(s => (s as IPronoun) == null || (s as IPronoun).BoundEntity != null)
                          .AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                      from s in v.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                      let sub = s as IPronoun == null ? s : (s as IPronoun).BoundEntity
                      where sub != null
                      from dobj in v.DirectObjects.DefaultIfEmpty()
                      from iobj in v.IndirectObjects.DefaultIfEmpty()

                      select new RelationshipTuple {
                          Subject = sub as NounPhrase ?? null,
                          Verbal = v as VerbPhrase ?? null,
                          Direct = dobj as NounPhrase ?? null,
                          Indirect = iobj as NounPhrase ?? null,
                          Prepositional = v.ObjectOfThePreoposition ?? null,
                          RelationshipWeight = sub.Weight + v.Weight + (dobj != null ? dobj.Weight : 0) + (iobj != null ? iobj.Weight : 0)
                      } into tupple
                      where
                        tupple.Direct != null ||
                        tupple.Indirect != null &&
                        tupple.Subject.Text != (tupple.Direct ?? tupple.Indirect).Text
                      select tupple).Distinct(new RelationshipComparer())
                 select svPair into svps

                 orderby svps.RelationshipWeight
                 select svps;
            return data.ToArray();
        }

        private static IEnumerable<KeyValuePair<string, float>> GetNounPhraseData(Document doc) {
            return from NP in doc.Phrases.GetNounPhrases().Distinct().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)

                   group NP by new
                   {
                       NP.Text,
                       NP.Weight
                   } into NP
                   select NP.Key into master
                   orderby master.Weight descending
                   select new KeyValuePair<string, float>(master.Text, ( float )Math.Round(master.Weight, 2));
        }
        public static IEnumerable<KeyValuePair<string, float>> GetAppropriateDataSet(Document document) {
            var valueList = chartKind == ChartKind.NounPhrasesOnly ? GetNounPhraseData(document) : chartKind == ChartKind.SubjectVerbObject ? GetSVOIData(document) : GetSVOIData(document);
            return valueList;
        }
        public static async Task BuildKeyRelationshipDisplay(Document document) {

            var transformedData = await Task.Factory.StartNew(() => {
                return CreateRelationshipData(ChartingManager.GetVerbWiseAssociationData(document));
            });
            var wpfToolKitDataGrid = new Microsoft.Windows.Controls.DataGrid {
                ItemsSource = transformedData,
            };
            var tab = new TabItem {
                Header = document.FileName,
                Content = wpfToolKitDataGrid
            };
            WindowManager.ResultsScreen.SVODResultsTabControl.Items.Add(tab);

        }

        public static async Task<IEnumerable<object>> CreateRelationshipDataAsync(IEnumerable<CrossDocumentJoiner.NVNN> elementsToSerialize) {

            return await Task.Run(() => CreateRelationshipData(elementsToSerialize));
        }

        public static IEnumerable<object> CreateRelationshipData(IEnumerable<CrossDocumentJoiner.NVNN> elementsToSerialize) {
            return CreateRelationshipData(
                            (from e in elementsToSerialize.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                             select new RelationshipTuple {
                                 Direct = e.Direct,
                                 Indirect = e.Indirect,
                                 Subject = e.Subject,
                                 Verbal = e.Verbal,
                                 Prepositional = e.ViaPreposition,
                                 RelationshipWeight = e.RelationshipWeight
                             }).Distinct(new RelationshipComparer()));
        }

        public static IEnumerable<object> CreateRelationshipData(IEnumerable<RelationshipTuple> elementsToSerialize) {
            var dataRows = from result in elementsToSerialize.Distinct(new RelationshipComparer())
                           orderby result.RelationshipWeight
                           select new
                           {
                               Subject = result.Subject != null ? result.Subject.Text : string.Empty,
                               Verbial = result.Verbal != null ? result.Verbal.Text : string.Empty,
                               Direct = result.Direct != null ? result.Direct.Text : string.Empty,
                               Indirect = result.Indirect != null ? result.Indirect.Text : string.Empty,
                               Prepositional = result.Prepositional != null ? result.Prepositional.Text : string.Empty
                           };
            return dataRows.Distinct();
        }
        private static Dictionary<Chart, Document> documentsByChart = new Dictionary<Chart, Document>();


        private const int chartItemLimit = 14;

        public static int ChartItemLimit {
            get {
                return chartItemLimit;
            }
        }




        public static ChartKind chartKind {
            get;
            set;
        }
    }
    #region Result Bulding Helper Structs
    /// <summary>
    /// A little data type which provides custom uniqueness logic for NounPhrase to VerbPhrase  relationships. Provides the implementation of I equality comparer which is to be passed to the 
    /// "Distinct()" method of en IEnummerable collectio  of RelationshipTuple instances.
    /// It is defined because distinct does not support lambda(read function) arguments like my query operatorrs do.
    /// Pay this type little heed
    /// </summary>
    struct RelationshipComparer : IEqualityComparer<RelationshipTuple>
    {
        public bool Equals(RelationshipTuple lhs, RelationshipTuple rhs) {

            if ((lhs as object == null || rhs as object == null)) {
                return !(lhs as object == null ^ rhs as object == null);
            }
            else if (lhs.Elements.Count != rhs.Elements.Count) {
                return false;
            }
            else {

                bool result = lhs.Verbal.Text == rhs.Verbal.Text || lhs.Verbal.IsSimilarTo(rhs.Verbal);
                result &= LexicalComparers<NounPhrase>.AliasOrSimilarity.Equals(lhs.Subject, rhs.Subject);
                if (lhs.Direct != null && rhs.Direct != null) {
                    result &= LexicalComparers<NounPhrase>.AliasOrSimilarity.Equals(lhs.Direct, rhs.Direct);
                }
                else if (lhs.Direct == null || rhs.Direct == null)
                    return false;
                if (lhs.Indirect != null && rhs.Indirect != null) {
                    result &= LexicalComparers<NounPhrase>.AliasOrSimilarity.Equals(lhs.Indirect, rhs.Indirect);
                }
                else if (lhs.Indirect == null || rhs.Indirect == null)
                    return false;
                return result;
            }
        }

        public int GetHashCode(RelationshipTuple obj) {
            return obj == null ? 0 : obj.Elements.Count;
        }
    }
    /// <summary>
    /// Sometimes an anonymous type simple will not do. So this little class is defined to 
    /// store temporary query data from transposed tables. god it is late. I can't document properly.
    /// </summary>
    public class RelationshipTuple
    {
        IEntity subject;

        public IEntity Subject {
            get {
                return subject;
            }
            set {
                subject = value;
                elements.Add(value);
            }
        }

        VerbPhrase verbal;

        public VerbPhrase Verbal {
            get {
                return verbal;
            }
            set {
                verbal = value;
                elements.Add(value);
            }
        }

        IEntity direct;

        public IEntity Direct {
            get {
                return direct;
            }
            set {
                direct = value;
                elements.Add(value);
            }
        }

        IEntity indirect;

        public IEntity Indirect {
            get {
                return indirect;
            }
            set {
                indirect = value;
                elements.Add(value);
            }
        }

        ILexical prepositional;

        public ILexical Prepositional {
            get {
                return prepositional;
            }
            set {
                prepositional = value;
                elements.Add(value);
            }
        }


        HashSet<ILexical> elements = new HashSet<ILexical>();

        public HashSet<ILexical> Elements {
            get {
                return elements;
            }
        }


        public double RelationshipWeight {
            get;
            set;
        }
        /// <summary>
        /// Returns a textual representation of the RelationshipTuple.
        /// </summary>
        /// <returns>A textual representation of the RelationshipTuple.</returns>
        public override string ToString() {
            var result = Subject.Text + Verbal.Text;
            if (Direct != null) {
                result += Direct.Text;
            }
            if (Indirect != null) {
                result += Indirect.Text;
            }
            return result;
        }


    }

    #endregion
}
