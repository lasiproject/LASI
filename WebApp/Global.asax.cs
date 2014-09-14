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
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            InitializeCoreHooks();
            ConfigurationManager.AppSettings["ResourcesDirectory"] = Server.MapPath(ConfigurationManager.AppSettings["ResourcesDirectory"]);
        }

        /// <summary>
        /// Application specific initialization for concurrency management, memory management, and logging.
        /// </summary>
        private void InitializeCoreHooks() {
            LASI.Interop.ResourceUsageManager.SetPerformanceLevel(LASI.Interop.ResourceUsageManager.Mode.High);
            MongoServerProcess = Process.Start(new ProcessStartInfo
            {
                FileName = mongodExecutableLocation,
                Arguments = string.Join(" ", "--dbpath", Server.MapPath(mongoDbPath))
            });
        }

        private static Lazy<MongoServer> server = new Lazy<MongoServer>(
            () => new MongoClient(connectionString).GetServer(),
            isThreadSafe: true
        );
        private static Lazy<MongoCollection<AccountModel>> users = new Lazy<MongoCollection<AccountModel>>(
            () => server.Value.GetDatabase("accounts").GetCollection<AccountModel>("users"),
            isThreadSafe: true
        );

        public static MongoServer MongoServer { get { return server.Value; } }

        public static MongoCollection<AccountModel> Users { get { return users.Value; } }

        public Process MongoServerProcess { get; private set; }

        private static readonly string mongodExecutableLocation = ConfigurationManager.AppSettings["MongodExecutableLocation"];
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
        private static readonly string mongoDbPath = ConfigurationManager.AppSettings["MongoDbPath"];
    }
}
