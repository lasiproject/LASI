using System;
using System.Diagnostics;
using System.Net.Sockets;
using MongoDB.Driver;

namespace AspSixApp.CustomIdentity.MongoDb
{
    public class MongoDbService : IDisposable
    {

        public MongoDbService(MongoDbConfiguration configuration) { this.Configuration = configuration; StartDatabaseProcess(); }
        public MongoDbConfiguration Configuration { get; }

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
            catch (SocketException e) when (e.InnerException is MongoConnectionException)
            {
                StartDatabaseProcess();
                return GetCollection<TDocument>();
            }
        }

        public void Dispose()
        {
            //((IDisposable)this.win64MongoDbProcess).Dispose();
        }
        ~MongoDbService()
        {
            Dispose();
        }
        private Process win64MongoDbProcess;
    }
}