using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.WebApp.Old.Models.Lexical;

namespace LASI.WebApp.Old.Models
{
    public class SentenceModel : TextualModel<Core.Sentence>
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

        public override string Text => ModelFor.Text;
        public override Style Style => new Style { CssClass = "sentence" };
        public IEnumerable<PhraseModel> PhraseModels { get; }
        public IEnumerable<ClauseModel> ClauseModels { get; }
        public ParagraphModel ParagraphModel { get; internal set; }

        private Core.Sentence sentence;
    }
}