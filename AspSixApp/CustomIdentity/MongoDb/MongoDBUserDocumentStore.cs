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
using AspSixApp.CustomIdentity.MongoDB;

namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDBUserDocumentStore : IInputDocumentStore<UserDocument>
    {


        public MongoDBUserDocumentStore(MongoDBService dbService)
        {
            documents = new Lazy<MongoCollection<UserDocument>>(() => dbService.GetCollection<UserDocument>());
        }


        public UserDocument GetUserInputDocumentById(string userId, string documentId) =>
            Documents.AsQueryable().FirstOrDefault(d => d._id == ObjectId.Parse(documentId) && d.UserId == userId);
        public IEnumerable<UserDocument> GetAllUserInputDocuments(string userId) =>
            Documents.FindAs<UserDocument>(Query.EQ("UserId", userId));

        public void AddUserDocument(string userId, UserDocument document) =>
            Documents.Insert(document);

        private MongoCollection<UserDocument> Documents => documents.Value;

        private readonly Lazy<MongoCollection<UserDocument>> documents;

    }
}