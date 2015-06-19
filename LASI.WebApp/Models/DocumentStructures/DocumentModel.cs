using System.Linq;
using System.Collections.Generic;
using LASI.Core;
using LASI.WebApp.Models.Lexical;
using System;
using Newtonsoft.Json;
using LASI.Utilities;

namespace LASI.WebApp.Models.DocumentStructures
{
    public class DocumentModel : TextualModel<Document, DocumentSetModel>
    {
        public DocumentModel(
            Document document,
            IEnumerable<Pair<string, float>> chartItems,
            string documentId,
            DocumentSetModel containingSetModel
        ) : this(document, chartItems, documentId)
        {
            DocumentSet = containingSetModel;
        }

        public DocumentModel(Document document, IEnumerable<Pair<string, float>> chartItems, string documentId) : base(document)
        {
            ChartData = chartItems;
            Pages = ModelFor.Paginate(80, 30).Select(page => new PageModel(page) { Document = this });
            Paragraphs = ModelFor.Paragraphs.Select(p => new ParagraphModel(p));
            Phrases = Paragraphs.SelectMany(paragraph => paragraph.Phrases);
            Id = documentId;
        }
        public new string Id { get; }
        public IEnumerable<Pair<string, float>> ChartData { get; }

        public override Style Style => new Style { CssClass = "document" };

        public IEnumerable<ParagraphModel> Paragraphs { get; }

        [JsonIgnore]
        public IEnumerable<PageModel> Pages { get; }

        public override DocumentSetModel Parent => DocumentSet;

        [JsonIgnore]
        public IEnumerable<PhraseModel> Phrases { get; }


        [JsonIgnore]
        public override string Text => ModelFor.Text;

        public string Title => ModelFor.Name;
        public DocumentSetModel DocumentSet { get; }

    }
}