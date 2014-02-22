using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LASI.WebService.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index(string returnUrl)
        {
            returnUrl = string.Empty;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
	}
}