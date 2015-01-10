using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LASI.WebApp.Models;
using MongoDB.Driver;

namespace LASI.WebApp
{
    using System.Collections;
    using System.Collections.Immutable;
    using System.IO;
    using LASI.Utilities;
    using Newtonsoft.Json;


    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            PerformCustomInitialization();
        }

        /// <summary>
        /// Application specific initialization for concurrency management, memory management, data sources, and logging.
        /// </summary>
        private void PerformCustomInitialization() {
            ConfigurationManager.AppSettings["ResourcesDirectory"] = Server.MapPath(ConfigurationManager.AppSettings["ResourcesDirectory"]);
            Interop.ResourceManagement.UsageManager.SetPerformanceLevel(Interop.ResourceManagement.UsageManager.Mode.High);
            AccountProvider = SetupUserAccounts(this);
            Output.SetToFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "LASI_log"));
        }

        private static IAccountProvider SetupUserAccounts(HttpApplication app) {
            switch (ConfigurationManager.AppSettings["AccountSource"]) {
                case "LocalFileSystem":
                    return new FileSystemAccountProvider(app);
                case "MongoDb":
                    return new MongoDbAccountProvider(app);
                default:
                    return new MongoDbAccountProvider(app);
            }
        }


        #region Properties

        internal static IAccountProvider AccountProvider { get; private set; }


        #endregion

    }
}
