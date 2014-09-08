using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.Core.DocumentStructures;
using LASI.WebApp.Models.Lexical;

namespace LASI.WebApp.Models
{
    public class DocumentSetModel
    {
        public DocumentSetModel(IEnumerable<Document> documents) {
            DocumentModels = documents.Select(document => new DocumentModel(document));
            foreach (var model in DocumentModels) { model.DocumentSetModel = this; }
        }
        public IEnumerable<DocumentModel> DocumentModels { get; private set; }
    }
}