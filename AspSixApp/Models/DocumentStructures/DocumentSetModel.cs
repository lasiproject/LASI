using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

namespace AspSixApp.Models.DocumentStructures
{
    public class DocumentSetModel : TextualModel<IEnumerable<Document>>
    {
        public DocumentSetModel(IEnumerable<Document> documents) : base(documents)
        {
            DocumentModels = from document in documents.ToList()
                             select new DocumentModel(document, Enumerable.Empty<object[]>(), this);
        }

        public DocumentSetModel(IEnumerable<DocumentModel> documentModels) : base(from model in documentModels select model.ModelFor)
        {
            DocumentModels = documentModels.ToList();
        }

        public override Style Style => new Style { CssClass = "documentlist" };
        // TODO: Fix name of lambda arg
        public override string Text => $@"{GetType()}:\n{string.Join("\n\n", DocumentModels.Select(m => m.Text))}";
        public IList<DocumentModel> DocumentModels { get; }

    }
}
