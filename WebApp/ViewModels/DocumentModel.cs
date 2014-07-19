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
            this.document = document;
            Name = document.Name;
            Id = System.Threading.Interlocked.Increment(ref IdGenerator);
            ParagraphViewModels = document.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            PageViewModels = document.Paginate(80, 30).Select(page => new PageModel(page));
            Style = new Style { CssClass = "document" };
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<ParagraphModel> ParagraphViewModels { get; private set; }
        public IEnumerable<PhraseModel> PhraseViewModels { get { return ParagraphViewModels.SelectMany(paragraph => paragraph.PhraseViewModels); } }
        public IEnumerable<PageModel> PageViewModels { get; private set; }
        public Style Style { get; private set; }

        private Document document;

        protected static int IdGenerator;

    }
}