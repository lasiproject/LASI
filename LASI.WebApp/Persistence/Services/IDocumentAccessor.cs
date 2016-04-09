using System.Collections.Generic;
using LASI.WebApp.Models;
using LASI.Content;

namespace LASI.WebApp.Persistence
{
    public interface IDocumentAccessor<TDocument> where TDocument : class, IRawTextSource
    {
        /// <summary>
        /// Adds the given document to the store associates it with the user indicated by <paramref name="userId"/>, and returns a string representation of its id.
        /// </summary>
        /// <param name="userId">The id of the user associated with the document.</param>
        /// <param name="document">The document to add.</param>
        /// <returns>A string representation of the added document's id.</returns>
        string AddForUser(string userId, TDocument document);
        /// <summary>
        /// Adds the given document to the store associates it with the user indicated by the document's <see cref="UserDocument.UserId"/> property, and returns a string representation of its id.
        /// </summary>
        /// <param name="document">The document to add.</param>
        /// <returns>A string representation of the added document's id.</returns>
        string AddForUser(TDocument document);
        IEnumerable<TDocument> GetAllForUser(string userId);
        TDocument GetById(string userId, string documentId);
        void RemoveById(string userId, string documentId);
    }
}