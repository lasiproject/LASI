using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.WebApp.Helpers;

namespace LASI.WebApp.Models.DocumentStructures
{
    public class PageModel : TextualModel<Document.Page, DocumentModel>
    {
        public PageModel(Document.Page page) : base(page)
        {
            Paragraphs = ModelFor.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            foreach (var model in Paragraphs)
            {
                model.Page = this;
            }
        }
        public override string Text => string.Join("", Enumerable.Repeat(TextHelper.HtmlSpace, 4)) + string.Join("", Paragraphs.Select(m => m.Text));
        public override Style Style => new Style { CssClass = "page" };
        public IEnumerable<ParagraphModel> Paragraphs { get; }
        public IEnumerable<SentenceModel> Sentences => Paragraphs.SelectMany(paragraph => paragraph.Sentences);
        public DocumentModel Document { get; internal set; }

        public override DocumentModel Parent => Document;
    }
}