using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LASI.Core;

namespace AspSixApp.Models.DocumentStructures
{
    using LASI.Utilities.Specialized.Enhanced.Linq.List;

    public class DocumentSetModel : TextualModel<IEnumerable<Document>>
    {
        public DocumentSetModel(IEnumerable<Document> documents) : base(documents)
        {
            DocumentModels = from document in documents.ToList()
                             select new DocumentModel(document, this);
        }

        public override Style Style => new Style { CssClass = "documentlist" };
        // TODO: Fix name of lambda arg
        public override string Text => $@"{GetType()}:\n{string.Join("\n\n", DocumentModels.Select(m => m.Text))}";
        public IList<DocumentModel> DocumentModels { get; }

    }
}
