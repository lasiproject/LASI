using System;
using System.Diagnostics;
using System.Net.Sockets;
using MongoDB.Driver;
using Directory = System.IO.Directory;

namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDBService : IDisposable
    {
        public MongoDBService(MongoDBConfiguration configuration)
        {
            this.configuration = configuration; StartDatabaseProcess();
        }

        private void StartDatabaseProcess()
        {
            if (!Directory.Exists(configuration.MongoFilesDirectory))
            {
                Directory.CreateDirectory(configuration.MongoFilesDirectory);
            }
            mongoDbProcess = Process.Start(
                fileName: configuration.MongodExePath,
                arguments: $"--dbpath {configuration.MongoFilesDirectory}"
            );
        }
        private MongoDatabase GetDatabase()
        {

            var mongoUrl = new MongoUrl(configuration.ConnectionString);
            return new MongoClient(
                mongoUrl).GetServer()
                .GetDatabase(configuration.ApplicationDatabaseName);


        }
        public MongoCollection<TDocument> GetCollection<TDocument>()
        {
            try
            {
                var name = configuration.CollectionNamesByType[typeof(TDocument)];
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
            ((IDisposable)this.mongoDbProcess).Dispose();
        }
        ~MongoDBService()
        {
            Dispose();
        }

        private readonly MongoDBConfiguration configuration;

        private Process mongoDbProcess;
    }
}