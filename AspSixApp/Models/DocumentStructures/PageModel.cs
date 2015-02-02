using System.Collections.Generic;
using System.Linq;
using LASI.Core;

namespace AspSixApp.Models.DocumentStructures
{
    class PageModel : TextualModel<Document.Page>
    {
        public PageModel(Document.Page page) : base(page) {
            this.page = page;
            ParagraphModels = page.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            foreach (var model in ParagraphModels) { model.PageModel = this; }

        }
        public override string Text { get { return "&nbsp;&nbsp;&nbsp;&nbsp;" + string.Join("\r\n\r\n", ParagraphModels.Select(paragraph => paragraph.Text)); } }
        public override Style Style { get { return new Style { CssClass = "page" }; } }
        public IEnumerable<ParagraphModel> ParagraphModels { get; private set; }
        public IEnumerable<SentenceModel> SentenceModels { get { return ParagraphModels.SelectMany(paragraph => paragraph.SentenceModels); } }

        public DocumentModel DocumentModel { get; internal set; }
        private Document.Page page;

    }
}