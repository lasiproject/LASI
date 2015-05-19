using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

namespace LASI.WebApp.Models.DocumentStructures
{
    public class DocumentSetModel : IViewModel
    {
        private static int IdGenerator = -1;

        public DocumentSetModel(IEnumerable<Document> documents)
        {
            Id = System.Threading.Interlocked.Decrement(ref IdGenerator);
            DocumentModels = from document in documents.ToList()
                             select new DocumentModel(document, chartData: Enumerable.Empty<object[]>(), containingSetModel: this);
        }

        public DocumentSetModel(IEnumerable<DocumentModel> documentModels)
        {
            DocumentModels = documentModels.ToList();
        }

        public Style Style => new Style { CssClass = "documentlist" };

        public string Text => $@"{GetType()}:\n{string.Join("\n\n", DocumentModels.Select(m => m.Text))}";
        public IList<DocumentModel> DocumentModels { get; }

        public int Id { get; }

        public string ContextmenuId => null;
    }
}
