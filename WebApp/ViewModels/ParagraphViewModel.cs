using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using LASI.Core.DocumentStructures;

namespace LASI.WebApp.ViewModels
{
    public class ParagraphViewModel
    {
        public ParagraphViewModel(Paragraph paragraph) {
            Text = paragraph.Text;
            Style = new Style { CssClass = "paragraph" };
            SentenceViewModels = paragraph.Sentences.Select(sentence => new SentenceViewModel(sentence));
            PhraseViewModels = paragraph.Phrases.Select(phrase => new PhraseViewModel(phrase));
        }
        public string Text { get; private set; }
        public Style Style { get; private set; }
        public IEnumerable<SentenceViewModel> SentenceViewModels { get; private set; }
        public IEnumerable<PhraseViewModel> PhraseViewModels { get; private set; }
    }
}