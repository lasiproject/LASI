using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using LASI.WebApp.Models;
using LASI.WebApp.Persistence;

namespace LASI.WebApp.Tests.Mocks
{
    class MockDocumentAccessor : IDocumentAccessor<UserDocument>
    {
        public MockDocumentAccessor(IEnumerable<UserDocument> documents)
        {
            this.documents = documents.GroupBy(d => d.UserId).ToDictionary(g => g.Key, g => g.AsEnumerable());
        }
        public string AddForUser(UserDocument document) => AddForUser(document.UserId, document);

        public string AddForUser(string userId, UserDocument document)
        {
            if (documents.ContainsKey(userId))
            {
                documents[userId] = documents[userId].Append(document);
            }
            return document.Id;
        }

        public IEnumerable<UserDocument> GetAllForUser(string userId) => documents.GetValueOrDefault(userId, Enumerable.Empty<UserDocument>());

        public UserDocument GetById(string userId, string documentId) => documents.GetValueOrDefault(userId).FirstOrDefault(d => d.Id == documentId && d.UserId == userId);

        public void RemoveById(string userId, string documentId)
        {
            if (documents.ContainsKey(userId))
            {
                documents[userId] = documents[userId].Where(d => d.Id != documentId.ToString() && d.UserId != userId);
            }
        }
        private Dictionary<string, IEnumerable<UserDocument>> documents;

    }
}
