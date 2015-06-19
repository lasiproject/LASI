using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LASI.WebApp.Models;
using Microsoft.Framework.ConfigurationModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using LASI.WebApp.Persistence.MongoDB;
using LASI.WebApp.Persistence.MongoDB.Extensions;
using LASI.Utilities.Validation;
using System.Diagnostics;

namespace LASI.WebApp.Persistence.MongoDB
{
    public class MongoDBDocumentProvider : IDocumentAccessor<UserDocument>
    {
        public MongoDBDocumentProvider(MongoDBService dbService)
        {
            documents = new Lazy<MongoCollection<UserDocument>>(() => dbService.GetCollection<UserDocument>());
        }


        public UserDocument GetById(string userId, string documentId) => Documents.FindAll().FirstOrDefault(d => d.UserId == userId && d.Id == documentId);
        public IEnumerable<UserDocument> GetAllForUser(string userId) => Documents.Find(Query.EQ("UserId", userId));


        /// <summary>
        /// Adds the given document to the store and returns a string representation of its id.
        /// </summary>
        /// <param name="userId">The id of the user associated with the document.</param>
        /// <param name="document">The document to add.</param>
        /// <returns>A string representation of the added document's id.</returns>
        public string AddForUser(string userId, UserDocument document)
        {
            Documents.Insert(document);
            var inserted = Documents.FindOne(Query.EQ("UserId", userId).And(Query.EQ("Name", document.Name)));
            return inserted.Id;
        }

        public string AddForUser(UserDocument document)
        {
            Validate.NeitherNullNorEmpty(document.UserId, nameof(document.UserId));
            return AddForUser(document.UserId, document);
        }

        public void RemoveById(string userId, string documentId)
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