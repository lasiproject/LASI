﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.LexicalInformationProviders;
using LASI.InteropLayer;


namespace LASI.UserInterface
{
    public static class ChartingManager
    {


        #region Chart Transposing Methods

        /// <summary>
        /// Instructs the Charting manager to recreate all Per-Document charts based on the provided ChartKind. 
        /// The given ChartKind will be used for all further chart operations until it is changed via another call to ChangeChartKind.
        /// </summary>
        /// <param name="chartKind">The ChartKind value determining the what data set is to be displayed.</param>
        public static void ChangeChartKind(ChartKind chartKind) {
            ChartKind = chartKind;
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
                data = data.Take(CHART_ITEM_LIMIT);
                chart.Series.Clear();
                chart.Series.Add(new BarSeries {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = data,
                    IsSelectionEnabled = true,

                });
                chart.Title = string.Format("Key Relationships in {0}", doc.Name);
                break;
            }
        }


        /// <summary>
        /// Reconfigures all charts to Subjects Column perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static async Task ToColumnCharts() {
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items.OfType<TabItem>().Select(item => item.Content as Chart).Where(c => c != null)) {
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
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items.OfType<TabItem>().Select(item => item.Content as Chart).Where(c => c != null)) {
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
            foreach (var chart in WindowManager.ResultsScreen.FrequencyCharts.Items.OfType<TabItem>().Select(item => item.Content as Chart).Where(c => c != null)) {
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
        /// <summary>
        /// Asynchronously initializes, creates and displays the default Chart for the given document. This method should only be called once.
        /// </summary>
        /// <param name="document">The document whose contents are to be charted.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        public static async Task InitChartDisplayAsync(Document document) {
            var chart = BuildBarChart(document);
            documentsByChart.Add(chart, document);
            var tab = new TabItem {
                Header = document.Name,
                Content = chart,
                Tag = chart
            };
            WindowManager.ResultsScreen.FrequencyCharts.Items.Add(tab);
            WindowManager.ResultsScreen.FrequencyCharts.SelectedItem = tab;
            await ToBarCharts();
        }


        private static Chart BuildBarChart(Document document) {

            var valueList = GetDataPointSourceForDoc(document, ChartKind);
            Series series = new BarSeries {
                DependentValuePath = "Value",
                IndependentValuePath = "Key",
                ItemsSource = valueList,
                IsSelectionEnabled = true,
                Tag = document,

            };

            var chart = new Chart {
                Title = string.Format("Key Subjects in {0}", document.Name),
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
        private static Chart BuildEntityActionEntityChart(Document document) {
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
                Title = string.Format("E A E Relationships in{0}", document.Name)
            };
            chart.Series.Add(horizontalEntitySeries);
            chart.Series.Add(verticalEntitySeries);
            return chart;
        }


        #endregion

        #region General Chart Building Methods
        /// <summary>
        /// Removes all current data points from the chart before adding the provided DataPointSeries to the chart.
        /// </summary>
        /// <param name="chart">The chart whose contents to replace with the given The DataPointSeries.</param>
        /// <param name="series">The DataPointSeries containing the data points which the chart will contain.</param>
        public static void ResetChartContent(Chart chart, DataPointSeries series) {
            chart.Series.Clear();
            chart.Series.Add(series);
        }

        private static IEnumerable<KeyValuePair<string, float>> GetItemSourceFor(Chart chart) {
            var chartSource = chart.Tag as IEnumerable<KeyValuePair<string, float>>;
            var items = (from i in chartSource.ToArray()
                         orderby i.Value descending
                         select new KeyValuePair<string, float>(i.Key.ToString(), i.Value)).Take(10);
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
        private static IEnumerable<KeyValuePair<string, float>> GetDataPointSourceForDoc(Document document, ChartKind chartKind) {
            var valueList = chartKind == ChartKind.NounPhrasesOnly ? GetNounPhraseData(document) : chartKind == ChartKind.SubjectVerbObject ? GetSVOIData(document) : GetSVOIData(document);
            return valueList;
        }
        /// <summary>
        /// Asynchronously generates, composeses and displays the key relationships view for the given Document.
        /// </summary>
        /// <param name="document">The document for which to build relationships.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        public static async Task BuildKeyRelationshipDisplay(Document document) {

            var transformedData = await Task.Factory.StartNew(() => {
                return CreateRelationshipData(GetVerbWiseAssociationData(document));
            });
            var wpfToolKitDataGrid = new Microsoft.Windows.Controls.DataGrid {
                ItemsSource = transformedData,
            };
            var tab = new TabItem {
                Header = document.Name,
                Content = wpfToolKitDataGrid
            };
            WindowManager.ResultsScreen.SVODResultsTabControl.Items.Add(tab);
            WindowManager.ResultsScreen.SVODResultsTabControl.SelectedItem = tab;

        }

        private static async Task<IEnumerable<object>> CreateRelationshipDataAsync(IEnumerable<RelationshipTuple> elementsToSerialize) {

            return await Task.Run(() => CreateRelationshipData(elementsToSerialize));
        }


        /// <summary>
        /// Creates and returns a sequence of textual display elements from the given sequence of RelationshipTuple elements.
        /// The resulting sequence is suitable for direct insertion into a DataGrid.
        /// </summary>
        /// <param name="elementsToSerialize">The sequence of Relationship Tuple to tranform into textual display elements.</param>
        /// <returns>A sequence of textual display elements from the given sequence of RelationshipTuple elements.</returns>
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
        /// <summary>
        /// Gets the ChartKind currently used by the ChartManager.
        /// </summary>
        public static ChartKind ChartKind {
            get;
            private set;
        }
        private static Dictionary<Chart, Document> documentsByChart = new Dictionary<Chart, Document>();


        private const int CHART_ITEM_LIMIT = 14;




    }
    #region Result Bulding Helper Structs
    /// <summary>
    /// A little data type which provides custom uniqueness logic for NounPhrase to VerbPhrase  relationships. Provides the implementation of I equality comparer which is to be passed to the 
    /// "Distinct()" method of en IEnummerable collectio  of RelationshipTuple instances.
    /// It is defined because distinct does not support lambda(read function) arguments like my query operatorrs do.
    /// Pay this type little heed
    /// </summary>
    class RelationshipComparer : IEqualityComparer<RelationshipTuple>
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

        IVerbal verbal;

        public IVerbal Verbal {
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
        public override bool Equals(object obj) {
            return this == obj as RelationshipTuple;
        }
        public override int GetHashCode() {
            return 1;
        }

        public static bool operator ==(RelationshipTuple lhs, RelationshipTuple rhs) {

            if ((lhs as object != null || rhs as object == null) || (lhs as object == null || rhs as object != null))
                return false;
            else if (lhs as object == null && rhs as object == null)
                return true;
            else {
                bool result = lhs.Verbal.Text == rhs.Verbal.Text || lhs.Verbal.IsSimilarTo(rhs.Verbal);
                result &= LexicalComparers<IEntity>.AliasOrSimilarity.Equals(lhs.Subject, rhs.Subject);
                if (lhs.Direct != null && rhs.Direct != null) {
                    result &= LexicalComparers<IEntity>.AliasOrSimilarity.Equals(lhs.Direct, rhs.Direct);
                }
                else if (lhs.Direct == null || rhs.Direct == null)
                    return false;
                if (lhs.Indirect != null && rhs.Indirect != null) {
                    result &= LexicalComparers<IEntity>.AliasOrSimilarity.Equals(lhs.Indirect, rhs.Indirect);
                }
                else if (lhs.Indirect == null || rhs.Indirect == null)
                    return false;
                return result;
            }
        }

        public static bool operator !=(RelationshipTuple lhs, RelationshipTuple rhs) {
            return !(lhs == rhs);
        }

    }

    #endregion
}
