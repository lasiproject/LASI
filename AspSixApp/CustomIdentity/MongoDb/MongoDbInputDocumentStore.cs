using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspSixApp.Models;
using Microsoft.Framework.ConfigurationModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace AspSixApp.CustomIdentity.MongoDb
{
    public class MongoDbInputDocumentStore : IInputDocumentStore<UserDocument>
    {
        private readonly Lazy<MongoDatabase> mongoDatabase;


        public MongoDbInputDocumentStore(MongoDbConfiguration configuration)
        {
            this.mongoDatabase = new Lazy<MongoDatabase>(
                () => new MongoClient(new MongoUrl(configuration.ConnectionString)).GetServer().GetDatabase(configuration.ApplicationDatabase)
            );
        }

        public UserDocument GetUserInputDocumentById(string userId, string documentId)
        {
            return (from document in Documents.AsQueryable()
                    where document._id == ObjectId.Parse(documentId)
                    where document.OwnerId == userId
                    select document).FirstOrDefault();
            //return Documents.FindOne(Query.And(Query.EQ("OwnerId", userId), Query.EQ("_id",documentId)));
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