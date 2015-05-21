using System;
using System.Collections.Generic;
using LASI.WebApp.Models;
using LASI.WebApp.Models.Organization;
using Microsoft.Framework.ConfigurationModel;

namespace LASI.WebApp.CustomIdentity.MongoDB
{
    public class MongoDBConfiguration
    {
        public MongoDBConfiguration(MongoDBOptions options)
        {
            Options = options;
            collectionNamesByType = new Dictionary<Type, string>
            {
                [typeof(ApplicationUser)] = options.UserCollectionName,
                [typeof(UserRole)] = options.UserRoleCollectionName,
                [typeof(UserDocument)] = options.UserDocumentCollectionName,
                [typeof(ApplicationOrganization)] = options.OrganizationCollectionName
            };
        }
        public MongoDBConfiguration(IConfiguration config, string applicationBasePath)
        {
            this.Options = new MongoDBOptions
            {
                UserCollectionName = "UserCollectionName",
                UserRoleCollectionName = "UserRoleCollectionName",
                UserDocumentCollectionName = "UserDocumentCollectionName",
                OrganizationCollectionName = "OrganizationCollectionName",
                MongodExePath = config["MongodExecutableLocation"],
                DataDbPath = applicationBasePath + config["MongoDataDbPath"],
                InstanceUrl = config["MongoDbInstanceUrl"],
                ApplicationDatabaseName = config["ApplicationDatabaseName"],
                CreateProcess = Convert.ToBoolean(config["CreateMongoDbProcess"])
            };
            collectionNamesByType = new Dictionary<Type, string>
            {
                [typeof(ApplicationUser)] = config["UserCollectionName"],
                [typeof(UserRole)] = config["UserRoleCollectionName"],
                [typeof(UserDocument)] = config["UserDocumentCollectionName"],
                [typeof(ApplicationOrganization)] = config["OrganizationCollectionName"]
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
        public string this[Type collectionType] => collectionNamesByType[collectionType];
        public MongoDBOptions Options { get; }
        private readonly IDictionary<Type, string> collectionNamesByType;

    }
}