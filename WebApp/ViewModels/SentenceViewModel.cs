using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LASI.Core.DocumentStructures;

namespace LASI.WebApp.ViewModels
{
    public class SentenceViewModel
    {
        public SentenceViewModel(Sentence sentence) {
            this.sentence = sentence;
            Text = sentence.Text;
            Style = new Style { CssClass = "sentence" };
            PhraseViewModels = sentence.Phrases.Select(phrase => new PhraseViewModel(phrase));
            ClauseViewModels = sentence.Clauses.Select(clause => new ClauseViewModel(clause));
        }

        public string Text { get; private set; }
        public Style Style { get; private set; }
        public IEnumerable<PhraseViewModel> PhraseViewModels { get; private set; }
        public IEnumerable<ClauseViewModel> ClauseViewModels { get; private set; }

        private Sentence sentence;

    }
}