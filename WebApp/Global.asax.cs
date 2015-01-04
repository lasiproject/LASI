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
            Accounts = SetupUserAccounts(this);
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

        internal static IAccountProvider Accounts { get; private set; }


        #endregion

        #region Fields
        private static readonly string mongodExecutableLocation = ConfigurationManager.AppSettings["MongodExecutableLocation"];

        #endregion

        internal interface IAccountProvider : IEnumerable<AccountModel>
        {
            void Insert(AccountModel account);
        }
        private class FileSystemAccountProvider : IAccountProvider
        {
            public FileSystemAccountProvider(HttpApplication app) {
                var userDirectory = app.Server.MapPath(USER_DIR);
                var accounts = from path in Directory.EnumerateFiles(userDirectory, "*.json", SearchOption.AllDirectories)
                               select JsonConvert.DeserializeObject<AccountModel>(File.ReadAllText(path));
                app.Disposed += (s, e) => accounts.ToList().ForEach(account => {
                    using (var writer = new StreamWriter(Path.Combine(userDirectory, account.Email + ".json"), append: false)) {
                        writer.Write(JsonConvert.SerializeObject(account));
                    }
                });
            }
            public void Insert(AccountModel account) {
                accounts = accounts.Add(account);
            }
            private IImmutableSet<AccountModel> accounts;
            public IEnumerator<AccountModel> GetEnumerator() {
                return accounts.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return accounts.GetEnumerator();
            }

            private const string USER_DIR = "~/App_Data/Users/";
        }
        private class MongoDbAccountProvider : IAccountProvider
        {
            public void Insert(AccountModel account) {
                accounts.Insert(account);

            }
            internal MongoDbAccountProvider(HttpApplication applicationContext) {
                this.applicationContext = applicationContext;

            }
            private MongoCollection<AccountModel> accounts = mongoServer.Value.GetDatabase("accounts").GetCollection<AccountModel>("users");

            public IEnumerator<AccountModel> GetEnumerator() {
                return accounts.AsQueryable().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return accounts.AsQueryable().GetEnumerator();
            }
            private void CreateMongoServer() {
                var mongoServerProcess = Process.Start(new ProcessStartInfo 
                {
                    FileName = mongodExecutableLocation,
                    Arguments = string.Join(" ", "--dbpath", applicationContext.Server.MapPath(mongoDbPath))
                });
                applicationContext.Disposed += (s, e) => mongoServerProcess.Dispose();
            }

            private readonly HttpApplication applicationContext;

            private static Lazy<MongoServer> mongoServer = new Lazy<MongoServer>(
                valueFactory: () => new MongoClient(connectionString).GetServer(),
                isThreadSafe: true
            );
            private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
            private static readonly string mongoDbPath = ConfigurationManager.AppSettings["MongoDbPath"];

        }
    }
}
