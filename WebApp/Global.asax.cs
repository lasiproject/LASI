using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LASI.WebApp.Models;
using MongoDB.Driver;

namespace LASI.WebApp
{
    using System.IO;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    using AccountCollection = MongoCollection<AccountModel>;
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
            Interop.ResourceMonitoring.UsageManager.SetPerformanceLevel(Interop.ResourceMonitoring.UsageManager.Mode.High);
            var mongoServerProcess = Process.Start(new ProcessStartInfo
            {
                FileName = mongodExecutableLocation,
                Arguments = string.Join(" ", "--dbpath", Server.MapPath(mongoDbPath))
            });
            Output.SetToFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "LASI_log"));
            Disposed += (s, e) => mongoServerProcess.Dispose();
        }
        #region Properties

        //public MongoServer MongoServer { get { return server.Value; } }

        public static MongoCollection<AccountModel> Accounts { get { return accounts.Value; } }


        #endregion

        #region Fields

        private static Lazy<MongoServer> server = new Lazy<MongoServer>(
            valueFactory: () => new MongoClient(connectionString).GetServer(),
            isThreadSafe: true
        );
        private static Lazy<AccountCollection> accounts = new Lazy<AccountCollection>(
            valueFactory: () => server.Value.GetDatabase("accounts").GetCollection<AccountModel>("users"),
            isThreadSafe: true
        );
        private static readonly string mongodExecutableLocation = ConfigurationManager.AppSettings["MongodExecutableLocation"];
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
        private static readonly string mongoDbPath = ConfigurationManager.AppSettings["MongoDbPath"];
        #endregion
    }
}
