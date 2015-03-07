using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspSixApp.Models;
using Microsoft.Framework.ConfigurationModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace AspSixApp.CustomIdentity.MongoDb
{
    public class MongoDbInputDocumentStore : IInputDocumentStore<UserDocument>
    {
        private Lazy<MongoDatabase> mongoDatabase;


        public MongoDbInputDocumentStore(MongoConfiguration configuration)
        {
            this.mongoDatabase = new Lazy<MongoDatabase>(valueFactory: () => new MongoClient(new MongoUrl(configuration.ConnectionString)).GetServer().GetDatabase(configuration.ApplicationDatabase));
        }

        public UserDocument GetUserInputDocumentByName(string userId, string sourceName)
        {
            return Documents.FindOne(Query.And(Query.EQ("OwnerId", userId), Query.EQ("SourceName", sourceName)));
        }
        public IEnumerable<UserDocument> GetAllUserInputDocuments(string userId)
        {
            return Documents.FindAs<UserDocument>(Query.EQ("OwnerId", userId));
        }
        public void AddUserInputDocument(string userId, UserDocument document)
        {
            Documents.Insert(document);
        }
        private MongoCollection<UserDocument> Documents => mongoDatabase.Value.GetCollection<UserDocument>("documents");

    }
}