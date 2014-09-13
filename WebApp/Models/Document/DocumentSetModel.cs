using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.Core.DocumentStructures;
using LASI.WebApp.Models.Lexical;

namespace LASI.WebApp.Models
{
    public class DocumentSetModel
    {
        private static readonly Style style = new Style { CssClass = "documentlist" };

        public DocumentSetModel(IEnumerable<Document> documents) {
            DocumentModels = documents.Select(document => new DocumentModel(document));
            foreach (var model in DocumentModels) { model.DocumentSetModel = this; }
        }
        public IEnumerable<DocumentModel> DocumentModels { get; private set; }

        //public int Id { get { return CreateNewUniqueId(); } }

        public Style Style { get { return style; } }
    }
}
