using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.FileSystem;
using LASI.Utilities;
using LASI.GuiInterop;
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
        public StringBuilder InfoProvider
        {
            get;
            private set;
        }

        private ProgressBar ProgressBar = null;
        private Label ProgressLabel = null;
        private float documentStepRatio;
        public async Task<IEnumerable<Document>> LoadAndAnalyseAllDocuments(ProgressBar progressBar, Label progressLabel)
        {
            ProgressBar = progressBar;
            ProgressLabel = progressLabel;
            UpdateProgressDisplay("Tagging Documents");

            await FileManager.TagTextFilesAsync();
            progressBar.Value = 10;
            await LoadThesaurus(progressBar);


            progressBar.Value += 5;

            documentStepRatio = 13f / FileManager.TextFiles.Count();
            var docs = new ConcurrentBag<LASI.Algorithm.DocumentConstructs.Document>();

            var tasks = (from doc in FileManager.TaggedFiles
                         select Task.Run(() => LoadTaggedDocument(doc))).ToList();
            while (tasks.Count > 0) {
                var finishedTask = await Task.WhenAny(tasks);
                var doc = await finishedTask;
                var statusMessage = string.Format("{0}: Transforming Data", doc.FileName);
                UpdateProgressDisplay(statusMessage);
                docs.Add(doc);
                tasks.Remove(finishedTask);

            }


            foreach (var doc in docs) {

                await ParseDocument(doc);
            }


            return docs;
        }

        private async Task LoadThesaurus(ProgressBar progressBar)
        {
            UpdateProgressDisplay("Loading Thesaurus");
            var thesaurusTasks = Thesaurus.GetThesaurusTasks().ToList();
            while (thesaurusTasks.Count > 0) {
                var finishedTask = await Task.WhenAny(thesaurusTasks);
                var message = await finishedTask;
                UpdateProgressDisplay(message);
                progressBar.Value += 5;
                thesaurusTasks.Remove(finishedTask);
            }
        }

        private async Task<Document> LoadTaggedDocument(FileSystem.FileTypes.TaggedFile taggedFile)
        {



            return await new TaggedFileParser(taggedFile).LoadDocumentAsync();
        }

        private async Task<Document> ParseDocument(Document doc)
        {
            var statusMessage = string.Format("{0}: Binding Structures", doc.FileName);
            UpdateProgressDisplay(statusMessage);
            await Binder.BindAsync(doc);
            statusMessage = string.Format("{0}: Weighting Relationships", doc.FileName);
            UpdateProgressDisplay(statusMessage);
            await Weighter.WeightAsync(doc);
            return doc;
        }

        private void UpdateProgressDisplay(string statusMessage)
        {
            ProgressLabel.Content = statusMessage;
            ProgressBar.ToolTip = statusMessage;
            ProgressBar.Value += documentStepRatio;
        }



        private async Task BindLexicals(IEnumerable<LASI.Algorithm.DocumentConstructs.Document> docs)
        {

            await Task.WhenAll((docs.ToList().Select(doc =>
            {

                return Task.Factory.StartNew(async () =>
                {
                    await Binder.BindAsync(doc);
                    var statusMessage = string.Format("Bound Lexicals in {0}", doc.FileName);
                    ProgressLabel.Content = statusMessage;
                    ProgressBar.ToolTip = statusMessage;
                    ProgressBar.Value += documentStepRatio;
                }, System.Threading.CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            })).ToArray());





        }
        private async Task<IEnumerable<Document>> InstantiateDocuments()
        {
            var result = new ConcurrentBag<LASI.Algorithm.DocumentConstructs.Document>();
            foreach (var doc in FileManager.TaggedFiles) {
                result.Add(await new TaggedFileParser(doc).LoadDocumentAsync());
                await Task.Yield();
            }
            return result;
        }

    }



}


