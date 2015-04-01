using System.Collections.Generic;
using AspSixApp.Models;
using LASI.Content;

namespace AspSixApp.CustomIdentity
{
    public interface IInputDocumentProvider<TDocument> where TDocument : class, IRawTextSource
    {
        /// <summary>
        /// Adds the given document to the store and returns a string representation of its id.
        /// </summary>
        /// <param name="userId">The id of the user associated with the document.</param>
        /// <param name="document">The document to add.</param>
        /// <returns>A string representation of the added document's id.</returns>
        string AddUserDocument(string userId, TDocument document);
        IEnumerable<TDocument> GetAllUserDocuments(string userId);
        TDocument GetUserDocumentById(string userId, string documentId);
        void Remove(string userId, string documentId);
    }
}