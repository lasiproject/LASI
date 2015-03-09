using System.Linq;
using System.Collections.Generic;
using LASI.Core;
using AspSixApp.Models.Lexical;

namespace AspSixApp.Models.DocumentStructures
{
    public class DocumentModel : TextualModel<Document>
    {
        public DocumentModel(Document document, IEnumerable<IEnumerable<object>> chartData, DocumentSetModel containinSetModel) : this(document, chartData)
        {
            DocumentSetModel = containinSetModel;
        }

        public DocumentModel(Document document, IEnumerable<IEnumerable<object>> chartData) : base(document)
        {
            Document = document;
            Title = document.Title;
            ParagraphModels = document.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
            PageModels = document.Paginate(80, 30).Select(page => new PageModel(page));
            foreach (var model in PageModels)
            {
                model.DocumentModel = this;
            }
            ChartData = chartData;


        }

        public IEnumerable<ParagraphModel> ParagraphModels { get; }
        public string Title { get; }
        public override string Text => ModelFor.Text;
        public override Style Style => new Style { CssClass = "document" };
        public IEnumerable<PhraseModel> PhraseModels => ParagraphModels.SelectMany(paragraph => paragraph.PhraseModels);
        public IEnumerable<PageModel> PageModels { get; }
        public DocumentSetModel DocumentSetModel { get; }
        public Document Document { get; }
        public IEnumerable<IEnumerable<object>> ChartData { get; private set; }
    }
}