using System.Collections.Generic;
using AspSixApp.Models;
using LASI.Content;

namespace AspSixApp.CustomIdentity
{
    public interface IInputDocumentStore<TDocument> where TDocument : class, IRawTextSource
    {
        void AddUserInputDocument(string userId, TDocument document);
        IEnumerable<TDocument> GetAllUserInputDocuments(string userId);
        TDocument GetUserInputDocumentByName(string userId, string sourceName);
    }
}