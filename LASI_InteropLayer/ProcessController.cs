using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Lookup;
using LASI.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.InteropLayer
{


    public class ProcessController
    {
        /// <summary>
        /// Gets a Task which, when awaited, loads, analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
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
        /// <returns>A Task which, when awaited, loads and analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</returns>
        public async Task<IEnumerable<Document>> AnalyseAllDocumentsAsync(IEnumerable<LASI.Algorithm.IRawTextSource> filesToProcess, Func<string, double, Task> onProgressUpdate) {
            discreteWorkLoads = filesToProcess.Count();
            documentStepRatio = 2d / discreteWorkLoads;
            UpdateProgressDisplay = onProgressUpdate;
            await LoadThesaurus();
            await UpdateProgressDisplay("Tagging Documents", 0);

            var taggingTasks = filesToProcess.Select(F => Task.Run(async () => await TaggerUtil.TaggedFromRawAsync(F))).ToList();
            var taggedFiles = new ConcurrentBag<Algorithm.ITaggedTextSource>();
            while (taggingTasks.Any()) {
                var currentTask = await Task.WhenAny(taggingTasks);
                var taggedFile = await currentTask;
                taggingTasks.Remove(currentTask);
                taggedFiles.Add(taggedFile);
                await UpdateProgressDisplay(string.Format("{0}: Tagged", taggedFile.Name), documentStepRatio);
            }
            await UpdateProgressDisplay("Tagged Documents", 3);
            var tasks = taggedFiles.Select(tagged => ProcessTaggedFileAsync(tagged)).ToList();
            var documents = new ConcurrentBag<Document>();
            foreach (var task in tasks) {
                documents.Add(await task);
            }


            return documents;
        }
        /// <summary>
        /// Gets a Task which, when awaited, loads, analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
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
        /// <returns>A Task which, when awaited, loads and analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</returns>
        public async Task<IEnumerable<Document>> AnalyseAllDocumentsAsync(IEnumerable<LASI.Algorithm.IRawTextSource> filesToProcess, Action<string, double> onProgressUpdate) {
            return await AnalyseAllDocumentsAsync(filesToProcess, async (s, d) => await Task.Run(() => onProgressUpdate(s, d)));
        }
        /// <summary>
        /// Gets a Task which, when awaited, loads and analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.
        /// </summary>
        /// <param name="filesToProcess">The collection of TextFiles to analyize.</param>
        /// <returns>A Task which, when awaited, loads, analyizes, and aggregates all of the provided TextFile instances as individual documents, collecting them as
        /// a sequence of Bound and Weighted LASI.Algorithm.Document instances.</returns>
        public async Task<IEnumerable<Document>> AnalyseAllDocumentsAsync(IEnumerable<LASI.Algorithm.IRawTextSource> filesToProcess) {
            return await AnalyseAllDocumentsAsync(filesToProcess, (s, d) => { });
        }

        private async Task<Document> ProcessTaggedFileAsync(LASI.Algorithm.ITaggedTextSource tagged) {
            var fileName = tagged.Name;
            await UpdateProgressDisplay(string.Format("{0}: Loading...", fileName), 0);
            var doc = await TaggerUtil.DocumentFromTaggedAsync(tagged);
            await UpdateProgressDisplay(string.Format("{0}: Loaded", fileName), 4);
            await UpdateProgressDisplay(string.Format("{0}: Analyzing Syntax...", fileName), 0);
            var bindingWorkUnits = Binder.GetBindingTasksForDocument(doc).ToList();
            foreach (var task in bindingWorkUnits) {
                await UpdateProgressDisplay(task.InitializationMessage, 0);
                await task.Task;
                await UpdateProgressDisplay(task.CompletionMessage, task.PercentWorkRepresented * 0.5 / discreteWorkLoads);
            }
            await UpdateProgressDisplay(string.Format("{0}: Correlating Relationships...", fileName), 0);
            var weightingWorkUnits = LASI.Algorithm.Weighting.Weighter.GetWeightingProcessingTasks(doc).ToList();
            foreach (var task in weightingWorkUnits) {
                await UpdateProgressDisplay(task.InitializationMessage, 0);
                await task.Task;
                await UpdateProgressDisplay(task.CompletionMessage, task.PercentWorkRepresented * 0.5 / discreteWorkLoads);
            }

            await UpdateProgressDisplay(string.Format("{0}: Completing Parse...", fileName), documentStepRatio);
            return doc;
        }

        private async Task LoadThesaurus() {
            await UpdateProgressDisplay("Loading Thesaurus...", documentStepRatio);
            var thesaurusTasks = LexicalLookup.YetUnloadedResoucesTasks.ToList();
            while (thesaurusTasks.Any()) {
                var currentTask = await Task.WhenAny(thesaurusTasks);
                var message = await currentTask;

                thesaurusTasks.Remove(currentTask);
                await UpdateProgressDisplay(message, 3);
            }

        }

        private double discreteWorkLoads;
        private double documentStepRatio;
        private Func<string, double, Task> UpdateProgressDisplay;
    }


}


