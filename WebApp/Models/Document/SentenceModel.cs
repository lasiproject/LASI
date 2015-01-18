using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.WebApp.Models.Lexical;

namespace LASI.WebApp.Models
{
	class SentenceModel : TextualModel<Core.Sentence>
	{
		public SentenceModel(Core.Sentence sentence) : base(sentence) {
			this.sentence = sentence;
			PhraseModels = sentence.Phrases.Select(phrase => new PhraseModel(phrase));
			foreach (var model in PhraseModels) {
				model.SentenceModel = this;
			}
			ClauseModels = sentence.Clauses.Select(clause => new ClauseModel(clause));
			foreach (var model in ClauseModels) {
				model.SentenceModel = this;
			}
		}

		public override string Text { get { return ModelFor.Text; } }
		public override Style Style { get { return new Style { CssClass = "sentence" }; } }
		public IEnumerable<PhraseModel> PhraseModels { get; private set; }
		public IEnumerable<ClauseModel> ClauseModels { get; private set; }
		public ParagraphModel ParagraphModel { get; internal set; }

		private Core.Sentence sentence;
	}
}