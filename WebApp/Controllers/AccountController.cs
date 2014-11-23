#define TEST

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LASI.WebApp.Models;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json.Serialization;
using LASI.WebApp.Filters;

namespace LASI.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private static MvcApplication.IAccountProvider Accounts => MvcApplication.Accounts;

        public ActionResult Login(LoginModel loginModel) {
            return View(new LoginModel());
        }
        public ActionResult Authenticate(LoginModel credentials) {
            bool authenticated = ValidateCredentials(credentials);
            return authenticated ? RedirectToAction(
                controllerName: "Home",
                actionName: "Index",
                routeValues: new {
                    account = Accounts.First(a => a.Email == credentials.UserName)
                }) : Failure();
        }

        private ActionResult Failure() {
            throw new NotImplementedException();
        }

        private bool ValidateCredentials(LoginModel credentials) {
            var account = Accounts.First(o => o.Email == credentials.UserName);
            return account != null && account.Password == credentials.Password;
        }
        public ActionResult Create() {
            return View(new AccountModel());
        }


        public ActionResult Settings(AccountModel account) {
            var profiles = from a in Accounts ?? Enumerable.Empty<AccountModel>()
                           where a.Email == account.Email
                           select account;
            return View(new AccountModel { });
        }
    }
}