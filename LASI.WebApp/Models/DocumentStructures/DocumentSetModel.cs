using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized;

namespace LASI.WebApp.Models.DocumentStructures
{
    public class DocumentSetModel : IViewModel
    {
        private static int IdGenerator = -1;

        public DocumentSetModel(IEnumerable<Document> documents)
        {
            Id = System.Threading.Interlocked.Increment(ref IdGenerator);
            Documents = from document in documents.ToList()
                        select new DocumentModel(
                            document: document,
                            chartItems: Enumerable.Empty<Pair<string, float>>(),
                            documentId: System.Threading.Interlocked.Increment(ref IdGenerator).ToString(),
                            containingSetModel: this
                        );
        }

        public DocumentSetModel(IEnumerable<DocumentModel> documentModels)
        {
            Documents = documentModels.ToList();
        }

        public Style Style => new Style { CssClass = "documentlist" };

        public string Text => $@"{GetType()}:\n{string.Join("\n\n", Documents.Select(m => m.Text))}";
        public IEnumerable<DocumentModel> Documents { get; }

        public int Id { get; }

        public string ContextmenuId => null;
    }
}
