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
             ParagraphModels = ModelFor.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            foreach (var model in ParagraphModels)
            {
                model.PageModel = this;
            }
        }
        public override string Text =>
            string.Join("", Enumerable.Repeat(TextHelper.HtmlSpace, 4)) +
            string.Join("", ParagraphModels.Select(m => m.Text));
        public override Style Style => new Style { CssClass = "page" };
        public IEnumerable<ParagraphModel> ParagraphModels { get; }
        public IEnumerable<SentenceModel> SentenceModels => ParagraphModels.SelectMany(paragraph => paragraph.SentenceModels);

        public DocumentModel DocumentModel { get; internal set; }

        public override DocumentModel Parent => DocumentModel;
    }
}