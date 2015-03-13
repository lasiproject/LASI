using System;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.Framework.ConfigurationModel;

namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDBConfiguration
    {
        public MongoDBConfiguration(IConfiguration config, AppDomain appDomain)
        {
            MongodExePath = config["MongodExecutableLocation"];
            MongoDbPath = config["MongoDbPath"];
            MongoFilesDirectory = appDomain.BaseDirectory + MongoDbPath;
            ConnectionString = config["MongoConnection"];
            ApplicationDatabaseName = config["ApplicationDatabaseName"];
            CollectionNamesByType = new Dictionary<Type, string>
            {
                [typeof(ApplicationUser)] = config["UserCollectionName"],
                [typeof(UserRole)] = config["UserRoleCollectionName"],
                [typeof(UserDocument)] = config["UserDocumentCollectionName"],
                [typeof(Organization)] = config["OrganizationCollectionName"]
            };
        }
        public string ApplicationDatabaseName { get; }
        public string ConnectionString { get; }
        public string MongoDbPath { get; }
        public string MongodExePath { get; }
        public string MongoFilesDirectory { get; }
        public IReadOnlyDictionary<Type, string> CollectionNamesByType { get; }
    }
}