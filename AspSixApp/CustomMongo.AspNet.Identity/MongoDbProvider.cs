using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System.Collections;
using AspSixApp.Models.ApplicationUsers;

namespace AspSixApp
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Framework.ConfigurationModel;
    using User = Models.ApplicationUsers.MongoUser;
    public class MongoDbProvider : IDisposable
    {
        public void Insert(User account) {
            Users.Insert(account);
        }
        public void Delete(User user) {
            var toDelete = Users.FindAllAs<User>().First(u => u.Equals(user));
        }
        internal MongoDbProvider(string webRoot, IConfiguration configuration) {
            this.webRoot = webRoot;
            this.ConfigurationManager = configuration;
            mongoServer = new Lazy<MongoServer>(valueFactory: () => new MongoClient(ConfigurationManager["MongoConnection"]).GetServer(), isThreadSafe: true);
            MongoDatabase = mongoServer.Value.GetDatabase("accounts");
        }

        private void CreateMongoServer() {
            mongoServerProcess = Process.Start(new ProcessStartInfo
            {
                FileName = mongodExecutableLocation,
                Arguments = string.Join(" ", "--dbpath", System.IO.Path.Combine(webRoot, mongoDbPath))
            });
        }

        public void Dispose() {
            ((IDisposable)mongoServerProcess).Dispose();
        }

        public MongoDatabase MongoDatabase { get; }


        #region Fields
        private MongoCollection<User> Users => MongoDatabase.GetCollection<User>("users");

        internal async Task<User> FindSingleAsync(Func<User, bool> p, CancellationToken cancellationToken = default(CancellationToken)) => await Users.AsQueryable().SingleAsync(x => p(x), cancellationToken);
        public IConfiguration ConfigurationManager { get; }
        private string mongoDbPath => ConfigurationManager["MongoDbPath"];
        private string mongodExecutableLocation => ConfigurationManager["MongodExecutableLocation"];
        private Lazy<MongoServer> mongoServer { get; }

        private readonly string webRoot;
        private Process mongoServerProcess;

        #endregion Fields
    }
}