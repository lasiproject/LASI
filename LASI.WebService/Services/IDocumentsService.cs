using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.WebService.Data;
using LASI.WebService.Data.Models;

namespace LASI.WebService.Services
{
    public interface IDocumentsService
    {
        IAsyncEnumerable<UploadDocument> GetAsync(ApplicationUser user);

        Task<UploadDocument> AddAsync(UploadDocument document);
    }
}
