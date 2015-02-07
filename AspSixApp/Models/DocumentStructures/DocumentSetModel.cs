using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Utilities;

namespace AspSixApp.Models.DocumentStructures
{
    class DocumentSetModel : TextualModel<IEnumerable<Document>>
    {
        public DocumentSetModel(IEnumerable<Document> documents) : base(documents)
        {
            foreach (var document in documents)
            {
                var model = new DocumentModel(document);
                model.DocumentSetModel = this;
            }
        }

        public override Style Style => new Style { CssClass = "documentlist" };
        // TODO: Fix name of lambda arg
        public override string Text => $@"{GetType()}:\n{string.Join("\n\n", DocumentModels.Select(m => m.Text))}";
        public IEnumerable<DocumentModel> DocumentModels { get; }

    }
}
