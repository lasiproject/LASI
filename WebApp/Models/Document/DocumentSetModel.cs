using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.Core.DocumentStructures;
using LASI.WebApp.Models.Lexical;

namespace LASI.WebApp.Models
{
    public class DocumentSetModel(IEnumerable<Document> documents)
    {
        IEnumerable<DocumentModel> DocumentViewModels { get; } = documents.Select(document => new DocumentModel(document));
    }
}