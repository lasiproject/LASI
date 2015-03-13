using System;
using System.Diagnostics;
using System.Net.Sockets;
using MongoDB.Driver;

namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDBService : IDisposable
    {

        public MongoDBService(MongoDBConfiguration configuration) { this.Configuration = configuration; StartDatabaseProcess(); }
        public MongoDBConfiguration Configuration { get; }

        private void StartDatabaseProcess()
        {
            win64MongoDbProcess = Process.Start(
                fileName: Configuration.MongodExePath,
                arguments: $"--dbpath {Configuration.MongoFilesDirectory}"
            );
        }
        private MongoDatabase GetDatabase()
        {

            var mongoUrl = new MongoUrl(Configuration.ConnectionString);
            return new MongoClient(
                mongoUrl).GetServer()
                .GetDatabase(Configuration.ApplicationDatabaseName);


        }
        public MongoCollection<TDocument> GetCollection<TDocument>()
        {
            try
            {
                var name = Configuration.CollectionNamesByType[typeof(TDocument)];
                return GetDatabase().GetCollection<TDocument>(name);
            }
            catch (MongoConnectionException e) when (e.InnerException is SocketException)
            {
                StartDatabaseProcess();
                return GetCollection<TDocument>();
            }
        }

        public void Dispose()
        {
            //((IDisposable)this.win64MongoDbProcess).Dispose();
        }
        ~MongoDBService()
        {
            Dispose();
        }
        private Process win64MongoDbProcess;
    }
}