using LASI.Core.DocumentStructures;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace LASI.WebApp.ViewModels
{
    public class DocumentViewModel
    {
        public DocumentViewModel(Document document) {
            this.document = document;
            Name = document.Name;
            Id = System.Threading.Interlocked.Increment(ref IdGenerator);
            ParagraphViewModels = document.Paragraphs.Select(paragraph => new ParagraphViewModel(paragraph));
            PageViewModels = document.Paginate(80, 30).Select(page => new PageViewModel(page));
            Style = new Style { CssClass = "document" };
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<ParagraphViewModel> ParagraphViewModels { get; private set; }
        public IEnumerable<PhraseViewModel> PhraseViewModels { get { return ParagraphViewModels.SelectMany(paragraph => paragraph.PhraseViewModels); } }
        public IEnumerable<PageViewModel> PageViewModels { get; private set; }
        public Style Style { get; private set; }

        private Document document;

        protected static int IdGenerator;

    }
}