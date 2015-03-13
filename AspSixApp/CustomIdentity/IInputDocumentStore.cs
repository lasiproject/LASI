using System.Collections.Generic;
using AspSixApp.Models;
using LASI.Content;

namespace AspSixApp.CustomIdentity
{
    public interface IInputDocumentStore<TDocument> where TDocument : class, IRawTextSource
    {
        void AddUserDocument(string userId, TDocument document);
        IEnumerable<TDocument> GetAllUserInputDocuments(string userId);
        TDocument GetUserInputDocumentById(string userId, string sourceName);
    }
}