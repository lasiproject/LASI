using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using LASI.WebApp.Old.Models.Lexical;
using System;

namespace LASI.WebApp.Old.Models
{
    public class DocumentModel : TextualModel<Core.Document>
    {
        public DocumentModel(Core.Document document) : base(document)
        {
            Document = document;
            Title = document.Name;
            ParagraphModels = document.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            PageModels = document.Paginate(80, 30).Select(page => new PageModel(page));
            foreach (var model in PageModels) { model.DocumentModel = this; }

        }

        public override string Text => ModelFor.Text;
        public string Title { get; }
        public IEnumerable<ParagraphModel> ParagraphModels { get; }
        public IEnumerable<PhraseModel> PhraseModels => ParagraphModels.SelectMany(paragraph => paragraph.PhraseModels);
        public IEnumerable<PageModel> PageModels { get; }
        public override Style Style => new Style { CssClass = "document" };
        public DocumentSetModel DocumentSetModel { get; internal set; }
        public Core.Document Document { get; }
    }
}