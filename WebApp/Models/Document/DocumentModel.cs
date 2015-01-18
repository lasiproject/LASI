using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using LASI.WebApp.Models.Lexical;
using System;

namespace LASI.WebApp.Models
{
	class DocumentModel : TextualModel<Core.Document>
	{
		public DocumentModel(Core.Document document) : base(document) {
			Document = document;
			Title = document.Title;
			ParagraphModels = document.Paragraphs.Select(paragraph => new ParagraphModel(paragraph));
			PageModels = document.Paginate(80, 30).Select(page => new PageModel(page));
			foreach (var model in PageModels) { model.DocumentModel = this; }

		}

		public override string Text { get { return ModelFor.Text; } }
		public string Title { get; private set; }
		public IEnumerable<ParagraphModel> ParagraphModels { get; private set; }
		public IEnumerable<PhraseModel> PhraseModels { get { return ParagraphModels.SelectMany(paragraph => paragraph.PhraseModels); } }
		public IEnumerable<PageModel> PageModels { get; private set; }
		public override Style Style { get { return new Style { CssClass = "document" }; } }
		public DocumentSetModel DocumentSetModel { get; internal set; }
		public Core.Document Document { get; private set; }
	}
}