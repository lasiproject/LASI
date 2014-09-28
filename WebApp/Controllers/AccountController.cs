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
    [Authorize]
    public class AccountController : Controller
    {

        private const string USER_DIR = "~/App_Data/Users/";

        public ActionResult Login(LoginModel loginModel) {
            return Authenticate(loginModel);

        }
        public ActionResult Authenticate(LoginModel credentials) {
            bool authenticated = ValidateCredentials(credentials);
            return authenticated ? RedirectToAction(
                controllerName: "Home",
                actionName: "Index",
                routeValues: new
            {
                account = MvcApplication.Accounts.AsQueryable().First(a => a.Email == credentials.UserName)
            }) : Failure();
        }

        private ActionResult Failure() {
            throw new NotImplementedException();
        }

        private bool ValidateCredentials(LoginModel credentials) {
            var account = MvcApplication.Accounts.FindAll().First(o => o.Email == credentials.UserName);
            return account != null && account.Password == credentials.Password;
        }
        public ActionResult CreateAccount() {
            return View(new AccountModel());
        }

        public ActionResult Create(AccountModel account) {
            MvcApplication.Accounts.Insert(account);

            return Login(new LoginModel { Password = account.Password, UserName = account.Email });
        }
        public ActionResult Settings(AccountModel account) {
            var profiles = from a in MvcApplication.Accounts.FindAll()
                           where a.Email == account.Email
                           select account;
            return View(profiles.SingleOrDefault());
        }
    }
}