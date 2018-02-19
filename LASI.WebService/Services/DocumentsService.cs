using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.WebService.Data;
using LASI.WebService.Data.Models;

namespace LASI.WebService.Services
{
    public class DocumentsService : IDocumentsService
    {
        DocumentsContext context;

        public DocumentsService(DocumentsContext context)
        {
            this.context = context;
        }

        public async Task<UploadDocument> AddAsync(UploadDocument document)
        {
            var result = context.Add(document);
            await context.SaveChangesAsync();
            return document;
        }

        public IAsyncEnumerable<UploadDocument> GetAsync(ApplicationUser user)
        {
            var results = from document in context.Uploads
                          where document.User == user
                          select document;

            return results.ToAsyncEnumerable();
        }
    }
}
