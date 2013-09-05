using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Lookup;
using LASI.Algorithm.Weighting;
using LASI.ContentSystem;
using LASI.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.InteropLayer
{

    /// <summary>
    /// Governs the complete analysis and processing of one or more text sources.
    /// Provides synchronous and asynchronoun callback based progress reports.
    /// </summary>
    public sealed class ProcessController : Progress<double>
    {

        /// <summary>
        /// Gets a Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt; which, when awaited, loads and analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.
        /// </summary>
        /// <param name="filesToProcess">The collection of TextFiles to analyize.</param>
        /// <returns>A Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt; which, when awaited, loads, analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</returns>
        public async Task<IEnumerable<Document>> AnalyseAllDocumentsAsync(IEnumerable<LASI.Algorithm.IUntaggedTextSource> filesToProcess) {
            return await AnalyseAllDocumentsAsync(filesToProcess, (s, d) => { });
        }
        /// <summary>
        /// Gets a Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt; which, when awaited, loads, analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances. Progress update logic is specified via a function parameter.
        /// </summary>
        /// <param name="filesToProcess">The collection of TextFiles to analyize.</param>
        /// <param name="onProgressUpdate">A void function to invoke with each progress increment.
        /// <example>
        /// Example lambda function:
        /// <code> (s, d) => { ... }
        /// </code>
        /// Example named function:
        /// <code>
        /// public void UpdateSomethingMethod(string s, double d){ ... }
        /// </code>
        /// </example>
        /// The function must take a string, representing a status message, and a double, representing the percentage of total work incremented.
        /// </param>
        /// <returns>A Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt; which, when awaited, loads and analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</returns>
        public async Task<IEnumerable<Document>> AnalyseAllDocumentsAsync(IEnumerable<LASI.Algorithm.IUntaggedTextSource> filesToProcess, Action<string, double> onProgressUpdate) {
            return await AnalyseAllDocumentsAsync(filesToProcess, async (s, d) => await Task.Run(() => onProgressUpdate(s, d)));
        }
        /// <summary>
        /// Gets a Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt; which, when awaited, loads, analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances. Progress update logic is specified via an asynchronous function parameter.
        /// </summary>
        /// <param name="filesToProcess">The collection of TextFiles to analyize.</param> 
        /// <param name="onProgressUpdate">A function to invoke with each progress increment. 
        /// The function must be asynchronous (must return a Task), and take a string, representing a status message, and a double, representing the percentage of total work incremented.
        /// <example>
        ///Example lambda function:
        ///<code>
        /// async (s, d) => { await ... }
        /// </code>
        /// Example named function:
        /// <code> 
        /// public async Task UpdateSomethingMethodAsync(string s, double d){ await ... }
        /// </code>
        /// </example>
        /// </param>
        /// <returns>A Task&lt;IEnumerable&lt;LASI.Algorithm.Document&gt;&gt;, when awaited, loads and analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</returns>
        public async Task<IEnumerable<Document>> AnalyseAllDocumentsAsync(IEnumerable<LASI.Algorithm.IUntaggedTextSource> filesToProcess, Func<string, double, Task> onProgressUpdate) {
            documentsInWorkLoad = filesToProcess.Count();
            stepSize = 2d / documentsInWorkLoad;
            updateProgressDisplay = onProgressUpdate;
            await LoadThesaurus();
            await updateProgressDisplay("Tagging Documents", 0);
            var taggingTasks = filesToProcess.Select(F => Task.Run(async () => await Tagger.TaggedFromRawAsync(F))).ToList();
            var taggedFiles = new ConcurrentBag<LASI.Algorithm.ITaggedTextSource>();
            while (taggingTasks.Any()) {
                var currentTask = await Task.WhenAny(taggingTasks);
                var taggedFile = await currentTask;
                taggingTasks.Remove(currentTask);
                taggedFiles.Add(taggedFile);
                await updateProgressDisplay(string.Format("{0}: Tagged", taggedFile.TextSourceName), stepSize + 1.5);
            }
            await updateProgressDisplay("Tagged Documents", 3);
            var tasks = taggedFiles.Select(tagged => ProcessTaggedFileAsync(tagged)).ToList();
            var documents = new ConcurrentBag<Document>();
            while (tasks.Any()) {
                var currentTask = await Task.WhenAny(tasks);
                var processedDocument = await currentTask;
                tasks.Remove(currentTask);
                documents.Add(processedDocument);
            }
            return documents;
        }


        private async Task<Document> ProcessTaggedFileAsync(LASI.Algorithm.ITaggedTextSource tagged) {
            var fileName = tagged.TextSourceName;
            await updateProgressDisplay(string.Format("{0}: Loading...", fileName), 0);
            var doc = await Tagger.DocumentFromTaggedAsync(tagged);
            await updateProgressDisplay(string.Format("{0}: Loaded", fileName), 4);
            await updateProgressDisplay(string.Format("{0}: Analyzing Syntax...", fileName), 0);
            var bindingWorkUnits = Binder.GetBindingTasksForDocument(doc).ToList();
            foreach (var task in bindingWorkUnits) {
                await updateProgressDisplay(task.InitializationMessage, 0);
                await task.Task;
                await updateProgressDisplay(task.CompletionMessage, task.PercentWorkRepresented * 0.5 / documentsInWorkLoad);
            }
            await updateProgressDisplay(string.Format("{0}: Correlating Relationships...", fileName), 0);
            var weightingWorkUnits = Weighter.GetWeightingProcessingTasks(doc).ToList();
            foreach (var task in weightingWorkUnits) {
                await updateProgressDisplay(task.InitializationMessage, 0);
                await task.Task;
                await updateProgressDisplay(task.CompletionMessage, task.PercentWorkRepresented * 0.5 / documentsInWorkLoad);
            }

            await updateProgressDisplay(string.Format("{0}: Completing Parse...", fileName), stepSize);
            return doc;
        }

        private async Task LoadThesaurus() {
            await updateProgressDisplay("Loading Thesaurus...", stepSize);
            var thesaurusTasks = LexicalLookup.GetLoadingTasks().ToList();
            while (thesaurusTasks.Any()) {
                var currentTask = await Task.WhenAny(thesaurusTasks);
                await updateProgressDisplay(await currentTask, 3);
                thesaurusTasks.Remove(currentTask);
            }
        }

        private double documentsInWorkLoad;
        private double stepSize;
        private Func<string, double, Task> updateProgressDisplay;
    }


}


