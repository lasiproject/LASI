using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LASI.Utilities;
using LASI.ContentSystem;
using LASI.Interop;
using LASI;
using System.Threading.Tasks;
using LASI.Core;
using Newtonsoft.Json;

namespace LASI.WebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly string USER_UPLOADED_DOCUMENTS_DIR = "~/App_Data/SourceFiles/";

        public ActionResult Index(string returnUrl) {
            returnUrl = string.Empty;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Upload() {
            var SERVER_PATH = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            if (!Directory.Exists(SERVER_PATH)) {
                Directory.CreateDirectory(SERVER_PATH);
            }
            for (var i = 0; i < Request.Files.Count; ++i) {

                var file = Request.Files[i];// file in Request.Files where file != null && file.ContentLength > 0 select file;
                foreach (var remnant in from remnant in new DirectoryInfo(SERVER_PATH).EnumerateFileSystemInfos()
                                        where remnant.Name.Contains(file.FileName.SplitRemoveEmpty('\\').Last())
                                        select remnant) {
                    var dir = remnant as DirectoryInfo;
                    if (dir != null) {
                        dir.Delete(true);
                    } else {
                        remnant.Delete();
                    }
                }
                var path = Path.Combine(SERVER_PATH, file.FileName);

                file.SaveAs(path);
            }
            return RedirectToAction("Example", "Home");
        }

        public async Task<ActionResult> Example() {
            var SERVER_PATH = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            ViewBag.ReturnUrl = "Example";
            var extensionMap = new ExtensionWrapperMap(UnknownFileTypeHandling.YieldNull);
            var files = Directory.EnumerateFiles(SERVER_PATH)
                .Select(file => {
                    try {
                        return extensionMap[file.SplitRemoveEmpty('.').Last()](file);
                    }
                    catch (ArgumentException) { return null; }
                })
                .Where(file => file != null);

            var analyzer = new AnalysisController(files);
            analyzer.ProgressChanged += (s, e) => { percentComplete = e.Increment; statusMessage = e.Message; };
            var loadingTask = await Task.Run(async () => await analyzer.ProcessAsync());

            ViewData.Add("docs", from task in loadingTask select task);

            ViewBag.Title = "Example";
            return View();
        }
        private static double percentComplete;

        public static double PercentComplete {
            get { return percentComplete; }
            set { percentComplete = value; }
        }
        private static string statusMessage;

        public static string StatusMessage {
            get { return statusMessage; }
            set { statusMessage = value; }
        }

        public ContentResult GetAnalysisProgress(string returnUrl) {
            return new ContentResult { Content = string.Join("", new JsonServicesController().Get()), ContentType = "JSON" };
            //("api/jsonservices");
            //var data = JsonConvert.SerializeObject(
            //        new
            //        {
            //            PercentComplete = percentComplete, StatusMessage = statusMessage ?? ""
            //        }
            //);
            //// ViewBag = data;
            //return View(data);

        }


    }
}