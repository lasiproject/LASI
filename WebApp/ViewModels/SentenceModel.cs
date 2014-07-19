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
            PhraseViewModels = sentence.Phrases.Select(phrase => new PhraseModel(phrase));
            ClauseViewModels = sentence.Clauses.Select(clause => new ClauseModel(clause));
        }

        public string Text { get; private set; }
        public Style Style { get; private set; }
        public IEnumerable<PhraseModel> PhraseViewModels { get; private set; }
        public IEnumerable<ClauseModel> ClauseViewModels { get; private set; }

        private Sentence sentence;

    }
}