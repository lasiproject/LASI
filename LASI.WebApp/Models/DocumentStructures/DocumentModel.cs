using System.Linq;
using System.Collections.Generic;
using LASI.Core;
using LASI.WebApp.Models.Lexical;
using System;

namespace LASI.WebApp.Models.DocumentStructures
{
    public class DocumentModel : TextualModel<Document, DocumentSetModel>
    {
        public DocumentModel(Document document, IEnumerable<IEnumerable<object>> chartData, DocumentSetModel containingSetModel) : this(document, chartData)
        {
            DocumentSetModel = containingSetModel;
        }

        public DocumentModel(Document document, IEnumerable<IEnumerable<object>> chartData) : base(document)
        {
            ChartData = chartData;
        }

        public IEnumerable<ParagraphModel> ParagraphModels => ModelFor.Paragraphs.Select(p => new ParagraphModel(p));
        public string Title => ModelFor.Name;
        public override string Text => ModelFor.Text;
        public override Style Style => new Style { CssClass = "document" };
        public IEnumerable<PhraseModel> PhraseModels => ParagraphModels.SelectMany(paragraph => paragraph.PhraseModels);
        public IEnumerable<PageModel> PageModels => ModelFor.Paginate(80, 30).Select(page => new PageModel(page) { DocumentModel = this });
        public DocumentSetModel DocumentSetModel { get; }
        public IEnumerable<IEnumerable<object>> ChartData { get; }

        public override DocumentSetModel Parent => DocumentSetModel;
    }
}