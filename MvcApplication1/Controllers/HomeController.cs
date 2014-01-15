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
        public ActionResult DocumentData(string returnUrl) {
            ViewBag.returnUrl = returnUrl;

           

            return View();
        }
    }
}