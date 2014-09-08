using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.Core.DocumentStructures;
using LASI.WebApp.Models.Lexical;

namespace LASI.WebApp.Models
{
    public class SentenceModel
    {
        public SentenceModel(Sentence sentence) {
            this.sentence = sentence;
            Text = sentence.Text;
            Style = new Style { CssClass = "sentence" };
            PhraseModels = sentence.Phrases.Select(phrase => new PhraseModel(phrase));
            foreach (var model in PhraseModels) {
                model.SentenceModel = this;
            }
            ClauseModels = sentence.Clauses.Select(clause => new ClauseModel(clause));
            foreach (var model in ClauseModels) {
                model.SentenceModel = this;
            }
        }

        public string Text { get; private set; }
        public Style Style { get; private set; }
        public IEnumerable<PhraseModel> PhraseModels { get; private set; }
        public IEnumerable<ClauseModel> ClauseModels { get; private set; }
        public ParagraphModel ParagraphModel { get; internal set; }

        private Sentence sentence;
    }
}