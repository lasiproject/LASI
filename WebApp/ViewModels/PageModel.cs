using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.Core.DocumentStructures;

namespace LASI.WebApp.Models
{
    public class PageModel
    {
        public PageModel(Document.Page page) {
            this.page = page;
            ParagraphViewModels = page.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            Style = new Style { CssClass = "page" };
        }
        public string Text { get { return "\t" + string.Join("\r\n\r\n", ParagraphViewModels.Select(paragraph => paragraph.Text)); } }
        public Style Style { get; private set; }
        public IEnumerable<ParagraphModel> ParagraphViewModels { get; private set; }
        public IEnumerable<SentenceModel> SentenceViewModels { get { return ParagraphViewModels.SelectMany(paragraph => paragraph.SentenceViewModels); } }

        private Document.Page page;
    }
}