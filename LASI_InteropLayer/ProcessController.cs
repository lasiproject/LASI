using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.FileSystem;
using LASI.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Weighting;
namespace LASI.InteropLayer
{


    public class ProcessController
    {

        public async Task<IEnumerable<Document>> AnalyseAllDocuments(ProgressBar bar, Label label) {
            progressBar = bar;
            progressLabel = label;
            discreteWorkLoads = FileManager.TextFiles.Count;
            documentStepRatio = 2d / discreteWorkLoads;
            await LoadThesaurus();
            await UpdateProgressDisplay("Tagging Documents", 0);
            await FileManager.TagTextFilesAsync();
            await UpdateProgressDisplay("Tagged Documents", 6);
            var documents = new ConcurrentBag<Document>();
            var tasks = FileManager.TaggedFiles.Select(tagged => ProcessTaggedFileAsync(tagged, tagged.NameSansExt)).ToList();

            foreach (var task in tasks) {
                documents.Add(await task);
            }


            return documents;
        }

        private async Task<Document> ProcessTaggedFileAsync(FileSystem.FileTypes.TaggedFile tagged, string fileName) {
            await UpdateProgressDisplay(string.Format("{0}: Loading...", fileName), 0);
            var doc = await new TaggedFileParser(tagged).LoadDocumentAsync();
            await UpdateProgressDisplay(string.Format("{0}: Loaded...", fileName), 4);
            await UpdateProgressDisplay(string.Format("{0}: Analyzing Syntax...", fileName), 0);
            var bindingWorkUnits = Binder.GetBindingTasksForDocument(doc).ToList();
            foreach (var task in bindingWorkUnits) {
                await UpdateProgressDisplay(task.InitializationMessage, 0);
                await task.WorkToPerform;
                await UpdateProgressDisplay(task.CompletionMessage, task.PercentWorkRepresented * 0.52 / discreteWorkLoads);
            }
            await UpdateProgressDisplay(string.Format("{0}: Correlating Relationships...", fileName), 0);
            var weightingWorkUnits = LASI.Algorithm.Weighting.Weighter.GetWeightingProcessingTasks(doc).ToList();
            foreach (var task in weightingWorkUnits) {
                await UpdateProgressDisplay(task.InitializationMessage, 0);
                await task.WorkToPerform;
                await UpdateProgressDisplay(task.CompletionMessage, task.PercentWorkRepresented * 0.60 / discreteWorkLoads);
            }

            await UpdateProgressDisplay(string.Format("{0}: Parsing Complete...", fileName));
            return doc;
        }

        private async Task LoadThesaurus() {
            await UpdateProgressDisplay("Loading Thesaurus");
            var thesaurusTasks = Thesaurus.GetTasksToLoadAllThesauri().ToList();
            while (thesaurusTasks.Any()) {
                var finishedTask = await Task.WhenAny(thesaurusTasks);
                var message = await finishedTask;

                thesaurusTasks.Remove(finishedTask);
                await UpdateProgressDisplay(message, 3);
            }
        }

        private async Task UpdateProgressDisplay(string statusMessage) {
            await UpdateProgressDisplay(statusMessage, documentStepRatio);
        }
        private async Task UpdateProgressDisplay(string statusMessage, double progressIncrement) {
            progressLabel.Content = statusMessage;
            progressBar.ToolTip = statusMessage;
            var animateStep = progressIncrement / 100d;
            for (int i = 0; i < 25d; ++i) {
                progressBar.Value += 4 * animateStep;
                await Task.Delay(1);

            }

            Output.WriteLine(statusMessage);
            Output.WriteLine(progressIncrement);
        }

        private double discreteWorkLoads;
        private ProgressBar progressBar = null;
        private Label progressLabel = null;
        private double documentStepRatio;
    }



}


