using AspSixApp.Models;
using LASI.Core;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AspSixApp.Models.DocumentStructures;
    using LASI.Interop;
    using LASI.Utilities;
    using Path = System.IO.Path;
    using JobStatusMap = System.Collections.Concurrent.ConcurrentDictionary<int, Models.Results.JobStatus>;
    using NaiveTopResultSelector = LASI.Core.Analysis.Heuristics.NaiveTopResultSelector;
    using System.Security.Principal;
    using CustomIdentity;

    [Authorize]
    public class ResultsController : Controller
    {
        public ResultsController(IInputDocumentStore<UserDocument> documentStore)
        {
            Phrase.VerboseOutput = true;
            this.documentStore = documentStore;
        }
        public async Task<ActionResult> Index()
        {
            TrackedJobs.Clear();
            CurrentOperation = string.Empty;
            PercentComplete = 0;
            return await Results();
        }
        [HttpGet("Results/Single/{documentId}")]
        public async Task<PartialViewResult> Single(string documentId)
        {
            //var docResult =  
            var doc = documentStore.GetUserInputDocumentById(User.Identity.GetUserId(), documentId);
            var results = await LoadResultDocument(doc);
            return PartialView("_Single", results.First());
        }

        public async Task<ViewResult> Results()
        {
            var udocuments = GetAllUserDocuments();
            var resultModels = await LoadResultDocument(udocuments.ToArray());

            ViewBag.Charts = resultModels.DistinctBy(chart => chart.Title).ToDictionary(chart => chart.Title, chart => chart.ChartData);
            ViewBag.Title = "Results";
            return await Task.FromResult(View(new DocumentSetModel(resultModels)));

        }
        private async Task<IEnumerable<DocumentModel>> LoadResultDocument(params UserDocument[] userDocuments)
        {
            var documents = from document in (await ProcessUserDocuments(userDocuments))
                            let topResults = NaiveTopResultSelector.GetTopResultsByEntity(document).Take(ChartItemLimit)
                            let chartData = from chartResult in topResults
                                            select new object[] { chartResult.First, chartResult.Second }
                            select new { Document = document, Title = document.Title, Rows = Newtonsoft.Json.Linq.JArray.FromObject(chartData) };

            return documents.Select(e => new DocumentModel(e.Document, e.Rows));
        }
        private IEnumerable<UserDocument> GetAllUserDocuments()
        {

            var userId = Context.User.Identity.GetUserId();

            var files = documentStore.GetAllUserInputDocuments(userId); return files;
        }

        private static async Task<IEnumerable<Document>> ProcessUserDocuments(params UserDocument[] userDocuments)
        {
            var analyzer = new AnalysisOrchestrator(userDocuments);
            analyzer.ProgressChanged += (sender, e) =>
            {
                PercentComplete += e.PercentWorkRepresented;
                CurrentOperation = e.Message;
            };
            var documents = await analyzer.ProcessAsync();
            PercentComplete = 100;
            CurrentOperation = "Analysis Complete.";
            processedDocuments = processedDocuments.Union(documents);
            return documents;
        }


        // These properties should be removed and replaced with a better solution to sharing
        // progress This was a hack to initially test the functionality
        public static string CurrentOperation { get; private set; }

        public static double PercentComplete { get; internal set; }

        public static JobStatusMap TrackedJobs { get; } = new JobStatusMap();
        private string ServerPath => System.IO.Directory.GetParent(HostingEnvironment.WebRoot).FullName;
        [Activate]
        private Microsoft.AspNet.Hosting.IHostingEnvironment HostingEnvironment { get; set; }
        private string UserDocumentsDirectory => Path.Combine(ServerPath, "App_Data", "SourceFiles");
        private const int ChartItemLimit = 5;

        private static System.Collections.Immutable.IImmutableSet<Document> processedDocuments =
            System.Collections.Immutable.ImmutableHashSet.Create(
                    ComparerFactory.Create<Document>((dx, dy) => dx.Title == dy.Title, d => d.Title.GetHashCode()));


        private readonly IInputDocumentStore<UserDocument> documentStore;
    }
}
