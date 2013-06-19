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
        public StringBuilder InfoProvider {
            get;
            private set;
        }

        private ProgressBar ProgressBar = null;
        private Label ProgressLabel = null;
        private float documentStepRatio;
        public async Task<IEnumerable<Document>> LoadAndAnalyseAllDocuments(ProgressBar progressBar, Label progressLabel) {
            ProgressBar = progressBar;
            ProgressLabel = progressLabel;
            documentStepRatio = 6f / FileManager.TextFiles.Count;
            await LoadThesaurus(progressBar);

            UpdateProgressDisplay("Tagging Documents");

            await FileManager.TagTextFilesAsync();
            progressBar.Value += 5;

            var documents = new List<Document>();

            foreach (var tagged in FileManager.TaggedFiles) {
                var fileName = tagged.NameSansExt;
                progressLabel.Content = string.Format("Loading {0}...", fileName);

                var doc = await new TaggedFileParser(tagged).LoadDocumentAsync();
                ProgressBar.Value += documentStepRatio;
                progressLabel.Content = string.Format("{0}: Analysing Syntax...", fileName);
                await LASI.Algorithm.Binding.Binder.BindAsync(doc);
                ProgressBar.Value += documentStepRatio * 3;
                progressLabel.Content = string.Format("{0}: Correlating Relationships...", fileName);
                var tasks = LASI.Algorithm.Weighting.Weighter.GetWeightingTasksForDocument(doc).ToList();
                foreach (var task in tasks) {

                    var message = await task;
                    progressLabel.Content = message;
                    ProgressBar.Value += documentStepRatio;

                }

                ProgressBar.Value += documentStepRatio;

                documents.Add(doc);

            }
            return documents;
        }

        private async Task LoadThesaurus(ProgressBar progressBar) {
            UpdateProgressDisplay("Loading Thesaurus");
            var thesaurusTasks = Thesaurus.GetTasksToLoadAllThesauri().ToList();
            while (thesaurusTasks.Count > 0) {
                var finishedTask = await Task.WhenAny(thesaurusTasks);
                var message = await finishedTask;
                UpdateProgressDisplay(message);
                progressBar.Value += 2;
                thesaurusTasks.Remove(finishedTask);
            }
        }

        private async Task<Document> LoadTaggedDocument(FileSystem.FileTypes.TaggedFile taggedFile) {
            return await new TaggedFileParser(taggedFile).LoadDocumentAsync();
        }

        private async Task<Document> ParseDocument(Document doc) {
            var statusMessage = string.Format("{0}: Analysing Syntax", doc.FileName);
            UpdateProgressDisplay(statusMessage);
            await Binder.BindAsync(doc);
            statusMessage = string.Format("{0}: Correlating Generalizations", doc.FileName);
            UpdateProgressDisplay(statusMessage);
            await Weighter.WeightAsync(doc);
            return doc;
        }

        private void UpdateProgressDisplay(string statusMessage) {
            ProgressLabel.Content = statusMessage;
            ProgressBar.ToolTip = statusMessage;
            ProgressBar.Value += documentStepRatio;
        }


    }



}


