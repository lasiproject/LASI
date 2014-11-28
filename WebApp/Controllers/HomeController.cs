using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LASI.ContentSystem;
using LASI.Core;
using LASI.Interop;
using LASI.Utilities;
using LASI.WebApp.Models;
using Newtonsoft.Json;

namespace LASI.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //static int timesExecuted = 0;
        [HttpGet]
        public ActionResult Index(AccountModel account = null, string returnUrl = "") {
            ViewBag.ReturnUrl = returnUrl;
            TrackedJobs.Clear();
            CurrentOperation = string.Empty;
            PercentComplete = 0;
            if (Directory.Exists(USER_UPLOADED_DOCUMENTS_DIR)) {
                foreach (var fileSystemInfo in new DirectoryInfo(USER_UPLOADED_DOCUMENTS_DIR).EnumerateFileSystemInfos()) {
                    fileSystemInfo.Delete();
                }
            }
            return View(new AccountModel());
        }

        public ActionResult Progress() {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ViewResult> Results() {
            var documents = await LoadResults();
            Phrase.VerboseOutput = true;
            var data = (from document in documents
                        let documentModel = new DocumentModel(document)
                        let naiveTopResults = NaiveResultSelector.GetTopResultsByEntity(document).Take(CHART_ITEM_MAX)
                        from result in naiveTopResults
                        orderby result.Value descending
                        group new object[] { result.Key, result.Value } by documentModel)
                            .ToDictionary(g => g.Key, g => JsonConvert.SerializeObject(g.ToArray()
                            .Take(CHART_ITEM_MAX)));
            ViewData["charts"] = data;
            ViewData["documents"] = data.Keys;
            ViewBag.Title = "Results";
            return View(new DocumentSetModel(documents));
        }

        [HttpPost]
        public async Task<ActionResult> Upload() {
            var serverPath = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            if (!Directory.Exists(serverPath)) {
                Directory.CreateDirectory(serverPath);
            }
            for (var i = 0; i < Request.Files.Count; ++i) {
                var file = Request.Files[i];// file in Request.Files where file != null && file.ContentLength > 0 select file;
                foreach (var remnant in from remnant in new DirectoryInfo(serverPath).EnumerateFileSystemInfos()
                                        where remnant.Name.Contains(file.FileName.SplitRemoveEmpty('\\').Last())
                                        select remnant) {
                    var dir = remnant as DirectoryInfo;
                    if (dir != null) {
                        dir.Delete(true);
                    } else {
                        remnant.Delete();
                    }
                }
                var path = Path.Combine(serverPath,
                    file.FileName.SplitRemoveEmpty('\\').Last());
                file.SaveAs(path);
            }
            return await Results();
        }

        private async Task<IEnumerable<Document>> LoadResults() {
            var serverPath = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            var extensionMap = new ExtensionWrapperMap(UnsupportedFormatHandling.YieldNull);
            var files = Directory.EnumerateFiles(serverPath)
                .Select(file => {
                    try {
                        return extensionMap[file.SplitRemoveEmpty('.').Last()](file);
                    } catch (ArgumentException) { return null; }
                })
                .Where(file => file != null && !processedDocuments.Any(d => d.Name == file.NameSansExt));
            var analyzer = new AnalysisOrchestrator(files);
            analyzer.ProgressChanged += (s, e) => {
                PercentComplete += e.PercentWorkRepresented;
                CurrentOperation = e.Message;
            };
            var documents = await analyzer.ProcessAsync();
            PercentComplete = 100;
            CurrentOperation = "Analysis Complete.";
            processedDocuments = processedDocuments.Union(documents);
            return processedDocuments;
        }

        // These properties should be removed and replaced with a better solution to sharing
        // progress This was a hack to initially test the functionality
        public static string CurrentOperation { get; private set; }

        public static double PercentComplete { get; internal set; }

        public static ConcurrentDictionary<int, JobStatus> TrackedJobs { get; } = new ConcurrentDictionary<int, JobStatus>();

        private const short CHART_ITEM_MAX = 5;
        private const string USER_UPLOADED_DOCUMENTS_DIR = "~/App_Data/SourceFiles/";

        private static IImmutableSet<Document> processedDocuments = ImmutableHashSet.Create<Document>(
                    CustomComparer.Create<Document>((dx, dy) => dx.Name == dy.Name, d => d.Name.GetHashCode()));

        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };
    }
}