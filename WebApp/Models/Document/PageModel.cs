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
            Style = new Style { CssClass = "page" };
            ParagraphModels = page.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            foreach (var model in ParagraphModels) { model.PageModel = this; }

        }
        public string Text {
            get {
                return "&nbsp;&nbsp;&nbsp;&nbsp;" + string.Join("\r\n\r\n", ParagraphModels.Select(paragraph => paragraph.Text));
            }
        }
        public Style Style { get; private set; }
        public IEnumerable<ParagraphModel> ParagraphModels { get; private set; }
        public IEnumerable<SentenceModel> SentenceModels {
            get {
                return ParagraphModels.SelectMany(paragraph => paragraph.SentenceModels);
            }
        }

        public DocumentModel DocumentModel { get; internal set; }

        private Document.Page page;
    }
}