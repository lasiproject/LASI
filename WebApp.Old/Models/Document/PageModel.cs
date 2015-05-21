using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace LASI.WebApp.Old.Models
{
    public class PageModel : TextualModel<Core.Document.Page>
    {
        public PageModel(Core.Document.Page page) : base(page)
        {
            this.page = page;
            ParagraphModels = page.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            foreach (var model in ParagraphModels) { model.PageModel = this; }
        }
        public override string Text => "&nbsp;&nbsp;&nbsp;&nbsp;" + string.Join("\r\n\r\n", ParagraphModels.Select(paragraph => paragraph.Text));
        public override Style Style => new Style { CssClass = "page" };
        public IEnumerable<ParagraphModel> ParagraphModels { get; }
        public IEnumerable<SentenceModel> SentenceModels => ParagraphModels.SelectMany(paragraph => paragraph.SentenceModels);

        public DocumentModel DocumentModel { get; internal set; }
        public Core.Document.Page page;
    }
}