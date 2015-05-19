using System;
using System.Net.Sockets;
using MongoDB.Driver;

namespace LASI.WebApp.CustomIdentity.MongoDB
{
    using Process = System.Diagnostics.Process;
    using Directory = System.IO.Directory;

    public class MongoDBService : IDisposable
    {
        public MongoDBService(MongoDBConfiguration config)
        {
            this.config = config;
            if (config.CreateProcess)
            {
                this.mongoDbProcess = StartDatabaseProcess();
            }
        }

        private Process StartDatabaseProcess()
        {
            if (!Directory.Exists(config.DataDbPath))
            {
                Directory.CreateDirectory(config.DataDbPath);
            }
            return Process.Start(
                 fileName: config.MongodExePath,
                 arguments: $"--dbpath {config.DataDbPath}"
             );
        }
        private MongoDatabase GetDatabase() =>
            new MongoClient(new MongoUrl(config.InstanceUrl))
            .GetServer()
            .GetDatabase(config.ApplicationDatabaseName);
        public MongoCollection<TDocument> GetCollection<TDocument>()
        {
            try
            {
                var name = config.CollectionNamesByType[typeof(TDocument)];
                return GetDatabase().GetCollection<TDocument>(name);
            }
            catch (MongoConnectionException e) when (e.InnerException is SocketException)
            {
                StartDatabaseProcess();
                return GetCollection<TDocument>();
            }
        }

        private MongoDBConfiguration config;

        private Process mongoDbProcess;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).          
                }
                mongoDbProcess?.Dispose();
                config = null;
                disposedValue = true;
            }
        }
        /// <summary>
        /// Disposes the <see cref="MongoDBService"/> instance.
        /// </summary>
        ~MongoDBService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}