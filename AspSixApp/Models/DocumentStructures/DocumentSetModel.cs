using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LASI.Core;
using LASI.Utilities;

namespace AspSixApp.Models.DocumentStructures
{
    public class DocumentSetModel : TextualModel<IEnumerable<Document>>
    {
        public DocumentSetModel(IEnumerable<Document> documents) : base(documents)
        {
            DocumentModels = DocumentModels.AddRange(
                documents.Select(d => new DocumentModel(d, this))
            );
        }

        public override Style Style => new Style { CssClass = "documentlist" };
        // TODO: Fix name of lambda arg
        public override string Text => $@"{GetType()}:\n{string.Join("\n\n", DocumentModels.Select(m => m.Text))}";
        public ImmutableList<DocumentModel> DocumentModels { get; } = ImmutableList<DocumentModel>.Empty;

    }
}
