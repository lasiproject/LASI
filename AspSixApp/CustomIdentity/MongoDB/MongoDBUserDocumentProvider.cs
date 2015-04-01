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
using AspSixApp.CustomIdentity.MongoDB.Extensions;

namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDBUserDocumentProvider : IInputDocumentProvider<UserDocument>
    {
        public MongoDBUserDocumentProvider(MongoDBService dbService)
        {
            documents = new Lazy<MongoCollection<UserDocument>>(() => dbService.GetCollection<UserDocument>());
        }


        public UserDocument GetUserDocumentById(string userId, string documentId) => Documents.FindAll().FirstOrDefault(d => d.UserId == userId && d._id == ObjectId.Parse(documentId));
        public IEnumerable<UserDocument> GetAllUserDocuments(string userId) => Documents.Find(Query.EQ("UserId", userId));


        /// <summary>
        /// Adds the given document to the store and returns a string representation of its id.
        /// </summary>
        /// <param name="userId">The id of the user associated with the document.</param>
        /// <param name="document">The document to add.</param>
        /// <returns>A string representation of the added document's id.</returns>
        public string AddUserDocument(string userId, UserDocument document)
        {
            Documents.Insert(document);
            var inserted = Documents.FindOne(Query.EQ("UserId", userId).And(Query.EQ("Name", document.Name)));
            return inserted._id.ToString();
        }

        public void Remove(string userId, string documentId)
        {
            var result = Documents.Remove(Query.EQ("UserId", userId).And(Query.EQ("_id", ObjectId.Parse(documentId))));
            if (result?.ErrorMessage != null)
            {
                throw new Exception($"failed with error {result.ErrorMessage} when removing document with id{documentId}");
            }
        }
        private MongoCollection<UserDocument> Documents => documents.Value;

        private readonly Lazy<MongoCollection<UserDocument>> documents;

    }
}