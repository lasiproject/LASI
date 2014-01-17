using LASI.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExperimentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public ActionResult Document() {

            var doc = System.Threading.Tasks.Task.Run(() => new LASI.Interop.AnalysisController(new LASI.ContentSystem.TextFile(@"C:\Users\Aluan\Desktop\documents\ducks.txt")).ProcessAsync().Result.First());
            ViewData.Add("doc", doc.Result);
            return View();
        }

    }
}