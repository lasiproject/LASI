using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using LASI.Core;
using LASI.Core.Analysis.Heuristics.Support;
using LASI.Interop;
using LASI.Utilities;
using static LASI.App.WindowManager;
using static LASI.App.ChartKind;
using System.Collections.Immutable;

namespace LASI.App
{
    /// <summary>
    /// Provides static methods for formatting and displaying results to the user.
    /// </summary>
    public static class Visualizer
    {
        #region Chart Transposing Methods

        /// <summary>
        /// Instructs the Charting manager to recreate all Per-Document charts based on the provided
        /// ChartKind. The given ChartKind will be used for all further chart operations until it is
        /// changed via another call to ChangeChartKind.
        /// </summary>
        /// <param name="chartKind">
        /// The ChartKind value determining the what data set is to be displayed.
        /// </param>
        public static async Task ChangeChartKindAsync(ChartKind chartKind)
        {
            ChartKind = chartKind;
            foreach (var pair in DocumentsByChart)
            {
                var document = pair.Value;
                var chart = pair.Key;
                var data = await CreateChartDataAsync(chartKind, document);
                chart.Series.Clear();
                var barSeries = new BarSeries
                {
                    ItemsSource = from point in data select point.ToKeyValuePair()
                };
                ConfigureDataPointSeries(barSeries);
                chart.Series.Add(barSeries);
                chart.Title = $"Key Relationships in {document.Name}";
            }
        }

        /// <summary>
        /// Asynchronously initializes, creates and displays the default Chart for the given
        /// document. This method should only be called once.
        /// </summary>
        /// <param name="document">The document whose contents are to be charted.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        public static async Task InitChartDisplayAsync(Document document)
        {
            var chart = await BuildBarChartAsync(document);
            DocumentsByChart = DocumentsByChart.Add(chart, document);
            var tab = new TabItem
            {
                Header = document.Name,
                Content = chart,
                Tag = chart
            };
            ResultsScreen.FrequencyCharts.Items.Add(tab);
            ResultsScreen.FrequencyCharts.SelectedItem = tab;
            ToBarChartsAsync();
        }

        /// <summary>
        /// Reconfigures all charts to Subjects Bar perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static void ToBarChartsAsync()
        {
            foreach (var chart in RetrieveCharts())
            {
                var barSeries = new BarSeries { ItemsSource = chart.GetItemSource() };
                ConfigureDataPointSeries(barSeries);
                ResetChartContent(chart, barSeries);

            }
        }

        /// <summary>
        /// Reconfigures all charts to Subjects Column perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static void ToColumnChartsAsync()
        {
            foreach (var chart in RetrieveCharts())
            {
                var items = chart.GetItemSource();
                var series = new ColumnSeries { ItemsSource = items };
                ConfigureDataPointSeries(series);
                ResetChartContent(chart, series);
            }
        }

        /// <summary>
        /// Sets properties to values common to all data point series created by the Visualizer.
        /// </summary>
        /// <param name="series"></param>
        private static void ConfigureDataPointSeries(DataPointSeries series)
        {
            series.DependentValuePath = "Value"; // this string is expected by the charting engine
            series.IndependentValuePath = "Key"; // this string is expected by the charting engine
            series.IsSelectionEnabled = true;
            series.MouseMove += (s, e) =>
            {
                series.ToolTip = (e.Source as DataPoint).IndependentValue;
            };
            series.IsMouseCaptureWithinChanged += delegate
            {
                series.ToolTip = ((DataPoint)series.SelectedItem).DependentValue;
            };
        }


        /// <summary>
        /// Reconfigures all charts to Subjects Pie perspective
        /// </summary>
        /// <returns>A Task which completes on the successful reconstruction of all charts</returns>
        public static void ToPieChartsAsync()
        {
            foreach (var chart in RetrieveCharts())
            {
                var items = chart.GetItemSource();
                var pieSeries = new PieSeries { ItemsSource = items };
                ConfigureDataPointSeries(pieSeries);

                ResetChartContent(chart, pieSeries);
            }
        }

        private static async Task<Chart> BuildBarChartAsync(Document document)
        {
            var topPoints = await GetTopResultDataPointsAsync(document);
            var series = new BarSeries
            {
                ItemsSource = topPoints.Take(ChartItemLimit),
                Tag = document,
            };
            ConfigureDataPointSeries(series);
            var chart = new Chart
            {
                Title = $"Key Subjects in {document.Name}",
                Tag = topPoints
            };
            //chart.MouseEnter += (sender, e) => chart.ToolTip = (e.Source as DataPoint).DependentValue + " " + (e.Source as DataPoint).IndependentValue;
            chart.Series.Add(series);
            return chart;
        }

        private static async Task<IEnumerable<(string key, float value)>> GetTopResultDataPointsAsync(Document document)
        {
            var dataPointSourceTask =
                ChartKind == NounPhrasesOnly
                ? GetNounWiseDataAsync(document)
                : GetVerbWiseRelationshipChartItemsAsync(document);

            return from point in await dataPointSourceTask.ConfigureAwait(false)
                   orderby point.value descending
                   select point;
        }

        private static async Task<IEnumerable<(string key, float value)>> CreateChartDataAsync(ChartKind chartKind, Document document)
        {
            switch (chartKind)
            {
                case SubjectVerbObject:
                    return await GetVerbWiseRelationshipChartItemsAsync(document);
                case NounPhrasesOnly:
                    return await GetNounWiseDataAsync(document);
                default:
                    return await GetNounWiseDataAsync(document);
            }
        }

        private static IEnumerable<Chart> RetrieveCharts() => ResultsScreen.FrequencyCharts.Items
            .OfType<TabItem>()
            .Select(tab => tab.Content)
            .OfType<Chart>();

        #endregion Chart Transposing Methods

        #region General Chart Building Methods

        /// <summary>
        /// Removes all current data points from the chart before adding the provided
        /// DataPointSeries to the chart.
        /// </summary>
        /// <param name="chart">The chart whose contents to replace with the given The DataPointSeries.</param>
        /// <param name="series">
        /// The DataPointSeries containing the data points which the chart will contain.
        /// </param>
        public static void ResetChartContent(Chart chart, DataPointSeries series)
        {
            chart.Series.Clear();
            chart.Series.Add(series);
        }

        private static IEnumerable<(string key, float value)> GetItemSource(this Chart chart) => (chart.Tag as IEnumerable<(string key, float value)>).Reverse();

        #endregion General Chart Building Methods

        /// <summary>
        /// Asynchronously generates, composes and displays the key relationships view for the given Document.
        /// </summary>
        /// <param name="document">The document for which to build relationships.</param>
        /// <returns>A Task representing the ongoing asynchronous operation.</returns>
        public static async Task DisplayKeyRelationshipsAsync(Document document)
        {
            var verbalRelationships = await GetVerbWiseRelationshipsAsync(document);
            var tab = new TabItem
            {
                Header = document.Name,
                Content = new Microsoft.Windows.Controls.DataGrid
                {
                    ItemsSource = verbalRelationships.ToGridRowData(),
                }
            };
            ResultsScreen.KeyRelationshipsResultsControl.Items.Add(tab);
            ResultsScreen.KeyRelationshipsResultsControl.SelectedItem = tab;
        }

        /// <summary>
        /// Transforms the given relationships into textual objects for display on a 4 column grid.
        /// The resulting sequence is suitable for direct insertion into a DataGrid.
        /// </summary>
        /// <param name="relationships">
        /// The sequence of Relationship Tuple to transform into textual Display elements.
        /// </param>
        /// <returns>A sequence of textual Display suitable for direct insertion into a DataGrid.</returns>
        internal static IEnumerable<object> ToGridRowData(this IEnumerable<SvoRelationship> relationships) =>
            from relationship in relationships
            orderby relationship.Weight descending
            select new
            {
                Subject = relationship.Subject?.Text,
                Verbal = FormatVerbalText(relationship.Verbal),
                Direct = FormatObjectText(relationship.Direct),
                Indirect = FormatObjectText(relationship.Indirect),
                Weight = Math.Round(relationship.Weight, 2)
            };

        private static string FormatObjectText(IEntity o) => (o as IPrepositionLinkable)?.LeftPrepositional?.Text + " " + o?.Text;

        private static string FormatVerbalText(IVerbal verbal)
        {
            var prepositionalText = verbal.Match((IPrepositionLinkable p) => p.LeftPrepositional?.Text);
            var adverbialText = string.Join(" ", verbal.AdverbialModifiers.Select(m => m.Text));
            return $"{prepositionalText} {verbal.Modality?.Text} {verbal.Text} {adverbialText}";
        }

        private static IEnumerable<(string key, float value)> GetNounWiseRelationshipChartItems(Document document) =>
            document.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                .OfNounPhrase()
                .Meld()
                .Select(entity => (entity.Text, (float)Math.Round(entity.Weight, 2)))
                .Distinct();

        private static async Task<IEnumerable<(string key, float value)>> GetNounWiseDataAsync(Document document) =>
            await MaterializeAsync(() => GetNounWiseRelationshipChartItems(document));

        private static async Task<IEnumerable<(string key, float value)>> GetVerbWiseRelationshipChartItems(Document document)
        {
            var verbWiseRelationships = await GetVerbWiseRelationshipsAsync(document).ConfigureAwait(false);
            var chartItems =
                from relationship in verbWiseRelationships
                let key = $@"{relationship.Subject.Text} -> {relationship.Verbal.Text}
                             {(relationship.Direct != null ? " -> " + relationship.Direct.Text : string.Empty)}
                             {(relationship.Indirect != null ? " -> " + relationship.Indirect.Text : string.Empty)}"
                let value = (float)Math.Round(relationship.Weight, 2)
                select (key, value);
            return chartItems.Distinct();
        }

        private static async Task<IEnumerable<(string key, float value)>> GetVerbWiseRelationshipChartItemsAsync(Document document) =>
            await Task.Run(async () => await GetVerbWiseRelationshipChartItems(document)).ConfigureAwait(false);

        private static async Task<IEnumerable<SvoRelationship>> GetVerbWiseRelationshipsAsync(Document document) => await MaterializeAsync(() =>
        {
            var consideredVerbals = document.Phrases
            .OfVerbal()
            .AsParallel()
            .WithDegreeOfParallelism(Concurrency.Max)
            .WithSubject(subject => subject is IReferencer r ? true : subject is IEntity e)
            .Distinct((x, y) => x.IsSimilarTo(y));

            return from verbal in consideredVerbals
                   select verbal into verbal
                   from entity in verbal.Subjects.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                   let subject = entity.Match()
                   .When((IReferencer r) => r.RefersTo.EmptyIfNull().Any())
                   .Then((IReferencer r) => r.RefersTo)
                   .Result(entity)
                   from direct in verbal.DirectObjects.DefaultIfEmpty()
                   from indirect in verbal.IndirectObjects.DefaultIfEmpty()
                   where direct != null || indirect != null
                   where subject.Text != (direct ?? indirect).Text
                   let relationship = new SvoRelationship(
                       verbal: verbal,
                       subject: verbal.AggregateSubject,
                       direct: verbal.AggregateDirectObject,
                       indirect: verbal.AggregateIndirectObject
                   )
                   group relationship by relationship into g
                   let relationship = g.Key
                   orderby relationship.Weight descending
                   select relationship;
        });

        private static async Task<IEnumerable<T>> MaterializeAsync<T>(Func<IEnumerable<T>> buildResultSet) =>
            await Task.Run(buildResultSet).ConfigureAwait(false);

        private static ChartKind ChartKind;
        private const int ChartItemLimit = 14;
        private static IImmutableDictionary<Chart, Document> DocumentsByChart = ImmutableDictionary<Chart, Document>.Empty;
    }
}