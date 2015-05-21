using System.Linq;
using System.Collections.Generic;
using LASI.Core;
using LASI.WebApp.Models.Lexical;
using System;
using Newtonsoft.Json;

namespace LASI.WebApp.Models.DocumentStructures
{
    public class DocumentModel : TextualModel<Document, DocumentSetModel>
    {
        public DocumentModel(Document document, IEnumerable<IEnumerable<object>> chartData, DocumentSetModel containingSetModel) : this(document, chartData)
        {
            DocumentSet = containingSetModel;
        }

        public DocumentModel(Document document, IEnumerable<IEnumerable<object>> chartData) : base(document)
        {
            ChartData = chartData;
        }

        public IEnumerable<ParagraphModel> Paragraphs => ModelFor.Paragraphs.Select(p => new ParagraphModel(p));
        public string Title => ModelFor.Name;
        [JsonIgnore]
        public override string Text => ModelFor.Text;
        [JsonIgnore]
        public override string DetailText => base.DetailText;
        public override Style Style => new Style { CssClass = "document" };
        public IEnumerable<PhraseModel> Phrases => Paragraphs.SelectMany(paragraph => paragraph.Phrases);
        public IEnumerable<PageModel> Pages => ModelFor.Paginate(80, 30).Select(page => new PageModel(page) { Document = this });
        public DocumentSetModel DocumentSet { get; }
        public IEnumerable<IEnumerable<object>> ChartData { get; }

        public override DocumentSetModel Parent => DocumentSet;
    }
}