using LASI.Core.DocumentStructures;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using LASI.WebApp.Models.Lexical;

namespace LASI.WebApp.Models
{
    public class DocumentModel
    {
        public DocumentModel(Document document) {
            Document = document;
            Name = document.Name;
            Id = System.Threading.Interlocked.Increment(ref IdGenerator);
            ParagraphModels = document.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            Style = new Style { CssClass = "document" };
            PageModels = document.Paginate(80, 30).Select(page => new PageModel(page));
            foreach (var model in PageModels) { model.DocumentModel = this; }

        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<ParagraphModel> ParagraphModels { get; private set; }
        public IEnumerable<PhraseModel> PhraseModels { get { return ParagraphModels.SelectMany(paragraph => paragraph.PhraseModels); } }
        public IEnumerable<PageModel> PageModels { get; private set; }
        public Style Style { get; private set; }
        public DocumentSetModel DocumentSetModel { get; internal set; }
        public Document Document { get; private set; }

        private static int IdGenerator;
    }
}