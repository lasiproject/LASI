using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Concurrent;
using LASI.Utilities;

namespace LASI.WebApp.Persistence
{
    public class InMemoryDocumentProvider : IDocumentAccessor<UserDocument>
    {
        public string AddForUser(UserDocument document)
        {
            userDocuments.AddOrUpdate(document.UserId, ImmutableHashSet.Create(document), (userId, documents) => documents.Add(document));
            return document.Id;
        }

        public string AddForUser(string userId, UserDocument document)
        {
            document.UserId = userId;
            return AddForUser(document);
        }

        public IEnumerable<UserDocument> GetAllForUser(string userId) => userDocuments.GetOrAdd(userId, ImmutableHashSet<UserDocument>.Empty);

        public UserDocument GetById(string userId, string documentId) => userDocuments[userId].First(document => document.Id == documentId);


        public void RemoveById(string userId, string documentId) => userDocuments[userId] = userDocuments[userId].Remove(userDocuments[userId].FirstOrDefault(document => document.Id == documentId));

        private ConcurrentDictionary<string, IImmutableSet<UserDocument>> userDocuments = new ConcurrentDictionary<string, IImmutableSet<UserDocument>>();
    }
}