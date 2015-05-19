using System;
using System.Collections.Generic;
using LASI.WebApp.Models;
using Microsoft.Framework.ConfigurationModel;

namespace LASI.WebApp.CustomIdentity.MongoDB
{
    public class MongoDBConfiguration
    {
        public MongoDBConfiguration(MongoDBOptions options)
        {
            Options = options;
        }
        public MongoDBConfiguration(IConfiguration config, string applicationBasePath)
        {
            this.Options = new MongoDBOptions
            {
                MongodExePath = config["MongodExecutableLocation"],
                DataDbPath = applicationBasePath + config["MongoDataDbPath"],
                InstanceUrl = config["MongoDbInstanceUrl"],
                ApplicationDatabaseName = config["ApplicationDatabaseName"],
                CreateProcess = Convert.ToBoolean(config["CreateMongoDbProcess"])
            };
            CollectionNamesByType = new Dictionary<Type, string>
            {
                [typeof(ApplicationUser)] = config["UserCollectionName"],
                [typeof(UserRole)] = config["UserRoleCollectionName"],
                [typeof(UserDocument)] = config["UserDocumentCollectionName"],
                [typeof(Organization)] = config["OrganizationCollectionName"]
            };
        }
        /// <summary>
        /// Gets a value indicating if the application should attempt start a mongod.exe instance and manage its lifetime.
        /// </summary>
        public bool CreateProcess => Options.CreateProcess;
        public string ApplicationDatabaseName => Options.ApplicationDatabaseName;
        public string InstanceUrl => Options.InstanceUrl;
        public string MongodExePath => Options.MongodExePath;
        public string DataDbPath => Options.DataDbPath;
        public IReadOnlyDictionary<Type, string> CollectionNamesByType { get; }
        public MongoDBOptions Options { get; }
    }
}