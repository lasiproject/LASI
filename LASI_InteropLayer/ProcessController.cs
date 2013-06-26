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

        public async Task<IEnumerable<Document>> AnalyseAllDocuments(ProgressBar progressBar, Label progressLabel) {
            ProgressBar = progressBar;
            ProgressLabel = progressLabel;
            documentStepRatio = 6f / FileManager.TextFiles.Count;
            await LoadThesaurus();
            UpdateProgressDisplay("Tagging Documents");
            await FileManager.TagTextFilesAsync();
            progressBar.Value += 4;
            var documents = new ConcurrentBag<Document>();
            var tasks = (from tagged in FileManager.TaggedFiles
                         select ProcessTaggedFileAsync(tagged, tagged.NameSansExt)).ToList();
            while (tasks.Any()) {
                var finishedTask = await Task.WhenAny(tasks);

                documents.Add(await finishedTask);
                tasks.Remove(finishedTask);
            }
            //foreach (var tagged in FileManager.TaggedFiles) {
            //    var fileName = tagged.NameSansExt;
            //    await ProcessTaggedFileAsync(documents, tagged, fileName);

            //}
            return documents;
        }

        private async Task<Document> ProcessTaggedFileAsync(FileSystem.FileTypes.TaggedFile tagged, string fileName) {
            UpdateProgressDisplay(string.Format("{0}: Loading...", fileName));
            var doc = await new TaggedFileParser(tagged).LoadDocumentAsync();
            UpdateProgressDisplay(string.Format("{0}: Analysing Syntax...", fileName));
            await LASI.Algorithm.Binding.Binder.BindAsync(doc);
            UpdateProgressDisplay(string.Format("{0}: Correlating Relationships...", fileName));
            var tasks = LASI.Algorithm.Weighting.Weighter.GetWeightingTasksForDocument(doc).ToList();
            foreach (var task in tasks) {
                var message = await task;
                UpdateProgressDisplay(message);
            }

            UpdateProgressDisplay(string.Format("{0}: Parsing Complete...", fileName));
            return doc;
        }

        private async Task LoadThesaurus() {
            UpdateProgressDisplay("Loading Thesaurus");
            var thesaurusTasks = Thesaurus.GetTasksToLoadAllThesauri().ToList();
            while (thesaurusTasks.Count > 0) {
                var finishedTask = await Task.WhenAny(thesaurusTasks);
                var message = await finishedTask;
                UpdateProgressDisplay(message);
                ProgressBar.Value += 2;
                thesaurusTasks.Remove(finishedTask);
            }
        }

        private void UpdateProgressDisplay(string statusMessage) {
            ProgressLabel.Content = statusMessage;
            ProgressBar.ToolTip = statusMessage;
            ProgressBar.Value += documentStepRatio;
        }


        private ProgressBar ProgressBar = null;
        private Label ProgressLabel = null;
        private float documentStepRatio;
    }



}


