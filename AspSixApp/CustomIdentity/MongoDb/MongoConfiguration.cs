using System;

namespace AspSixApp.CustomIdentity.MongoDb
{
    public class MongoConfiguration
    {
        public MongoConfiguration(Microsoft.Framework.ConfigurationModel.IConfiguration config, AppDomain appDomain)
        {
            MongodExePath = config["MongodExecutableLocation"];
            MongoDbPath = config["MongoDbPath"];
            MongoFilesDirectory = appDomain.BaseDirectory + MongoDbPath;
            ConnectionString = config["MongoConnection"];
            ApplicationDatabase = config["ApplicationDatabaseName"];
        }

        public string ApplicationDatabase { get; }
        public string ConnectionString { get; }
        public string MongoDbPath { get; }
        public string MongodExePath { get; }
        public string MongoFilesDirectory { get; }
    }
}