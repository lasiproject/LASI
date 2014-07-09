using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.Core.DocumentStructures;

namespace LASI.WebApp.ViewModels
{
    public class DocumentSetViewModel(IEnumerable<Document> documents)
    {
        IEnumerable<DocumentViewModel> DocumentViewModels { get; } = documents.Select(document => new DocumentViewModel(document));
    }
}